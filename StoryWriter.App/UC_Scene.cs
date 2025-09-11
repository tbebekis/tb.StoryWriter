namespace StoryWriter
{
    public partial class UC_Scene : UserControl, IPanel, IEditorHandler
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }
        Scene Scene;
        string TitleText;

        void ControlInitialize()
        {
            this.Scene = Info as Scene;
            TitleChanged();

            ucBodyText.RtfText = this.Scene.BodyText;
            ucSynopsis.RtfText = this.Scene.Synopsis;

            Ui.RunOnce((Info) => {
                ucBodyText.Modified = false;
                ucBodyText.InitializeEditor(false);
                ucBodyText.EditorModifiedChanged += EditorModifiedChanged;
                ucBodyText.EditorHandler = this;
                //ucBodyText.Title = Scene.ToString();
                ucBodyText.Focus();

                ucSynopsis.Modified = false;
                ucSynopsis.InitializeEditor(true);
                ucSynopsis.EditorModifiedChanged += EditorModifiedChanged;
                ucSynopsis.EditorHandler = this;
                //ucSynopsis.Title = Scene.ToString();

            }, 1500, null);
        }

        // ● overrides  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
                ControlInitialize();
        }

        // ● construction
        public UC_Scene()
        {
            InitializeComponent();
        }

        // ● public
        public void Close()
        {
            TabControl Pager = ParentTabPage.Parent as TabControl;
            if ((Pager != null) && (Pager.TabPages.Contains(ParentTabPage)))
                Pager.TabPages.Remove(ParentTabPage);
        }

        void AdjustTabTitle()
        {
            ParentTabPage.Text = ucBodyText.Modified ? TitleText + "*" : TitleText;
        }

        public void EditorModifiedChanged(object Sender, EventArgs e)
        {
            RichTextBox Editor = Sender as RichTextBox;

            if (Editor.Modified)
                App.AddDirtyEditor(Editor);

            AdjustTabTitle();
        }
        public void SaveEditorText(RichTextBox Editor)
        {
            string Message;

            if (ucBodyText.IsOwnEditor(Editor))
            {
                Scene.BodyText = Editor.Rtf;
                Scene.UpdateBodyText();
                Message = $"Scene: {Scene.ToString()}. - Text saved";
            }
            else
            {
                Scene.Synopsis = Editor.Rtf;
                Scene.UpdateSynopsisText();
                Message = $"Scene: {Scene.ToString()}. - Synopsis saved";
            }


            Editor.Modified = false;

            LogBox.AppendLine(Message);
        }
        public void TitleChanged()
        {
            TitleText = Scene.ToString();
            ParentTabPage.Text = $"Scene: " + (ucBodyText.Modified ? TitleText + "*" : TitleText);
            ucBodyText.Title = TitleText;
            ucSynopsis.Title = TitleText;
        }

        // ● properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Id { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Info { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public UC_RichText ucRichText { get { return ucBodyText; } }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CloseableByUser { get; set; } = true;
    }
}
