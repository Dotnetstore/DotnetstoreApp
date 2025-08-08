namespace Dotnetstore.Intranet.Organization.Users.Create;

internal sealed class ApplicationUserCreateEndpoint(
    IApplicationUserService applicationUserService,
    ILogger<ApplicationUserCreateEndpoint> logger) : Ep.Req<ApplicationUserRegisterRequest>.NoRes
{
    public override void Configure()
    {
        Post(ApiEndpoints.Organization.ApplicationUser.Create);
        Description(x => x
            .Produces(StatusCodes.Status200OK)
            .ProducesProblemDetails()
            .AutoTagOverride("Application Users"));
        Summary(s =>
        {
            s.Summary = "Create Application User";
            s.Description = "Creates a new application user in the organization.";
            s.ExampleRequest = new ApplicationUserRegisterRequest
            {
                FirstName = "John",
                LastName = "Doe",
                EmailAddress = "test@test.com",
                Password = "Password123!",
                ConfirmPassword = "Password123!",
                DateOfBirth = DateTime.UtcNow.AddYears(-30),
                IsMale = true,
                SocialSecurityNumber = null
            };
        });
        AllowAnonymous();
    }

    public override async Task HandleAsync(ApplicationUserRegisterRequest req, CancellationToken ct)
    {
        var result = await applicationUserService.CreateAsync(req, ct).ConfigureAwait(false);

        if (!result.IsSuccess)
        {
            var message = string.Join(", ", result.Errors);
            logger.LogError("Failed to create user: {Error}", message);
            AddError(message);
            await Send.ErrorsAsync(cancellation: ct).ConfigureAwait(false);
            return;
        }

        await Send.OkAsync(StatusCodes.Status200OK, ct);
        // await Send.CreatedAtAsync<ApplicationUserGetByIdEndpoint>(new { id = result.Value.Id }, "User created successfully.", cancellation: ct).ConfigureAwait(false);
    }
}