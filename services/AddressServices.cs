using AutoMapper;
using ECommerce.Constants.AppStatusCode;
using ECommerce.Constants.LocalString;
using ECommerce.Data;
using ECommerceApp.ApiResponse;
using Microsoft.EntityFrameworkCore;

public interface IAddressServices
{
    Task<ApiResponse<AddressResponse>> addAddress(CreateAddress address);
    Task<ApiResponse<List<AddressResponse>>> getAddressByUserId(int userId);
    Task<ApiResponse<string>> deleteAddress(int AddressId);
}

public class AddressService : IAddressServices
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public AddressService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<AddressResponse>> addAddress(CreateAddress address)
    {
        var user = await _context.User.FirstOrDefaultAsync(c => c.Id == address.UserId);
        if (user == null)
        {
            return new ApiResponse<AddressResponse>(
                AppStatusCode.NotFound,
                LocalString.userNotFound
            );
        }
        var dbAddress = _mapper.Map<Address>(address);
        _context.Address.Add(dbAddress);
        _context.SaveChanges();
        var response = _mapper.Map<AddressResponse>(dbAddress);

        return new ApiResponse<AddressResponse>(
            AppStatusCode.Created,
            response,
            LocalString.addressAdded
        );
    }

    public async Task<ApiResponse<List<AddressResponse>>> getAddressByUserId(int userId)
    {
        var user = await _context.User.FindAsync(userId);
        if (user == null)
        {
            return new ApiResponse<List<AddressResponse>>(
                AppStatusCode.NotFound,
                LocalString.userNotFound
            );
        }
        var addressList = _context.Address.Where(a => a.UserId == userId);

        var addressResponseList = _mapper.Map<List<AddressResponse>>(addressList.ToList());
        return new ApiResponse<List<AddressResponse>>(
            AppStatusCode.Created,
            addressResponseList,
            LocalString.addressFetched
        );
    }

    public async Task<ApiResponse<string>> deleteAddress(int addressId)
    {
        var address = await _context.Address.FindAsync(addressId);
        if (address == null)
        {
            return new ApiResponse<string>(AppStatusCode.NotFound, LocalString.addressNotFound);
        }
        _context.Address.Remove(address);
        await _context.SaveChangesAsync();

        return new ApiResponse<string>(AppStatusCode.Success, LocalString.addressDeleted);
    }
}
