using GeekBurger.Orders.API.Model;
using GeekBurger.Orders.Contract.DTOs;

namespace GeekBurger.Orders.API.Contracts
{
    public interface IPayService
    {
        void Pay(Order order, PaymentToUpsert request);
    }
}
