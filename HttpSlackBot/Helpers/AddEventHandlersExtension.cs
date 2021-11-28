using HttpSlackBot.CommandHandlers;
using HttpSlackBot.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HttpSlackBot.Helpers
{
    public static class AddEventHandlerExtension
    {
        public static void AddEventHandlers(this IServiceCollection services, params Type[] typesInAssembly)
        {
            var eventHandlerRegistry = new HandlersRegistry<SlackEventHandler>();

            services.AddSingleton(eventHandlerRegistry);

            services.AddScoped<SlackCallbackDispatcher>();

            var eventHandlerTypes = new List<Type>();

            foreach (var type in typesInAssembly)
            {
                eventHandlerTypes.AddRange(type.Assembly.GetTypesInNamespace<SlackEventHandler>(type.Namespace));
            }

            foreach (var eventHandler in eventHandlerTypes)
            {
                var eventHandlerAttribute = eventHandler.GetCustomAttribute<EventHandlerAttribute>();

                if (eventHandlerAttribute == null)
                {
                    throw new InvalidOperationException("Event handler must have event attribute");
                }

                eventHandlerRegistry.AddEventHandler(eventHandlerAttribute.EventName, eventHandler);

                services.AddScoped(eventHandler);
            }
        }
    }
}
