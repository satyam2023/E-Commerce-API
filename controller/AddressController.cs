using ECommerce.Constants.AppEndPoint;
using ECommerceApp.ApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route(AppEndPoints.address)]
public class AddressController : ControllerBase
{
    private readonly IAddressServices _addressServices;

    public AddressController(IAddressServices addressServices)
    {
        _addressServices = addressServices;
    }

    [HttpPost(AppEndPoints.addAddress)]
    public async Task<ActionResult<AddressResponse>> AddAddress([FromBody] CreateAddress address)
    {
        ApiResponse<AddressResponse> response = await _addressServices.addAddress(address);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost(AppEndPoints.getAddressByUserId)]
    public async Task<ActionResult<List<AddressResponse>>> GetAddressByUserId(int userId)
    {
        ApiResponse<List<AddressResponse>> response = await _addressServices.getAddressByUserId(
            userId
        );
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpDelete(AppEndPoints.deleteAddress)]
    public async Task<ActionResult<string>> DeleteAddress(int addressId)
    {
        ApiResponse<string> response = await _addressServices.deleteAddress(addressId);
        return StatusCode((int)response.StatusCode, response);
    }
}
