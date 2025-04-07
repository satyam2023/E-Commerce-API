namespace ECommerce.Helper.Regex
{
    public static class RegexExp
    {
        public const string EMAIL_REGEX = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        public const string PHONE_REGEX = @"^[6-9]\d{9}$";

        public const string PASSWORD_REGEX =
            @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$";
    }
}
