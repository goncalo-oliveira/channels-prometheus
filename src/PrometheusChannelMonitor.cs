using Faactory.Channels;

internal sealed class PrometheusChannelMonitor : IChannelMonitor
{
    private readonly ChannelMetrics metrics = new();

    public void ChannelClosed( IChannelInfo channelInfo )
    {
        metrics.ChannelsActiveTotal( channelInfo )
            .Dec();
    }

    public void ChannelCreated( IChannelInfo channelInfo )
    {
        metrics.ChannelsActiveTotal( channelInfo )
            .Inc();
    }

    public void CustomEvent( IChannelInfo channelInfo, string name, object? data )
    { }

    public void DataReceived( IChannelInfo channelInfo, byte[] data )
    {
        if ( ( data == null ) || ( data.Length == 0 ) )
        {
            return;
        }

        metrics.ChannelsDataReceived( channelInfo )
            .Inc( data.Length );
    }

    public void DataSent( IChannelInfo channelInfo, int sent )
    {
        if ( sent == 0 )
        {
            return;
        }

        metrics.ChannelsDataSent( channelInfo )
            .Inc( sent );
    }
}
