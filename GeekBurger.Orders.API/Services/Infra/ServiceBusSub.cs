using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Contracts.Bus;
using GeekBurger.Orders.API.Contracts.Infra;
using GeekBurger.Orders.API.Infra;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.API.Services.Infra
{
    public abstract class ServiceBusSub : IServiceBusSub
    {
        public abstract string _topic { get; set; }
        public abstract string _subscriptionName { get; set; }

        private IConfiguration _configuration;
        private Task _lastTask;
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
    }
}
