namespace StoryWriter
{
    public partial class UC_ComponentTypeList : UserControl, IPanel
    {

        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }

        DataTable tblComponentTypes;
        BindingSource bsComponentTypes = new();

        void ControlInitialize()
        {
            ParentTabPage.Text = "Comp. Types";

            Grid.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

            btnAddComponentType.Click += (s, e) => AddComponentType();
            btnEditComponentType.Click += (s, e) => EditComponentType();
            btnDeleteComponentType.Click += (s, e) => DeleteComponentType();
            btnAddToQuickView.Click += (s, e) => AddToQuickView();
            lboComponents.MouseDoubleClick += (s, e) => EditComponentText();

            ReLoad();

            App.StoryClosed += StoryClosed;
            App.StoryOpened += StoryOpened;
            App.TagToComponetsChanged += (s, e) => ReLoad();
        }

        void ReLoad()
        {
            if (tblComponentTypes == null)
            {
                tblComponentTypes = new DataTable("ComponentTypes");

                tblComponentTypes.Columns.Add("Id", typeof(string));
                tblComponentTypes.Columns.Add("Name", typeof(string));
                tblComponentTypes.Columns.Add("OBJECT", typeof(object));

                bsComponentTypes.DataSource = tblComponentTypes;

                Grid.AutoGenerateColumns = false;
                Grid.DataSource = bsComponentTypes;
                Grid.InitializeReadOnly();

                bsComponentTypes.PositionChanged += (s, e) => SelectedItemChanged();
            }
 

            if (App.CurrentStory != null)
            {
                bsComponentTypes.SuspendBinding();
                tblComponentTypes.Rows.Clear();

                foreach (ComponentType item in App.CurrentStory.ComponentTypeList)
                {
                    tblComponentTypes.Rows.Add(item.Id, item.Name, item);
                }

                tblComponentTypes.AcceptChanges();
                Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                bsComponentTypes.ResumeBinding();
                SelectedItemChanged();
            }
        }
        void SelectedItemChanged()
        {
            DataRow Row = bsComponentTypes.CurrentDataRow();

            if (Row != null)
            {
                ComponentType ComponentType = Row["OBJECT"] as ComponentType;
                if (ComponentType == null)
                    return;

                lboComponents.BeginUpdate();
                lboComponents.Items.Clear();
                lboComponents.Items.AddRange(ComponentType.GetComponentList().ToArray());
                lboComponents.EndUpdate();
            }
            else
            {
                lboComponents.Items.Clear();
            }
        }


        // ● add/edit/delete
        void AddComponentType()
        {
            string Message;
            string ResultName = "";

            if (EditItemDialog.ShowModal("Add Component Type", App.CurrentStory.Name, ref ResultName))
            {
                if (App.CurrentStory.ComponentTypeExists(ResultName))
                {
                    Message = $"Component Type '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                ComponentType ComponentType = new();
                ComponentType.Name = ResultName;
                ComponentType.Id = Sys.GenId(UseBrackets: false);

                if (ComponentType.Insert())
                {
                    DataRow Row = tblComponentTypes.Rows.Add(ComponentType.Id, ComponentType.Name, ComponentType);
                    tblComponentTypes.AcceptChanges();
                    Grid.PositionToRow(Row);

                    Message = $"Component Type '{ResultName}' added.";
                    LogBox.AppendLine(Message);
                }
                else
                {
                    Message = "Add Component Type. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        void EditComponentType()
        {
            DataRow Row = bsComponentTypes.CurrentDataRow();
            if (Row == null)
                return;

            ComponentType ComponentType = Row["OBJECT"] as ComponentType;
            if (ComponentType == null)
                return;

            string Message;
            string ResultName = ComponentType.Name;

            if (EditItemDialog.ShowModal("Edit Component Type", App.CurrentStory.Name, ref ResultName))
            {
                if (App.CurrentStory.ComponentTypeExists(ResultName, ComponentType.Id))
                {
                    Message = $"Component Type'{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                ComponentType.Name = ResultName;

                if (ComponentType.Update())
                {
                    Row["Name"] = ComponentType.Name;

                    Message = $"Component Type '{ComponentType.Name}' updated.";
                    LogBox.AppendLine(Message);
                }
                else
                {
                    Message = "Edit Component Type. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        void DeleteComponentType()
        {
            string Message;
            DataRow Row = bsComponentTypes.CurrentDataRow();
            if (Row == null)
                return;

            ComponentType ComponentType = Row["OBJECT"] as ComponentType;
            if (ComponentType == null)
                return;

            if (!App.QuestionBox($"Are you sure you want to delete the Component Type '{ComponentType.Name}'?"))
                return;

            if (ComponentType.GetComponentList().Count > 0)
            {
                Message = "The Component Type has components and cannot be deleted.";
                App.ErrorBox(Message);
                LogBox.AppendLine(Message);
                return;
            }

            ComponentType.Delete();
            Row.Delete();
            tblComponentTypes.AcceptChanges();

            SelectedItemChanged();

            Message = $"Component Type '{ComponentType.Name}' deleted.";
            LogBox.AppendLine(Message);
        }
        void EditComponentText()
        {
            Component Component = lboComponents.SelectedItem as Component;
            if (Component == null)
                return;

            App.ContentPagerHandler.ShowPage(typeof(UC_Component), Component.Id, Component);
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
            SelectedItemChanged();
            lboComponents.Items.Clear();
        }
        void StoryOpened(object sender, EventArgs e)
        {
            ReLoad();
            SelectedItemChanged();
        }

        // ● overrides  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
                ControlInitialize();
        }

        // ● construction
        public UC_ComponentTypeList()
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
