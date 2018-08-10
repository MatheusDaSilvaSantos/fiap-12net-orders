using GeekBurger.Orders.Contract.Enums;
using System;

namespace GeekBurger.Orders.Contract.Messages
{
    public class OrderChangedMessage
    {
        public Guid OrderId { get; set; }
        public Guid StoreId { get; set; }
        public OrderState State { get; set; }
        public decimal Valor { get; set; }
    }
}
