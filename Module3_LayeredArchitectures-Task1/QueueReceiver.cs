using System;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Module3_LayeredArchitectures_Task1
{
    public class QueueReceiver
    {
        private const string ServiceBusConnectionString = "App.ServiceBus";
        private readonly string _topicName;
        private readonly string _subscription;
        private readonly string connectionString;

        // the client that owns the connection and can be used to create senders and receivers
        static ServiceBusClient client;

        // the processor that reads and processes messages from the queue
        static ServiceBusProcessor processor;

        public QueueReceiver(IConfiguration configuration)
        {
            connectionString = configuration[ServiceBusConnectionString];
            _topicName = configuration["ServiceBus.Topic.ItemUpdate"];
            _subscription = configuration["ServiceBus.Topic.Subscription"];
        }
        // handle received messages
        static async Task Handler(ProcessMessageEventArgs args)
        {
            string body = args.Message.Body.ToString();
            var item = JsonSerializer.Deserialize<Item>(body);


            // complete the message. messages is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }

        // handle any errors when receiving messages
        static Task ErrorHandler(ProcessErrorEventArgs args)
        {
            return Task.CompletedTask;
        }

        public async Task SetUp()
        {
            // The Service Bus client types are safe to cache and use as a singleton for the lifetime
            // of the application, which is best practice when messages are being published or read
            // regularly.
            //

            // Create the client object that will be used to create sender and receiver objects
            client = new ServiceBusClient(connectionString);

            // create a processor that we can use to process the messages
            processor = client.CreateProcessor(_topicName, _subscription, new ServiceBusProcessorOptions());

            try
            {
                // add handler to process messages
                processor.ProcessMessageAsync += Handler;

                // add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                // start processing 
                await processor.StartProcessingAsync();

                Console.WriteLine("Wait for a minute and then press any key to end the processing");
                Console.ReadKey();

                // stop processing 
                Console.WriteLine("\nStopping the receiver...");
                await processor.StopProcessingAsync();
                Console.WriteLine("Stopped receiving messages");
            }
            finally
            {
                // Calling DisposeAsync on client types is required to ensure that network
                // resources and other unmanaged objects are properly cleaned up.
                //processor.DisposeAsync();
                //client.DisposeAsync();
            }
        }
    }
}
