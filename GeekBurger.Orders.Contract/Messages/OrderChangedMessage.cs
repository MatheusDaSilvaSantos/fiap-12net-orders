using GeekBurger.Orders.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Orders.Contract.Messages
{
    public class OrderChangedMessage
    {
        public Guid OrderId { get; set; }
        public OrderState State { get; set; }
    }
}
