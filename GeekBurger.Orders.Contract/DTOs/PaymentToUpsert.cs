using GeekBurger.Orders.Contract.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Orders.Contract.DTOs
{
    public class PaymentToUpsert
    {
        public int OrderId { get; set; }
        public Guid StoreId { get; set; }
        public PaymentMethod PayType { get; set; }
        public string CardNumber { get; set; }
        public string CardOwnerName { get; set; }
        public string SecurityCode { get; set; }
        public string ExpirationDate { get; set; }
        public Guid RequesterId { get; set; }
    }
}
