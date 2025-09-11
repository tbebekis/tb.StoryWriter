namespace StoryWriter
{
    public partial class UC_Note : UserControl, IPanel, IEditorHandler
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }
        Note Note;
        string TitleText;

        void ControlInitialize()
        {
            this.Note = Info as Note;
            TitleChanged();

            ucBodyText.RtfText = this.Note.BodyText;

            Ui.RunOnce((Info) => {
                ucBodyText.Modified = false;
                ucBodyText.InitializeEditor(true);
                ucBodyText.EditorModifiedChanged += EditorModifiedChanged;
                ucBodyText.EditorHandler = this;
                ucBodyText.Focus();

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
        public UC_Note()
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
            Note.BodyText = Editor.Rtf;
            Note.UpdateBodyText();
            Editor.Modified = false;
            ucBodyText.Modified = false;

            string Message = $"Note: {Note.ToString()}. - saved";
            LogBox.AppendLine(Message);
        }
        public void TitleChanged()
        {
            TitleText = $"Note: {Note.ToString()}";
            ParentTabPage.Text = ucBodyText.Modified ? TitleText + "*" : TitleText;
            ucBodyText.Title = TitleText;
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
