using AutoMapper;
using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Contracts.Bus;
using GeekBurger.Orders.API.Model;
using GeekBurger.Orders.API.Services.Infra;
using GeekBurger.Ui.Contracts.Messages;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Orders.API.Services.Bus
{
    public class NewOrderService : ServiceBusSub, INewOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public override string _topic { get; set; } = "NewOrder";
        public override string _subscriptionName { get; set; } = "SubscriptionNewOrderPay";

        public NewOrderService(IConfiguration configuration, ILogService logService, IOrderRepository orderRepository, IMapper mapper)
            : base(configuration, logService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            ReceiveMessages(Handle);
        }

        private Task Handle(Message message, CancellationToken arg2)
        {
            var newOrderString = Encoding.UTF8.GetString(message.Body);
            var newOrder = JsonConvert.DeserializeObject<NewOrderMessage>(newOrderString);
            var order = _mapper.Map<Order>(newOrder);
            _orderRepository.Save(order);
            return Task.CompletedTask;
        }

    }
}
