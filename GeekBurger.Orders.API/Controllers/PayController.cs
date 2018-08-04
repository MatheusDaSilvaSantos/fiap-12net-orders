using System;
using AutoMapper;
using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.Contract.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Orders.API.Controllers
{
    [Route("api/pay")]
    public class PayController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPayService _payService;
        private IMapper _mapper;

        public PayController(IOrderRepository orderRepository, IPayService payService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _payService = payService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post([FromBody]PaymentToUpsert order)
        {
            return Ok();
        }
    }
}
