using System.Net;
using System.Net.Sockets;
using QRCoder;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.AddSimpleConsole(c => c.SingleLine = true);
var app = builder.Build();
app.Logger.LogInformation("Starting...");

var addresses = Dns.GetHostEntry("").AddressList
    .Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).Distinct().ToList();
if (addresses.Count == 0) throw new PlatformNotSupportedException("Can't find any local IP");
if (addresses.Count > 1) app.Logger.LogWarning("More than one local IP available: {IPs}", addresses);
var host = $"http://{addresses[0]}:7384/";
app.Urls.Add(host);

app.UseWebSockets(new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(1)
});

using var controller = new Controller();
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetService<ILogger<Controller>>()!;
    if (context.Request.Path == "/ws" && context.WebSockets.IsWebSocketRequest)
    {
        logger.LogInformation($"New connection from {context.Connection.RemoteIpAddress}");
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync("tmphonepad");
        await controller.Connect(webSocket, logger, context.RequestAborted);
    }
    else await next(context);
});

app.UseFileServer(new FileServerOptions
{
    StaticFileOptions = { OnPrepareResponse = (ctx) =>
        ctx.Context.Response.GetTypedHeaders().CacheControl = new() { NoStore = true } }
});

app.Start();

var qr = AsciiQRCodeHelper.GetQRCode(host, 1, "  ", "██", QRCodeGenerator.ECCLevel.L);
app.Logger.LogInformation($"Started! Open {host} on your touchscreen device: \n{qr}");
app.WaitForShutdown();
