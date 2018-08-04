using AutoMapper;
using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Contracts.Bus;
using GeekBurger.Orders.API.Contracts.Infra;
using GeekBurger.Orders.API.Middleware;
using GeekBurger.Orders.API.Repository;
using GeekBurger.Orders.API.Services;
using GeekBurger.Orders.API.Services.Bus;
using GeekBurger.Orders.API.Services.Infra;
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

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info
            {
                Title = "GeekBurger - Orders",
                Version = "v2"
            }));

            services.AddAutoMapper();

            services.AddDbContext<OrdersContext>(o => o.UseInMemoryDatabase("geekburger-orders"));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPayService, PayService>();
            services.AddSingleton<ILogService, LogService>();

            services.AddScoped(typeof(IServiceBusPub<>), typeof(ServiceBusPub<>));
            services.AddScoped<IServiceBusSub, ServiceBusSub>();

            
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderChangedService, OrderChangedService>();
            services.AddScoped<INewOrderService, NewOrderService>();


            mvcCoreBuilder
                .AddFormatterMappings()
                .AddJsonFormatters()
                .AddCors()
                .AddApiExplorer();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //    app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeekBurger Orders API"));

            app.UseMiddleware(typeof(ExceptionMiddleware));

            app.UseMvc();
        }
    }
}
