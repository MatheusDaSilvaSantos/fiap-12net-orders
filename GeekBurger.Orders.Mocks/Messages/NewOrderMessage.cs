using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Orders.Mocks.Messages
{

    [Obsolete("Essa classe devem vir do contrato do GeekBurger.UI.Contract")]
    public class NewOrderMessage
    {
        public Guid OrderId { get; set; }
        public decimal Total { get; set; }
        public List<NewOrderProductMessage> Products { get; set; }
        public List<Guid> ProductionIds { get; set; }
    }
}
