using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Configuration
{
    public static class OptionsMonitorExtensions
    {
        public static void Save<T>(this IOptionsMonitor<T> monitor) where T : IConfigSection
        {
            var root = JsonNode.Parse(File.ReadAllText(T.FilePath))!;
            root[T.SectionName] = JsonSerializer.SerializeToNode(monitor.CurrentValue);

            File.WriteAllText(T.FilePath, root.ToJsonString(new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}
