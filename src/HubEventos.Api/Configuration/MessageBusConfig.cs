using MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HubEventos.Api.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration config)
        {
            services.AddMessageBus(config.GetMessageQueueConnection("MessageBus"));
        }
    }
}
