using AutoMapper;
using ECommerce.Constants.AppStatusCode;
using ECommerce.Constants.LocalString;
using ECommerce.Data;
using ECommerce.Helper.JwtAuthCore;
using ECommerceApp.ApiResponse;
using Microsoft.EntityFrameworkCore;

public interface IProductServices
{
    public Task<ApiResponse<List<ProductDetail>>> getProductList();
    public Task<ApiResponse<Product>> createProduct(CreateProducts product, string token);
}

public class ProductServices : IProductServices
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public ProductServices(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<ApiResponse<List<ProductDetail>>> getProductList()
    {
        var products = await _context.Product.AsNoTracking().ToListAsync();
        List<ProductDetail> productDetail = _mapper.Map<List<ProductDetail>>(products);

        return new ApiResponse<List<ProductDetail>>(
            AppStatusCode.Success,
            productDetail,
            LocalString.productFetchSuccess
        );
    }

    public async Task<ApiResponse<Product>> createProduct(CreateProducts product, string token)
    {
        var categoryExists = await _context.Category.AnyAsync(c =>
            c.CategoryId == product.CategoryId
        );

        if (!categoryExists)
        {
            return new ApiResponse<Product>(AppStatusCode.NotFound, LocalString.categoryIdNotExist);
        }

        var productEntity = _mapper.Map<Product>(product);
        productEntity.CreatedBy = AuthCore.ExtractUserNameFromToken(token);

        await _context.Product.AddAsync(productEntity);
        await _context.SaveChangesAsync();

        return new ApiResponse<Product>(
            AppStatusCode.Created,
            productEntity,
            LocalString.productCreatedSuccessfully
        );
    }
}
