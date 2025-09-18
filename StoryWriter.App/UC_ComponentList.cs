using System;

namespace StoryWriter
{
    public partial class UC_ComponentList : UserControl, IPanel
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }

        DataTable tblComponents;
        BindingSource bsComponents = new ();
 
        void ControlInitialize()
        {
            ParentTabPage.Text = "Components";
            ucRichText.SetTopPanelVisible(false);
            ucRichText.SetEditorReadOnly(true);

            gridComponents.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

            btnAddComponent.Click += (s, e) => AddComponent();
            btnEditComponent.Click += (s, e) => EditComponent();
            btnDeleteComponent.Click += (s, e) => DeleteComponent();
            btnEditRtfText.Click += (s, e) => EditComponentText();
            btnAddToQuickView.Click += (s, e) => AddToQuickView();
 
            edtFilter.KeyDown += (s, e) => {
                if (e.KeyData == Keys.Enter)
                    FilterChanged();
            };

            gridComponents.MouseDoubleClick += (s, e) => EditComponentText();

            btnAdjustComponentTags.Click += (s, e) => AdjustComponentTags(); 

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
        }
        void ReLoad()
        {
            if (tblComponents == null)
            {
                tblComponents = new DataTable ("Components");

                tblComponents.Columns.Add("Id", typeof(string));
                tblComponents.Columns.Add("Name", typeof(string));
                tblComponents.Columns.Add("Description", typeof(string));
                tblComponents.Columns.Add("Category", typeof(string));
                tblComponents.Columns.Add("TagList", typeof(string));
                tblComponents.Columns.Add("OBJECT", typeof(object));

                bsComponents.DataSource = tblComponents;

                gridComponents.AutoGenerateColumns = false;
                gridComponents.DataSource = bsComponents;
                gridComponents.InitializeReadOnly();                

                bsComponents.PositionChanged += (s, e) => SelectedItemChanged();
            }


            if (App.CurrentStory != null)
            { 
                bsComponents.SuspendBinding();
                tblComponents.Rows.Clear();

                List<Component> ComponentList = App.CurrentStory.ComponentList.OrderBy(x => x.Category).ToList();

                foreach (Component item in ComponentList)
                {
                    tblComponents.Rows.Add(item.Id, item.Name, item.Description, item.Category, item.GetTagsAsLine(), item);
                }

                tblComponents.AcceptChanges();
                gridComponents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                bsComponents.ResumeBinding(); 

                SelectedItemChanged();
            }
        }
        void SelectedItemChanged()
        {
            lblTitle.Text = "No selection";
            ucRichText.Clear();
            lboTags.Items.Clear();

            DataRow Row = bsComponents.CurrentDataRow();

            if (Row != null)
            {
                Component Component = Row["OBJECT"] as Component;
                if (Component == null)
                    return;

                lblTitle.Text = Component.ToString();
                ucRichText.RtfText = Component.BodyText;

                lboTags.BeginUpdate();
                lboTags.Items.Clear();
                lboTags.Items.AddRange(Component.GetTagList().ToArray());
                lboTags.EndUpdate();
            }
 
        }
        void FilterChanged()
        {
            string S = edtFilter.Text.Trim();

            if (!string.IsNullOrWhiteSpace(S) && S.Length > 2)
            {
                bsComponents.Filter = $"Name LIKE '%{S}%' OR Description LIKE '%{S}%' OR Category LIKE '%{S}%' OR TagList LIKE '%{S}%'";
                SelectedItemChanged();
            }
            else
            {
                bsComponents.Filter = string.Empty;
            }
        }

        // ● add/edit/delete
        void AddComponent()
        {
            string Message;

            if (App.CurrentStory == null)
                return;

            if (App.CurrentStory.ComponentTypeList.Count == 0)
            {
                Message = "You need to add at least one Component Type.";
                App.ErrorBox(Message);
                LogBox.AppendLine(Message);
                return;  
            }
 
            Component Component = new();
            Component.Id = Sys.GenId(UseBrackets: false);
            Component.Name = "New Component";


            if (EditComponentDialog.ShowModal("Add Component", Component))
            {
                if (App.CurrentStory.ComponentExists(Component))
                {
                    Message = $"Component '{Component.Name}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }              
                

                if (Component.Insert())
                {
                    DataRow Row = tblComponents.Rows.Add(Component.Id, Component.Name, Component.Description, Component.Category, Component.GetTagsAsLine(), Component);
                    tblComponents.AcceptChanges();
                    gridComponents.PositionToRow(Row);

                    Message = $"Component '{Component.Name}' added.";
                    LogBox.AppendLine(Message);                    
                }
                else
                {
                    Message = "Add Component. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        void EditComponent()
        {
            DataRow Row = bsComponents.CurrentDataRow();
            if (Row == null)
                return;

            Component Component = Row["OBJECT"] as Component;
            if (Component == null)
                return;

            string Message; 

            if (EditComponentDialog.ShowModal("Edit Component", Component))
            {
                if (App.CurrentStory.ComponentExists(Component))
                {
                    Message = $"Component '{Component.Name}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                } 

                if (Component.Update())
                {
                    bsComponents.SuspendBinding();
                    gridComponents.DataSource = null;
                    Row["Name"] = Component.Name;
                    Row["Category"] = Component.Category;
                    Row["Description"] = Component.Description;
                    bsComponents.ResumeBinding();

                    gridComponents.DataSource = bsComponents;
                    gridComponents.PositionToRow(Row);

                    TabPage Page = App.ContentPagerHandler.FindTabPage(Component.Id);
                    if (Page != null)
                    {
                        UC_Component UC = Page.Tag as UC_Component;
                        if (UC != null)
                            UC.TitleChanged();
                    }                  
                    
                    Message = $"Component '{Component.Name}' updated.";
                    LogBox.AppendLine(Message);
                }
                else
                {
                    Message = "Edit Component. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        void DeleteComponent()
        {
            DataRow Row = bsComponents.CurrentDataRow();
            if (Row == null)
                return;

            Component Component = Row["OBJECT"] as Component;
            if (Component == null)
                return;

            if (!App.QuestionBox($"Are you sure you want to delete the component '{Component.Name}'?"))
                return;             

            App.ContentPagerHandler.ClosePage(Component.Id);

            TagToComponent.DeleteByComponent(Component.Id);
            ComponentToScene.DeleteByComponent(Component.Id);

            Component.Delete();
            Row.Delete();
            tblComponents.AcceptChanges();

            string Message = $"Component '{Component.Name}' deleted.";
            LogBox.AppendLine(Message);
        }
        void EditComponentText()
        {
            DataRow Row = bsComponents.CurrentDataRow();
            if (Row == null)
                return;

            Component Component = Row["OBJECT"] as Component;
            if (Component == null)
                return;

            App.ContentPagerHandler.ShowPage(typeof(UC_Component), Component.Id, Component);
        }
        void AdjustComponentTags()
        {
            string Message;

            if (App.CurrentStory == null)
                return;

            DataRow Row = bsComponents.CurrentDataRow();
            if (Row == null)
                return;

            Component Component = Row["OBJECT"] as Component;
            if (Component == null)
                return;

            if (App.CurrentStory.TagList.Count == 0)
            {
                Message = "You need to add at least one Tag.";
                App.ErrorBox(Message);
                LogBox.AppendLine(Message);
                return;
            }

            App.AddTagsToComponent(Component);

            Row = tblComponents.FindDataRowById(Component.Id);
            if (Row != null)
                gridComponents.PositionToRow(Row);
        }
        void AddToQuickView()
        {
            DataRow Row = bsComponents.CurrentDataRow();
            if (Row != null)
            {
                string ComponentId = Row.AsString("Id");
                Component Component = App.CurrentStory.ComponentList.FirstOrDefault(x => x.Id == ComponentId);
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
        void StoryClosed(object sender, EventArgs e)
        {
            SelectedItemChanged();
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
        public UC_ComponentList()
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
