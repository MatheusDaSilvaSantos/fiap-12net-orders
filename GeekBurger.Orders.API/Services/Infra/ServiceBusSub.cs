using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Contracts.Bus;
using GeekBurger.Orders.API.Contracts.Infra;
using GeekBurger.Orders.API.Infra;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SubscriptionClient = Microsoft.Azure.ServiceBus.SubscriptionClient;

namespace GeekBurger.Orders.API.Services.Infra
{
    public abstract class ServiceBusSub : IServiceBusSub
    {
        public abstract string _topic { get; set; }
        public abstract string _subscriptionName { get; set; }
        public abstract string _storeId { get; set; }

        private IConfiguration _configuration;
        private IServiceBusNamespace _namespace;
        private ILogService _logService;

        public ServiceBusSub(IConfiguration configuration, ILogService logService)
        {
            _configuration = configuration;
            _logService = logService;
            _namespace = _configuration.GetServiceBusNamespace();
            EnsureTopicIsCreated();
            EnsureSubscriptionsIsCreated();
        }

        public void EnsureTopicIsCreated()
        {
            if (!_namespace.Topics
                           .List()
                           .Any(topic => topic.Name.Equals(_topic, StringComparison.InvariantCultureIgnoreCase)))
                _namespace.Topics
                          .Define(_topic)
                          .WithSizeInMB(1024).Create();
        }

        public void EnsureSubscriptionsIsCreated()
        {
            var topic = _namespace.Topics.GetByName(_topic);

            if (!topic.Subscriptions
                      .List()
                      .Any(subscription => subscription.Name.Equals(_subscriptionName, StringComparison.InvariantCultureIgnoreCase)))
                topic.Subscriptions
                     .Define(_subscriptionName)
                     .Create();
        }

        protected async void ReceiveMessages(Func<Message, CancellationToken, Task> handler)
        {

            var serviceBusConfiguration = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var subscriptionClient = new SubscriptionClient(serviceBusConfiguration.ConnectionString, _topic, _subscriptionName);

            //TODO: tratar exceção
            var rules = await subscriptionClient.GetRulesAsync();
            if (rules.Any(x => x.Name == "$Default"))
                await subscriptionClient.RemoveRuleAsync("$Default");

            if(!rules.Any(x => x.Name == "filter-store"))
                await subscriptionClient.AddRuleAsync(new RuleDescription
                {
                    Filter = new CorrelationFilter { Label = _storeId },
                    Name = "filter-store"
                });

            var mo = new MessageHandlerOptions(ExceptionHandle) { AutoComplete = true };

            subscriptionClient.RegisterMessageHandler(handler, mo);
        }

        private static Task ExceptionHandle(ExceptionReceivedEventArgs arg)
        {
            //TODO: melhorar.
            Console.WriteLine($"Message handler encountered an exception {arg.Exception}.");
            var context = arg.ExceptionReceivedContext;
            Console.WriteLine($"- Endpoint: {context.Endpoint}, Path: {context.EntityPath}, Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
