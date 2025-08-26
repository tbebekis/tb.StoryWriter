namespace StoryWriter
{
    public partial class UC_ComponentList : UserControl, IPanel
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }
        BindingSource bs = new ();

        void ControlInitialize()
        {
            ParentTabPage.Text = "Components";

            btnAddComponent.Click += (s, e) => AddComponent();
            btnEditComponent.Click += (s, e) => EditComponent();
            btnDeleteComponent.Click += (s, e) => DeleteComponent();

            ReLoadComponents();

            App.ProjectClosed += ProjectClosed;
            App.ProjectOpened += ProjectOpened;

            SelectedComponentChanged();
        }
        void ReLoadComponents()
        {

        }
        void SelectedComponentChanged()
        {

        }

        // ● components
        void AddComponent()
        {

        }
        void EditComponent()
        {

        }
        void DeleteComponent()
        {

        }
        // ● event handlers
        void ProjectClosed(object sender, EventArgs e)
        {

            SelectedComponentChanged();
        }
        void ProjectOpened(object sender, EventArgs e)
        {
            ReLoadComponents();
            SelectedComponentChanged();
        }



        // ● overrides  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
                ControlInitialize();
        }

        // ● construction
        public UC_ComponentList()
        {
            InitializeComponent();
        }

        // ● public
        public void Close()
        {
            App.ProjectClosed -= ProjectClosed;
            App.ProjectOpened -= ProjectOpened;

            TabControl Pager = ParentTabPage.Parent as TabControl;
            if ((Pager != null) && (Pager.TabPages.Contains(ParentTabPage)))
                Pager.TabPages.Remove(ParentTabPage);
        }

        // ● properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Id { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Info { get; set; }
    }
}
