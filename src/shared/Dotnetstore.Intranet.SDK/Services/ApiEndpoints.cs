namespace Dotnetstore.Intranet.SDK.Services;

public static class ApiEndpoints
{
    private const string Api = "/api";

    public static class Organization
    {
        public static class ApplicationUser
        {
            private const string Users = $"{Api}/users";
            
            public const string GetById = $"{Users}/{{id:guid}}";
            public const string Login = $"{Users}/login";
            public const string Create = Users;
            public const string RenewRefreshToken = $"{Users}/renew-refresh-token";
        }
    }
}