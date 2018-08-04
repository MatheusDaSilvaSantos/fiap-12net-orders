using GeekBurger.Orders.API.Model;

namespace GeekBurger.Orders.API.Contracts
{
    public interface IPayService
    {
        bool Pay(Order order);
    }
}
