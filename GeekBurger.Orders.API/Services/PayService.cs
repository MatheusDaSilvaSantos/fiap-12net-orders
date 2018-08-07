using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Model;
using GeekBurger.Orders.Contract.Enums;
using System;

namespace GeekBurger.Orders.API.Services
{
    public class PayService : IPayService
    {
        public void Pay(Order order, Payment payment)
        {
            Random ran = new Random();
            int randomIndex = ran.Next(3);
            var state = (OrderState)Enum.ToObject(typeof(OrderState), randomIndex + 1);
            order.SetState(state);
        }
    }
}
