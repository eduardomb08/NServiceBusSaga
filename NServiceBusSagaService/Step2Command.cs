using NServiceBus;
using System;
using System.Threading.Tasks;

namespace NServiceBusSagaService
{
    public class Step2Command : ICommand
    {
        public Guid MyEntityId { get; set; }
    }

    public class Step2CompletedEvent : IEvent
    {
        public Guid MyEntityId { get; set; }
    }

    public class Step2CommandHandler : IHandleMessages<Step2Command>
    {
        public async Task Handle(Step2Command message, IMessageHandlerContext context)
        {
            await Console.Out.WriteLineAsync($"[{message.MyEntityId}] Step 2 complete at handler");
            await context.Publish(new Step2CompletedEvent() { MyEntityId = message.MyEntityId });
        }
    }
}
