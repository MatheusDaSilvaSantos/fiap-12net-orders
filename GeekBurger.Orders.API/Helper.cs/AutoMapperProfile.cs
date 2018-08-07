using AutoMapper;
using GeekBurger.Orders.API.Helper.cs;
using GeekBurger.Orders.API.Model;
using GeekBurger.Orders.Contract.DTOs;
using GeekBurger.Ui.Contracts.Messages;

namespace GeekBurger.Orders.API.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<NewOrderMessage, Order>().AfterMap<MathOrderFromNewOrderMessage>();
            CreateMap<PaymentToUpsert, Payment>();
        }
    }
}
