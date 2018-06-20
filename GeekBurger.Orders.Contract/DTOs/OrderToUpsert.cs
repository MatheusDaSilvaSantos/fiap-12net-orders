using GeekBurger.Orders.Contract.DTOs;
using System;
using System.Collections.Generic;

namespace GeekBurger.Orders.Contract
{
    public class OrderToUpsert
    {
        public Guid OrderId { get; set; }
        public List<OrderProductToUpsert> Products { get; set; }
        public List<Guid> ProductionIds { get; set; }
    }
}
