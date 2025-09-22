namespace StoryWriter
{
    public partial class UC_NoteList : UserControl, IPanel
    {
        // ● private
        TabPage ParentTabPage { get { return this.Parent as TabPage; } }

        DataTable tblNotes;
        BindingSource bsNotes = new();
 
        void ControlInitialize()
        {
            ParentTabPage.Text = "Notes";

            lblItemTitle.Text = "No selection";
            ucRichText.Clear();
            Grid.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);

            ucRichText.SetTopPanelVisible(false);
            ucRichText.SetEditorReadOnly(true);
 
            btnAddNote.Click += (s, e) => AddNote();
            btnEditNote.Click += (s, e) => EditNote();
            btnDeleteNote.Click += (s, e) => DeleteNote();
            btnEditRtfText.Click += (s, e) => EditNoteText();
            btnAddToQuickView.Click += (s, e) => AddToQuickView();

            edtFilter.TextChanged += (s, e) => FilterChanged();
            Grid.MouseDoubleClick += (s, e) => EditNoteText();
            Grid.KeyDown += (s, e) => {
                if (e.KeyCode == Keys.F2)
                    btnEditNote.PerformClick();
            };

            App.StoryClosed += StoryClosed;
            App.StoryOpened += StoryOpened;
            App.ItemChanged += (object Sender, BaseEntity Item) =>
            {
                if (Sender != this)
                {
                    // nothing, this user control updates itself on changes
                }
            };

            ReLoad();
        }
        void ReLoad()
        {
            if (tblNotes == null)
            {
                tblNotes = new DataTable("Notes");
                tblNotes.Columns.Add("Id", typeof(string));
                tblNotes.Columns.Add("Name", typeof(string));
                tblNotes.Columns.Add("OBJECT", typeof(object));

                bsNotes.DataSource = tblNotes;

                Grid.AutoGenerateColumns = false;
                Grid.DataSource = bsNotes;
                Grid.InitializeReadOnly();
                Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                bsNotes.PositionChanged += (s, e) => SelectedRowChanged();
            }

            if (App.CurrentStory != null)
            {
                bsNotes.SuspendBinding();

                tblNotes.Rows.Clear();
 

                foreach (Note item in App.CurrentStory.NoteList)
                {
                    tblNotes.Rows.Add(item.Id, item.Name, item);
                }

                tblNotes.AcceptChanges();
                Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                bsNotes.ResumeBinding();

                SelectedRowChanged();
            }
        }
        void SelectedRowChanged()
        {
            lblItemTitle.Text = "No selection";
            ucRichText.Clear();

            if (App.CurrentStory == null)
                return;

            DataRow Row = bsNotes.CurrentDataRow();
            if (Row == null)
                return;

            Note Note = Row["OBJECT"] as Note;           
 
            if (Note != null)
            {
                lblItemTitle.Text = Note.Name;
                ucRichText.RtfText = Note.BodyText;               
            }
        } 
        void FilterChanged()
        {
            if (App.CurrentStory != null)
            {
                string S = edtFilter.Text.Trim();

                if (!string.IsNullOrWhiteSpace(S) && S.Length > 2)
                {
                    bsNotes.Filter = $"Name LIKE '%{S}%'";
                    SelectedRowChanged();
                }
                else
                {
                    bsNotes.Filter = string.Empty;
                }
            }
        }

        // ● add/edit/delete
        void AddNote()
        {
            if (App.CurrentStory == null)
                return;

            string Message;
            string ResultName = "";

            if (EditItemDialog.ShowModal("Add Note", App.CurrentStory.Name, ref ResultName))
            {
                if (App.CurrentStory.NoteExists(ResultName))
                {
                    Message = $"Note '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Note Note = new();
                Note.Name = ResultName;
                Note.Id = Sys.GenId(UseBrackets: false);

                if (Note.Insert())
                {
                    DataRow Row = tblNotes.Rows.Add(Note.Id, Note.Name, Note);
                    tblNotes.AcceptChanges();
                    Grid.PositionToRow(Row);

                    Message = $"Note '{ResultName}' added.";
                    LogBox.AppendLine(Message);

                    App.PerformItemListChanged(this, ItemType.Scene);
                }
                else
                {
                    Message = "Add Note. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        void EditNote()
        {
            if (App.CurrentStory == null)
                return;

            DataRow Row = bsNotes.CurrentDataRow();
            if (Row == null)
                return;

            Note Note = Row["OBJECT"] as Note;
            if (Note == null)
                return;

            string Message;
            string ResultName = Note.Name;

            if (EditItemDialog.ShowModal("Edit Note", App.CurrentStory.Name, ref ResultName))
            {
                if (App.CurrentStory.NoteExists(ResultName, Note.Id))
                {
                    Message = $"Note '{ResultName}' already exists.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                Note.Name = ResultName;

                if (Note.Update())
                {
                    Row["Name"] = Note.Name;
                    TabPage Page = App.ContentPagerHandler.FindTabPage(Note.Id);
                    if (Page != null)
                    {
                        UC_Note UC = Page.Tag as UC_Note;
                        if (UC != null)
                            UC.TitleChanged();
                    }

                    Message = $"Note '{Note.Name}' updated.";
                    LogBox.AppendLine(Message);

                    App.PerformItemChanged(this, Note);
                }
                else
                {
                    Message = "Edit Note. Operation failed.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }
            }
        }
        void DeleteNote()
        {
            if (App.CurrentStory == null)
                return;

            DataRow Row = bsNotes.CurrentDataRow();
            if (Row == null)
                return;

            Note Note = Row["OBJECT"] as Note;
            if (Note == null)
                return;

            string Message = $"Are you sure you want to delete the Note '{Note.Name}'?";

            if (!App.QuestionBox(Message))
                return;            

            App.ContentPagerHandler.ClosePage(Note.Id);
             
            if (Note.Delete())
            {
                tblNotes.Rows.Remove(Row);
                tblNotes.AcceptChanges();

                Message = $"Note '{Note.Name}' deleted.";
                LogBox.AppendLine(Message);

                App.PerformItemListChanged(this, ItemType.Scene);
            }
            else
            {
                Message = "Delete Note. Operation failed.";
                App.ErrorBox(Message);
                LogBox.AppendLine(Message);
            }
        }
        void EditNoteText()
        {
            if (App.CurrentStory == null)
                return;

            DataRow Row = bsNotes.CurrentDataRow();
            if (Row == null)
                return;

            Note Note = Row["OBJECT"] as Note;
            if (Note == null)
                return;

            App.ContentPagerHandler.ShowPage(typeof(UC_Note), Note.Id, Note);
        }
 
        void AddToQuickView()
        {
            if (App.CurrentStory == null)
                return;

            DataRow Row = bsNotes.CurrentDataRow();
            if (Row != null)
            {
                string NoteId = Row.AsString("Id");
                Note Note = App.CurrentStory.NoteList.FirstOrDefault(x => x.Id == NoteId);
                if (Note != null)
                {
                    LinkItem LinkItem = new();
                    LinkItem.ItemType = ItemType.Note;
                    LinkItem.Place = LinkPlace.Title;
                    LinkItem.Name = Note.ToString();
                    LinkItem.Item = Note;

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
            SelectedRowChanged();
        }
        void StoryOpened(object sender, EventArgs e)
        {
            ReLoad();
            SelectedRowChanged();
        }

        // ● overrides  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
                ControlInitialize();
        }

        // ● construction
        public UC_NoteList()
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
