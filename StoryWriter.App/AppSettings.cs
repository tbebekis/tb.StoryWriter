namespace StoryWriter
{
    public class AppSettings: AppSettingsBase
    {
        public AppSettings()
            : base(IsReloadable:false)
        {
        }

        public bool LoadLastProjectOnStartup { get; set; } = true;
        public string LastProject { get; set; } = "Story";
        public bool AutoSave { get; set; } = true;
        public int AutoSaveSecondsInterval { get; set; } = 30;
        public string FontFamily { get; set; } = "Times New Roman";
        public int FontSize { get; set; } = 13; 
    }
}
