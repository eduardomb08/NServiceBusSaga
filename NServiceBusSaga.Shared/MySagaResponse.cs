using NServiceBus;
using System;

namespace NServiceBusSaga.Shared
{
    public class MySagaResponse : IMessage
    {
        public Guid MyEntityId { get; set; }
        public string Data { get; set; }
    }
}

