using GeekBurger.Orders.API.Contracts.Infra;
using System.Threading.Tasks;

namespace GeekBurger.Orders.API.Contracts.Bus
{
    public interface ILogService /*: IServiceBusPub<string>*/
    {
        Task Log(string message);
    }
}
