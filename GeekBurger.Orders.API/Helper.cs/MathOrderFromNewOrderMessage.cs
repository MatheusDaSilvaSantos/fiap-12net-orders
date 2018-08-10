using AutoMapper;
using GeekBurger.Orders.API.Model;
using GeekBurger.Ui.Contracts.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeekBurger.Orders.API.Helper.cs
{
    public class MathOrderFromNewOrderMessage : IMappingAction<NewOrderMessage, Order>
    {
        public void Process(NewOrderMessage source, Order destination)
        {
            var list = source.Products?.Select(x => x.ProductId).ToList() ?? new List<Guid>();
            destination.Products = string.Join("|", list);
            destination.ProductionIds = string.Join("|", source.ProductionIds);
        }
    }
}
