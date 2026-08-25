namespace Configuration.Config
{
    public class Appsettings : IConfigSection
    {
        public static string FilePath => "Config/Appsettings.json";
        public static string SectionName => nameof(Appsettings);

        public int LogMaxValue { get; set; }
        public string? LogFolderPath { get; set; }
    }
}
