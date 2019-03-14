using NServiceBus;
using NServiceBusSaga.Shared;
using System.Threading.Tasks;

namespace NServiceBusSagaService
{
    public class MyRequestMessageHandler : IHandleMessages<MyRequestMessage>
    {
        public async Task Handle(MyRequestMessage message, IMessageHandlerContext context)
        {
            await context.SendLocal(new Step1Command() { MyEntityId = message.MyEntityId});
            await context.SendLocal(new Step2Command() { MyEntityId = message.MyEntityId });
        }
    }
}

