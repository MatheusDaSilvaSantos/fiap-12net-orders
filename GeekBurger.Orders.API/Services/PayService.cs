using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Model;

namespace GeekBurger.Orders.API.Services
{
    public class PayService : IPayService
    {
        public bool Pay(Order order)
        {
            return true;
        }
    }
}
