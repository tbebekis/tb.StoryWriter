namespace StoryWriter
{
    public partial class UC_ComponentTree : UserControl, IPanel
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }

        void ControlInitialize()
        {
            ParentTabPage.Text = "Components";
 
            tvComponents.MouseDoubleClick += (s, e) =>
            {
                if (btnEditComponentText.Enabled)
                    btnEditComponentText.PerformClick();
            };

            btnAddTopGroup.Click += (s, e) => AddTopGroup();
            btnAddSubGroup.Click += (s, e) => AddSubGroup();
            btnAddComponent.Click += (s, e) => AddComponent();
            btnEditComponent.Click += (s, e) => EditComponent();
            btnDeleteComponent.Click += (s, e) => DeleteComponent();
            btnEditComponentText.Click += (s, e) => EditComponentText();

            mnuAddTopGroup.Click += (s, e) => AddTopGroup();
            mnuAddSubGroup.Click += (s, e) => AddSubGroup();
            mnuAddComponent.Click += (s, e) => AddComponent();
            mnuEditComponent.Click += (s, e) => EditComponent();
            mnuDeleteComponent.Click += (s, e) => DeleteComponent();

            ReLoadComponents();

            App.ProjectClosed += ProjectClosed;
            App.ProjectOpened += ProjectOpened;

            tvComponents.AfterSelect += (s, e) => SelectedNodeChanged();
            mnuComponents.Opening += (s, e) => SelectedNodeChanged();

            SelectedNodeChanged();
        }
        void ReLoadComponents()
        {
            void AddComponentList(TreeNodeCollection Nodes, List<Component> ComponentTypeList)
            {
                foreach (var item in ComponentTypeList)
                {
                    var Node = Nodes.Add(item.Name);
                    Node.Tag = item;
                    if (item.Children.Count > 0)
                        AddComponentList(Node.Nodes, item.Children);
                }
            }

            if (App.CurrentProject != null)
            {
                tvComponents.Nodes.Clear();
                AddComponentList(tvComponents.Nodes, App.CurrentProject.TreeComponentList);
            }
        }
        void EnableCommands()
        {
            var SelectedNode = tvComponents.SelectedNode;
            var SelectedComponent = SelectedNode?.Tag as Component;

            btnAddTopGroup.Enabled = App.CurrentProject != null;
            btnAddSubGroup.Enabled = App.CurrentProject != null && (SelectedComponent != null) && SelectedComponent.CanAddGroup();
            btnAddComponent.Enabled = App.CurrentProject != null && (SelectedComponent != null) && SelectedComponent.CanAddComponent();
            btnEditComponent.Enabled = App.CurrentProject != null && (SelectedComponent != null);
            btnDeleteComponent.Enabled = App.CurrentProject != null && (SelectedComponent != null) && !SelectedComponent.HasChildNodes;
            btnEditComponentText.Enabled = App.CurrentProject != null && (SelectedComponent != null) && !SelectedComponent.IsGroup;

            mnuAddTopGroup.Enabled = btnAddTopGroup.Enabled;
            mnuAddSubGroup.Enabled = btnAddSubGroup.Enabled;
            mnuAddComponent.Enabled = btnAddComponent.Enabled;
            mnuEditComponent.Enabled = btnEditComponent.Enabled;
            mnuDeleteComponent.Enabled = btnDeleteComponent.Enabled;
        }

        // ● event handlers
        void ProjectClosed(object sender, EventArgs e)
        {
            tvComponents.Nodes.Clear();
            SelectedNodeChanged();
        }
        void ProjectOpened(object sender, EventArgs e)
        {
            ReLoadComponents();
            SelectedNodeChanged();
        }
        void SelectedNodeChanged()
        {
            EnableCommands();
        }
        


        // ● groups
        void AddTopGroup()
        {
            string ResultName = "";

            if (EditItemDialog.ShowModal("Add Top Group", "Top Level", ref ResultName))
            {
                if (App.CurrentProject.ComponentExists(ResultName))
                {
                    string Message = $"Component '{ResultName}' already exists.";
                    App.ErrorBox(Message); 
                    LogBox.AppendLine(Message);
                    return;
                }

                Component Group = new ();
                Group.Id = Sys.GenId(UseBrackets: false);
                Group.ParentId = "";
                Group.Name = ResultName;
                Group.IsGroup = true;
                Group.Notes = ""; 
 
                if (Group.Insert())
                {
                    var Node = tvComponents.Nodes.Add(Group.Name);
                    Node.Tag = Group;
                    tvComponents.SelectedNode = Node;
                    Node.EnsureVisible();
                }
                else
                {
                    string Message = "Add Top Group. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        void AddSubGroup()
        {
            var ParentNode = tvComponents.SelectedNode;
            var ParentComponent = ParentNode?.Tag as Component;
            if (ParentComponent == null)
                return;

            string ResultName = "";

            if (EditItemDialog.ShowModal("Add SubGroup", ParentComponent.Name, ref ResultName))
            {
                if (App.CurrentProject.ComponentExists(ResultName))
                {
                    string Message = $"Component '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Component Comp = new ();
                Comp.Id = Sys.GenId(UseBrackets: false);
                Comp.ParentId = ParentComponent.Id;
                Comp.Name = ResultName;
                Comp.IsGroup = true;
                Comp.Notes = "";

                if (Comp.Insert())
                {
                    var Node = ParentNode.Nodes.Add(Comp.Name);
                    Node.Tag = Comp;
                    tvComponents.SelectedNode = Node;
                    Node.EnsureVisible();
                }
                else
                {
                    string Message = "Add SubGroup. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }

        // ● components
        void AddComponent()
        {
            var ParentNode = tvComponents.SelectedNode;
            var ParentComponent = ParentNode?.Tag as Component;
            if (ParentComponent == null)
                return;
 
            string ResultName = "";

            if (EditItemDialog.ShowModal("Add Component", ParentComponent.Name, ref ResultName))
            {
                if (App.CurrentProject.ComponentExists(ResultName))
                {
                    string Message = $"Component '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Component Comp = new ();
                Comp.Id = Sys.GenId(UseBrackets: false);
                Comp.Parent = ParentComponent; 
                Comp.ParentId = ParentComponent.Id;
                Comp.Name = ResultName;
                Comp.IsGroup = false;
                Comp.Notes = "";

                if (Comp.Insert())
                {
                    var Node = ParentNode.Nodes.Add(Comp.Name);
                    Node.Tag = Comp;
                    tvComponents.SelectedNode = Node;
                    Node.EnsureVisible();
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
            var SelectedNode = tvComponents.SelectedNode;
            var SelectedComponent = SelectedNode?.Tag as Component;
            if (SelectedComponent == null)
                return;

            string ParentName = SelectedComponent.HasNoParent() ? "Top Level" : SelectedComponent.Parent.Name;
            string ResultName = SelectedComponent.Name;

            if (EditItemDialog.ShowModal("Edit Component", ParentName, ref ResultName))
            {
                if (App.CurrentProject.ComponentExists(ResultName, SelectedComponent.Id))
                {
                    string Message = $"Component '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Component Comp = SelectedComponent;
                Comp.Name = ResultName;

                if (Comp.Update())
                {
                    SelectedNode.Text = Comp.Name;
                }
                else
                {
                    string Message = "Edit Component. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        void DeleteComponent()
        {
            var SelectedNode = tvComponents.SelectedNode;
            var SelectedComponent = SelectedNode?.Tag as Component;
            if (SelectedComponent == null)
                return;

            if (SelectedComponent.Delete())
            {
                SelectedNode.Remove();
                ReLoadComponents();

                string Message = $"Component '{SelectedComponent.Name}' deleted.";
                LogBox.AppendLine(Message);
            }
            else
            {
                string Message = "Delete Component. Operation failed.";
                App.ErrorBox(Message);
                LogBox.AppendLine(Message);
            }
        }
        void EditComponentText()
        {
            var SelectedNode = tvComponents.SelectedNode;
            var SelectedComponent = SelectedNode?.Tag as Component;
            if (SelectedComponent == null)
                return;

            App.ContentPagerHandler.ShowPage(typeof(UC_Component), SelectedComponent.Id, SelectedComponent);
        }
 
        // ● overrides  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
                ControlInitialize();
        }

        // ● construction
        public UC_ComponentTree()
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
