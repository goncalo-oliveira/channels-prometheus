# Prometheus Metrics

Extends [Channels](https://github.com/goncalo-oliveira/channels) by providing Prometheus metrics.

The exported metrics are:

- `channels_active_total`: Total number of active channels.
- `channels_bytes_received_total`: Total number of bytes received.
- `channels_bytes_sent_total`: Total number of bytes sent.

The project makes use of [prometheus-net](https://github.com/prometheus-net/prometheus-net).

## Usage

Install the package from NuGet

```bash
dotnet add package Faactory.Channels.Prometheus
```

To calculate the metrics, you just need to register the metrics service. This will automatically calculate the metrics for all channels.

```csharp
IServiceCollection services = ...;

services.AddChannelMetrics();
```

To export the metrics, the easiest way is to install `prometheus-net.AspNetCore` package and register the pre-built middleware. If no other HTTP functionality is required, this is a good option.

```csharp
IServiceCollection services = ...;

services.AddMetricServer( options =>
{
    options.Port = 8081;
} );
```

You can find a more detailed sample project under the [example](./example) folder. There are other ways to export the metrics, which can be found in the [prometheus-net](https://github.com/prometheus-net/prometheus-net) documentation.

## Labels

By default, metrics are created just with the `channelId` label, which contains the channel identifier. However, it is possible to extend this and add more labels to the metrics.

To add more labels, you can add items to the channel's data dictionary. The middleware will automatically add all items with the prefix `prometheus.label.` to the metrics.

```csharp
public class SampleIdentityHandler : ChannelHandler<IdentityInformation>
{
    public override Task ExecuteAsync( IChannelContext context, IdentityInformation data )
    {
        // ...

        /*
        This adds an `uuid` label to the metrics. The label will be included in all metrics.
        */
        context.Channel.Data["prometheus.label.uuid"] = data.UUId;

        return Task.CompletedTask;
    }
}
```
