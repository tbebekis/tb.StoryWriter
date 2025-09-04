namespace StoryWriter
{
    public partial class UC_TagList : UserControl, IPanel
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }

        DataTable tblTags;
        DataTable tblTagToComponents;

        BindingSource bsTags = new();
        BindingSource bsTagToComponents = new();

        void ControlInitialize()
        {
            ParentTabPage.Text = "Tags";

            gridComponents.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
            gridTags.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

            btnAddTag.Click += (s, e) => AddTag();
            btnDeleteTag.Click += (s, e) => DeleteTag();
            btnAddDefaultTags.Click += (s, e) => AddDefaultTags();
            btnAddComponentsToTag.Click += (s, e) => AddComponentsToTag();
            btnAddToQuickView.Click += (s, e) => AddToQuickView();


            edtFilter.TextChanged += (s, e) => FilterChanged();
            gridComponents.MouseDoubleClick += (s, e) => EditComponentText();

            ReLoadTags();

            App.ProjectClosed += ProjectClosed;
            App.ProjectOpened += ProjectOpened;  
            App.TagToComponetsChanged += (s, e) => ReLoadTags();

            
            SelectedTagChanged();
        }
        void ReLoadTags()
        {
            if (tblTags == null)
            {
                tblTags = new DataTable("Tags");
                tblTags.Columns.Add("Id", typeof(string));
                tblTags.Columns.Add("Name", typeof(string));
                tblTags.Columns.Add("OBJECT", typeof(object));

                bsTags.DataSource = tblTags;

                gridTags.AutoGenerateColumns = false;
                gridTags.DataSource = bsTags;
                gridTags.InitializeReadOnly();                

                bsTags.PositionChanged += (s, e) => SelectedTagChanged();
            }

            if (tblTagToComponents == null)
            {
                tblTagToComponents = new DataTable("TagToComponents");
                tblTagToComponents.Columns.Add("TagId", typeof(string));
                tblTagToComponents.Columns.Add("ComponentId", typeof(string));
                tblTagToComponents.Columns.Add("Name", typeof(string));
                tblTagToComponents.Columns.Add("OBJECT", typeof(object));

                bsTagToComponents.DataSource = tblTagToComponents;

                gridComponents.AutoGenerateColumns = false;
                gridComponents.DataSource = bsTagToComponents;
                gridComponents.InitializeReadOnly();                
            }

            if (App.CurrentProject != null)
            {
                bsTags.SuspendBinding();
                bsTagToComponents.SuspendBinding();

                tblTags.Rows.Clear();
                tblTagToComponents.Rows.Clear();

                foreach (Tag item in App.CurrentProject.TagList)
                {
                    tblTags.Rows.Add(item.Id, item.Name, item);
                }

                foreach (TagToComponent item in App.CurrentProject.TagToComponentList)
                {
                    tblTagToComponents.Rows.Add(item.Tag.Id, item.Component.Id, item.Component.Name, item);
                }

                tblTags.AcceptChanges();
                tblTagToComponents.AcceptChanges();

                gridComponents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                gridTags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                bsTagToComponents.ResumeBinding();
                bsTags.ResumeBinding();

                SelectedTagChanged();
            }
        }
        void SelectedTagChanged()
        {
            DataRow Row = bsTags.CurrentDataRow();

            if (Row != null)
            {
                bsTagToComponents.Filter = $"TagId = '{Row["Id"]}'";
            }
            else
            {
                bsTagToComponents.Filter = $"ComponentId = 'NOT EXISTEND ID'";
            }
        }
        void FilterChanged()
        {
            string S = edtFilter.Text.Trim();

            if (!string.IsNullOrWhiteSpace(S) && S.Length > 2)
            {
                bsTags.Filter = $"Name LIKE '%{S}%'";
                SelectedTagChanged();
            }
            else
            {
                bsTags.Filter = string.Empty;
            }
        }

        // ● add/delete
        void AddTag()
        {
            string ResultName = "";

            if (EditItemDialog.ShowModal("Add Tag", App.CurrentProject.Name, ref ResultName))
            {
                if (App.CurrentProject.TagExists(ResultName))
                {
                    string Message = $"Tag '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Tag Tag = new();
                Tag.Id = Sys.GenId(UseBrackets: false);
                Tag.Name = ResultName;             

                if (Tag.Insert())
                {
                    DataRow Row = tblTags.Rows.Add(Tag.Id, Tag.Name, Tag);
                    tblTags.AcceptChanges();
                    gridTags.PositionToRow(Row);
                }
                else
                {
                    string Message = "Add Tag. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        void DeleteTag()
        {
            DataRow Row = bsTags.CurrentDataRow();
            if (Row == null)
                return;

            Tag Tag = Row["OBJECT"] as Tag;
            if (Tag == null)
                return;

            if (!App.QuestionBox($"Are you sure you want to delete the tag '{Tag.Name}'?"))
                return;

            App.InfoBox("Delete Tag. NOT YET IMPLEMENTED.");
        }
        void AddDefaultTags()
        {
            if (App.Settings.DefaultTags.Count == 0)
            {
                string Message = "No default tags defined in Application Settings";
                App.ErrorBox(Message);
                LogBox.AppendLine(Message);
                return;
            }

            foreach (string TagName in App.Settings.DefaultTags)
            {
                if (App.CurrentProject.TagExists(TagName))
                {
                    string Message = $"Tag '{TagName}' already exists.";
                    LogBox.AppendLine(Message);
                    continue;
                }

                Tag Tag = new();
                Tag.Id = Sys.GenId(UseBrackets: false);
                Tag.Name = TagName;                

                if (Tag.Insert())
                {
                    DataRow Row = tblTags.Rows.Add(Tag.Id, Tag.Name, Tag);
                    tblTags.AcceptChanges();
                    gridTags.PositionToRow(Row);
                }
                else
                {
                    string Message = "Add Tag. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }

            }
        }
        void EditComponentText()
        {
            DataRow Row = bsTagToComponents.CurrentDataRow();
            if (Row == null)
                return;

            TagToComponent TagToComponent = Row["OBJECT"] as TagToComponent;
            if (TagToComponent == null)
                return;

            Component Component = TagToComponent.Component;
            if (Component == null)
                return;

            App.ContentPagerHandler.ShowPage(typeof(UC_Component), Component.Id, Component);
        }
        void AddComponentsToTag()
        {
            DataRow Row = bsTags.CurrentDataRow();
            if (Row == null)
                return;

            Tag Tag = Row["OBJECT"] as Tag;
            if (Tag == null)
                return;

            App.AddComponentsToTag(Tag);

            Row = tblTags.FindDataRowById(Tag.Id);
            if (Row != null)
                gridTags.PositionToRow(Row);
        }
        void AddToQuickView()
        {
            DataRow Row = bsTagToComponents.CurrentDataRow();
 
            if (Row != null)
            {
                string ComponentId = Row.AsString("ComponentId");
                Component Component = App.CurrentProject.ComponentList.FirstOrDefault(x => x.Id == ComponentId);
                if (Component != null)
                {
                    LinkItem LinkItem = new();
                    LinkItem.ItemType = ItemType.Component;
                    LinkItem.Place = LinkPlace.Title;
                    LinkItem.Name = Component.ToString();
                    LinkItem.Item = Component;

                    TabPage Page = App.SideBarPagerHandler.FindTabPage(nameof(UC_QuickViewList));
                    if (Page != null)
                    {
                        UC_QuickViewList ucQuickViewList = Page.Tag as UC_QuickViewList;
                        ucQuickViewList.AddToQuickView(LinkItem);
                    }
                }
            }
        }

        // ● event handlers
        void ProjectClosed(object sender, EventArgs e)
        {
            bsTags.SuspendBinding();
            bsTagToComponents.SuspendBinding();

            tblTags.Rows.Clear();
            tblTagToComponents.Rows.Clear(); 

            tblTags.AcceptChanges();
            tblTagToComponents.AcceptChanges();

            bsTagToComponents.ResumeBinding();
            bsTags.ResumeBinding();
        }
        void ProjectOpened(object sender, EventArgs e)
        {
            ReLoadTags();
        }

        // ● overrides  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
                ControlInitialize();
        }

        // ● construction
        public UC_TagList()
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool CloseableByUser { get; set; } = false;
    }
}
