using NServiceBusSaga.Shared;
using System;
using System.Threading.Tasks;

namespace NServiceBusSagaService
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var bus = await BusConfigurator.CreateBus(EndpointConstants.SagaServiceEndpointName);

            var endpoint = await bus.Start()
                .ConfigureAwait(false);

            try
            {
                Console.WriteLine("Listening...");
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
            }
            finally
            {
                await endpoint.Stop()
                    .ConfigureAwait(false);
            }
        }
    }
}
