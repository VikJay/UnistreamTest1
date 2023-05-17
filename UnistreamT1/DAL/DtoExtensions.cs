using System.Text.Json;

namespace UnistreamT1.DAL;

internal static class DtoExtensions
{
    internal static T DeepClone<T>(this T source)
    {
        var stringSource = JsonSerializer.Serialize(source);
        return JsonSerializer.Deserialize<T>(stringSource)!;
    }
}