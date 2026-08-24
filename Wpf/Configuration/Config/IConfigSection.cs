namespace Configuration.Config
{
    public interface IConfigSection
    {
        static abstract string FilePath { get; }
        static abstract string SectionName { get; }
    }
}
