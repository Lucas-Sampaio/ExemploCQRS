using Microsoft.Extensions.Configuration;

namespace MessageBus
{
    public static class ConfigurationExtensions
    {
        public static string GetMessageQueueConnection(this IConfiguration config, string name)
        {
            return config?.GetSection("MessageQueueConnection")?[name];
        }
    }
}
