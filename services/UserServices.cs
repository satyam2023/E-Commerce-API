using AutoMapper;
using BCrypt.Net;
using ECommerce.Constants.AppStatusCode;
using ECommerce.Constants.LocalString;
using ECommerce.Data;
using ECommerce.Helper.JwtAuthCore;
using ECommerce.Models.Dtos.User;
using ECommerceApp.ApiResponse;
using Microsoft.EntityFrameworkCore;

public interface IUserServices
{
    Task<ApiResponse<UserResponse>> registerUser(CreateUser user);
    Task<ApiResponse<UserResponse>> logIn(LogInUserRequest user);
    Task<ApiResponse<string>> deleteUser(int userId);

    Task<ApiResponse<UserResponse>> updateUser(UpdateUser user, int userId);

    Task<ApiResponse<UserTokenResponse>> refreshToken(RefreshTokenRequest token);
}

public class UserServices : IUserServices
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public UserServices(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApiResponse<UserResponse>> registerUser(CreateUser user)
    {
        bool isUserAlreadyExist = _context.User.Any(c =>
            c.Email == user.Email || c.PhoneNumber == user.PhoneNumber
        );

        if (isUserAlreadyExist)
            return new ApiResponse<UserResponse>(
                AppStatusCode.AlreadyExists,
                LocalString.emailOrPhoneAlreadyExist
            );

        var dbUser = _mapper.Map<User>(user);
        dbUser.Password = BCrypt.Net.BCrypt.HashPassword(dbUser.Password);
        var refreshToken = AuthCore.GenerateRefreshToken();
        var accessToken = AuthCore.GenerateAccessToken(dbUser);
        dbUser.RefreshToken = refreshToken;
        _context.Add(dbUser);
        await _context.SaveChangesAsync();

        var createdUser = _mapper.Map<UserResponse>(dbUser);
        createdUser.AccessToken = accessToken;

        return new ApiResponse<UserResponse>(
            AppStatusCode.Created,
            createdUser,
            LocalString.userCreated
        );
    }

    public async Task<ApiResponse<UserResponse>> logIn(LogInUserRequest user)
    {
        var userDetail = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);

        if (userDetail == null)
        {
            return new ApiResponse<UserResponse>(AppStatusCode.NotFound, LocalString.userNotFound);
        }

        if (!BCrypt.Net.BCrypt.Verify(user.Password, userDetail.Password))
        {
            return new ApiResponse<UserResponse>(
                AppStatusCode.Unauthorized,
                LocalString.incorrectPassword
            );
        }
        var refreshToken = AuthCore.GenerateRefreshToken();
        var accessToken = AuthCore.GenerateAccessToken(userDetail);

        var userData = _mapper.Map<UserResponse>(userDetail);
        userData.AccessToken = accessToken;

        return new ApiResponse<UserResponse>(
            AppStatusCode.Success,
            userData,
            LocalString.userLogInSucess
        );
    }

    public async Task<ApiResponse<string>> deleteUser(int userId)
    {
        var user = await _context.User.FindAsync(userId);

        if (user == null)
            return new ApiResponse<string>(AppStatusCode.NotFound, LocalString.userNotFound);

        _context.User.Remove(user);
        await _context.SaveChangesAsync();

        return new ApiResponse<string>(AppStatusCode.Success, LocalString.userDeleted);
    }

    public async Task<ApiResponse<UserResponse>> updateUser(UpdateUser user, int userId)
    {
        var userDetail = await _context.User.FindAsync(userId);
        if (userDetail == null)
            return new ApiResponse<UserResponse>(AppStatusCode.NotFound, LocalString.userNotFound);

        if (user.Name != null)
            userDetail.Name = user.Name;
        if (user.PhoneNumber != null)
            userDetail.PhoneNumber = user.PhoneNumber;

        _context.User.Update(userDetail);
        await _context.SaveChangesAsync();

        var updatedUser = _mapper.Map<UserResponse>(userDetail);

        return new ApiResponse<UserResponse>(
            AppStatusCode.Success,
            updatedUser,
            LocalString.userUpdatedSuccessfully
        );
    }

    public async Task<ApiResponse<UserTokenResponse>> refreshToken(RefreshTokenRequest token)
    {
        var user = await _context.User.FirstOrDefaultAsync(u =>
            u.RefreshToken == token.RefreshToken
        );
        if (user == null)
        {
            return new ApiResponse<UserTokenResponse>(
                AppStatusCode.NotFound,
                LocalString.invalidRefreshToken
            );
        }
        var accessToken = AuthCore.GenerateAccessToken(user);

        var response = new UserTokenResponse { AccessToken = accessToken };
        return new ApiResponse<UserTokenResponse>(
            AppStatusCode.Success,
            response,
            LocalString.newAccessToken
        );
    }
}
