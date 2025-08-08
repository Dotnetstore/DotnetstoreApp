using Dotnetstore.Intranet.AppHost.Tests.Helpers;
using Shouldly;

namespace Dotnetstore.Intranet.AppHost.Tests.Playwright;

public class IndexTests(AspireManager aspireManager) : BasePlaywrightTests(aspireManager)
{
    [Fact]
    public async Task IndexPage_Should_LoadSuccessfully()
    {
        await ConfigureAsync<Projects.Dotnetstore_Intranet_AppHost>();

        await InteractWithPageAsync("webui", async page =>
        {
            await page.GotoAsync("/");
            var titleText = await page.GetByTestId("title_index").InnerTextAsync();
            titleText.ShouldNotBeEmpty();
        });
    }
}