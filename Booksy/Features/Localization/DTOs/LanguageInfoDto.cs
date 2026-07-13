namespace Booksy.Features.Localization.DTOs;

/// <summary>
/// Describes a supported language/culture
/// </summary>
public class LanguageInfoDto
{
    /// <summary>BCP-47 language tag (e.g. "en", "ar")</summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>Display name in its own language</summary>
    public string NativeName { get; set; } = string.Empty;

    /// <summary>Display name in English</summary>
    public string EnglishName { get; set; } = string.Empty;

    /// <summary>True for right-to-left scripts</summary>
    public bool IsRtl { get; set; }

    /// <summary>True if this is the currently active culture for the request</summary>
    public bool IsCurrent { get; set; }
}
