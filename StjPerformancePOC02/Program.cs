using System.Diagnostics;
using System.Text.Json;

namespace StjPerformancePOC02
{
    public record Forecast(DateTime Date, int TemperatureC, string Summary);
    internal class Program
    {
        static void Main()
        {
            Forecast forecast = new(DateTime.Now, 40, "Hot");
            var iterations = 100000;

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++) 
                Serialize(forecast, JsonOptions.SxcUnsafeJsonSerializerOptions);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.SxcUnsafeJsonSerializerOptions)} - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");

            watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++) 
                Serialize(forecast, JsonOptions.Sxc01);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.Sxc01)} - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");

            watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                Serialize(forecast, JsonOptions.Sxc02);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.Sxc02)} - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");

            watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                Serialize(forecast, JsonOptions.Sxc03);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.Sxc03)} - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");

            watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                Serialize(forecast, JsonOptions.Sxc04);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.Sxc04)} - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");

            watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                Serialize(forecast, JsonOptions.SxcUnsafeJsonSerializerOptions);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.SxcUnsafeJsonSerializerOptions)} - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");
        }

        private static string Serialize(Forecast forecast, JsonSerializerOptions options) 
            => JsonSerializer.Serialize<Forecast>(forecast, options);
    }
}
