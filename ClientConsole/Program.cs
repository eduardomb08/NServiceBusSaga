using NServiceBus;
using NServiceBusSaga.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var bus = await BusConfigurator.CreateBus(EndpointConstants.EndpointName);

            var endpoint = await bus.Start();

            try
            {
                while (true)
                {
                    await Console.Out.WriteLineAsync("Press any key to send a message...");
                    Console.ReadKey();


                    var so = new SendOptions();
                    so.SetDestination(EndpointConstants.SagaServiceEndpointName);

                    var response = await endpoint.Request<MySagaResponse>(new MyStartSagaCommand()
                    { MyEntityId = Guid.NewGuid(), Data = "Hello World" }, so)
                        .ConfigureAwait(false);

                    await Console.Out.WriteLineAsync($"[{response.MyEntityId}]: {response.Data}");
                }
            }
            finally
            {
                await endpoint.Stop();
            }
        }
    }
}
