using System;
using System.Collections.Generic;
using System.Text;

namespace GeekBurger.Orders.Mocks.Messages
{
    [Obsolete("Essa classe devem vir do contrato do GeekBurger.UI.Contract")]
    public class NewOrderProductMessage
    {
        public Guid ProductId { get; set; }
    }
}
