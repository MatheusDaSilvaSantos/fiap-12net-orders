using GeekBurger.Orders.API.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekBurger.Orders.API.Repository
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options)
            :base(options)
        {}

        public DbSet<Order> Orders { get; set; }
    }
}
