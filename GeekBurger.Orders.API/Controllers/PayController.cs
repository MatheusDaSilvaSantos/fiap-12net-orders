using AutoMapper;
using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Model;
using GeekBurger.Orders.Contract.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurger.Orders.API.Controllers
{
    [Route("api/pay")]
    public class PayController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPayService _payService;
        private readonly IOrderService _orderService;
        private IMapper _mapper;

        public PayController(IOrderRepository orderRepository, IPayService payService, IOrderService orderService, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _payService = payService;
            _mapper = mapper;
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]PaymentToUpsert request)
        {
            var payment = _mapper.Map<Payment>(request);
            var order = _orderRepository.GetProductById(payment.OrderId);
            if (order == null)
                return NotFound();
            _payService.Pay(order, payment);
            _orderRepository.Save(order);
            _orderService.SendOrderChangedToServiceBus(order);
            return Ok();
        }
    }
}
