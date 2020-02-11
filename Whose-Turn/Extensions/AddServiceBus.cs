using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using Whose_Turn.ConfigModels;
using Whose_Turn.Servicebus;

namespace Whose_Turn.Extensions
{
    public static class ServiceBusExtension
    {
        public static void AddServiceBus(this IServiceCollection services, IConfiguration config)
        {
            // Loads the service bus configuration
            var serviceBusConfig = new ServiceBusConfig();
            config.Bind("ServiceBus", serviceBusConfig);
            services.AddSingleton(serviceBusConfig);

            var endpointConfiguration = new EndpointConfiguration(serviceBusConfig.ApiQueue);
            endpointConfiguration.SendFailedMessagesTo(serviceBusConfig.ErrorQueue);

            var persistence = endpointConfiguration.UsePersistence<LearningPersistence>();
            persistence.SagaStorageDirectory("ServiceBus/Sagas");

            endpointConfiguration.UseSerialization<NewtonsoftSerializer>();
            endpointConfiguration.UseContainer<ServicesBuilder>(
                customizations: customizations => { customizations.ExistingServices(services); });

            endpointConfiguration.EnableInstallers();
            //LogManager.Use<SerilogFactory>();


            var transport = endpointConfiguration.UseTransport<LearningTransport>();
            transport.StorageDirectory("ServiceBus/TransportFiles");

            var routing = transport.Routing();

            routing.RouteToEndpoint(typeof(SendEmail).Assembly, "Whose_Turn.Servicebus",
                serviceBusConfig.ApiQueue);

            var endpoint = Endpoint.Start(endpointConfiguration).Result;

            services.AddSingleton(endpoint);
        }
    }
}
