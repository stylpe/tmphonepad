using System.Buffers;
using System.Buffers.Binary;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using Nefarius.ViGEm.Client;
using Nefarius.ViGEm.Client.Targets;
using Nefarius.ViGEm.Client.Targets.Xbox360;

internal sealed class Controller : IDisposable
{
    public Controller()
    {
        client = new();
        controller = client.CreateXbox360Controller();
        controller.Connect();
    }
    readonly ViGEmClient client;
    readonly IXbox360Controller controller;

    internal async Task Connect(WebSocket webSocket, ILogger<Controller> logger, CancellationToken ct)
    {
        using var owner = MemoryPool<byte>.Shared.Rent(1024);
        var mem = owner.Memory;
        var mslice = mem[..2];

        // Receive first message indicating client's native byte order
        var result = await webSocket.ReceiveAsync(mem, ct);
        CheckResult(result);
        Func<Memory<byte>, short> cast = m => MemoryMarshal.Cast<byte, short>(m.Span)[0];
        short endiannessTest = cast(mslice);

        if (endiannessTest != 1)
        {
            cast = m => BinaryPrimitives.ReadInt16BigEndian(m.Span);
            if (cast(mslice) != 1) cast = m => BinaryPrimitives.ReadInt16LittleEndian(m.Span);
            if (cast(mslice) != 1) throw new InvalidDataException("Client didn't send the expected byte alignment message: " + Convert.ToHexString(mslice.Span));
            logger.LogWarning("Congrats, client and server byte order are different, you win extra work :(");
        }

        while (!ct.IsCancellationRequested && webSocket.State is WebSocketState.Open)
        {
            result = await webSocket.ReceiveAsync(mem, ct);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                logger.LogInformation("Client disconnected");
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Bye!", ct);
                return;
            }
            CheckResult(result);
            short axis = cast(mslice);
            logger.LogInformation($"Axis value: {axis}");
            // TODO: use channels
            controller.SetAxisValue(Xbox360Axis.LeftThumbX, axis);
        }
    }

    private static void CheckResult(ValueWebSocketReceiveResult result)
    {
        switch (result)
        {
            case { EndOfMessage: false }:
                throw new InvalidDataException("Received a partial message... Surely this will never happen in practice?");
            case { MessageType: WebSocketMessageType.Binary, Count: not 2 }:
                throw new InvalidDataException($"Message length was invalid: {result.Count} (should always be 2)");
        }
    }

    public void Dispose()
    {
        try
        {
            controller.Disconnect();
        }
        finally
        {
            client.Dispose();
        }
    }
}

