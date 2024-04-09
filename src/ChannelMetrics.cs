using Faactory.Channels;
using Prometheus;

internal sealed class ChannelMetrics
{
    private readonly ( string Name, string Description ) MetricActiveTotal
        = ( "channels_active_total", "Total number of active channels." );

    private readonly ( string Name, string Description ) MetricBytesReceivedTotal
        = ( "channels_bytes_received_total", "Total number of bytes received." );

    private readonly ( string Name, string Description ) MetricBytesSentTotal
        = ( "channels_bytes_sent_total", "Total number of bytes sent." );

    public Gauge ChannelsActiveTotal( IChannelInfo channel )
        => Metrics.CreateGauge( 
            MetricActiveTotal.Name,
            MetricActiveTotal.Description
        );

    public Counter ChannelsDataReceived( IChannelInfo channel )
        => Counter( MetricBytesReceivedTotal, channel );

    public Counter ChannelsDataSent( IChannelInfo channel )
        => Counter( MetricBytesSentTotal, channel );

    private static Counter Counter( ( string Name, string Description ) metric, IChannelInfo channel )
    {
        var labels = new Dictionary<string, string>
        {
            { "channelId", channel.Id }
        };

        var additionalLabels = channel.Data.Where( x => x.Key.StartsWith( "prometheus.label." ) )
            .ToArray();

        foreach ( var label in additionalLabels )
        {
            labels.Add( label.Key.Substring( "prometheus.label.".Length ), label.Value );
        }

        var counter = Metrics.WithLabels( labels ).CreateCounter( 
            metric.Name,
            metric.Description
        );

        return ( counter );
    }
}
