using GeekBurger.Orders.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.API.Contracts
{
    public interface IOrderService 
    {
        Task SendOrderChangedToServiceBus(Order order);
    }
}
