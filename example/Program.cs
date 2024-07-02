using Faactory.Examples;
using Prometheus;

var builder = Host.CreateApplicationBuilder( args );

/*
Configure default channel
*/
builder.Services.AddChannels( channel =>
{
    channel.AddInputHandler<EchoHandler>();
} );

/*
Add metrics
*/
builder.Services.AddChannelMetrics();

/*
Configure our server to listen on port 8080
*/
builder.Services.AddTcpChannelListener( options =>
{
    options.Port = 8080;
    options.Backlog = 5;
} );

/*
Configure stand-alone Kestrel server to listen on port 9090 for Prometheus metrics.
*/
builder.Services.AddMetricServer( options =>
{
    options.Port = 9090;
} );

var app = builder.Build();

/*
optional: Suppress the default metrics that are registered by the .NET runtime.
*/
Metrics.SuppressDefaultMetrics();

app.Run();
