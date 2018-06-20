using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Orders.Contract.Messages
{
    public class NewOrderProductMessage
    {
        public Guid ProductId { get; set; }
    }
}
