using BlazorUi.Resources;
using Microsoft.Extensions.Localization;

namespace BlazorUi.Services;

internal sealed class ResXMudLocalizer(IStringLocalizer<ResXLanguageResource> localizer)
{
    public string this[string key] => localizer[key];

    public string this[string key, params object[] arguments] => localizer[key, arguments];
}