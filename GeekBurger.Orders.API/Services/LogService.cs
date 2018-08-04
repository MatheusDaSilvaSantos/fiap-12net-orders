using GeekBurger.Orders.API.Contracts;

namespace GeekBurger.Orders.API.Services
{
    public class LogService : ILogService
    {
        public void Log(string log)
        {
            var texto = "[Order] - " + log;
        }

        public void SendMessagesAsync(string message) {
            var texto = "[Order] - " + message;

        }
    }
}
