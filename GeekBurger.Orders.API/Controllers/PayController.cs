using System;
using GeekBurger.Orders.Contract.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Orders.API.Controllers
{
    [Route("api/[controller]")]
    public class PayController : Controller
    {

        [HttpPost]
        public IActionResult Post([FromBody]PaymentToUpsert order)
        {
            throw new Exception();
            return Ok();
        }
    }
}
