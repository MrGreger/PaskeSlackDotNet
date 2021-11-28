using HttpSlackBot.CommandHandlers;
using HttpSlackBot.Commands;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace HttpSlackBot.Helpers
{
    public static class AddCommandHandlersExtension
    {
        public static void AddCommandHandlers(this IServiceCollection services, params Type[] typesInAssembly)
        {
            var commandHandlerRegistry = new HandlersRegistry<ICommandHandler>();

            services.AddSingleton(commandHandlerRegistry);

            services.AddScoped<CommandDispatcher>();

            var commandHandlerTypes = new List<Type>();

            foreach (var type in typesInAssembly)
            {
                commandHandlerTypes.AddRange(type.Assembly.GetTypesInNamespace<ICommandHandler>(type.Namespace));
            }

            foreach (var eventHandler in commandHandlerTypes)
            {
                var eventHandlerAttribute = eventHandler.GetCustomAttribute<CommandHandlerAttribute>();

                if (eventHandlerAttribute == null)
                {
                    throw new InvalidOperationException("Command handler must have command attribute");
                }

                commandHandlerRegistry.AddEventHandler(eventHandlerAttribute.CommandName, eventHandler);

                services.AddScoped(eventHandler);
            }
        }
    }
}
