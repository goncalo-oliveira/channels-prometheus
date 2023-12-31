﻿using Faactory.Channels;
using Prometheus;

//var builder = WebApplication.CreateBuilder( args );
var builder = Host.CreateApplicationBuilder( args );

/*
Configure our server to listen on port 8080 and use the EchoHandler to handle incoming data.
*/
builder.Services.AddChannelsHostedService( channel =>
{
    channel.Configure( options =>
    {
        options.Port = 8080;
        options.Backlog = 5;
    } );

    channel.AddInputHandler<EchoHandler>();

    channel.UsePrometheusMetrics();
} );

/*
Configure stand-alone Kestrel server to listen on port 8081.
*/
builder.Services.AddMetricServer( options =>
{
    options.Port = 8081;
} );

var app = builder.Build();

/*
Suppress the default metrics that are registered by the .NET runtime.
*/
Metrics.SuppressDefaultMetrics();

app.Run();
