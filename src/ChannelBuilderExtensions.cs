using Microsoft.Extensions.DependencyInjection;

namespace Faactory.Channels;

public static class ChannelBuilderPrometheusExtensions
{
    public static IChannelBuilder UsePrometheusMetrics( this IChannelBuilder builder )
    {
        if ( builder == null )
        {
            throw new ArgumentNullException( nameof( builder ) );
        }

        builder.Services.AddSingleton<IChannelEvents, PrometheusChannelEvents>();

        return ( builder );
    }
}
