using GeekBurger.Orders.API.Contracts.Infra;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using GeekBurger.Orders.API.Contracts;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using AutoMapper;
using GeekBurger.Orders.API.Infra;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;
using System.Threading;

namespace GeekBurger.Orders.API.Services.Infra
{
    public abstract class ServiceBusPub<TEntity> : IServiceBusPub<TEntity> where TEntity : class
    {
        public abstract string _topic { get; set; }
        
        protected IConfiguration _configuration;
        protected IMapper _mapper;
        protected List<Message> _messages;
        protected Task _lastTask;
        protected IServiceBusNamespace _namespace;
        protected ILogService _logService;

        public ServiceBusPub(IMapper mapper, IConfiguration configuration, ILogService logService)
        {
            _mapper = mapper;
            _configuration = configuration;
            _logService = logService;
            _messages = new List<Message>();
            _namespace = _configuration.GetServiceBusNamespace();
            EnsureTopicIsCreated();
        }

        public void EnsureTopicIsCreated()
        {
            if (!_namespace.Topics.List()
                .Any(topic => topic.Name
                    .Equals(_topic, StringComparison.InvariantCultureIgnoreCase)))
                _namespace.Topics.Define(_topic)
                    .WithSizeInMB(1024).Create();

        }

        public abstract void AddToMessageList(IEnumerable<TEntity> changes);

        protected abstract Message GetMessage(TEntity entity);

        public async void SendMessagesAsync()
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return;

            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var topicClient = new TopicClient(config.ConnectionString, _topic);

            _logService.SendMessagesAsync("Product was changed");

            _lastTask = SendAsync(topicClient);

            await _lastTask;

            var closeTask = topicClient.CloseAsync();
            await closeTask;
            HandleException(closeTask);
        }

        public async Task SendAsync(TopicClient topicClient)
        {
            int tries = 0;
            Message message;
            while (true)
            {
                if (_messages.Count <= 0)
                    break;

                lock (_messages)
                {
                    message = _messages.FirstOrDefault();
                }

                var sendTask = topicClient.SendAsync(message);
                await sendTask;
                var success = HandleException(sendTask);

                if (!success)
                    Thread.Sleep(10000 * (tries < 60 ? tries++ : tries));
                else
                    _messages.Remove(message);
            }
        }

        public bool HandleException(Task task)
        {
            if (task.Exception == null || task.Exception.InnerExceptions.Count == 0) return true;

            task.Exception.InnerExceptions.ToList().ForEach(innerException =>
            {
                Console.WriteLine($"Error in SendAsync task: {innerException.Message}. Details:{innerException.StackTrace} ");

                if (innerException is ServiceBusCommunicationException)
                    Console.WriteLine("Connection Problem with Host. Internet Connection can be down");
            });

            return false;
        }
    }
}
