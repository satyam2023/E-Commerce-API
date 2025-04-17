using AutoMapper;
using ECommerce.Constants.AppStatusCode;
using ECommerce.Constants.LocalString;
using ECommerce.Data;
using ECommerceApp.ApiResponse;
using Microsoft.EntityFrameworkCore;

public interface ICategoryServices
{
    public Task<ApiResponse<CategoryDetails>> addCategory(CreateCategory category);
    public Task<ApiResponse<string>> deleteCategory(int categoryId);
    public Task<ApiResponse<CategoryDetails>> updateCategory(
        int categoryId,
        UpdateCategoryDetail category
    );
    public Task<ApiResponse<List<CategoryDetails>>> getAllCategories();
}

public class CategoryService : ICategoryServices
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public CategoryService(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ApiResponse<CategoryDetails>> addCategory(CreateCategory category)
    {
        var cat = _context.Category.Any(c =>
            c.CategoryName.ToLower() == category.CategoryName.ToLower()
        );
        if (cat)
        {
            return new ApiResponse<CategoryDetails>(
                AppStatusCode.AlreadyExists,
                LocalString.categoryAlreadyExist
            );
        }

        var dbCategory = _mapper.Map<Category>(category);
        _context.Add(dbCategory);

        await _context.SaveChangesAsync();

        var categoryResponse = _mapper.Map<CategoryDetails>(dbCategory);

        return new ApiResponse<CategoryDetails>(
            AppStatusCode.Created,
            categoryResponse,
            LocalString.categoryCreated
        );
    }

    public async Task<ApiResponse<string>> deleteCategory(int categoryId)
    {
        var category = _context.Category.Find(categoryId);
        if (category == null)
        {
            return new ApiResponse<string>(AppStatusCode.NotFound, "Customer Id Not found");
        }
        _context.Category.Remove(category);
        await _context.SaveChangesAsync();

        return new ApiResponse<string>(AppStatusCode.Success, "Category Removed Successfully");
    }

    public async Task<ApiResponse<CategoryDetails>> updateCategory(
        int categoryId,
        UpdateCategoryDetail category
    )
    {
        var existingCategory = await _context.Category.FindAsync(categoryId);
        if (existingCategory == null)
        {
            return new ApiResponse<CategoryDetails>(AppStatusCode.NotFound, "InValid Customer Id");
        }

        if (category.CategoryName != null)
            existingCategory.CategoryName = category.CategoryName;
        if (category.Description != null)
            existingCategory.Description = category.Description;
        if (category.ImageUrl != null)
            existingCategory.ImageUrl = category.ImageUrl;

        _context.Update(existingCategory);
        _context.SaveChanges();

        var responseCategory = _mapper.Map<CategoryDetails>(existingCategory);

        return new ApiResponse<CategoryDetails>(
            AppStatusCode.Success,
            responseCategory,
            "Category Updated Successfully"
        );
    }

    public async Task<ApiResponse<List<CategoryDetails>>> getAllCategories()
    {
        var allCategories = await _context.Category.AsNoTracking().ToListAsync();
        var categoryDetails = _mapper.Map<List<CategoryDetails>>(allCategories);
        return new ApiResponse<List<CategoryDetails>>(
            AppStatusCode.Success,
            categoryDetails,
            "Categories fetched successfully"
        );
    }
}
