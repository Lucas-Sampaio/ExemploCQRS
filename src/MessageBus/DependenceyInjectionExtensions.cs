﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace MessageBus
{
    public static class DependenceyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException(nameof(connection), "Informe a string de conexão");
            services.AddSingleton<IMessageBus>(new MessageBus(connection));
            return services;
        }
    }
}
