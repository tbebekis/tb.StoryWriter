using System;

namespace StoryWriter
{
    public partial class UC_ComponentList : UserControl, IPanel
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }

        DataTable tblComponents;
        DataTable tblTagToComponents;

        BindingSource bsComponents = new ();
        BindingSource bsTagToComponents = new();


        void ControlInitialize()
        {
            ParentTabPage.Text = "Components";

            btnAddComponent.Click += (s, e) => AddComponent();
            btnEditComponent.Click += (s, e) => EditComponent();
            btnDeleteComponent.Click += (s, e) => DeleteComponent();
            btnEditRtfText.Click += (s, e) => EditComponentText();

            edtFilter.TextChanged += (s, e) => FilterChanged();
            gridComponents.MouseDoubleClick += (s, e) => EditComponentText();

            btnAdjustComponentTags.Click += (s, e) => AdjustComponentTags(); 

            ReLoadComponents();

            App.ProjectClosed += ProjectClosed;
            App.ProjectOpened += ProjectOpened;
            App.TagToComponetsChanged += (s, e) => ReLoadComponents();
        }
        void ReLoadComponents()
        {
            if (tblComponents == null)
            {
                tblComponents = new DataTable ("Components");

                tblComponents.Columns.Add("Id", typeof(string));
                tblComponents.Columns.Add("Name", typeof(string));
                tblComponents.Columns.Add("OBJECT", typeof(object));

                bsComponents.DataSource = tblComponents;

                gridComponents.AutoGenerateColumns = false;
                gridComponents.DataSource = bsComponents;
                gridComponents.InitializeReadOnly();                

                bsComponents.PositionChanged += (s, e) => SelectedComponentChanged();
            }

            if (tblTagToComponents == null)
            {
                tblTagToComponents = new DataTable("TagToComponents");
                tblTagToComponents.Columns.Add("TagId", typeof(string));
                tblTagToComponents.Columns.Add("ComponentId", typeof(string));
                tblTagToComponents.Columns.Add("Name", typeof(string));
                tblTagToComponents.Columns.Add("OBJECT", typeof(object));

                bsTagToComponents.DataSource = tblTagToComponents;

                gridTags.AutoGenerateColumns = false;
                gridTags.DataSource = bsTagToComponents;
                gridTags.InitializeReadOnly();
            }

            if (App.CurrentProject != null)
            { 
                bsComponents.SuspendBinding();
                bsTagToComponents.SuspendBinding();

                tblComponents.Rows.Clear();
                tblTagToComponents.Rows.Clear();

                foreach (Component item in App.CurrentProject.ComponentList)
                {
                    tblComponents.Rows.Add(item.Id, item.Name, item);
                }

                foreach (TagToComponent item in App.CurrentProject.TagToComponentList)
                {
                    tblTagToComponents.Rows.Add(item.Tag.Id, item.Component.Id, item.Tag.Name, item);
                }

                tblComponents.AcceptChanges();
                tblTagToComponents.AcceptChanges();

                gridComponents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                gridTags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                bsTagToComponents.ResumeBinding();
                bsComponents.ResumeBinding(); 

                SelectedComponentChanged();
            }
        }
        void SelectedComponentChanged()
        {
            DataRow Row = bsComponents.CurrentDataRow();

            if (Row != null)
            {
                bsTagToComponents.Filter = $"ComponentId = '{Row["Id"]}'";
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
                bsComponents.Filter = $"Name LIKE '%{S}%'";
            }
            else
            {
                bsComponents.Filter = string.Empty;
            }
        }

        // ● add/edit/delete
        void AddComponent()
        {
            string ResultName = "";

            if (EditItemDialog.ShowModal("Add Component", App.CurrentProject.Name, ref ResultName))
            {

                if (App.CurrentProject.ComponentExists(ResultName))
                {
                    string Message = $"Component '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Component Component = new();
                Component.Name = ResultName;
                Component.Id = Sys.GenId(UseBrackets: false);

                if (Component.Insert())
                {
                    DataRow Row = tblComponents.Rows.Add(Component.Id, Component.Name, Component);                    
                    tblComponents.AcceptChanges();
                    gridComponents.PositionToRow(Row);
                }
                else
                {
                    string Message = "Add Component. Operation failed.";
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

            string ResultName = Component.Name;

            if (EditItemDialog.ShowModal("Component Chapter", App.CurrentProject.Name, ref ResultName))
            {
                if (App.CurrentProject.ComponentExists(ResultName, Component.Id))
                {
                    string Message = $"Component '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Component.Name = ResultName;

                if (Component.Update())
                {
                    Row["Name"] = Component.Name;
                }
                else
                {
                    string Message = "Component Chapter. Operation failed.";
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

            App.InfoBox("Delete Component. NOT YET IMPLEMENTED.");
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
            DataRow Row = bsComponents.CurrentDataRow();
            if (Row == null)
                return;

            Component Component = Row["OBJECT"] as Component;
            if (Component == null)
                return;

            App.AddTagsToComponent(Component);

            Row = tblComponents.FindDataRowById(Component.Id);
            if (Row != null)
                gridComponents.PositionToRow(Row);
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
