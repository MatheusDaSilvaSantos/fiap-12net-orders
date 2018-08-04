using GeekBurger.Orders.API.Model;
using System;

namespace GeekBurger.Orders.API.Contracts
{
    public interface IOrderRepository
    {
        Order GetProductById(Guid orderId);
    }
}
