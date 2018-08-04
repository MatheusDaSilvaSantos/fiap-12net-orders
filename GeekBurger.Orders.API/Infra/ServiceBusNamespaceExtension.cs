﻿using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.API.Infra
{
    public static class ServiceBusNamespaceExtension
    {
        public static IServiceBusNamespace GetServiceBusNamespace(this IConfiguration configuration)
        {
            var config = configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();

            var credentials = SdkContext.AzureCredentialsFactory
                .FromServicePrincipal(config.ClientId, config.ClientSecret,
                        config.TenantId, AzureEnvironment.AzureGlobalCloud);

            var serviceBusManager = ServiceBusManager.Authenticate(credentials, config.SubscriptionId);
            return serviceBusManager.Namespaces.GetByResourceGroup(config.ResourceGroup, config.NamespaceName);
        }
    }
}
