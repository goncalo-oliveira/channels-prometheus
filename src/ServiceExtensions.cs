using Faactory.Channels;

namespace Microsoft.Extensions.DependencyInjection;

public static class ChannelsPrometheusMetricsServiceExtensions
{
    /// <summary>
    /// Adds Channels Prometheus metrics to the service collection.
    /// </summary>
    public static IServiceCollection AddChannelMetrics( this IServiceCollection services )
    {
        services.AddSingleton<IChannelMonitor, PrometheusChannelMonitor>();

        return services;
    }
}
