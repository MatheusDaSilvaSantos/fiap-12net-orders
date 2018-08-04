using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.API.Contracts.Infra
{
    public interface IServiceBusPub<TEntity> where TEntity : class
    {
        void EnsureTopicIsCreated();

        void AddToMessageList(IEnumerable<EntityEntry<TEntity>> changes);

        Message GetMessage(EntityEntry<TEntity> entity);

        void SendMessagesAsync();

        Task SendAsync(TopicClient topicClient);

        bool HandleException(Task task);
    }
}
