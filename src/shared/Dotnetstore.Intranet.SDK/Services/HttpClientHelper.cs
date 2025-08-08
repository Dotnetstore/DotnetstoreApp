using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Dotnetstore.Intranet.SDK.Services;

internal abstract class HttpClientHelper(IHttpClientFactory factory)
{
    private JsonSerializerOptions JsonSerializerOptions { get; } = new(JsonSerializerDefaults.Web);

    protected async ValueTask<HttpResponseMessage> PostAsync<T>(
        string clientName,
        string url,
        T content,
        CancellationToken cancellationToken)
    {
        var client = factory.CreateClient(clientName);
        return await client.PostAsync(url, GetRequestContent(content), cancellationToken);
    }

    private StringContent GetRequestContent<T>(T content)
    {
        return new StringContent(
            JsonSerializer.Serialize(content, JsonSerializerOptions),
            Encoding.UTF8,
            MediaTypeNames.Application.Json);
    }
}