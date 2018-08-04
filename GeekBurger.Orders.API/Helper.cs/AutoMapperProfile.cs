using AutoMapper;
using GeekBurger.Orders.API.Model;
using System.Linq;

namespace GeekBurger.Orders.API.Helper
{
    public class AutoMapperProfile : Profile
    {
        protected AutoMapperProfile()
        {
            CreateMap<Ui.Contracts.Messages.NewOrderMessage, Order>()
                .ForMember(x => x.OrderId, x => x.MapFrom(y => y.OrderId))
                .ForMember(x => x.Products, x => x.MapFrom(y => y.Products.Select(z => z.ProductId).ToList()))
                .ForMember(x => x.ProductionIds, x => x.MapFrom(y => y.ProductionIds))
                .ForMember(x => x.Total, x => x.MapFrom(y => y.Total));
        }
    }
}
