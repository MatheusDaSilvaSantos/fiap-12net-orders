﻿using GeekBurger.Orders.API.Contracts.Infra;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.Management.ServiceBus.Fluent;
using GeekBurger.Orders.API.Infra;
using System.Threading;
using GeekBurger.Orders.API.Contracts.Bus;

namespace GeekBurger.Orders.API.Services.Infra
{
    public abstract class ServiceBusPub<TEntity> : IServiceBusPub<TEntity> where TEntity : class
    {
        public abstract string _topic { get; set; }
        
        protected IConfiguration _configuration;
        protected ILogService _logService;
        protected List<Message> _messages;
        protected Task _lastTask;
        protected IServiceBusNamespace _namespace;

        public ServiceBusPub(IConfiguration configuration, ILogService logService)
            :this(configuration)
        {
            _logService = logService;
        }

        public ServiceBusPub(IConfiguration configuration)
        {
            _configuration = configuration;
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

        public abstract void AddToMessageList(TEntity changes);

        protected abstract Message GetMessage(TEntity entity);

        public async void SendMessagesAsync()
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return;

            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var topicClient = new TopicClient(config.ConnectionString, _topic);

            var logTask = _logService.Log("Order Sent");

            _lastTask = SendAsync(topicClient);

            await _lastTask;

            var closeTask = topicClient.CloseAsync();
            await closeTask;
            HandleException(closeTask);
            await logTask;
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
