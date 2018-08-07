using GeekBurger.Orders.Contract.Enums;

namespace GeekBurger.Orders.Contract.Messages
{
    public class OrderChangedMessage
    {
        public int OrderId { get; set; }
        public OrderState State { get; set; }
    }
}
