"use strict";

const isLittleEndian = (() => {
    const array = new Uint8Array(4);
    const view = new Uint32Array(array.buffer);
    return (view[0] = 1) & array[0];
})();


function start() {
    const svg = document.getElementById("main");
    document.getElementById("fullscreen").addEventListener("click", () => svg.requestFullscreen({ navigationUI: "hide" }));
    const filler = svg.getElementById("filler");
    const text = svg.getElementById("text");
    const latency = svg.getElementById("latency");

    const url = new URL(location.href)
    url.protocol = "ws"
    url.pathname = "/ws"
    const socket = new WebSocket(url, "tmphonepad")
    // TODO: Connection Manager (auto reconnect)

    const axis = new Int16Array(1)
    const SHORT_MIN = -32768, SHORT_MAX = 32767;
    function clamp(num, min, max) {
        return num <= min ? min :
            num >= max ? max :
                num;
    }

    function send(value) {
        value = clamp(value, SHORT_MIN, SHORT_MAX)
        if (axis[0] != value) {
            axis[0] = value
            socket.send(axis);
        }
    }

    function down(ev) {
        if (!ev.isPrimary) return;
        const x = ev.offsetX;

        const margin = 0.9, deadzone = 0.75

        const rect = svg.getBoundingClientRect();
        const offset = ((x * 2 - rect.width)) / (rect.width * margin);
        const deadzoneOffset = offset / deadzone;

        send(Math.round(deadzoneOffset * SHORT_MAX))

        filler.width.baseVal.value = Math.abs(offset) * 100;
        filler.x.baseVal.value = offset < 0 ? offset * 100 : 0;

        text.textContent = Math.abs(clamp(deadzoneOffset * 100, -100, 100)).toFixed(2) + "%";
    }

    function up() {
        send(0);
        filler.width.baseVal.value = 0;
        text.textContent = "0.00%"
    }

    var pingMark
    function ping() {
        socket.send("ping");
        pingMark = performance.now();
    }
    function pong(ev) {
        if(ev.data === "pong") {
            // Calculate input latency as half round trip time of ping
            latency.textContent = ((performance.now() - pingMark) / 2).toFixed(2);
        }
    }

    const abortController = new AbortController();
    const abort = abortController.signal;
    socket.addEventListener("close", ev => abortController.abort(ev.reason));

    // Connection opened
    socket.addEventListener("open", (opened) => {
        // Initializer to agree on byte order
        send(1);
 
        const eventOpts = { signal: abortController.signal, passive: false,  };
        document.body.addEventListener("pointerdown", down, eventOpts);
        document.body.addEventListener("pointermove", down, eventOpts);
        document.body.addEventListener("pointerup", up, eventOpts);
        document.body.addEventListener("pointercancel", up, eventOpts)

        socket.addEventListener("message", pong, { signal: abortController.signal, passive: true });
        setInterval(ping, 1000, eventOpts);
    });
}

if (document.readyState === "loading") {
    // Loading hasn't finished yet
    document.addEventListener("DOMContentLoaded", start);
} else {
    // `DOMContentLoaded` has already fired
    start();
}
