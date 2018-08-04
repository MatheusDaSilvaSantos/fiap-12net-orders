using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Model;
using System;
using System.Linq;

namespace GeekBurger.Orders.API.Repository
{   
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersContext _context;
        public OrderRepository(OrdersContext context) 
            => _context = context;
        
        public Order GetProductById(Guid orderId) 
            => _context.Orders?.FirstOrDefault(order => order.OrderId == orderId);

        public void Save(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
