namespace Dotnetstore.Intranet.Organization.Users.ConfirmEmail;

internal sealed class ApplicationUserConfirmEmailEndpoint(
    IApplicationUserService applicationUserService,
    ILogger<ApplicationUserConfirmEmailEndpoint> logger) : Ep.Req<ApplicationUserConfirmEmailRequest>.NoRes
{
    public override void Configure()
    {
        Post(ApiEndpoints.Organization.ApplicationUser.ConfirmEmailAddress);
        Description(x => x
            .ProducesProblemDetails(statusCode: StatusCodes.Status400BadRequest)
            .ProducesProblemDetails(statusCode: StatusCodes.Status404NotFound)
            .AutoTagOverride("Application Users"));
        Summary(s =>
        {
            s.Summary = "Confirm an application user's email";
            s.Description = "This endpoint allows an application user to confirm their email address using a confirmation token.";
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(ApplicationUserConfirmEmailRequest req, CancellationToken ct)
    {
        logger.LogInformation("Handling email confirmation request with code: {Code}", req.Code);
        var result = await applicationUserService.ConfirmUsersEmailAddressAsync(req.Code, ct).ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            var message = string.Join(", ", result.Errors);
            logger.LogWarning("Email confirmation failed for code {Code}: {Message}", req.Code, message);
            AddError(message);

            if (result.Status == ResultStatus.NotFound)
            {
                await Send.ErrorsAsync(statusCode: StatusCodes.Status404NotFound, ct).ConfigureAwait(false);
                return;
            }

            await Send.ErrorsAsync(statusCode: StatusCodes.Status400BadRequest, ct).ConfigureAwait(false);
            return;
        }

        logger.LogInformation("Email confirmation successful for code: {Code}", req.Code);
        await Send.OkAsync(cancellation: ct).ConfigureAwait(false);
    }
}