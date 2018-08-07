using System;

namespace GeekBurger.Orders.API.Model
{
    public class Payment
    {
        public Guid OrderId { get; set; }
        public Guid StoreId { get; set; }
        public PaymentType PayType { get; set; }
        public string CardNumber { get; set; }
        public string CardOwnerName { get; set; }
        public string SecurityCode { get; set; }
        public string ExpirationDate { get; set; }
        public Guid RequesterId { get; set; }
    }
}
