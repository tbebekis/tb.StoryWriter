namespace StoryWriter
{
    public class AppSettings: AppSettingsBase
    {
        protected override void BeforeLoad()
        {
            base.BeforeLoad();
        }
        protected override void AfterLoad()
        {
            base.AfterLoad();
        }
        public AppSettings()
            : base(IsReloadable:false)
        {
        }

        public bool LoadLastStoryOnStartup { get; set; } = true;
        public string LastStory { get; set; } = "Story";
        public bool AutoSave { get; set; } = true;
        public int AutoSaveSecondsInterval { get; set; } = 30;
        public string FontFamily { get; set; } = "Times New Roman";
        public int FontSize { get; set; } = 13; 
        public decimal ZoomFactor { get; set; } = 1.0m;
    }
}
