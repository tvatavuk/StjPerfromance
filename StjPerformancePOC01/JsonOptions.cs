using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace StjPerformancePOC01
{
    public class JsonOptions
    {
        public const int DefaultMaxModelBindingRecursionDepth = 32;

        /// <summary>
        /// Compared to the default encoder, the UnsafeRelaxedJsonEscaping encoder is more permissive about allowing characters to pass through unescaped:
        /// - It doesn't escape HTML-sensitive characters such as <, >, &, and '.
        /// - It doesn't offer any additional defense-in-depth protections against XSS or information disclosure attacks, such as those which might result from the client and server disagreeing on the charset.
        /// Use the unsafe encoder only when it's known that the client will be interpreting the resulting payload as UTF-8 encoded JSON.
        /// For example, you can use it if the server is sending the response header Content-Type: application/json; charset=utf-8.
        /// Never allow the raw UnsafeRelaxedJsonEscaping output to be emitted into an HTML page or a <script> element.
        /// https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-character-encoding#serialize-all-characters
        /// </summary>
        public static JsonSerializerOptions SxcUnsafeJsonSerializerOptions
        {
            get => _sxcUnsafeJsonSerializerOptions ??= GetSxcUnsafeJsonSerializerOptions;
            set => _sxcUnsafeJsonSerializerOptions = value;
        }
        private static JsonSerializerOptions _sxcUnsafeJsonSerializerOptions;

        public static JsonSerializerOptions GetSxcUnsafeJsonSerializerOptions => new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            Converters = { new DateTimeConverter(), new JsonStringEnumConverter() },
            //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            IncludeFields = true,
            // Limit the object graph we'll consume to a fixed depth. This prevents stackoverflow exceptions
            // from deserialization errors that might occur from deeply nested objects.
            // This value is the same for model binding and Json.Net's serialization.
            MaxDepth = DefaultMaxModelBindingRecursionDepth,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = null, // leave property names unchanged (PascalCase for c#)
            ReadCommentHandling = JsonCommentHandling.Skip,
            WriteIndented = false,
        };

        // this one shoudl be safe for injecting in html
        public static JsonSerializerOptions SxcAttributeJsonSerializerOptions = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            Converters = { new DateTimeConverter(), new JsonStringEnumConverter() },
            //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.CurrencySymbols/*, UnicodeRanges.Latin1Supplement, UnicodeRanges.LatinExtendedA, UnicodeRanges.LatinExtendedB, UnicodeRanges.LatinExtendedC, UnicodeRanges.LatinExtendedD, UnicodeRanges.LatinExtendedE, UnicodeRanges.LatinExtendedAdditional*/),
            IncludeFields = true,
            // Limit the object graph we'll consume to a fixed depth. This prevents stackoverflow exceptions
            // from deserialization errors that might occur from deeply nested objects.
            // This value is the same for model binding and Json.Net's serialization.
            MaxDepth = DefaultMaxModelBindingRecursionDepth,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = null, // leave property names unchanged (PascalCase for c#)
            ReadCommentHandling = JsonCommentHandling.Skip,
            WriteIndented = false,
        };

        public static JsonSerializerOptions FeaturesJsonSerializerOptions = new JsonSerializerOptions()
        {
            AllowTrailingCommas = true,
            Converters = { new ShortDateTimeConverter() },
            //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            IncludeFields = true,
            // Limit the object graph we'll consume to a fixed depth. This prevents stackoverflow exceptions
            // from deserialization errors that might occur from deeply nested objects.
            // This value is the same for model binding and Json.Net's serialization.
            MaxDepth = DefaultMaxModelBindingRecursionDepth,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = null, // leave property names unchanged (PascalCase for c#)
            ReadCommentHandling = JsonCommentHandling.Skip,
            WriteIndented = false,
        };

        public static JsonDocumentOptions SxcJsonDocumentOptions = new JsonDocumentOptions()
        {
            AllowTrailingCommas = true,
            CommentHandling = JsonCommentHandling.Skip,
            // Limit the object graph we'll consume to a fixed depth. This prevents stackoverflow exceptions
            // from deserialization errors that might occur from deeply nested objects.
            // This value is the same for model binding and Json.Net's serialization.
            MaxDepth = DefaultMaxModelBindingRecursionDepth,
        };

        public static JsonNodeOptions SxcJsonNodeOptions = new JsonNodeOptions()
        {
            PropertyNameCaseInsensitive = true,
        };



        /// <summary>
        /// Compared to the default encoder, the UnsafeRelaxedJsonEscaping encoder is more permissive about allowing characters to pass through unescaped:
        /// - It doesn't escape HTML-sensitive characters such as <, >, &, and '.
        /// - It doesn't offer any additional defense-in-depth protections against XSS or information disclosure attacks, such as those which might result from the client and server disagreeing on the charset.
        /// Use the unsafe encoder only when it's known that the client will be interpreting the resulting payload as UTF-8 encoded JSON.
        /// For example, you can use it if the server is sending the response header Content-Type: application/json; charset=utf-8.
        /// Never allow the raw UnsafeRelaxedJsonEscaping output to be emitted into an HTML page or a <script> element.
        /// https://learn.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-character-encoding#serialize-all-characters
        /// </summary>
    }
}
