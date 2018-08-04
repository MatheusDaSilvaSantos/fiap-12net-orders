using GeekBurger.Orders.API.Contracts.Infra;
using GeekBurger.Orders.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.API.Contracts.Bus
{
    public interface IOrderChangedService : IServiceBusPub<Order>
    {

    }
}
