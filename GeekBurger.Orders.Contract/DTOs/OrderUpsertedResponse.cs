using System;

namespace GeekBurger.Orders.Contract
{
    public class OrderUpsertedResponse
    {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
    }
}
