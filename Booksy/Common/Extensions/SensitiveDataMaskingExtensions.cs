using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Booksy.Security
{
    /// <summary>
    /// Extensions to mask sensitive data in logs and responses
    /// Prevents PII, credentials, tokens from being exposed
    /// </summary>
    public static class SensitiveDataMaskingExtensions
    {
        private static readonly HashSet<string> SensitiveFields = new(StringComparer.OrdinalIgnoreCase)
        {
            "password", "passwd", "pwd",
            "token", "authorization", "authtoken",
            "apikey", "api_key", "secretkey", "secret",
            "creditcard", "cardnumber", "ssn", "socialsecurity",
            "email", "emailaddress",
            "phone", "phonenumber",
            "pincode", "pin",
            "address",
            "dateofbirth", "dob",
            "bankaccount", "accountnumber"
        };

        /// <summary>
        /// Mask all sensitive fields in object for safe logging
        /// </summary>
        public static object MaskSensitiveData(this object? obj)
        {
            if (obj == null)
                return "null";

            if (obj is string str)
                return MaskString(str);

            if (obj is IDictionary dict)
                return MaskDictionary(dict);

            // For custom objects, use reflection
            var type = obj.GetType();
            if (IsSimpleType(type))
                return obj;

            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.IgnoreCase);
            var masked = new Dictionary<string, object?>();

            foreach (var prop in properties)
            {
                try
                {
                    var value = prop.GetValue(obj);
                    if (IsSensitiveField(prop.Name))
                    {
                        masked[prop.Name] = "***REDACTED***";
                    }
                    else if (value != null && !IsSimpleType(value.GetType()))
                    {
                        masked[prop.Name] = MaskSensitiveData(value);
                    }
                    else
                    {
                        masked[prop.Name] = value;
                    }
                }
                catch
                {
                    masked[prop.Name] = "***ERROR***";
                }
            }

            return masked;
        }

        /// <summary>
        /// Mask dictionary values for sensitive keys
        /// </summary>
        private static Dictionary<string, object?> MaskDictionary(IDictionary dict)
        {
            var masked = new Dictionary<string, object?>();

            foreach (DictionaryEntry entry in dict)
            {
                var key = entry.Key?.ToString() ?? "null";
                if (IsSensitiveField(key))
                {
                    masked[key] = "***REDACTED***";
                }
                else
                {
                    masked[key] = entry.Value;
                }
            }

            return masked;
        }

        /// <summary>
        /// Mask string value if it looks like sensitive data
        /// </summary>
        private static string MaskString(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            // Mask email addresses
            if (IsEmail(value))
                return MaskEmail(value);

            // Mask phone numbers
            if (IsPhoneNumber(value))
                return "***" + value.Substring(Math.Max(0, value.Length - 4));

            // Mask credit card patterns
            if (IsCreditCardNumber(value))
                return "****-****-****-" + value.Substring(Math.Max(0, value.Length - 4));

            // Mask tokens/keys
            if (value.Length > 20 && (value.Contains("-") || value.Contains("_") || char.IsLetterOrDigit(value[0])))
                return "***" + value.Substring(Math.Max(0, value.Length - 8));

            return value;
        }

        /// <summary>
        /// Check if field name indicates sensitive data
        /// </summary>
        private static bool IsSensitiveField(string fieldName)
        {
            return SensitiveFields.Any(sensitive =>
                fieldName.Contains(sensitive, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Mask email address (show domain only)
        /// </summary>
        private static string MaskEmail(string email)
        {
            var parts = email.Split('@');
            if (parts.Length != 2)
                return "***@***";

            var username = parts[0];
            var domain = parts[1];
            var maskedUsername = username.Length > 2
                ? username[0] + new string('*', username.Length - 2) + username[^1]
                : "***";

            return $"{maskedUsername}@{domain}";
        }

        /// <summary>
        /// Check if string is email format
        /// </summary>
        private static bool IsEmail(string value)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(value);
                return addr.Address == value;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Check if string is phone number
        /// </summary>
        private static bool IsPhoneNumber(string value)
        {
            return Regex.IsMatch(value, @"^[\d\-\(\)\+\s]{10,20}$");
        }

        /// <summary>
        /// Check if string is credit card number
        /// </summary>
        private static bool IsCreditCardNumber(string value)
        {
            var digitsOnly = Regex.Replace(value, @"\D", "");
            return digitsOnly.Length >= 13 && digitsOnly.Length <= 19;
        }

        /// <summary>
        /// Check if type is simple (primitive or string)
        /// </summary>
        private static bool IsSimpleType(Type type)
        {
            return type.IsPrimitive || type == typeof(string) || type == typeof(decimal) ||
                   type == typeof(DateTime) || type == typeof(DateTimeOffset) ||
                   type == typeof(Guid) || type.IsEnum;
        }

        /// <summary>
        /// Create safe log message with masked data
        /// </summary>
        public static string CreateSafeLogMessage(string message, object? data = null)
        {
            if (data == null)
                return InputSanitizer.SanitizeForLogging(message);

            var maskedData = MaskSensitiveData(data);
            return $"{InputSanitizer.SanitizeForLogging(message)} - Data: {System.Text.Json.JsonSerializer.Serialize(maskedData)}";
        }
    }
}
