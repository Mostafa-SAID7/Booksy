using Booksy.Features.Localization.DTOs;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace Booksy.Features.Localization;

/// <summary>
/// Localization endpoints — language discovery and culture switching
/// </summary>
[Route("api/localization")]
[ApiController]
[Tags("Localization")]
public class LocalizationApiController : ControllerBase
{
    private readonly RequestLocalizationOptions _localizationOptions;

    public LocalizationApiController(IOptions<RequestLocalizationOptions> localizationOptions)
    {
        _localizationOptions = localizationOptions.Value;
    }

    /// <summary>
    /// List all supported languages
    /// </summary>
    [HttpGet("languages")]
    [ProducesResponseType(typeof(IEnumerable<LanguageInfoDto>), StatusCodes.Status200OK)]
    public IActionResult GetLanguages()
    {
        var currentCulture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;

        var rtlLanguages = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "ar", "he", "fa", "ur" };

        var languages = (_localizationOptions.SupportedUICultures ?? Enumerable.Empty<CultureInfo>())
            .Select(c => new LanguageInfoDto
            {
                Code        = c.TwoLetterISOLanguageName,
                NativeName  = c.NativeName,
                EnglishName = c.EnglishName,
                IsRtl       = rtlLanguages.Contains(c.TwoLetterISOLanguageName),
                IsCurrent   = string.Equals(c.TwoLetterISOLanguageName, currentCulture,
                                StringComparison.OrdinalIgnoreCase)
            })
            .ToList();

        return Ok(languages);
    }

    /// <summary>
    /// Get the current request culture
    /// </summary>
    [HttpGet("current")]
    [ProducesResponseType(typeof(LanguageInfoDto), StatusCodes.Status200OK)]
    public IActionResult GetCurrentCulture()
    {
        var feature = HttpContext.Features.Get<IRequestCultureFeature>();
        var culture = feature?.RequestCulture.UICulture ?? CultureInfo.CurrentUICulture;

        var rtlLanguages = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "ar", "he", "fa", "ur" };

        var dto = new LanguageInfoDto
        {
            Code        = culture.TwoLetterISOLanguageName,
            NativeName  = culture.NativeName,
            EnglishName = culture.EnglishName,
            IsRtl       = rtlLanguages.Contains(culture.TwoLetterISOLanguageName),
            IsCurrent   = true
        };

        return Ok(dto);
    }

    /// <summary>
    /// Set the preferred language via a response cookie (browser convenience)
    /// </summary>
    [HttpPost("set-language/{lang}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult SetLanguage(string lang)
    {
        var supported = (_localizationOptions.SupportedUICultures ?? Enumerable.Empty<CultureInfo>())
            .Select(c => c.TwoLetterISOLanguageName)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        if (!supported.Contains(lang))
            return BadRequest(new { error = $"Language '{lang}' is not supported. Supported: {string.Join(", ", supported)}" });

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(lang)),
            new CookieOptions
            {
                Expires  = DateTimeOffset.UtcNow.AddYears(1),
                HttpOnly = false,
                SameSite = SameSiteMode.Lax,
                IsEssential = true
            }
        );

        return NoContent();
    }
}
