using System.Diagnostics;
using System.Text.Json;
using ToSic.Eav.Data;
using ToSic.Eav.DataSources;
using ToSic.Eav.ImportExport.Json.V1;
using ToSic.Eav.WebApi.Dto;
using ToSic.Eav.WebApi.Formats;
using ToSic.Sxc.WebApi.Usage.Dto;

namespace StjPerformancePOC03
{
    //public record Forecast(DateTime Date, int TemperatureC, string Summary);
    internal class Program
    {
        static void Main()
        {
            var editDto = new EditDto()
            {
                Items = new List<BundleWithHeader<JsonEntity>>()
                {
                    new BundleWithHeader<JsonEntity>()
                    {
                        Entity = new JsonEntity() 
                        {
                            Assets = new List<JsonAsset>(), 
                            Attributes = new JsonAttributes(),
                            For = new JsonMetadataFor(),
                            Guid = Guid.NewGuid(),
                            Id = 1,
                            Metadata = new List<JsonEntity>(),
                            Owner = "owner",
                            Type = new JsonType(),
                            Version = 1
                        },
                        Header = new ItemIdentifier()
                        {
                            Add = true,
                            ContentBlockAppId = 1,
                            ContentTypeName = "ContentTypeName",
                            DuplicateEntity = 1,
                            EditInfo = new EditInfoDto() { ReadOnly = true, ReadOnlyMessage = "ReadOnlyMessage" },
                            EntityId = 1,
                            Field = "Field",
                            For = new JsonMetadataFor(),
                            Guid = Guid.NewGuid(),
                            Index = 1,
                            IsContentBlockMode = true,
                            IsEmpty = true,
                            IsEmptyAllowed = true,
                            Parent = Guid.NewGuid(),
                            Prefill = true
                        }
                    },
                    new BundleWithHeader<JsonEntity>()
                    {
                        Entity = new JsonEntity()
                        {
                            Assets = new List<JsonAsset>(),
                            Attributes = new JsonAttributes(),
                            For = new JsonMetadataFor(),
                            Guid = Guid.NewGuid(),
                            Id = 1,
                            Metadata = new List<JsonEntity>(),
                            Owner = "owner",
                            Type = new JsonType(),
                            Version = 1
                        },
                        Header = new ItemIdentifier()
                        {
                            Add = true,
                            ContentBlockAppId = 1,
                            ContentTypeName = "ContentTypeName",
                            DuplicateEntity = 1,
                            EditInfo = new EditInfoDto() { ReadOnly = true, ReadOnlyMessage = "ReadOnlyMessage" },
                            EntityId = 1,
                            Field = "Field",
                            For = new JsonMetadataFor(),
                            Guid = Guid.NewGuid(),
                            Index = 1,
                            IsContentBlockMode = true,
                            IsEmpty = true,
                            IsEmptyAllowed = true,
                            Parent = Guid.NewGuid(),
                            Prefill = true
                        }
                    },
                    new BundleWithHeader<JsonEntity>()
                    {
                        Entity = new JsonEntity()
                        {
                            Assets = new List<JsonAsset>(),
                            Attributes = new JsonAttributes(),
                            For = new JsonMetadataFor(),
                            Guid = Guid.NewGuid(),
                            Id = 1,
                            Metadata = new List<JsonEntity>(),
                            Owner = "owner",
                            Type = new JsonType(),
                            Version = 1
                        },
                        Header = new ItemIdentifier()
                        {
                            Add = true,
                            ContentBlockAppId = 1,
                            ContentTypeName = "ContentTypeName",
                            DuplicateEntity = 1,
                            EditInfo = new EditInfoDto() { ReadOnly = true, ReadOnlyMessage = "ReadOnlyMessage" },
                            EntityId = 1,
                            Field = "Field",
                            For = new JsonMetadataFor(),
                            Guid = Guid.NewGuid(),
                            Index = 1,
                            IsContentBlockMode = true,
                            IsEmpty = true,
                            IsEmptyAllowed = true,
                            Parent = Guid.NewGuid(),
                            Prefill = true
                        }
                    }
                }
            };
            var iterations = 100000;

            var watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                Serialize(editDto, JsonOptions.SxcUnsafeJsonSerializerOptions);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.SxcUnsafeJsonSerializerOptions)} - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");

            watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                Serialize(editDto, JsonOptions.Sxc01);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.Sxc01)}#1 - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");

            watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                Serialize(editDto, JsonOptions.Sxc01);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.Sxc01)}#2 - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");

            watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                Serialize(editDto, JsonOptions.Sxc01);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.Sxc01)}#3 - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");

            watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                Serialize(editDto, JsonOptions.Sxc01);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.Sxc01)}#4 - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");

            watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                Serialize(editDto, JsonOptions.Sxc02);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.Sxc02)} - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");

            watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                Serialize(editDto, JsonOptions.Sxc03);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.Sxc03)} - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");

            watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                Serialize(editDto, JsonOptions.Sxc04);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.Sxc04)} - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");

            watch = Stopwatch.StartNew();
            for (int i = 0; i < iterations; i++)
                Serialize(editDto, JsonOptions.SxcUnsafeJsonSerializerOptions);
            watch.Stop();
            Console.WriteLine($"{nameof(JsonOptions.SxcUnsafeJsonSerializerOptions)} - Elapsed time using one options instance: {watch.ElapsedMilliseconds}");
        }

        private static string Serialize(EditDto forecast, JsonSerializerOptions options)
            => JsonSerializer.Serialize<EditDto>(forecast, options);
    }
}
