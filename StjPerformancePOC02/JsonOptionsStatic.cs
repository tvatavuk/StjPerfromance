using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StjPerformancePOC02
{
    internal static class JsonOptionsStatic
    {
        public const int DefaultMaxModelBindingRecursionDepth = 32;

        // used in Oqtane
        public static void SetUnsafeJsonSerializerOptions(this JsonSerializerOptions value)
        {
            value.AllowTrailingCommas = true;
            value.Converters.Add(new DateTimeConverter());
            value.Converters.Add(new JsonStringEnumConverter());
            //value.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
            value.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            value.IncludeFields = true;
            // Limit the object graph we'll consume to a fixed depth. This prevents stackoverflow exceptions
            // from deserialization errors that might occur from deeply nested objects.
            // This value is the same for model binding and Json.Net's serialization.
            value.MaxDepth = DefaultMaxModelBindingRecursionDepth;
            value.NumberHandling = JsonNumberHandling.AllowReadingFromString;
            value.PropertyNameCaseInsensitive = true;
            value.PropertyNamingPolicy = null; // leave property names unchanged (PascalCase for c#)
            value.ReadCommentHandling = JsonCommentHandling.Skip;
            value.WriteIndented = false;
        }
    }
}
