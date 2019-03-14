using NServiceBus;
using System;

namespace NServiceBusSaga.Shared
{
    public class MyRequestMessage : ICommand
    {
        public Guid MyEntityId { get; set; }
    }
}

