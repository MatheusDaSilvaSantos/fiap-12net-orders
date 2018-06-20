using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurger.Orders.Contract.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Orders.API.Controllers
{
    [Route("api/[controller]")]
    public class PayController : Controller
    {

        // POST api/pay
        [HttpPost]
        public IActionResult Post([FromBody]PaymentToUpsert order)
        {
            return new OkResult();
        }
    }
}
