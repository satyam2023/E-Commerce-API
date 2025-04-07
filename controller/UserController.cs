using ECommerce.Constants.AppEndPoint;
using ECommerce.Constants.AppStatusCode;
using ECommerce.Models.Dtos.User;
using ECommerceApp.ApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route(AppEndPoints.user)]
public class UserController : ControllerBase
{
    private IUserServices _userServices;

    public UserController(IUserServices userServices)
    {
        _userServices = userServices;
    }

    [HttpPost(AppEndPoints.registerUser)]
    public async Task<ActionResult<UserResponse>> RegisterUser([FromBody] CreateUser user)
    {
        ApiResponse<UserResponse> response = await _userServices.registerUser(user);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost(AppEndPoints.logInUser)]
    public async Task<ActionResult<UserResponse>> LogInUser([FromBody] LogInUserRequest user)
    {
        ApiResponse<UserResponse> response = await _userServices.logIn(user);
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize]
    [HttpDelete(AppEndPoints.deleteUser)]
    public async Task<ActionResult<string>> DeleteUser(int userId)
    {
        ApiResponse<string> response = await _userServices.deleteUser(userId);
        return StatusCode((int)response.StatusCode, response);
    }

    [Authorize]
    [HttpPut(AppEndPoints.updateUser)]
    public async Task<ActionResult<UserResponse>> UpdateUser(int userId, [FromBody] UpdateUser user)
    {
        ApiResponse<UserResponse> response = await _userServices.updateUser(user, userId);
        return StatusCode((int)response.StatusCode, response);
    }

    [HttpPost(AppEndPoints.refreshToken)]
    public async Task<ActionResult<UserTokenResponse>> RefreshToken(
        [FromBody] RefreshTokenRequest token
    )
    {
        ApiResponse<UserTokenResponse> response = await _userServices.refreshToken(token);
        return StatusCode((int)response.StatusCode, response);
    }
}
