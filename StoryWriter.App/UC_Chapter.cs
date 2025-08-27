namespace StoryWriter
{
    public partial class UC_Chapter : UserControl, IPanel, IEditorHandler
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }
        Chapter Chapter = null; 
        Scene CurrentScene = null;
        bool RenumberingScenes = false;
        bool ChangingScene = false;

        Dictionary<RichTextBox, bool> IsFirstTimeModified = new();
        
        string TitleText;

        void ControlInitialize()
        {
            this.Chapter = Info as Chapter;
            TitleText = this.Chapter.ToString();
            ParentTabPage.Text = TitleText;

            ucBodyText.RtfText = this.Chapter.BodyText;
            ucSynopsisText.RtfText = this.Chapter.Synopsis;
            ucConceptText.RtfText = this.Chapter.Concept;
            ucOutcomeText.RtfText = this.Chapter.Outcome;

            /// this is used to suppress the * put in tab title 
            /// when editor text is modified for the first time.
            /// That modification is false and it happens
            /// because we assign their Rtf text initially from Chapter object
            IsFirstTimeModified.Add(ucBodyText.Editor, false);
            IsFirstTimeModified.Add(ucSynopsisText.Editor, false);
            IsFirstTimeModified.Add(ucConceptText.Editor, false);
            IsFirstTimeModified.Add(ucOutcomeText.Editor, false);
            IsFirstTimeModified.Add(ucSceneText.Editor, false);     // ucSceneText is a special case

            InitalizeRichTextEditors();

            /// we run this because RichTextBoxes are not functioning when they are not visible for the first time.
            /// so we display them one by one, so they get the text and the font settings from AppSettings (see UC_RichText.cs)
            /// and then we hook the ModifiedChanged event
            Ui.RunOnce((Info) => {
                //InitalizeRichTextEditors();
            }, 1500, null);

            // ● scenes  
            btnAdd.Click += (s, e) => AddScene();
            btnEdit.Click += (s, e) => EditScene();
            btnDelete.Click += (s, e) => DeleteScene();             

            btnUp.Click += (s, e) => MoveScene(true);
            btnDown.Click += (s, e) => MoveScene(false);

            App.ZoomFactorChanged += (e, a) =>
            {
                ucBodyText.Editor.ZoomFactor = (float)App.ZoomFactor;
                ucSynopsisText.Editor.ZoomFactor = (float)App.ZoomFactor;
                ucConceptText.Editor.ZoomFactor = (float)App.ZoomFactor;
                ucOutcomeText.Editor.ZoomFactor = (float)App.ZoomFactor;
                ucSceneText.Editor.ZoomFactor = (float)App.ZoomFactor;
            };

            ReLoadScenes(); 
            lboScenes.SelectedIndexChanged += (s, e) => SelectedSceneChanged();

            ucBodyText.Title = Chapter.ToString();

            SelectedSceneChanged();
        }
        void InitalizeRichTextEditors()
        {
            foreach (TabPage Page in pagerChapter.TabPages)
            {
                pagerChapter.SelectedTab = Page;
                Application.DoEvents();
            }

            pagerChapter.SelectedTab = tabBodyText;
            ucBodyText.Focus();

            ucBodyText.Editor.Modified = false;
            ucSynopsisText.Editor.Modified = false;
            ucConceptText.Editor.Modified = false;
            ucOutcomeText.Editor.Modified = false;
            ucSceneText.Editor.Modified = false;

            Application.DoEvents();

            ucBodyText.InitializeEditor(false);
            ucSynopsisText.InitializeEditor(true);
            ucConceptText.InitializeEditor(true);
            ucOutcomeText.InitializeEditor(true);
            ucSceneText.InitializeEditor(true);

            ucBodyText.Editor.ModifiedChanged += EditorModifiedChanged;
            ucSynopsisText.Editor.ModifiedChanged += EditorModifiedChanged;
            ucConceptText.Editor.ModifiedChanged += EditorModifiedChanged;
            ucOutcomeText.Editor.ModifiedChanged += EditorModifiedChanged;
            ucSceneText.Editor.ModifiedChanged += EditorModifiedChanged;

            ucBodyText.EditorHandler = this;
            ucSynopsisText.EditorHandler = this;
            ucConceptText.EditorHandler = this;
            ucOutcomeText.EditorHandler = this;
            ucSceneText.EditorHandler = this;
        }

        // ● scenes  
        void ReLoadScenes()
        {
            lboScenes.BeginUpdate();
            lboScenes.Items.Clear();
            lboScenes.Items.AddRange(Chapter.SceneList.ToArray());
            if (lboScenes.Items.Count > 0)
                lboScenes.SelectedIndex = 0;
            lboScenes.EndUpdate();
        }
        void AddScene()
        {
            string ResultName = "";

            if (EditItemDialog.ShowModal("Add Scene", Chapter.Name, ref ResultName))
            {
                if (Chapter.SceneExists(ResultName))
                {
                    string Message = $"Scene '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Scene Scene = new();
                Scene.Id = Sys.GenId(UseBrackets: false);
                Scene.ChapterId = Chapter.Id;
                Scene.Name = ResultName;                                         
                Scene.OrderIndex = Chapter.SceneList.Count + 1;

                if (Scene.Insert())
                {
                    lboScenes.Items.Add(Scene);
                    lboScenes.SelectedItem = Scene;
                    Chapter.SceneList.Add(Scene);
                }
                else
                {
                    string Message = "Add Scene. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        void EditScene()
        {
            Scene Scene = lboScenes.SelectedItem as Scene;
            if (Scene == null)
                return;

            string ResultName = Scene.Name;

            if (EditItemDialog.ShowModal("Edit Scene", Chapter.Name, ref ResultName))
            {
                if (Chapter.SceneExists(ResultName, Scene.Id))
                {
                    string Message = $"Scene '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Scene.Name = ResultName;

                if (Scene.Update())
                {
                    lboScenes.Refresh();
                }
                else
                {
                    string Message = "Edit Scene. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        void DeleteScene()
        {
            Scene Scene = lboScenes.SelectedItem as Scene;
            if (Scene == null)
                return;

            if (!App.QuestionBox($"Are you sure you want to delete the scene '{Scene}'?"))
                return;

            if (Scene.Delete())
            {
                Chapter.SceneList.Remove(Scene);
                CurrentScene = null;
                ReLoadScenes();

                string Message = $"Scene: {Scene}. Deleted";
                LogBox.AppendLine(Message);            }
            else
            {
                string Message = "Delete Scene. Operation failed.";
                App.ErrorBox(Message);
                LogBox.AppendLine(Message);
            }
        }
 
        void MoveScene(bool Up)
        {
            Scene Scene = lboScenes.SelectedItem as Scene;
            int Index = lboScenes.SelectedIndex;

            if (Index < 0)
                return;

            RenumberingScenes = true;
            try
            {
                Chapter.SceneList.Move(Index, Up);
                Chapter.RenumberScenes();

                ReLoadScenes();
                lboScenes.SelectedItem = Scene;
            }
            finally
            {
                RenumberingScenes = false;
            }            
        }
        void UpdateUpDownButtons()
        {
            int Index = lboScenes.SelectedIndex;
            if (Index < 0)
            {
                btnUp.Enabled = false;
                btnDown.Enabled = false;
                return;
            }
            btnUp.Enabled = lboScenes.Items.CanMove(Index, true);
            btnDown.Enabled = lboScenes.Items.CanMove(Index, false);
        }
        void SelectedSceneChanged()
        {
            UpdateUpDownButtons();

            if (!RenumberingScenes)
            {       
                btnAdd.Enabled = App.CurrentProject != null;
                btnEdit.Enabled = App.CurrentProject != null && (lboScenes.SelectedItem != null);
                btnDelete.Enabled = App.CurrentProject != null && (lboScenes.SelectedItem != null);

                if (CurrentScene != null && Chapter.SceneExists(CurrentScene.Name, CurrentScene.Id))
                {
                    CurrentScene.Update();
                }

                CurrentScene = lboScenes.SelectedItem as Scene;
                if (CurrentScene != null)
                { 
                    ChangingScene = true;
                    try
                    {                         
                        ucSceneText.RtfText = CurrentScene.BodyText;
                        ucSceneText.Title = CurrentScene.ToString(); ;
                    }
                    finally
                    {
                        ChangingScene = false;
                    }

                }
            }
        }


        // ● overrides  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
                ControlInitialize();
        }

        // ● construction
        public UC_Chapter()
        {
            InitializeComponent();
        }

        // ● public
        public void Close()
        {
            TabControl Pager = ParentTabPage.Parent as TabControl;
            if ((Pager != null) && (Pager.TabPages.Contains(ParentTabPage)))
                Pager.TabPages.Remove(ParentTabPage);
        }

        TabPage GetEditorPage(RichTextBox Editor)
        {
            Control Container = Editor.Parent;
            while (Container != null)
            {
                if (Container is TabPage)
                    return Container as TabPage;
                Container = Container.Parent;
            }

            return null;
        }
        void AdjustTabTitle(TabPage Page, bool Modified)
        {
            if (Page == null)
                return;

            string Title = Page.Text;
            Title = Title.TrimEnd('*', ' '); // new char[] { '*', ' ' }

            Title += (Modified ? "*" : " ");
            Page.Text = Title;
        }

        public void EditorModifiedChanged(object Sender, EventArgs e)
        {
            RichTextBox Editor = Sender as RichTextBox;
            TabPage Page = GetEditorPage(Editor);

            if (Page == null)
                return;

            if (!IsFirstTimeModified[Editor])
            {
                IsFirstTimeModified[Editor] = true;
                Editor.Modified = false;
                AdjustTabTitle(Page, false);
                return;
            }

            if (Editor.Modified)
                App.AddDirtyEditor(Editor);
                

            AdjustTabTitle(Page, Editor.Modified);
        }
        public void SaveEditorText(RichTextBox Editor)
        {
            string Message = string.Empty;
            TabPage Page = GetEditorPage(Editor);

            if (Page == tabBodyText)
            {
                Chapter.BodyText = Editor.Rtf;
                Chapter.UpdateBodyText();
                Message = $"Chapter: {Chapter}. Body saved";
            }
            else if (Page == tabSynopsis)
            {
                Chapter.Synopsis = Editor.Rtf;
                Chapter.UpdateSynopsis();
                Message = $"Chapter: {Chapter}. Synopsis saved";
            }
            else if (Page == tabConcept)
            {
                Chapter.Concept = Editor.Rtf;
                Chapter.UpdateConcept();
                Message = $"Chapter: {Chapter}. Concept saved";
            }                
            else if (Page == tabOutcome)
            {
                Chapter.Outcome = Editor.Rtf;
                Chapter.UpdateOutcome();
                Message = $"Chapter: {Chapter}. Outcome saved";
            }
            else if (Page == tabScenes)
            {
                if (CurrentScene != null && !ChangingScene)
                {
                    if (Chapter.SceneExists(CurrentScene.Name, CurrentScene.Id)) // when deleting and reloading scenes causes a problem
                    {
                        CurrentScene.BodyText = Editor.Rtf;
                        CurrentScene.UpdateBodyText();
                        Message = $"Chapter: {Chapter}. - Scene: {CurrentScene} saved";
                    }

                }
            }

            Editor.Modified = false;
            AdjustTabTitle(Page, false);

            if (!string.IsNullOrWhiteSpace(Message))
                LogBox.AppendLine(Message);
        }

        // ● properties
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Id { get; set; }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public object Info { get; set; }
    }
}
