using AutoMapper;
using ECommerce.Constants.AppStatusCode;
using ECommerce.Constants.LocalString;
using ECommerce.Data;
using ECommerceApp.ApiResponse;
using Microsoft.EntityFrameworkCore;

public interface IOrderService
{
    public Task<ApiResponse<OrderDetail>> createOrder(CreateOrder order);
}

public class OrderServices : IOrderService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public OrderServices(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    async Task<bool> handleProductCounts(ICollection<CreateOrderItem> orderItems)
    {
        var productIds = orderItems.Select(o => o.ProductId).Distinct().ToList();

        var products = await _context
            .Product.Where(p => productIds.Contains(p.ProductId))
            .ToListAsync();

        foreach (var order in orderItems)
        {
            var product = products.FirstOrDefault(p => p.ProductId == order.ProductId);

            if (product != null && product.AvlStock >= order.Quantity)
                product.AvlStock -= order.Quantity;
            else
                return false;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<ApiResponse<OrderDetail>> createOrder(CreateOrder order)
    {
        if (order.OrderItems == null)
        {
            return new ApiResponse<OrderDetail>(AppStatusCode.BadRequest, LocalString.itemRequired);
        }
        bool isStockAvl = await handleProductCounts(order.OrderItems);
        if (!isStockAvl)
        {
            return new ApiResponse<OrderDetail>(AppStatusCode.BadRequest, LocalString.outOfStock);
        }
        var orderToPlace = _mapper.Map<Order>(order);
        Console.WriteLine(orderToPlace);
        _context.Order.Add(orderToPlace);
        await _context.SaveChangesAsync();
        var orderResponse = _mapper.Map<OrderDetail>(orderToPlace);
        return new ApiResponse<OrderDetail>(
            AppStatusCode.Created,
            orderResponse,
            LocalString.orderPlacedSuccess
        );
    }
}
