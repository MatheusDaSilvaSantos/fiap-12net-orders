using AutoMapper;
using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Contracts.Bus;
using GeekBurger.Orders.API.Model;
using GeekBurger.Orders.API.Services.Infra;
using GeekBurger.Orders.Contract.Messages;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public override void AddToMessageList(IEnumerable<Order> orders)
        {
            _messages.AddRange(orders.Select(GetMessage));
        }

        protected override Message GetMessage(Order order)
        {
            var productChanged = Mapper.Map<OrderChangedMessage>(order);
            var productChangedSerialized = JsonConvert.SerializeObject(productChanged);
            var productChangedByteArray = Encoding.UTF8.GetBytes(productChangedSerialized);

            return new Message
            {
                Body = productChangedByteArray,
                MessageId = Guid.NewGuid().ToString(),
                Label = $"[Orders] - OrderChanged: {productChanged.OrderId.ToString()}"
            };
        }
    }
}
