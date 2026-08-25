namespace Configuration.Config
{
    public class DbSettings : IConfigSection
    {
        public static string FilePath => "Config/DbSettings.json";
        public static string SectionName => nameof(DbSettings);

        public string? Ip { get; set; }
        public string? Id { get; set; }
        public string? Password { get; set; }
        public double Test { get; set; }
    }
}
