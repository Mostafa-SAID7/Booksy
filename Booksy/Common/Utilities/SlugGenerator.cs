using System.Text.RegularExpressions;

namespace Booksy.Common.Utilities
{
    /// <summary>
    /// Utility for generating URL-friendly slugs from text
    /// </summary>
    public static class SlugGenerator
    {
        /// <summary>
        /// Generate a URL-friendly slug from text
        /// </summary>
        /// <param name="text">Input text to slugify</param>
        /// <returns>URL-friendly slug</returns>
        public static string Generate(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            // Convert to lowercase
            text = text.ToLowerInvariant();

            // Remove accents/diacritics
            text = RemoveDiacritics(text);

            // Replace spaces with hyphens
            text = Regex.Replace(text, @"\s+", "-");

            // Remove any characters that are not alphanumeric or hyphen
            text = Regex.Replace(text, @"[^a-z0-9\-]", string.Empty);

            // Replace multiple consecutive hyphens with single hyphen
            text = Regex.Replace(text, @"-+", "-");

            // Remove leading/trailing hyphens
            text = text.Trim('-');

            return text;
        }

        /// <summary>
        /// Remove diacritical marks (accents) from text
        /// </summary>
        /// <param name="text">Text with potential diacritics</param>
        /// <returns>Text without diacritics</returns>
        private static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(System.Text.NormalizationForm.FormD);
            var stringBuilder = new System.Text.StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(System.Text.NormalizationForm.FormC);
        }

        /// <summary>
        /// Generate a unique slug by appending a number suffix if needed
        /// </summary>
        /// <param name="baseSlug">Base slug to make unique</param>
        /// <param name="existingSlugs">Collection of existing slugs to check against</param>
        /// <returns>Unique slug</returns>
        public static string GenerateUnique(string baseSlug, IEnumerable<string> existingSlugs)
        {
            if (!existingSlugs.Contains(baseSlug))
                return baseSlug;

            int counter = 1;
            string uniqueSlug = $"{baseSlug}-{counter}";

            while (existingSlugs.Contains(uniqueSlug))
            {
                counter++;
                uniqueSlug = $"{baseSlug}-{counter}";
            }

            return uniqueSlug;
        }
    }
}
