using EventsBot;
using EventsBot.EventConsumers.Account;
using EventsBot.EventConsumers.Shop;
using EventsBot.InteractionHandlers;
using EventsBot.Settings;
using GreenPipes;
using HttpSlackBot.Blocks.Checkbox;
using HttpSlackBot.Helpers;
using HttpSlackBot.Messaging;
using HttpSlackBot.Options;
using HttpSlackBot.Templates;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PaskeApp.Events.ProfileEvents;
using SalaryBot;
using SalaryBot.CommandHandlers;
using SalaryBot.EventHandlers;
using SalaryBot.InteractionHandlers;
using ShopList.Application.AppSettings;
using SlackBot.Common;
using SlackBot.Common.EventSending;
using SlackBot.Database;
using SlackBot.MapperProfiles;
using SlackBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackBot
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SlackContext>(options => options.UseNpgsql(Configuration.GetConnectionString("SlackContext")));

            services.Configure<SalaryBotOptions>(x =>
            {
                x.Token = Configuration.GetValue<string>("SalaryBot:Token");
            });

            services.Configure<EventsBotOptions>(x =>
            {
                x.Token = Configuration.GetValue<string>("EventsBot:Token");
            });

            services.AddScoped<SalaryBotMessageSender>();
            services.AddScoped<EventsBotMessageSender>();


            services.AddScoped<ITemplatesProvider, TemplatesProvider>();

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SlackBot", Version = "v1" });
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddAutoMapper(typeof(DefaultProfile));

            services.AddCommandHandlers(typeof(RequestPaymentCommand));
            services.AddEventHandlers(typeof(MessageHandler));
            services.AddInteracionHandlers(typeof(CreateRequestInteractionHandler), typeof(AcceptShopVerificationHandler));

            services.AddHttpClient();

            services.AddScoped<IAppSettingsStorage, AppSettingsService>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<AppLoginEventConsumer>();
                x.AddConsumer<ShopCreatedEventConsumer>();
                x.AddConsumer<ShopVerifyFailedEventConsumer>();
                x.AddConsumer<ShopVerifyRequestedEventConsumer>();
                x.AddConsumer<ShopVerifiedEventConsumer>();

                x.UsingRabbitMq((context, x) =>
                {
                    x.UseRetry(x => x.Interval(5, TimeSpan.FromMinutes(1)));

                    x.Host(new Uri(Configuration.GetValue<string>("RabbitMq:Url")), h =>
                    {
                        h.Username(Configuration.GetValue<string>("RabbitMq:Username"));
                        h.Password(Configuration.GetValue<string>("RabbitMq:Password"));
                    });


                    x.ReceiveEndpoint("logged-in-queue", x =>
                    {
                        x.Consumer<AppLoginEventConsumer>(context);
                    });

                    x.ReceiveEndpoint("shop-created-queue", x =>
                    {
                        x.Consumer<ShopCreatedEventConsumer>(context);
                    });

                    x.ReceiveEndpoint("shop-verify-failed-queue", x =>
                    {
                        x.Consumer<ShopVerifyFailedEventConsumer>(context);
                    });

                    x.ReceiveEndpoint("shop-verify-requested-queue", x =>
                    {
                        x.Consumer<ShopVerifyRequestedEventConsumer>(context);
                    });

                    x.ReceiveEndpoint("shop-verified-queue", x =>
                    {
                        x.Consumer<ShopVerifiedEventConsumer>(context);
                    });
                });
            });

            services.AddScoped<IEventsSender, RabbitMqEventsSender>();

            services.AddMassTransitHostedService();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SlackContext context)
        {
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            context.SeedSettings(typeof(SalaryBotAppSettingIds), typeof(EventsBotSettings));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SlackBot v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
