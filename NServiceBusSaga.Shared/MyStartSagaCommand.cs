using NServiceBus;
using System;

namespace NServiceBusSaga.Shared
{
    public class MyStartSagaCommand : ICommand
    {
        public Guid MyEntityId { get; set; }

        public string Data { get; set; }
    }
}

