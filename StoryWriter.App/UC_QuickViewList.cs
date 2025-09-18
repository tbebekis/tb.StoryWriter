namespace StoryWriter
{
    public partial class UC_QuickViewList : UserControl, IPanel
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }

        DataTable tblList;

        BindingSource bsList = new();

        void ControlInitialize()
        {
            ParentTabPage.Text = "Quick View";

            lblItemTitle.Text = "No selection";
            ucRichText.Clear();
            Grid.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

            ucRichText.SetTopPanelVisible(false);
            ucRichText.SetEditorReadOnly(true);
 
            btnEditRtfText.Click += (s, e) => ShowLinkItemPage();
            btnRemoveItem.Click += (s, e) => RemoveLinkItem();
            btnRemoveAll.Click += (s, e) => RemoveAllLinkItems();

            btnUp.Click += (s, e) => MoveRow(Up: true);
            btnDown.Click += (s, e) => MoveRow(Up: false);

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

            bsList.PositionChanged += (s, e) => SelectedLinkItemRowChanged();

            LoadQuickViewList();
        }
        void SelectedLinkItemRowChanged()
        {
            lblItemTitle.Text = "No selection";
            ucRichText.Clear();

            DataRow Row = bsList.CurrentDataRow();
            if (Row != null)
            {
                LinkItem LinkItem = Row["OBJECT"] as LinkItem;

                switch (LinkItem.ItemType)
                {
                    case ItemType.Component:
                        Component Component = LinkItem.Item as Component;
                        lblItemTitle.Text = $"Component: {Component}"; // Component.ToString();
                        ucRichText.RtfText = Component.BodyText;
                        break;
                    case ItemType.Chapter:
                        Chapter Chapter = LinkItem.Item as Chapter;
                        lblItemTitle.Text = $"Chapter: {Chapter}"; // Chapter.ToString();
                        ucRichText.RtfText = Chapter.BodyText;
                        break;
                    case ItemType.Scene:
                        Scene Scene = LinkItem.Item as Scene;
                        lblItemTitle.Text = $"Scene: {Scene}"; // Scene.ToString();
                        ucRichText.RtfText = Scene.BodyText;
                        break;
                }

            }
        }
        void ClearResults()
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
                    App.ContentPagerHandler.ShowPage(typeof(UC_Scene), Scene.Id, Scene);
                    break;
            }
        }
        void RemoveLinkItem()
        {
            DataRow Row = bsList.CurrentDataRow();
            if (Row != null)
            {
                LinkItem LinkItem = Row["OBJECT"] as LinkItem;

                if (!App.QuestionBox($"Are you sure you want to remove '{LinkItem.Item}' from Quick View?"))
                    return;

                Row.Delete();
                tblList.AcceptChanges();

                SaveQuickViewList();
            }
        }
        void RemoveAllLinkItems()
        {
            if (tblList.Rows.Count == 0)
                return;

            if (!App.QuestionBox("Are you sure you want to remove all items from Quick View?"))
                return;

            tblList.DeleteRows();
            tblList.AcceptChanges();

            SaveQuickViewList();
        }
 
        void MoveRow(bool Up)
        {
            if (App.CurrentStory == null)
                return;

            DataRow Row = bsList.CurrentDataRow();
            if (Row == null)
                return;

            if (Up)
                bsList.MoveRowUp();
            else
                bsList.MoveRowDown();

            SaveQuickViewList();
        }

        public void SaveQuickViewList()
        {
            if (App.CurrentStory == null)
                return;            

            List<LinkItem> LinkItems = tblList.Rows.Cast<DataRow>().Select(x => x["OBJECT"] as LinkItem).ToList();

            LinkItemProxyList ProxyList = new();

            foreach (LinkItem LinkItem in LinkItems)
            {
                LinkItemProxy LinkItemProxy = new LinkItemProxy();
                LinkItemProxy.FromLinkItem(LinkItem);
                ProxyList.List.Add(LinkItemProxy);
            }

            string QuickViewListFilePath = App.QuickViewListFilePath;

            Json.SaveToFile(ProxyList, QuickViewListFilePath);

            string Message = $"Quick View List saved to {QuickViewListFilePath}";
            LogBox.AppendLine(Message);
        }
        public void LoadQuickViewList()
        {
            if (App.CurrentStory == null)
                return;

            string QuickViewListFilePath = App.QuickViewListFilePath;

            if (File.Exists(QuickViewListFilePath))
            {
                string JsonText = File.ReadAllText(QuickViewListFilePath);
                LinkItemProxyList ProxyList = Json.Deserialize<LinkItemProxyList>(JsonText);

                foreach (LinkItemProxy LinkItemProxy in ProxyList.List)
                {
                    LinkItem LinkItem = LinkItemProxy.ToLinkItem();
                    tblList.Rows.Add(LinkItem.ItemType, LinkItem.Place, LinkItem.Item.ToString(), LinkItem);
                }

                tblList.AcceptChanges();

                string Message = $"Quick View List loaded from {QuickViewListFilePath}";
                LogBox.AppendLine(Message);
            }
        }

        // ● event handlers
        void StoryClosed(object sender, EventArgs e)
        {
            ClearResults();
        }
        void StoryOpened(object sender, EventArgs e)
        {
            ClearResults();
            LoadQuickViewList();
        }

        // ● overrides  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
                ControlInitialize();
        }

        // ● construction
        public UC_QuickViewList()
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
        public void AddToQuickView(LinkItem LinkItem)
        {
            if (App.CurrentStory == null)
                return;

            List<LinkItem> LinkItems = tblList.Rows.Cast<DataRow>().Select(x => x["OBJECT"] as LinkItem).ToList();
            LinkItem Item = LinkItems.FirstOrDefault(x => (x.Item as BaseEntity).Id == (LinkItem.Item as BaseEntity).Id);

            if (Item == null)
            {
                tblList.Rows.Add(LinkItem.ItemType, LinkItem.Place, LinkItem.Name, LinkItem);

                SaveQuickViewList();

                string Message = $"{LinkItem.ItemType} - {LinkItem.Name} added to Quick View";
                LogBox.AppendLine(Message);
            }
            else
            {
                string Message = $"{LinkItem.ItemType} - {LinkItem.Name} is already in Quick View";
                LogBox.AppendLine(Message);
            }

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
