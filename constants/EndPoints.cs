namespace ECommerce.Constants.AppEndPoint;

public static class AppEndPoints
{
    public const string mainUrl = "e-com/api";
    public const string user = $"{mainUrl}/user";
    public const string registerUser = "registerUser";
    public const string logInUser = "loginUser";
    public const string deleteUser = "deleteUser";

    public const string updateUser = "updateUser";

    public const string refreshToken = "refreshToken";

    public const string address = $"{mainUrl}/address";

    public const string addAddress = "addAddress";
    public const string getAddressByUserId = "getAddressByUserId";
    public const string deleteAddress = "deleteAddress";
}
