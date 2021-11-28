using HttpSlackBot.Interactions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HttpSlackBot.Helpers
{
    public static class AddInteractionHandlerExtension
    {
        public static void AddInteracionHandlers(this IServiceCollection services, params Type[] typesInAssembly)
        {
            var interactionHandlerRegistry = new HandlersRegistry<InteractionHandler>();

            services.AddSingleton(interactionHandlerRegistry);

            services.AddScoped<InteractionDispatcher>();

            var commandHandlerTypes = new List<Type>();

            foreach (var type in typesInAssembly)
            {
                commandHandlerTypes.AddRange(type.Assembly.GetTypesInNamespace<InteractionHandler>(type.Namespace));
            }

            foreach (var eventHandler in commandHandlerTypes)
            {
                var eventHandlerAttribute = eventHandler.GetCustomAttribute<InteractionHandlerAttribute>();

                if (eventHandlerAttribute == null)
                {
                    throw new InvalidOperationException("Interaction handler must have command attribute");
                }

                interactionHandlerRegistry.AddEventHandler(eventHandlerAttribute.InteractionActionName, eventHandler);

                services.AddScoped(eventHandler);
            }
        }
    }
}
