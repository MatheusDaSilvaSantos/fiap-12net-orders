using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Orders.Contract.DTOs
{
    public class OrderProductToUpsert
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
