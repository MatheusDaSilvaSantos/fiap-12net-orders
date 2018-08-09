using GeekBurger.Orders.API.Contracts.Bus;
using GeekBurger.Orders.API.Infra;
using GeekBurger.Orders.API.Services.Infra;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Orders.API.Services.Bus
{
    public class LogService : ServiceBusPub<string>, ILogService
    {
        public string _microService { get; set; } = "Orders";
        public override string _topic { get; set; } = "Log";

        public LogService(IConfiguration configuration)
            :base(configuration)
        {}

        public async void SendMessagesAsync(string message)
        {
            if (_lastTask != null && !_lastTask.IsCompleted)
                return;
            var config = _configuration.GetSection("serviceBus").Get<ServiceBusConfiguration>();
            var topicClient = new TopicClient(config.ConnectionString, _topic);

            _lastTask = SendAsync(topicClient);
            await _lastTask;
            var closeTask = topicClient.CloseAsync();
            await closeTask;
            HandleException(closeTask);
        }

        public async Task Log(string message)
        {
            await Task.Run(() =>
            {
                AddToMessageList(message);
                SendMessagesAsync(message);
            });
        }

        public override void AddToMessageList(string changes)
            => _messages.Add(GetMessage(changes));

        protected override Message GetMessage(string message)
        {
            var productChangedByteArray = Encoding.UTF8.GetBytes(message);
            return new Message
            {
                Body = productChangedByteArray,
                MessageId = Guid.NewGuid().ToString(),
                Label = _microService
            };
        }
    }
}
