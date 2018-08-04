namespace GeekBurger.Orders.API.Contracts
{
    public interface ILogService
    {
        void Log(string log);

        void SendMessagesAsync(string message);
    }
}
