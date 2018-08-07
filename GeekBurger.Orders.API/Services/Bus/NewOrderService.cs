using GeekBurger.Orders.API.Contracts.Bus;
using GeekBurger.Orders.API.Model;
using GeekBurger.Orders.API.Repository;
using GeekBurger.Orders.API.Services.Infra;
using GeekBurger.Ui.Contracts.Messages;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeekBurger.Orders.API.Services.Bus
{
    public class NewOrderService : ServiceBusSub, INewOrderService
    {
        
        //private readonly IMapper _mapper;
        public override string _topic { get; set; } = "NewOrder";
        public override string _subscriptionName { get; set; } = "SubscriptionNewOrderPay";
        public override string _storeId { get; set; } = "8048e9ec-80fe-4bad-bc2a-e4f4a75c834e";

        public NewOrderService(IConfiguration configuration, ILogService logService)
            : base(configuration, logService)
            => ReceiveMessages(HandleAsync);

        private async Task<Task> HandleAsync(Message message, CancellationToken arg2)
        {
            var newOrderString = Encoding.UTF8.GetString(message.Body);
            var newOrder = JsonConvert.DeserializeObject<NewOrderMessage>(newOrderString);
            var order = new Order
            {
                OrderId = newOrder.OrderId,
                ProductionIds = string.Join("|",newOrder.ProductionIds),
                Products = string.Join("|", newOrder.Products.Select(x => x.ProductId)),
                State = StateOrder.Pending
            };
            using (OrdersContext context = new OrdersContext())
            {
                var orderRepository = new OrderRepository(context);
                await orderRepository.SaveAsync(order);
            }
            return Task.CompletedTask;
        }

    }
}
