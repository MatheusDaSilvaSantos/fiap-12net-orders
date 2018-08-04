using GeekBurger.Orders.API.Contracts.Infra;

namespace GeekBurger.Orders.API.Contracts.Bus
{
    public interface ILogService /*: IServiceBusPub<string>*/
    {
        void Log(string message);
    }
}
