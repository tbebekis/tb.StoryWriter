using StoryWriter.Export;

namespace StoryWriter
{
    static public partial class App
    {
        // ● private
        const string SProjectsFolder = "Projects";
        static SqlProvider SqlProvider;

        static AutoSaveService AutoSaveService;
        static System.Threading.Lock syncLock = new();
        static List<UC_RichText> DirtyEditors = new();

        static void AutoSaveProc()
        {
            lock (syncLock)
            {
                if (DirtyEditors.Count > 0)
                {
                    while (DirtyEditors.Count > 0)
                    {
                        UC_RichText ucRichText = DirtyEditors[0];
                        DirtyEditors.RemoveAt(0);
                        try
                        {
                            if (ucRichText.Editor.Modified)
                                ucRichText.SaveText();
                        }
                        catch  
                        {
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

                if (!Directory.Exists(ProjectsPath))
                    Directory.CreateDirectory(ProjectsPath);

                LoadLastProject();

                AutoSaveService = new AutoSaveService(AutoSaveProc);
                AutoSaveService.Enabled = Settings.AutoSave;
            }
        }
 
        /// <summary>
        /// Closes all opened UI.
        /// </summary>
        static public void CloseAllUi()
        {
            SideBarPagerHandler.CloseAll();
            ContentPagerHandler.CloseAll();
        }

        
        static public void ShowSettingsDialog()
        {
            string Message = @"This will close all opened UI. 
Any unsaved changes will be lost. 
Do you want to continue?
";
            if (!App.QuestionBox(Message))
                return;

            App.CloseProject();
            Application.DoEvents();

            if (AppSettingDialog.ShowModal())
            {
                AutoSaveService.Enabled = false;

                App.Settings.Save();

                Message = $"Application Settings saved.";
                LogBox.AppendLine(Message);

                App.LoadLastProject();

                AutoSaveService.AutoSaveSecondsInterval = Settings.AutoSaveSecondsInterval;
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

        // ● project related
        /// <summary>
        /// Returns a list of all project names found in the Projects folder
        /// </summary>
        static public List<string> GetProjectNameList()
        {
            string[] ProjectDatabases = System.IO.Directory.GetFiles(ProjectsPath, "*.db3", System.IO.SearchOption.TopDirectoryOnly);
            List<string> ProjectNames = new ();
            foreach (string ProjectDatabasePath in ProjectDatabases)
            {
                string ProjectName = Project.ProjectFileNameToProjectName(Path.GetFileName(ProjectDatabasePath));
                ProjectNames.Add(ProjectName);
            }
            return ProjectNames;
        }
        /// <summary>
        /// Returns true if a project with the given name exists
        /// </summary>
        static public bool ProjectExists(string ProjectName)
        {
            return GetProjectNameList().Contains(ProjectName);
        }
        /// <summary>
        /// Loads the last opened project
        /// </summary>
        static public void LoadLastProject()
        {
            CloseAllUi();

            if (Settings.LoadLastProjectOnStartup && !string.IsNullOrWhiteSpace(Settings.LastProject) && ProjectExists(Settings.LastProject))
            {
                LoadProject(Settings.LastProject);
            } 
        }
        /// <summary>
        /// Loads a project by name
        /// </summary>
        static public void LoadProject(string ProjectName)
        {
            string Message;
            CloseProject();

            if (!ProjectExists(ProjectName))
            {
                Message = $"Project '{ProjectName}' does not exist.";
                ErrorBox(Message);
                LogBox.AppendLine(Message);
                return;
            }

            string ProjectFileName = Project.ProjectNameToProjectFileName(ProjectName);
            string ProjectFilePath = Path.Combine(ProjectsPath, ProjectFileName);
            SqlConnectionInfo CI = new(ProjectName, SqlProvider.Name, "", ProjectFilePath, "", "");
            SqlStore = new SqlStore(CI);
            CurrentProject = new Project(ProjectName);

            CurrentProject.Open();
            ProjectOpened?.Invoke(null, EventArgs.Empty);

            Settings.LastProject = ProjectName;
            Settings.Save();

            SideBarPagerHandler.ShowPage(typeof(UC_ComponentTree), nameof(UC_ComponentTree), null);
            SideBarPagerHandler.ShowPage(typeof(UC_ChapterList), nameof(UC_ChapterList), null);

            Message = $"Project '{ProjectName}' opened.";
            LogBox.AppendLine(Message);
          
        }
        /// <summary>
        /// Closes the current project. Closes all open UI too.
        /// </summary>
        static public void CloseProject()
        {
            if (CurrentProject != null)
            {
                ProjectClosed?.Invoke(null, EventArgs.Empty);

                string ProjectName = CurrentProject.Name;

                CloseAllUi();
                CurrentProject.Close();
                CurrentProject = null;
                SqlStore = null;

                string Message = $"Project '{ProjectName}' closed.";
                LogBox.AppendLine(Message);
            } 
        }
        /// <summary>
        /// Opens an existing project and makes it the current project
        /// </summary>
        static public void OpenProject()
        {
            List<string> List = GetProjectNameList();

            if (List.Count == 0)
            {
                string Message = "No projects found. Please create a new project.";
                ErrorBox(Message);
                LogBox.AppendLine(Message);
                return;
            }
        
            if (SelectProjectDialog.ShowModal(List, out string ProjectName))
            {
                LoadProject(ProjectName);
            }    

        }
        /// <summary>
        /// Creates a new project and makes it the current project
        /// </summary>
        static public void CreateNewProject()
        {
            if (NewProjectDialog.ShowModal(out string ProjectName))
            {
                string Message;

                if (ProjectExists(ProjectName))
                {
                    Message = $"Project '{ProjectName}' already exists. Please choose another name.";
                    ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                string ProjectFileName = Project.ProjectNameToProjectFileName(ProjectName);
                string ProjectFilePath = Path.Combine(ProjectsPath, ProjectFileName);
 
                SqlConnectionInfo CI = new (Sys.DEFAULT, SqlProvider.Name, "", ProjectFilePath, "", "");                
                SqlProvider.CreateDatabase(CI.ConnectionString);

                Db.Connections = null;
                Db.Connections = new List<SqlConnectionInfo>() { CI };                    

                //Db.Connections.Add(CI); // add the connection info to the global connections list 

                SqlStore = new SqlStore(CI);
                CurrentProject = new Project(ProjectName);
                CurrentProject.CreateSchema();

                CurrentProject.Open();
                ProjectOpened?.Invoke(null, EventArgs.Empty);

                Settings.LastProject = ProjectName;
                Settings.Save();

                Message = $"Project '{ProjectName}' created.";
                LogBox.AppendLine(Message);

                LoadProject(ProjectName);
            }
        }
 
        /// <summary>
        /// Exports the current project to RTF format
        /// </summary>
        static public void ExportCurrentProjectToRtf(string FilePath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);

            using var rtb = new RichTextBox { DetectUrls = false };
            rtb.Clear();

            for (int i = 0; i < CurrentProject.ChapterList.Count; i++)
            {
                var ch = CurrentProject.ChapterList[i];
                var title = string.IsNullOrWhiteSpace(ch?.Name) ? $"Chapter {i + 1}" : ch!.Name;

                // Τίτλος κεφαλαίου (bold, λίγο μεγαλύτερο)
                rtb.Select(rtb.TextLength, 0);
                rtb.SelectionFont = new Font("Arial", 14, FontStyle.Bold);
                rtb.AppendText(title + Environment.NewLine + Environment.NewLine);

                // Κείμενο κεφαλαίου ως RTF (κρατά format)
                if (!string.IsNullOrWhiteSpace(ch?.BodyText))
                {
                    rtb.Select(rtb.TextLength, 0);
                    rtb.SelectedRtf = ch!.BodyText;   // merge με τους πίνακες format του RTB
                }

                // Page break μεταξύ κεφαλαίων
                if (i < CurrentProject.ChapterList.Count - 1)
                {
                    rtb.Select(rtb.TextLength, 0);
                    // μικρό RTF fragment για page break
                    rtb.SelectedRtf = @"{\rtf1\ansi\pard\page\par}";
                    rtb.AppendText(Environment.NewLine);
                }
            }

            rtb.SaveFile(FilePath, RichTextBoxStreamType.RichText);
        }
        /// <summary>
        /// Exports the current project to DOCX format
        /// </summary>
        static public void ExportCurrentProjectToDocx(string FilePath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);

            var opts = new DocxExportOptions
            {
                IncludeToc = true,
                PageBreakBetweenChapters = true,
                Mode = ChapterExportMode.AltChunkRtf // ή PlainText για συμβατότητα με Libre/μη-Word viewers
            };

            OpenXmlExporter.ExportProjectToDocx(CurrentProject, FilePath, opts);
        }
        /// <summary>
        /// Exports the current project to ODT format
        /// </summary>
        static public void ExportCurrentProjectToOdt(string FilePath)
        {
            if (!LibreOfficeExporter.LibreOfficeExists())
            {
                string Message = "LibreOffice is not installed. Please install it and try again.";
                ErrorBox(Message);
                LogBox.AppendLine(Message);
                return;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);

            string RtfFilePath = Path.ChangeExtension(FilePath, ".rtf");
            ExportCurrentProjectToRtf(RtfFilePath);

            LibreOfficeExporter.Export(RtfFilePath, DocFormatType.Odt);
        }
        /// <summary>
        /// Exports the current project to TXT format
        /// </summary>
        static public void ExportCurrentProjectToTxt(string FilePath)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            FilePath = Path.ChangeExtension(FilePath, ".md");
            TextExporter.Export(CurrentProject, FilePath);
        }


        /// <summary>
        /// Adds a dirty editor to the list or dirty editors for auto-save.
        /// </summary>
        static public void AddDirtyEditor(RichTextBox Editor)
        {
            lock(syncLock)
            {
                try
                {
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
        /// The physical path of the Projects folder
        /// </summary>
        static public string ProjectsPath => Path.Combine(BinPath, SProjectsFolder);
        /// <summary>
        /// Application settings
        /// </summary>
        static public AppSettings Settings { get; } = new AppSettings();
        /// <summary>
        /// The currently opened project, could be null.
        /// </summary>
        static public Project CurrentProject { get; private set; }

        /// <summary>
        /// The SQL store of the currently opened project, could be null.
        /// </summary>
        static public SqlStore SqlStore { get; private set; }

        /// <summary>
        /// The main form of the application
        /// </summary>
        static public MainForm MainForm { get; private set; }

        static public PagerHandler SideBarPagerHandler { get; set; }
        static public PagerHandler ContentPagerHandler { get; set; }
        //static public AutoSaveService AutoSaveService { get; set; }

        // ● events
        static public event EventHandler ProjectClosed;
        static public event EventHandler ProjectOpened;


    }
}
