using GeekBurger.Orders.API.Model;

namespace GeekBurger.Orders.API.Contracts
{
    public interface IPayService
    {
        void Pay(Order order, Payment payment);
    }
}
