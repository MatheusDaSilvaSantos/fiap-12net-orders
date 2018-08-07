using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Model;
using System;

namespace GeekBurger.Orders.API.Services
{
    public class PayService : IPayService
    {
        public void Pay(Order order, Payment payment)
        {
            Random ran = new Random();
            int randomIndex = ran.Next(3);
            var state = (StateOrder)Enum.ToObject(typeof(StateOrder), randomIndex + 1);
            order.SetState(state);
        }
    }
}
