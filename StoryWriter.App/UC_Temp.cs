namespace StoryWriter
{
    public partial class UC_Temp : UserControl, IPanel, IEditorHandler
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }

        string Title = "Temp";

        void ControlInitialize()
        {
            ParentTabPage.Text = Title;

            ucRichText.Modified = false;
            ucRichText.InitializeEditor(false);
            //ucRichText.SetTopPanelVisible(true);
            //ucRichText.SetEditorReadOnly(true);
            ucRichText.EditorModifiedChanged += EditorModifiedChanged;
            ucRichText.EditorHandler = this;
            //ucRichText.Title = "Temp Doc";
            ucRichText.Focus();
 
            App.StoryClosed += StoryClosed;
            App.StoryOpened += StoryOpened;

            ReLoad();
        }
        void ReLoad()
        {
           ucRichText.RtfText = string.Empty;
           
           if (File.Exists(App.CurrentStoryTempDocFilePath))
           {
               string RtfText = File.ReadAllText(App.CurrentStoryTempDocFilePath);
               ucRichText.RtfText = RtfText;
               ucRichText.Modified = false;

                string Message = $"Temp Doc. Text loaded from {App.CurrentStoryTempDocFilePath}.";
                LogBox.AppendLine(Message);
            }

        }
        public void EditorModifiedChanged(object Sender, EventArgs e)
        {
            if (App.CurrentStory == null)
                return;

            RichTextBox Editor = Sender as RichTextBox;

            if (Editor.Modified)
            {
                ParentTabPage.Text = Title + "*";
                App.AddDirtyEditor(Editor);
            }
                     
        }
        public void SaveEditorText(RichTextBox Editor)
        {
            if (App.CurrentStory == null)
                return;

            string Message;

            string RtfText = Editor.Rtf;
            File.WriteAllText(App.CurrentStoryTempDocFilePath, RtfText);
            Editor.Modified = false;
            ParentTabPage.Text = Title;

            Message = $"Temp Doc. Text saved to {App.CurrentStoryTempDocFilePath}.";
            LogBox.AppendLine(Message);
        }

        // ● event handlers
        void StoryClosed(object sender, EventArgs e)
        {
           /// ucRichText.RtfText = string.Empty;
        }
        void StoryOpened(object sender, EventArgs e)
        {
            ReLoad();
        }

        // ● overrides  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
                ControlInitialize();
        }

        // ● construction
        public UC_Temp()
        {
            InitializeComponent();
        }

        // ● public
        public void Close()
        {
            App.StoryClosed -= StoryClosed;
            App.StoryOpened -= StoryOpened;

            TabControl Pager = ParentTabPage.Parent as TabControl;
            if ((Pager != null) && (Pager.TabPages.Contains(ParentTabPage)))
                Pager.TabPages.Remove(ParentTabPage);
        }

        // ● properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Id { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Info { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CloseableByUser { get; set; } = false;
    }
}
