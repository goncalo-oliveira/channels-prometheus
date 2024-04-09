using Faactory.Channels;
using Faactory.Channels.Handlers;

namespace Faactory.Examples;

public class EchoHandler : ChannelHandler<byte[]>
{
    public override Task ExecuteAsync( IChannelContext context, byte[] data )
    {
        context.Output.Write( data );

        /*
        Additional metric labels can be added by setting values in the Data
        using the "prometheus.label." prefix.
        */
        context.Channel.Data["prometheus.label.test"] = "test";

        return Task.CompletedTask;
    }
}
