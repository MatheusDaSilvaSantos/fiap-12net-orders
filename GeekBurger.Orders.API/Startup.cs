using AutoMapper;
using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Contracts.Bus;
using GeekBurger.Orders.API.Middleware;
using GeekBurger.Orders.API.Repository;
using GeekBurger.Orders.API.Services;
using GeekBurger.Orders.API.Services.Bus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace GeekBurger.Orders.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;
        

        public void ConfigureServices(IServiceCollection services)
        {
            
            var mvcCoreBuilder = services.AddMvcCore();

            services.AddAutoMapper();

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info
            {
                Title = "GeekBurger - Orders",
                Version = "v2"
            }));

            services.AddDbContext<OrdersContext>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPayService, PayService>();
            services.AddSingleton<ILogService, LogService>();

            services.AddSingleton<IOrderChangedService, OrderChangedService>();
            services.AddSingleton<INewOrderService, NewOrderService>();
            services.AddScoped<IOrderService, OrderService>();

            mvcCoreBuilder
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddCors()
                .AddApiExplorer();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //    app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeekBurger Orders API"));

            app.UseMiddleware(typeof(ExceptionMiddleware));

            app.UseMvc();

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var newOrderService = scope.ServiceProvider.GetService<INewOrderService>();
            }
        }
    }
}
