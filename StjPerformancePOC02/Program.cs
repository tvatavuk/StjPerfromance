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
            JsonSerializerOptions options = JsonOptions.GetSxcUnsafeJsonSerializerOptions;
            int iterations = 100000;

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                Serialize(forecast, JsonOptions.SxcUnsafeJsonSerializerOptions);
            }
            watch.Stop();
            Console.WriteLine($"Elapsed time using one options instance: {watch.ElapsedMilliseconds}");

            watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
            {
                Serialize(forecast);
            }
            watch.Stop();
            Console.WriteLine($"Elapsed time creating new options instances: {watch.ElapsedMilliseconds}");
        }

        private static void Serialize(Forecast forecast, JsonSerializerOptions? options = null)
        {
            _ = JsonSerializer.Serialize<Forecast>(
                forecast,
                options ?? JsonOptions.GetSxcUnsafeJsonSerializerOptions);
        }
    }
}
