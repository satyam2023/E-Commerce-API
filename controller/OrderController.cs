using ECommerce.Constants.AppEndPoint;
using ECommerceApp.ApiResponse;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route(AppEndPoints.order)]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost(AppEndPoints.createOrder)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrder order)
    {
        ApiResponse<OrderDetail> response = await _orderService.createOrder(order);
        return StatusCode((int)response.StatusCode, response);
    }
}
