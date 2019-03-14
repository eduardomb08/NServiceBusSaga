using NServiceBus;
using NServiceBusSaga.Shared;
using System;
using System.Threading.Tasks;

namespace NServiceBusSagaService
{
    public class MySaga : Saga<MySagaData>, 
        IAmStartedByMessages<MyStartSagaCommand>,
        IAmStartedByMessages<Step1CompletedEvent>,
        IAmStartedByMessages<Step2CompletedEvent>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MySagaData> mapper)
        {
            mapper.ConfigureMapping<MyStartSagaCommand>(message => message.MyEntityId)
                .ToSaga(sagaData => sagaData.MyEntityId);
            mapper.ConfigureMapping<Step1CompletedEvent>(message => message.MyEntityId)
                .ToSaga(sagaData => sagaData.MyEntityId);
            mapper.ConfigureMapping<Step2CompletedEvent>(message => message.MyEntityId)
                .ToSaga(sagaData => sagaData.MyEntityId);
        }

        public async Task Handle(MyStartSagaCommand message, IMessageHandlerContext context)
        {
            MyTimer.Start();
            Data.Data = message.Data;
            await Console.Out.WriteLineAsync($"[{Data.MyEntityId}] Saga has been started: {Data.Data}");

            await context.SendLocal(new MyRequestMessage() {MyEntityId = Data.MyEntityId});
        }

        public async Task Handle(Step1CompletedEvent message, IMessageHandlerContext context)
        {
            await Console.Out.WriteLineAsync($"[{message.MyEntityId}] Step 1 complete at saga");
            Data.Step1Complete = true;
            await IfDoneReplyAndMarkComplete(context);
        }

        public async Task Handle(Step2CompletedEvent message, IMessageHandlerContext context)
        {
            await Console.Out.WriteLineAsync($"[{message.MyEntityId}] Step 2 complete at saga");
            Data.Step2Complete = true;
            await IfDoneReplyAndMarkComplete(context);
        }

        private async Task IfDoneReplyAndMarkComplete(IMessageHandlerContext context)
        {
            if (!AllTasksComplete())
                return;

            MyTimer.Stop();

            await Console.Out.WriteLineAsync($"[{Data.MyEntityId}] Saga complete. Sending response...");
            await Console.Out.WriteLineAsync($"[{Data.MyEntityId}] Saga duration: {MyTimer.ElapsedMilliseconds()}ms");
            await ReplyToOriginator(context,
                    new MySagaResponse() {Data = Data.MyEntityId.ToString(), MyEntityId = Data.MyEntityId})
                .ConfigureAwait(false);

            MarkAsComplete();
        }

        private bool AllTasksComplete()
        {
            return Data.Step1Complete && Data.Step2Complete;
        }
    }
}

