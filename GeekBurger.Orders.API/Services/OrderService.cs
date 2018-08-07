using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Contracts.Bus;
using GeekBurger.Orders.API.Model;
using System.Threading.Tasks;

namespace GeekBurger.Orders.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly INewOrderService _newOrderService;
        private readonly IOrderChangedService _orderChangedService;

        public OrderService(INewOrderService newOrderService,
            IOrderChangedService orderChangedService)
        {
            _newOrderService = newOrderService;
            _orderChangedService = orderChangedService;
        }

        public async Task SendOrderChangedToServiceBus(Order order)
        {
            await Task.Run(() =>
            {
                _orderChangedService.AddToMessageList(order);
                _orderChangedService.SendMessagesAsync();
            });
        }
    }
}
