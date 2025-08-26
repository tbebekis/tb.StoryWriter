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

        string Filter = string.Empty;

        void ControlInitialize()
        {
            ParentTabPage.Text = "Tags";

            btnAddTag.Click += (s, e) => AddTag();
            btnDeleteTag.Click += (s, e) => DeleteTag();

            edtFilter.TextChanged += (s, e) => FilterChanged();

            ReLoadTags();

            App.ProjectClosed += ProjectClosed;
            App.ProjectOpened += ProjectOpened;  
            App.TagToComponetsChanged += (s, e) => ReLoadTags();
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
            DataRow Row = gridTags.CurrentDataRow();

            if (Row != null)
            {
                bsTagToComponents.Filter = $"TagId = '{Row["Id"]}'";
            }
            else
            {
                bsTagToComponents.Filter = string.Empty;
            }
        }
        void FilterChanged()
        {
            string S = edtFilter.Text.Trim();

            if (!string.IsNullOrWhiteSpace(S) && S.Length > 2)
            {
                bsTags.Filter = $"Name LIKE '%{S}%'";
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
                Tag Tag = new();
                Tag.Name = ResultName;

                if (App.CurrentProject.ItemExists(Tag))
                {
                    string Message = $"Tag '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Tag.Id = Sys.GenId(UseBrackets: false);


                if (Tag.Insert())
                {
                    tblTags.Rows.Add(Tag.Id, Tag.Name, Tag);
                    tblTags.AcceptChanges();

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
            // TODO: DeleteTag
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
    }
}
