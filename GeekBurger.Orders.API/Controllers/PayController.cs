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
            var order = _orderRepository.GetProductById(request.OrderId);
            if (order == null)
                return NotFound();
            _payService.Pay(order, request);
            _orderRepository.Save(order);
            _orderService.SendOrderChangedToServiceBus(order);
            //TODO: publicar mensagem no OrderChanged
            //_orderService.Send(order);
            return Ok();
        }
    }
}
