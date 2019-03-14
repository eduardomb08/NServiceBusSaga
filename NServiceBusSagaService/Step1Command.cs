using NServiceBus;
using System;
using System.Threading.Tasks;

namespace NServiceBusSagaService
{
    public class Step1Command : ICommand
    {
        public Guid MyEntityId { get; set; }
    }

    public class Step1CompletedEvent : IEvent
    {
        public Guid MyEntityId { get; set; }
    }

    public class Step1CommandHandler : IHandleMessages<Step1Command>
    {
        public async Task Handle(Step1Command message, IMessageHandlerContext context)
        {
            await Console.Out.WriteLineAsync($"[{message.MyEntityId}] Step 1 complete at handler");
            await context.Publish(new Step1CompletedEvent() { MyEntityId = message.MyEntityId });
        }
    }
}
