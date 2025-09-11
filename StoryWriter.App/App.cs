namespace StoryWriter
{
    static public partial class App
    {
        // ● private
        const string SStoriesFolder = "Stories";
        static SqlProvider SqlProvider;
        static decimal fZoomFactor = 1.0M;

        

        static AutoSaveService AutoSaveService;
        static System.Threading.Lock syncLock = new();
        static List<UC_RichText> DirtyEditors = new();

        static void AutoSaveProc()
        {
            lock (syncLock)
            {
                if (CurrentStory != null)
                {
                    if (DirtyEditors.Count > 0)
                    {
                        while (DirtyEditors.Count > 0)
                        {
                            UC_RichText ucRichText = DirtyEditors[0];
                            DirtyEditors.RemoveAt(0);
                            try
                            {
                                if (ucRichText.Modified)
                                    ucRichText.SaveText();
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
        }

        // ● public 
        /// <summary>
        /// Initializes the App class
        /// </summary>
        static public void Initialize(MainForm MainForm)
        {
            if (!IsInitialized)
            {
                App.MainForm = MainForm;

                SqlProvider = SqlProviders.GetSqlProvider(SqlProvider.SQLite);

                if (!Directory.Exists(StoriesFolderPath))
                    Directory.CreateDirectory(StoriesFolderPath);

                ZoomFactor = Settings.ZoomFactor;

                LoadLastStory();

                AutoSaveService = new AutoSaveService(AutoSaveProc);
                AutoSaveService.Enabled = Settings.AutoSave;
            }
        }

        // ● message boxes
        /// <summary>
        /// Shows a message box
        /// </summary>
        static public void ErrorBox(string Message)
        {
            MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        /// <summary>
        /// Shows a message box
        /// </summary>
        static public void WarningBox(string Message)
        {
            MessageBox.Show(Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// Shows a message box
        /// </summary>
        static public void InfoBox(string Message)
        {
            MessageBox.Show(Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Shows a message box
        /// </summary>
        static public bool QuestionBox(string Message)
        {
            return MessageBox.Show(Message, "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        // ● miscs
        /// <summary>
        /// Adds a dirty editor to the list or dirty editors for auto-save.
        /// </summary>
        static public void AddDirtyEditor(RichTextBox Editor)
        {
            lock (syncLock)
            {
                try
                {
                    if (AutoSaveService == null)
                        return;

                    if (Editor.Modified && Editor.Parent is UC_RichText)
                    {
                        UC_RichText ucRichText = Editor.Parent as UC_RichText;
                        if (!DirtyEditors.Contains(ucRichText))
                        {
                            DirtyEditors.Add(ucRichText);
                            AutoSaveService.MarkAsDirty();
                        }
                    }
                }
                catch
                {
                }
            }

        }
        /// <summary>
        /// Triggers the <see cref="SearchTermIsSet"/> event
        /// </summary>
        static public void SetSearchTerm(string Term)
        {
            SearchTermIsSet?.Invoke(null, Term);
        }

        /// <summary>
        /// Returns the <see cref="DataRow"/> with the given Id
        /// </summary>
        static public DataRow FindDataRowById(this DataTable Table, string Id)
        {
            foreach (DataRow Row in Table.Rows)
            {
                if (Row["Id"]?.ToString() == Id)
                    return Row;
            }
            return null;
        }
        /// <summary>
        /// Waits until a file exists and can be opened for read access.
        /// </summary>
        /// <param name="path">The file path.</param>
        /// <param name="timeoutMilliseconds">Maximum wait time in milliseconds (0 means infinite).</param>
        /// <param name="checkIntervalMilliseconds">Interval between checks in milliseconds.</param>
        /// <returns>True if file is available within the timeout, otherwise false.</returns>
        static public bool WaitForFileAvailable(string path, int timeoutMilliseconds = 30 * 1000, int checkIntervalMilliseconds = 300)
        {
            var start = DateTime.UtcNow;

            while (true)
            {
                if (File.Exists(path))
                {
                    try
                    {
                        using (FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            return true; // Success: file can be read
                        }
                    }
                    catch (IOException)
                    {
                        // File exists but is still locked, retry
                    }
                }

                if (timeoutMilliseconds > 0 && (DateTime.UtcNow - start).TotalMilliseconds > timeoutMilliseconds)
                    return false; // Timeout

                Thread.Sleep(checkIntervalMilliseconds);
            }
        }

        // ● event triggers
        /// <summary>
        /// Triggers the <see cref="SearchResultsChanged"/> event
        /// </summary>
        static public void PerformSearchResultsChanged(List<LinkItem> LinkItems)
        {
            SearchResultsChanged?.Invoke(null, LinkItems);
        }
        /// <summary>
        /// Triggers the <see cref="ItemListChanged"/> event
        /// </summary>
        static public void PerformItemListChanged(ItemType ItemType)
        {
            ItemListChanged?.Invoke(null, ItemType);
        }

        // ● import / export
        static public void Export()
        {
            LogBox.AppendLine("Exporting current Story...Please wait...");
            Application.DoEvents();

            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            try
            {
                ExportContext Context = new();
                Context.Execute();
                string Message = @$"Current Story exported to 
{Context.ExportFolderPath}";
                LogBox.AppendLine(Message);
                Application.DoEvents();

                App.InfoBox(Message);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                Application.DoEvents();
            }


        }
        static public void Import()
        {

        }

        // ● properties 
        /// <summary>
        /// True if the App class has been initialized
        /// </summary>
        static public bool IsInitialized => MainForm != null;
        /// <summary>
        /// The physical path of the output folder
        /// <para>e.g. C:\MyApp\bin\Debug\net9.0\  </para>
        /// </summary>
        static public string BinPath => System.AppContext.BaseDirectory;
        /// <summary>
        /// The physical path of the Storys folder
        /// </summary>
        static public string StoriesFolderPath => Path.Combine(BinPath, SStoriesFolder);
        /// <summary>
        /// Application settings
        /// </summary>
        static public AppSettings Settings { get; } = new AppSettings();

        /// <summary>
        /// The SQL store of the currently opened project, could be null.
        /// </summary>
        static public SqlStore SqlStore { get; private set; }

        /// <summary>
        /// The main form of the application
        /// </summary>
        static public MainForm MainForm { get; private set; }
        /// <summary>
        /// The <see cref="PagerHandler"/> for the side bar
        /// </summary>
        static public PagerHandler SideBarPagerHandler { get; set; }
        /// <summary>
        /// The <see cref="PagerHandler"/> for the content
        /// </summary>
        static public PagerHandler ContentPagerHandler { get; set; }
        /// <summary>
        /// The zoom factor to be used by all rich edit boxes in this application
        /// </summary>
        static public decimal ZoomFactor
        {
            get { return fZoomFactor; }
            set
            {
                fZoomFactor = value;
                ZoomFactorChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        /// <summary>
        /// The currently opened story, could be null.
        /// </summary>
        static public Story CurrentStory { get; private set; }

        // ● events
        /// <summary>
        /// Triggered when a new project is closed
        /// </summary>
        static public event EventHandler StoryClosed;
        /// <summary>
        /// Triggered when a new project is opened
        /// </summary>
        static public event EventHandler StoryOpened;
        /// <summary>
        /// Triggered when a tag is added to a component
        /// </summary>
        static public event EventHandler TagToComponetsChanged;
        /// <summary>
        /// Triggered when the global zoom factor is changed
        /// </summary>
        static public event EventHandler ZoomFactorChanged;
        /// <summary>
        /// Triggered when the search term is set
        /// </summary>
        static public event EventHandler<string> SearchTermIsSet;
        /// <summary>
        /// Triggered when the search results are changed
        /// </summary>
        static public event EventHandler<List<LinkItem>> SearchResultsChanged;
        /// <summary>
        /// Triggered when the item list is changed
        /// </summary>
        static public event EventHandler<ItemType> ItemListChanged;
    }
}
