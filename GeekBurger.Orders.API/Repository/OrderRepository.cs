using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Model;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurger.Orders.API.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private OrdersContext _context;
        public OrderRepository(OrdersContext context) 
            => _context = context;
        
        public Order GetProductById(Guid orderId) 
            => _context.Orders?.FirstOrDefault(order => order.OrderId == orderId);

        public async Task SaveAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
        }
    }
}
