using GeekBurger.Orders.API.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekBurger.Orders.API.Repository
{
    public class OrdersContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseInMemoryDatabase("geekburger-orders");
    }
}
