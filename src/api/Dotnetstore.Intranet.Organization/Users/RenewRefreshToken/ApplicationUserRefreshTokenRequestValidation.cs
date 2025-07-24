using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using FastEndpoints;
using FluentValidation;

namespace Dotnetstore.Intranet.Organization.Users.RenewRefreshToken;

internal sealed class ApplicationUserRefreshTokenRequestValidation : Validator<ApplicationUserRefreshTokenRequest>
{
    public ApplicationUserRefreshTokenRequestValidation()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Refresh token is required.");
    }
}