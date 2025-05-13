using ECommerce.Constants.AppEndPoint;
using ECommerceApp.ApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route(AppEndPoints.product)]
public class ProductController : ControllerBase
{
    private readonly IProductServices _productService;

    public ProductController(IProductServices productService)
    {
        _productService = productService;
    }

    [HttpGet(AppEndPoints.getProductList)]
    public async Task<IActionResult> GetProductList()
    {
        ApiResponse<List<ProductDetail>> response = await _productService.getProductList();
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize]
    [HttpPost(AppEndPoints.createProduct)]
    public async Task<IActionResult> CreateProduct(
        [FromBody] CreateProducts product,
        [FromHeader(Name = "Authorization")] string authorizationHeader
    )
    {
        Console.WriteLine(authorizationHeader);
        ApiResponse<Product> response = await _productService.createProduct(
            product,
            authorizationHeader.Substring(7)
        );
        return StatusCode((int)response.StatusCode, response);
    }
}
