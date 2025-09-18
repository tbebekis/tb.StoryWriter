namespace StoryWriter
{
    public partial class UC_TagList : UserControl, IPanel
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }

        DataTable tblTags;

        BindingSource bsTags = new();
 
        void ControlInitialize()
        {
            ParentTabPage.Text = "Tags";
 
            Grid.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

            btnAddTag.Click += (s, e) => AddTag();
            btnDeleteTag.Click += (s, e) => DeleteTag();
            btnAddComponentsToTag.Click += (s, e) => AddComponentsToTag();
            btnAddToQuickView.Click += (s, e) => AddToQuickView();

            edtFilter.TextChanged += (s, e) => FilterChanged();
            lboComponents.MouseDoubleClick += (s, e) => EditComponentText();

            ReLoad();

            App.StoryClosed += StoryClosed;
            App.StoryOpened += StoryOpened;  
            App.TagToComponetsChanged += (s, e) => ReLoad();
            App.ItemChanged += (object Sender, BaseEntity Item) =>
            {
                if (Sender != this)
                {
                    // nothing, this user control updates itself on changes
                }
            };

            SelectedItemChanged();
        }
        void ReLoad()
        {
            if (tblTags == null)
            {
                tblTags = new DataTable("Tags");
                tblTags.Columns.Add("Id", typeof(string));
                tblTags.Columns.Add("Name", typeof(string));
                tblTags.Columns.Add("OBJECT", typeof(object));

                bsTags.DataSource = tblTags;

                Grid.AutoGenerateColumns = false;
                Grid.DataSource = bsTags;
                Grid.InitializeReadOnly();                

                bsTags.PositionChanged += (s, e) => SelectedItemChanged();
            } 

            if (App.CurrentStory != null)
            {
                bsTags.SuspendBinding();
                tblTags.Rows.Clear();

                foreach (Tag item in App.CurrentStory.TagList)
                {
                    tblTags.Rows.Add(item.Id, item.Name, item);
                }

                tblTags.AcceptChanges();
                Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                bsTags.ResumeBinding();
                SelectedItemChanged();
            }
        }
        void SelectedItemChanged()
        {
            DataRow Row = bsTags.CurrentDataRow();

            if (Row != null)
            {
                Tag Tag = Row["OBJECT"] as Tag;
                if (Tag == null)
                    return;

                lboComponents.BeginUpdate();
                lboComponents.Items.Clear();
                lboComponents.Items.AddRange(Tag.GetComponentList().ToArray());
                lboComponents.EndUpdate();
            }
            else
            {
                lboComponents.Items.Clear();
            }
        }
        void FilterChanged()
        {
            string S = edtFilter.Text.Trim();

            if (!string.IsNullOrWhiteSpace(S) && S.Length > 2)
            {
                bsTags.Filter = $"Name LIKE '%{S}%'";
                SelectedItemChanged();
            }
            else
            {
                bsTags.Filter = string.Empty;
            }
        }

        // ● add/delete
        void AddTag()
        {
            string Message;
            string ResultName = "";

            if (EditItemDialog.ShowModal("Add Tag", App.CurrentStory.Name, ref ResultName))
            {
                if (App.CurrentStory.TagExists(ResultName))
                {
                    Message = $"Tag '{ResultName}' already exists.";
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
                    Grid.PositionToRow(Row);

                    Message = $"Tag '{Tag.Name}' added.";
                    LogBox.AppendLine(Message);
                }
                else
                {
                    Message = "Add Tag. Operation failed.";
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

            //App.InfoBox("Delete Tag. NOT YET IMPLEMENTED.");

            TagToComponent.DeleteByTag(Tag.Id);

            Tag.Delete();
            Row.Delete();
            tblTags.AcceptChanges();

            SelectedItemChanged();

            string Message = $"Tag '{Tag.Name}' deleted.";
            LogBox.AppendLine(Message);
        }
        void EditComponentText()
        {
            Component Component = lboComponents.SelectedItem as Component;
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
                Grid.PositionToRow(Row);
        }
        void AddToQuickView()
        {
            Component Component = lboComponents.SelectedItem as Component;
            if (Component == null)
                return;

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

        // ● event handlers
        void StoryClosed(object sender, EventArgs e)
        {
            bsTags.SuspendBinding();

            tblTags.Rows.Clear();

            tblTags.AcceptChanges();


            bsTags.ResumeBinding();

            lboComponents.Items.Clear();
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
        public UC_TagList()
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
