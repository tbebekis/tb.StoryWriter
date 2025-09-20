namespace StoryWriter
{
    public partial class UC_Search : UserControl, IPanel
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }

        DataTable tblList;
        BindingSource bsList = new();

        void ControlInitialize()
        {
            ParentTabPage.Text = "Search";

            lblItemTitle.Text = "No selection";
            ucRichText.Clear();
            Grid.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

            ucRichText.SetTopPanelVisible(false);
            ucRichText.SetEditorReadOnly(true);

            btnAddToQuickView.Click += (s, e) => AddToQuickView();
            //edtSearch.TextChanged += (s, e) => SearchTextChanged();
            edtSearch.KeyDown += (s, e) => {
                if (e.KeyData == Keys.Enter)
                    SearchTextChanged();
            };
            Grid.MouseDoubleClick += (s, e) => ShowLinkItemPage(); 

            tblList = new DataTable("Results");
            tblList.Columns.Add("Type", typeof(string));
            tblList.Columns.Add("Place", typeof(LinkPlace));
            tblList.Columns.Add("Name", typeof(string));
            tblList.Columns.Add("OBJECT", typeof(object));

            bsList.DataSource = tblList;

            Grid.AutoGenerateColumns = false;
            Grid.DataSource = bsList;
            Grid.InitializeReadOnly();
            Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            App.StoryClosed += StoryClosed;
            App.StoryOpened += StoryOpened;
            App.SearchTermIsSet += SearchTermIsSet;
            App.SearchResultsChanged += SearchResultsChanged;
            App.ItemChanged += (object Sender, BaseEntity Item) =>
            {
                if (Sender != this)
                {
                    foreach (DataRow Row in tblList.Rows)
                    {
                        LinkItem LinkItem = Row["OBJECT"] as LinkItem;
                        BaseEntity RowItem = LinkItem.Item as BaseEntity;
                        if (RowItem != null && RowItem.Id == Item.Id)
                        {
                            Row["Name"] = Item.ToString();
                            break;
                        }
                    }
                }
            };
            App.ItemListChanged += (object Sender, ItemType ItemType) =>
            {
                if (Sender != this)
                {
                    List<DataRow> DeleteRowList = new List<DataRow>();

                    foreach (DataRow Row in tblList.Rows)
                    {
                        LinkItem LinkItem = Row["OBJECT"] as LinkItem;
                        if (!App.ItemExists(LinkItem))
                            DeleteRowList.Add(Row);

                        if (DeleteRowList.Count > 0)
                        {
                            bsList.SuspendBinding();
                            foreach (DataRow RowToDelete in DeleteRowList)
                                tblList.Rows.Remove(RowToDelete);
                            tblList.AcceptChanges();
                            bsList.ResumeBinding();
                        } 
                    }
                }
            };

            bsList.PositionChanged += (s, e) => SelectedLinkItemRowChanged();
        }

 

        void SearchTextChanged()
        {
            if (App.CurrentStory == null) 
                return;

            string Term = edtSearch.Text.Trim();
 
            if (Term.Length > 2)
            {
                ClearAll();
                App.CurrentStory.SearchItems(Term);
            }
            else
            {
                ClearAll();
            }
        }
        void SelectedLinkItemRowChanged()
        {
            lblItemTitle.Text = "No selection";
            ucRichText.Clear();

            DataRow Row = bsList.CurrentDataRow();
            if (Row != null)
            {
                LinkItem LinkItem = Row["OBJECT"] as LinkItem;
                App.UpdateLinkItemUi(LinkItem, lblItemTitle, ucRichText); 
            }
 
        }
        void ClearAll()
        {
            bsList.SuspendBinding();
            tblList.DeleteRows();
            tblList.AcceptChanges();
            bsList.ResumeBinding();

            lblItemTitle.Text = "No selection";
            ucRichText.Clear();
        }
        /// <summary>
        /// Shows the page for a specified link item, i.e. shows a component page or a chapter page.
        /// </summary>
        void ShowLinkItemPage()
        {
            if (App.CurrentStory == null)
                return;

            DataRow Row = bsList.CurrentDataRow();
            if (Row == null)
                return;

            LinkItem LinkItem = Row["OBJECT"] as LinkItem;
            App.ShowLinkItemPage(LinkItem); 
        }
        void AddToQuickView()
        {
            DataRow Row = bsList.CurrentDataRow();
            if (Row != null)
            {
                LinkItem LinkItem = Row["OBJECT"] as LinkItem;

                TabPage Page = App.SideBarPagerHandler.FindTabPage(nameof(UC_QuickViewList));
                if (Page != null)
                {
                    UC_QuickViewList ucQuickViewList = Page.Tag as UC_QuickViewList;
                    ucQuickViewList.AddToQuickView(LinkItem);
                }
            } 
        }

        // ● event handlers
        void StoryClosed(object sender, EventArgs e)
        {
            ClearAll();
        }
        void StoryOpened(object sender, EventArgs e)
        { 
        }
        void SearchTermIsSet(object sender, string NewTerm)
        {
            this.edtSearch.Text = NewTerm;
            SearchTextChanged();
        }
        void SearchResultsChanged(object sender, List<LinkItem> LinkItems)
        {
            ClearAll();

            bsList.SuspendBinding();
            foreach (var LinkItem in LinkItems)
            {
                tblList.Rows.Add(LinkItem.ItemType, LinkItem.Place, LinkItem.Name, LinkItem);
            }
            tblList.AcceptChanges();
            bsList.ResumeBinding();

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
