using GeekBurger.Orders.API.Contracts.Bus;
using GeekBurger.Orders.API.Infra;
using GeekBurger.Orders.API.Services.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.API.Services.Bus
{
    public class NewOrderService : ServiceBusSub, INewOrderService
    {
    }
}
