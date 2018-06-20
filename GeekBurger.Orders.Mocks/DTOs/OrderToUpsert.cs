using GeekBurger.Orders.Mocks.DTOs;
using System;
using System.Collections.Generic;

namespace GeekBurger.Orders.Mocks
{
    [Obsolete("Essa classe devem vir do contrato do GeekBurger.UI.Contract")]
    public class OrderToUpsert
    {
        public Guid OrderId { get; set; }
        public List<OrderProductToUpsert> Products { get; set; }
        public List<Guid> ProductionIds { get; set; }
    }
}
