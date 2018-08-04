using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Contracts.Bus;
using GeekBurger.Orders.API.Services.Infra;
using Microsoft.Extensions.Configuration;

namespace GeekBurger.Orders.API.Services.Bus
{
    public class NewOrderService : ServiceBusSub, INewOrderService
    {
        public override string _topic { get; set; } = "NewOrder";
        public override string _subscriptionName { get; set; } = "SubscriptionNewOrderPay";

        public NewOrderService(IConfiguration configuration, ILogService logService)
            : base(configuration, logService)
        {
        }
        
    }
}
