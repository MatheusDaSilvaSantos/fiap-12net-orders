using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Orders.Mocks.DTOs
{
    [Obsolete("Essa classe devem vir do contrato do GeekBurger.UI.Contract")]
    public class OrderProductToUpsert
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
    }
}
