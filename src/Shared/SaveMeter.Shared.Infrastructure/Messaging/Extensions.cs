﻿using Microsoft.Extensions.DependencyInjection;
using SaveMeter.Shared.Abstractions.Messaging;
using SaveMeter.Shared.Infrastructure.Messaging.Brokers;
using SaveMeter.Shared.Infrastructure.Messaging.Contexts;
using SaveMeter.Shared.Infrastructure.Messaging.Dispatchers;

namespace SaveMeter.Shared.Infrastructure.Messaging;

public static class Extensions
{
    private const string SectionName = "messaging";
        
    public static IServiceCollection AddMessaging(this IServiceCollection services)
    {
        services.AddTransient<IMessageBroker, InMemoryMessageBroker>();
        services.AddTransient<IAsyncMessageDispatcher, AsyncMessageDispatcher>();
        services.AddSingleton<IMessageChannel, MessageChannel>();
        services.AddSingleton<IMessageContextProvider, MessageContextProvider>();
        services.AddSingleton<IMessageContextRegistry, MessageContextRegistry>();

        var messagingOptions = services.GetOptions<MessagingOptions>(SectionName);
        services.AddSingleton(messagingOptions);

        if (messagingOptions.UseAsyncDispatcher)
        {
            services.AddHostedService<AsyncDispatcherJob>();
        }
            
        return services;
    }
}