namespace Configuration.Config
{
    public class DbSettings : IConfigSection
    {
        public static string FilePath => "Config/DbSettings.json";
        public static string SectionName => nameof(DbSettings);

        public string? Path { get; set; }
    }
}
