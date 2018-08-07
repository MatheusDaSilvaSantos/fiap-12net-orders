using AutoMapper;
using GeekBurger.Orders.API.Contracts;
using GeekBurger.Orders.API.Model;
using GeekBurger.Orders.Contract.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Post([FromBody]PaymentToUpsert request)
        {
            var payment = _mapper.Map<Payment>(request);
            var order = _orderRepository.GetProductById(payment.OrderId);
            if (order == null)
                return NotFound();
            _payService.Pay(order, payment);
           await Task.Run(() =>_orderRepository.UpdateAsync(order));
           await _orderService.SendOrderChangedToServiceBus(order);
            return Ok();
        }
    }
}
