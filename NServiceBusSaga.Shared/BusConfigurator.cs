using NServiceBus;
using System;
using System.Threading.Tasks;
using NServiceBus.Configuration.AdvancedExtensibility;

namespace NServiceBusSaga.Shared
{
    public static class BusConfigurator
    {
        private static readonly string _transportFolder = @"C:\Temp\NServiceBusTransport";

        public static Task<IStartableEndpoint> CreateBus(string endpointName)
        {
            var ec = new EndpointConfiguration(endpointName);
            ec.UsePersistence<InMemoryPersistence>();
            ec.EnableInstallers();
            ec.EnableCallbacks();
            ec.MakeInstanceUniquelyAddressable(Guid.NewGuid().ToString());

            //ec.Recoverability().Delayed(delayed => delayed.NumberOfRetries(0));

            //var transport = ec.UseTransport<LearningTransport>();
            //transport.StorageDirectory(_transportFolder);

            var transport = ec.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString("host=localhost;username=guest;password=guest");

            var bus = Endpoint.Create(ec);
            return bus;
        }
    }
}
