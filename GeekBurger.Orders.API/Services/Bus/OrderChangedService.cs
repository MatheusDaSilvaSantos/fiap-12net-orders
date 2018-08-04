using AutoMapper;
using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Contracts.Bus;
using GeekBurger.Orders.API.Model;
using GeekBurger.Orders.API.Services.Infra;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.API.Services.Bus
{
    public class OrderChangedService : ServiceBusPub<Order>, IOrderChangedService
    {
        public override string _topic { get; set; } = "OrderChanged";

        public OrderChangedService(IMapper mapper, IConfiguration configuration, ILogService logService)
            : base(mapper, configuration, logService)
        {
        }

        public override void AddToMessageList(IEnumerable<EntityEntry<Order>> changes)
        {
            throw new NotImplementedException();
        }

        public override Message GetMessage(EntityEntry<Order> entity)
        {
            throw new NotImplementedException();
        }
    }
}
