namespace StoryWriter
{
    public class AppSettings: AppSettingsBase
    {
        protected override void BeforeLoad()
        {
            base.BeforeLoad();
            DefaultTags.Clear();
        }
        protected override void AfterLoad()
        {
            base.AfterLoad();

            if (this.DefaultTags.Count == 0)
            {
                DefaultTags.AddRange(new string[] { "Character", "Location", "People", "Trait", "Event", "Artifact", "Planet" });
            }
        }
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
        public decimal ZoomFactor { get; set; } = 1.0m;
        public List<string> DefaultTags = new List<string>();
    }
}
