using GoodHamburger.Application.Interfaces.Orders;
using GoodHamburger.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.Controllers;

    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly ICreateOrderService _createOrderService;
        private readonly IUpdateOrderService _updateOrderService;
        private readonly IDeleteOrderService _deleteOrderService;
        private readonly IGetOrderByIdService _getOrderByIdService;
        private readonly IGetAllOrderService _getAllOrderService;

        public OrdersController(
            ICreateOrderService createOrderService,
            IUpdateOrderService updateOrderService,
            IDeleteOrderService deleteOrderService,
            IGetOrderByIdService getOrderByIdService,
            IGetAllOrderService getAllOrderService)
        {
            _createOrderService = createOrderService;
            _updateOrderService = updateOrderService;
            _deleteOrderService = deleteOrderService;
            _getOrderByIdService = getOrderByIdService;
            _getAllOrderService = getAllOrderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderRequest request)
        {
            var response = await _createOrderService.CreateOrderAsync(request);

            if (!response.IsSucess)
                return BadRequest(response);

            return Created($"/api/orders/{response.Data?.Id}", response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _getAllOrderService.GetAllOrdersAsync();

            if (!response.IsSucess)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _getOrderByIdService.GetOrderByIdAsync(id);

            if (!response.IsSucess)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] OrderRequest request)
        {
            request.Id = id;

            var response = await _updateOrderService.UpdateOrderAsync(request);

            if (!response.IsSucess)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _deleteOrderService.DeleteOrderAsync(id);

            if (!response.IsSucess)
                return NotFound(response);

            return Ok(response);
        }
    }