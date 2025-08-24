namespace StoryWriter
{
    public partial class UC_ChapterList : UserControl, IPanel
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }
 
        void ControlInitialize()
        {
            ParentTabPage.Text = "Chapters";

            btnAdd.Click += (s, e) => AddChapter();
            btnEdit.Click += (s, e) => EditChapter();
            btnDelete.Click += (s, e) => DeleteChapter();
            btnEditRtfText.Click += (s, e) => EditChapterText();

            btnUp.Click += (s, e) => MoveChapter(true);
            btnDown.Click += (s, e) => MoveChapter(false); 

            lboChapters.MouseDoubleClick += (s, e) =>
            {
                if (btnEditRtfText.Enabled)
                    btnEditRtfText.PerformClick();
            };

            ReLoadChapters();

            App.ProjectClosed += ProjectClosed;
            App.ProjectOpened += ProjectOpened;

            lboChapters.SelectedIndexChanged += (s, e) => SelectedChapterChanged();

            SelectedChapterChanged();
        }
        void ReLoadChapters()
        {
            if (App.CurrentProject != null)
            {
                lboChapters.BeginUpdate();
                lboChapters.Items.Clear();
                lboChapters.Items.AddRange(App.CurrentProject.ChapterList.ToArray());
                if (lboChapters.Items.Count > 0)
                    lboChapters.SelectedIndex = 0;
                lboChapters.EndUpdate();
            }
        }

        void AddChapter()
        {
            string ResultName = "";

            if (EditItemDialog.ShowModal("Add Chapter", App.CurrentProject.Name, ref ResultName))
            {
                if (App.CurrentProject.ChapterExists(ResultName))
                {
                    string Message = $"Chapter '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Chapter Chapter = new();
                Chapter.Id = Sys.GenId(UseBrackets: false);
                Chapter.Name = ResultName;                
                Chapter.OrderIndex = App.CurrentProject.ChapterList.Count + 1;

                if (Chapter.Insert())
                {
                    lboChapters.Items.Add(Chapter);
                    lboChapters.SelectedItem = Chapter;
                    App.CurrentProject.ChapterList.Add(Chapter);
                }
                else
                {
                    string Message = "Add Chapter. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        void EditChapter()
        {
            Chapter Chapter = lboChapters.SelectedItem as Chapter;
            if (Chapter == null)
                return;

            string ResultName = Chapter.Name;

            if (EditItemDialog.ShowModal("Edit Chapter", App.CurrentProject.Name, ref ResultName))
            {
                if (App.CurrentProject.ChapterExists(ResultName, Chapter.Id))
                {
                    string Message = $"Chapter '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }
 
                Chapter.Name = ResultName;

                if (Chapter.Update())
                {
                    lboChapters.Refresh();
                }
                else
                {
                    string Message = "Edit Chapter. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        void DeleteChapter()
        {
            Chapter Chapter = lboChapters.SelectedItem as Chapter;
            if (Chapter == null)
                return;

            if (Chapter.Delete())
            { 
                App.CurrentProject.ChapterList.Remove(Chapter);
                ReLoadChapters();

                string Message = $"Chapter: {Chapter.ToString()}. Deleted";
                LogBox.AppendLine(Message);
            }
            else
            {
                string Message = "Delete Chapter. Operation failed.";
                App.ErrorBox(Message);
                LogBox.AppendLine(Message);
            }
        }
        void EditChapterText()
        {
            Chapter Chapter = lboChapters.SelectedItem as Chapter;
            if (Chapter == null)
                return;

            App.ContentPagerHandler.ShowPage(typeof(UC_Chapter), Chapter.Id, Chapter);
        }

        void MoveChapter(bool Up)
        {
            Chapter Chapter = lboChapters.SelectedItem as Chapter;
            int Index = lboChapters.SelectedIndex;
            if (Index < 0)
                return;
 
            App.CurrentProject.ChapterList.Move(Index, Up);
            App.CurrentProject.RenumberChapters();

            ReLoadChapters();
            lboChapters.SelectedItem = Chapter;
        }
        void UpdateUpDownButtons()
        {
            int Index = lboChapters.SelectedIndex;
            if (Index < 0)
            {
                btnUp.Enabled = false;
                btnDown.Enabled = false;
                return;
            }
            btnUp.Enabled = lboChapters.Items.CanMove(Index, true);
            btnDown.Enabled = lboChapters.Items.CanMove(Index, false);
        }
        void SelectedChapterChanged()
        {
            UpdateUpDownButtons();

            btnAdd.Enabled = App.CurrentProject != null;
            btnEdit.Enabled = App.CurrentProject != null && (lboChapters.SelectedItem != null);
            btnDelete.Enabled = App.CurrentProject != null && (lboChapters.SelectedItem != null);
            btnEditRtfText.Enabled = App.CurrentProject != null && (lboChapters.SelectedItem != null);
        }

        // ● event handlers
        void ProjectClosed(object sender, EventArgs e)
        {
            lboChapters.Items.Clear();
            SelectedChapterChanged();
        }
        void ProjectOpened(object sender, EventArgs e)
        {
            lboChapters.Items.AddRange(App.CurrentProject.ChapterList.ToArray());
            SelectedChapterChanged();
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
