using Faactory.Channels;
using Faactory.Examples;
using Prometheus;

//var builder = WebApplication.CreateBuilder( args );
var builder = Host.CreateApplicationBuilder( args );

/*
Configure default channel pipeline
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
Suppress the default metrics that are registered by the .NET runtime.
*/
Metrics.SuppressDefaultMetrics();

app.Run();
