using System.Collections.Generic;
using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Contracts.Bus;
using GeekBurger.Orders.API.Services.Infra;
using Microsoft.Azure.ServiceBus;

namespace GeekBurger.Orders.API.Services.Bus
{
    public class LogService : /*ServiceBusPub<string>,*/ ILogService
    {
        private const string Topic = "Log";

        public LogService()
        {
            
        }
        public void Log(string message)
        {

        }
    }
}
