using NServiceBus;
using System;

namespace NServiceBusSagaService
{
    public class MySagaData : ContainSagaData
    {
        public Guid MyEntityId { get; set; }

        public string Data { get; set; }

        public TimeSpan StarTimeSpan { get; set; }
        public TimeSpan EndTimeSpan { get; set; }

        public bool Step1Complete { get; set; } = false;

        public bool Step2Complete { get; set; } = false;
    }
}

