namespace StoryWriter
{
    public partial class UC_Component : UserControl, IPanel, IEditorHandler
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }
        Component Component;
        bool IsFirstTimeModified = false;


        string TitleText;

        void ControlInitialize()
        {
            this.Component = Info as Component;
            TitleText = this.Component.Title;
            ParentTabPage.Text = TitleText;

            ucComponentText.RtfText = this.Component.BodyText;       
            App.ZoomFactorChanged += (e, a) => ucComponentText.Editor.ZoomFactor = (float)App.ZoomFactor;

            Ui.RunOnce((Info) => {
                ucComponentText.Editor.Modified = false;
                ucComponentText.InitializeEditor(true);
                ucComponentText.Editor.ModifiedChanged += EditorModifiedChanged;             
                ucComponentText.EditorHandler = this;
                ucComponentText.Focus();

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
        public UC_Component()
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
            ParentTabPage.Text = ucComponentText.Editor.Modified ? TitleText + "*": TitleText;
        }

        public void EditorModifiedChanged(object Sender, EventArgs e)
        {
            RichTextBox Editor = Sender as RichTextBox;            

            if (!IsFirstTimeModified)
            {
                IsFirstTimeModified = true;
                Editor.Modified = false;
            } 

            if (Editor.Modified)
                App.AddDirtyEditor(Editor);

            AdjustTabTitle();
        }
        public void SaveEditorText(RichTextBox Editor)
        {
            Component.BodyText = Editor.Rtf;
            Component.Update();
            Editor.Modified = false;
            ucComponentText.Editor.Modified = false;

            string Message = $"Component: {Component.Title}. - saved";
            LogBox.AppendLine(Message);
        }

        // ● properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Id { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Info { get; set; }
    }
}
