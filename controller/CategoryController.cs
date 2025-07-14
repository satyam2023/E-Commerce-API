using ECommerce.Constants.AppEndPoint;
using ECommerceApp.ApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route(AppEndPoints.category)]
public class CategoryController : ControllerBase
{
    private readonly ICategoryServices _categoryServices;

    public CategoryController(ICategoryServices categoryServices)
    {
        _categoryServices = categoryServices;
    }

    [HttpPost(AppEndPoints.createCategory)]
    public async Task<ActionResult> AddCategory([FromBody] CreateCategory category)
    {
        ApiResponse<CategoryDetails> response = await _categoryServices.addCategory(category);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete(AppEndPoints.deleteCategory)]
    public async Task<ActionResult> DeleteCategory(int categoryId)
    {
        ApiResponse<string> response = await _categoryServices.deleteCategory(categoryId);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPut(AppEndPoints.updateCategory)]
    public async Task<ActionResult> UpdateCategory(
        [FromRoute] int categoryId,
        [FromBody] UpdateCategoryDetail category
    )
    {
        ApiResponse<CategoryDetails> response = await _categoryServices.updateCategory(
            categoryId,
            category
        );
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpGet(AppEndPoints.getAllCategory)]
    public async Task<ActionResult> GetAllCategory()
    {
        ApiResponse<List<CategoryDetails>> response = await _categoryServices.getAllCategories();
        return StatusCode((int)response.StatusCode, response);
    }
}
