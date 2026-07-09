using System.Text.RegularExpressions;

namespace Booksy.Security
{
    /// <summary>
    /// Centralized input sanitization to prevent XSS, injection attacks
    /// </summary>
    public static class InputSanitizer
    {
        private static readonly Regex HtmlTagRegex = new Regex(@"<[^>]*>", RegexOptions.Compiled);
        private static readonly Regex SpecialCharsRegex = new Regex(@"[^\w\s\-_.]", RegexOptions.Compiled);
        private static readonly Regex SqlInjectionRegex = new Regex(
            @"(\bunion\b|\bselect\b|\binsert\b|\bupdate\b|\bdelete\b|\bdrop\b|\bexec\b|\bscript\b)",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML tags from input
        /// </summary>
        public static string SanitizeHtml(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return HtmlTagRegex.Replace(input, string.Empty).Trim();
        }

        /// <summary>
        /// Remove special characters except allowed ones
        /// </summary>
        public static string SanitizeSpecialChars(string? input, string allowedChars = "-_.")
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var pattern = $"[^\\w\\s{Regex.Escape(allowedChars)}]";
            return Regex.Replace(input, pattern, string.Empty).Trim();
        }

        /// <summary>
        /// Check for potential SQL injection patterns
        /// </summary>
        public static bool ContainsSqlInjectionPatterns(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            return SqlInjectionRegex.IsMatch(input);
        }

        /// <summary>
        /// Sanitize email address
        /// </summary>
        public static string SanitizeEmail(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // Remove any HTML tags
            var cleaned = SanitizeHtml(input);
            // Email should only contain basic chars
            return Regex.Replace(cleaned, @"[^\w\-_.@]", string.Empty).Trim().ToLower();
        }

        /// <summary>
        /// Sanitize URL/slug
        /// </summary>
        public static string SanitizeSlug(string? input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return Regex.Replace(input.ToLower().Trim(), @"[^\w\-]", "-")
                .Trim('-')
                .Replace("--", "-");
        }

        /// <summary>
        /// Sanitize filename for security
        /// </summary>
        public static string SanitizeFilename(string? filename)
        {
            if (string.IsNullOrEmpty(filename))
                return "file";

            // Remove path traversal attempts
            var cleaned = Path.GetFileName(filename);
            // Remove special characters
            return Regex.Replace(cleaned, @"[^a-zA-Z0-9._-]", "_");
        }

        /// <summary>
        /// Truncate and sanitize for logging (prevent log injection)
        /// </summary>
        public static string SanitizeForLogging(string? input, int maxLength = 100)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            var truncated = input.Length > maxLength ? input.Substring(0, maxLength) + "..." : input;
            // Remove newlines and control characters
            return Regex.Replace(truncated, @"[\r\n\t\0-\x1F]", " ");
        }
    }
}
