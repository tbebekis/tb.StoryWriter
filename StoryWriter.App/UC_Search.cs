namespace StoryWriter
{
    public partial class UC_Search : UserControl, IPanel
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }

        DataTable tblResults;

        BindingSource bsResults = new();


        void ControlInitialize()
        {
            ParentTabPage.Text = "Search";

            edtSearch.TextChanged += (s, e) => PerformSearch();
            Grid.MouseDoubleClick += (s, e) =>
            {
                if (App.CurrentProject == null)
                    return;

                DataRow Row = bsResults.CurrentDataRow();
                if (Row != null)
                {
                    LinkItem LI = Row["OBJECT"] as LinkItem;
                    ShowPageByLinkItem(LI);
                }
            };

            tblResults = new DataTable("Results");
            tblResults.Columns.Add("Type", typeof(string));
            tblResults.Columns.Add("Place", typeof(string));
            tblResults.Columns.Add("Name", typeof(string));
            tblResults.Columns.Add("OBJECT", typeof(object));

            bsResults.DataSource = tblResults;

            Grid.AutoGenerateColumns = false;
            Grid.DataSource = bsResults;
            Grid.InitializeReadOnly();
            Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            App.ProjectClosed += ProjectClosed;
            App.ProjectOpened += ProjectOpened;
            App.SearchResultsChanged += SearchResultsChanged;
        }
        void PerformSearch()
        {
            if (App.CurrentProject == null) 
                return;
            string Term = edtSearch.Text.Trim();
            if (Term.Length > 2)
            {
                App.CurrentProject.SearchItems(Term); 
            }
        }
        void ClearResults()
        {
            bsResults.SuspendBinding();
            tblResults.DeleteRows();
            tblResults.AcceptChanges();
            bsResults.ResumeBinding();
        }
        /// <summary>
        /// Shows the page for a specified link item, i.e. shows a component page or a chapter page.
        /// </summary>
        public void ShowPageByLinkItem(LinkItem LinkItem)
        {
            switch (LinkItem.ItemType)
            {
                case ItemType.Component:
                    Component Component = LinkItem.Item as Component;
                    App.ContentPagerHandler.ShowPage(typeof(UC_Component), Component.Id, Component);
                    break;
                case ItemType.Chapter:
                    Chapter Chapter = LinkItem.Item as Chapter;
                    App.ContentPagerHandler.ShowPage(typeof(UC_Chapter), Chapter.Id, Chapter);
                    break;
                case ItemType.Scene:
                    Scene Scene = LinkItem.Item as Scene;
                    App.ContentPagerHandler.ShowPage(typeof(UC_Chapter), Scene.Chapter.Id, Scene.Chapter);
                    break;

            }
        }

        // ● event handlers
        void ProjectClosed(object sender, EventArgs e)
        {
            ClearResults();
        }
        void ProjectOpened(object sender, EventArgs e)
        { 
        }
        void SearchResultsChanged(object sender, List<LinkItem> LinkItems)
        {
            ClearResults();

            bsResults.SuspendBinding();
            foreach (var LinkItem in LinkItems)
            {
                tblResults.Rows.Add(LinkItem.ItemType, LinkItem.Place, LinkItem.Name, LinkItem);
            }
            tblResults.AcceptChanges();
            bsResults.ResumeBinding();

            (ParentTabPage.Parent as TabControl).SelectedTab = ParentTabPage;
        }
        // ● overrides  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
                ControlInitialize();
        }

        // ● construction
        public UC_Search()
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
