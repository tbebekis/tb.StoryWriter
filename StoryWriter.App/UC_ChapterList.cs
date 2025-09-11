namespace StoryWriter
{
    public partial class UC_ChapterList : UserControl, IPanel
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }

        void ControlInitialize()
        {
            ParentTabPage.Text = "Chapters";

            ucBodyText.SetTopPanelVisible(false);
            ucBodyText.SetEditorReadOnly(true);
            ucSynopsis.SetTopPanelVisible(false);
            ucSynopsis.SetEditorReadOnly(true);

            //btnAddItem.Click += (s, e) => AddItem();
            mnuAddChapter.Click += (s, e) => AddChapter();
            mnuAddScene.Click += (s, e) => AddScene();
            btnEditItem.Click += (s, e) => EditItem();
            btnDeleteItem.Click += (s, e) => DeleteItem();
            btnEditRtfText.Click += (s, e) => EditNoteText();
            btnAdjustSceneComponents.Click += (s, e) => AdjustSceneComponents();

            btnUp.Click += (s, e) => MoveNode(Up: true);
            btnDown.Click += (s, e) => MoveNode(Up: false);

            tv.MouseDoubleClick += (s, e) => EditNoteText();
            tv.AfterSelect += (s, e) => SelectedNodeChanged();
            lboComponents.MouseDoubleClick += (s, e) => ShowComponent();

            App.StoryClosed += StoryClosed;
            App.StoryOpened += StoryOpened;

            ReLoad();
        }
        void ReLoad()
        {
            //lblSceneTitle.Text = "Components: No Scene selected";

            tv.BeginUpdate();
            try
            {
                tv.Nodes.Clear();

                if (App.CurrentStory == null)
                    return;

                List<Scene> SceneList;
                TreeNode ChapterNode;
                TreeNode SceneNode;
                foreach (Chapter Chapter in App.CurrentStory.ChapterList)
                {
                    ChapterNode = tv.Nodes.Add(Chapter.ToString());
                    ChapterNode.Tag = Chapter;
                    ChapterNode.ImageIndex = 0;
                    ChapterNode.SelectedImageIndex = ChapterNode.ImageIndex;
                    ChapterNode.SelectedImageIndex = ChapterNode.ImageIndex;

                    SceneList = Chapter.GetSceneList();
                    foreach (Scene Scene in SceneList)
                    {
                        SceneNode = ChapterNode.Nodes.Add(Scene.ToString());
                        SceneNode.Tag = Scene;
                        SceneNode.ImageIndex = 1;
                        SceneNode.SelectedImageIndex = SceneNode.ImageIndex;
                        SceneNode.SelectedImageIndex = SceneNode.ImageIndex;
                    }
                }
            }
            finally
            {
                tv.EndUpdate();
            }

            SelectedNodeChanged();
        }
        void SelectedNodeChanged()
        {
            lblTitle.Text = "No selection";
            ucBodyText.Clear();
            ucSynopsis.Clear();
            lboComponents.Items.Clear();

            if (App.CurrentStory == null)
                return;

            TreeNode Node = tv.SelectedNode;
            if (Node == null)
                return;

            if (Node.Tag is Scene)
            {
                Scene Scene = Node.Tag as Scene;
                lblTitle.Text = $"Scene: {Scene}";

                List<Component> ComponentList = App.CurrentStory.GetSceneComponents(Scene);
                lboComponents.Items.AddRange(ComponentList.ToArray());

                ucBodyText.RtfText = Scene.BodyText;
                ucSynopsis.RtfText = Scene.Synopsis;
            }
            else if (Node.Tag is Chapter)
            {
                Chapter Chapter = Node.Tag as Chapter;
                lblTitle.Text = $"Chapter: {Chapter}";

                ucBodyText.RtfText = Chapter.BodyText;
                ucSynopsis.RtfText = Chapter.Synopsis;
            }
        }
        void ShowComponent()
        {
            if (lboComponents.SelectedItem is Component Component)
            {
                App.ContentPagerHandler.ShowPage(typeof(UC_Component), Component.Id, Component);
            }
        }

        public void AddChapter()
        {
            if (App.CurrentStory == null)
                return;

            string Message;
            string ResultName = "";

            if (EditItemDialog.ShowModal("Add Chapter", App.CurrentStory.Name, ref ResultName))
            {
                if (App.CurrentStory.ChapterExists(ResultName))
                {
                    Message = $"Chapter '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Chapter Chapter = new();
                Chapter.Name = ResultName;
                Chapter.Id = Sys.GenId(UseBrackets: false);
                Chapter.OrderIndex = App.CurrentStory.ChapterList.Count + 1;

                if (Chapter.Insert())
                {
                    TreeNode ChapterNode = tv.Nodes.Add(Chapter.ToString());
                    ChapterNode.Tag = Chapter;
                    ChapterNode.ImageIndex = 0;
                    ChapterNode.SelectedImageIndex = ChapterNode.ImageIndex;
                    ChapterNode.SelectedImageIndex = ChapterNode.ImageIndex;

                    App.CurrentStory.RenumberChapters();

                    Message = $"Chapter '{ResultName}' added.";
                    LogBox.AppendLine(Message);

                    tv.SelectedNode = ChapterNode;
                }
                else
                {
                    Message = "Add Chapter. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        public void EditChapter()
        {
            TreeNode ChapterNode = tv.SelectedNode;
            Chapter Chapter = ChapterNode.Tag as Chapter;

            string Message;
            string ResultName = Chapter.Name;

            if (EditItemDialog.ShowModal("Edit Chapter", App.CurrentStory.Name, ref ResultName))
            {
                if (App.CurrentStory.ChapterExists(ResultName, Chapter.Id))
                {
                    Message = $"Chapter '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Chapter.Name = ResultName;

                if (Chapter.Update())
                {
                    ChapterNode.Text = Chapter.ToString();

                    TabPage Page = App.ContentPagerHandler.FindTabPage(Chapter.Id);
                    if (Page != null)
                    {
                        UC_Chapter UC = Page.Tag as UC_Chapter;
                        if (UC != null)
                            UC.TitleChanged();
                    }

                    Message = $"Chapter '{ResultName}' updated.";
                    LogBox.AppendLine(Message);
                }
                else
                {
                    Message = "Edit Chapter. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        public void DeleteChapter()
        {
            TreeNode ChapterNode = tv.SelectedNode;
            Chapter Chapter = ChapterNode.Tag as Chapter;

            string Message = $@"Deleting a Chapter deletes all of its Scenes too.
Are you sure you want to delete the Chapter
'{Chapter.Name}'?
";

            if (!App.QuestionBox(Message))
                return;

            tv.Nodes.Remove(ChapterNode);

            foreach (Scene Scene in Chapter.GetSceneList())
                App.ContentPagerHandler.ClosePage(Scene.Id);

            App.ContentPagerHandler.ClosePage(Chapter.Id);

            Chapter.Delete();            
        }
         
        public void AddScene()
        {
            if (App.CurrentStory == null)
                return;

            string Message;

            if  (tv.SelectedNode == null)
            {
                Message = "Please, select a Chapter first.";
                App.ErrorBox(Message);
                LogBox.AppendLine(Message);
                return;
            }

            TreeNode ChapterNode = null;
            Chapter Chapter = null;

            if (tv.SelectedNode.Tag is Chapter)
            {
                ChapterNode = tv.SelectedNode;
                Chapter = ChapterNode.Tag as Chapter;
            }
            else
            {
                ChapterNode = tv.SelectedNode.Parent;
                Chapter = ChapterNode.Tag as Chapter;
            }
 

            string ResultName = "";

            if (EditItemDialog.ShowModal("Add Scene", Chapter.Name, ref ResultName))
            {
                if (App.CurrentStory.SceneExists(ResultName))
                {
                    Message = $"Scene '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Scene Scene = new();
                Scene.Name = ResultName;
                Scene.Id = Sys.GenId(UseBrackets: false);
                Scene.ChapterId = Chapter.Id;
                Scene.OrderIndex = Chapter.GetSceneList().Count + 1;

                if (Scene.Insert())
                {
                    TreeNode SceneNode = ChapterNode.Nodes.Add(Scene.ToString());
                    SceneNode.Tag = Scene;
                    SceneNode.ImageIndex = 1;
                    SceneNode.SelectedImageIndex = SceneNode.ImageIndex;
                    SceneNode.SelectedImageIndex = SceneNode.ImageIndex;

                    Chapter.RenumberScenes();

                    Message = $"Scene '{ResultName}' added.";
                    LogBox.AppendLine(Message);

                    tv.SelectedNode = SceneNode;
                }
                else
                {
                    Message = "Add Scene. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }

        }
        public void EditScene()
        {
            TreeNode SceneNode = tv.SelectedNode;
            Scene Scene = SceneNode.Tag as Scene;

            string Message;
            string ResultName = Scene.Name;

            if (EditItemDialog.ShowModal("Edit Scene", Scene.Chapter.Name, ref ResultName))
            {
                if (App.CurrentStory.SceneExists(ResultName, Scene.Id))
                {
                    Message = $"Scene '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Scene.Name = ResultName;

                if (Scene.Update())
                {
                    SceneNode.Text = Scene.ToString();
                    TabPage Page = App.ContentPagerHandler.FindTabPage(Scene.Id);
                    if (Page != null)
                    {
                        UC_Scene UC = Page.Tag as UC_Scene;
                        if (UC != null)
                            UC.TitleChanged();
                    }

                    Message = $"Scene '{ResultName}' updated.";
                    LogBox.AppendLine(Message);
                }
                else
                {
                    Message = "Edit Scene. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        public void DeleteScene()
        {
            TreeNode SceneNode = tv.SelectedNode;
            Scene Scene = SceneNode.Tag as Scene;

            string Message = $@"Are you sure you want to delete the Scene
'{Scene.Name}'?
";

            if (!App.QuestionBox(Message))
                return;

            Chapter Chapter = Scene.Chapter;

            App.ContentPagerHandler.ClosePage(Scene.Id);

            tv.Nodes.Remove(SceneNode);
            Scene.Delete();
            ComponentToScene.DeleteByScene(Scene.Id);
            Chapter.RenumberScenes();            
        }

        // ● add/edit/delete
        void EditItem()
        {
            if (App.CurrentStory == null)
                return;

            TreeNode Node = tv.SelectedNode;
            if (Node == null)
                return;

            if (Node.Tag is Chapter)
                EditChapter();
            else
                EditScene();
        }
        void DeleteItem()
        {
            if (App.CurrentStory == null)
                return;

            TreeNode Node = tv.SelectedNode;
            if (Node == null)
                return;

            if (Node.Tag is Chapter)
                DeleteChapter();
            else
                DeleteScene();
        }
        void EditNoteText()
        {
            if (App.CurrentStory == null)
                return;

            TreeNode Node = tv.SelectedNode;
            if (Node == null)
                return;

            if (Node.Tag is Chapter)
                App.ContentPagerHandler.ShowPage(typeof(UC_Chapter), (Node.Tag as Chapter).Id, Node.Tag);
            else
                App.ContentPagerHandler.ShowPage(typeof(UC_Scene), (Node.Tag as Scene).Id, Node.Tag); 
        }

        void AdjustSceneComponents()
        {
            string Message;

            if (App.CurrentStory == null)
                return;

            TreeNode Node = tv.SelectedNode;
            if (Node == null)
                return;

            if (Node.Tag is Scene)
            {
                if (App.CurrentStory.ComponentList.Count == 0)
                {
                    Message = "You need to add at least one Component.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Scene Scene = Node.Tag as Scene;

                App.AddComponentsToScene(Scene);

                SelectedNodeChanged();
            }
 
        }
        
        void MoveChapter(Chapter Chapter, TreeNode Node, bool Up)
        {
            TreeNodeCollection Nodes = tv.Nodes;

            int Index = Nodes.IndexOf(Node);
            if ((Up && Index == 0) || (!Up && Index == Nodes.Count - 1))
                return;

            Index = App.CurrentStory.ChapterList.IndexOf(Chapter);
            App.CurrentStory.ChapterList.Move(Index, Up);
            App.CurrentStory.RenumberChapters();

            Nodes.RemoveAt(Index);
            Nodes.Insert(Chapter.OrderIndex - 1, Node);

            foreach (TreeNode Node2 in Nodes)
            {
                Node2.Text = Node2.Tag.ToString();
            }
        }
        void MoveScene(Scene Scene, TreeNode Node, bool Up)
        {
            TreeNodeCollection Nodes = Node.Parent.Nodes;

            int Index = Nodes.IndexOf(Node);
            if ((Up && Index == 0) || (!Up && Index == Nodes.Count - 1))
                return;

            List<Scene> SceneList = Scene.Chapter.GetSceneList();
            SceneList.Move(Index, Up);
            Scene.Chapter.RenumberScenes(SceneList);
 
            Nodes.RemoveAt(Index);
            Nodes.Insert(Scene.OrderIndex - 1, Node);

            foreach (TreeNode Node2 in Nodes)
            {
                Node2.Text = Node2.Tag.ToString();
            }
        }
        void MoveNode(bool Up)
        {
            TreeNode Node = tv.SelectedNode;
            if (Node == null)
                return;

            if (Node.Tag is Chapter)
                MoveChapter(Node.Tag as Chapter, Node, Up);
            else if (Node.Tag is Scene)
                MoveScene(Node.Tag as Scene, Node, Up);

            tv.SelectedNode = Node;
        }

        // ● event handlers
        void StoryClosed(object sender, EventArgs e)
        {
            tv.Nodes.Clear();
            lboComponents.Items.Clear();
            SelectedNodeChanged();
        }
        void StoryOpened(object sender, EventArgs e)
        {
            ReLoad();
            SelectedNodeChanged();
        }

        // ● overrides  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
                ControlInitialize();
        }

        // ● construction
        public UC_ChapterList()
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
