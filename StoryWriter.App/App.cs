namespace StoryWriter
{
    static public partial class App
    {
        // ● private
        const string SProjectsFolder = "Projects";
        static SqlProvider SqlProvider; 

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

                //LoadConnectionStrings();
                //CreateDatabases();

                if (!Directory.Exists(ProjectsPath))
                    Directory.CreateDirectory(ProjectsPath);

                LoadAll();
            }
        }
        /// <summary>
        /// Loads database configuration settings.
        /// </summary>
        static void LoadConnectionStrings()
        {
            SysConfig.SqlConnectionsFolder = typeof(App).Assembly.GetFolder();
            SqlConnectionInfoList ConnectionInfoList = new ();
            Db.Connections = ConnectionInfoList.SqlConnections;
        }
        /// <summary>
        /// Creates any non-existing creatable database.
        /// </summary>
        static void CreateDatabases()
        {
            SqlProvider Provider;
            string ConnectionString;

            SqlConnectionInfo DefaultConnectionInfo = Db.DefaultConnectionInfo;

            Provider = DefaultConnectionInfo.GetSqlProvider();
            ConnectionString = DefaultConnectionInfo.ConnectionString;

            if (!Provider.DatabaseExists(ConnectionString) && Provider.CanCreateDatabases)
            {
                Provider.CreateDatabase(ConnectionString);
            }

            foreach (var ConInfo in Db.Connections)
            {
                if (ConInfo != DefaultConnectionInfo)
                {
                    Provider = ConInfo.GetSqlProvider();
                    ConnectionString = ConInfo.ConnectionString;

                    if (!Provider.DatabaseExists(ConnectionString) && Provider.CanCreateDatabases)
                    {
                        Provider.CreateDatabase(ConnectionString);
                    }
                }
            }

        }

        /// <summary>
        /// Closes all opened UI.
        /// </summary>
        static public void CloseAll()
        {
            SideBarPagerHandler.CloseAll();
            ContentPagerHandler.CloseAll();
        }

        static public void LoadAll()
        {
            CloseAll();

            if (Settings.LoadLastProjectOnStartup && !string.IsNullOrWhiteSpace(Settings.LastProject) && ProjectExists(Settings.LastProject))
            {
                LoadProject(Settings.LastProject);
            }

            SideBarPagerHandler.ShowPage(typeof(UC_ComponentTree), nameof(UC_ComponentTree), null);
            SideBarPagerHandler.ShowPage(typeof(UC_ChapterList), nameof(UC_ChapterList), null);
        }
        static public void ShowSettingsDialog()
        {
            string Message = @"This will close all opened UI. 
Any unsaved changes will be lost. 
Do you want to continue?
";
            if (!App.QuestionBox(Message))
                return;

            App.CloseAll();

            if (AppSettingDialog.ShowModal())
            {
                App.Settings.Save();
                App.LoadAll();
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
        /// Loads a project by name
        /// </summary>
        static public void LoadProject(string ProjectName)
        {
            string Message;
            if (CurrentProject != null)
            {
                ProjectClosed?.Invoke(null, EventArgs.Empty);

                CurrentProject.Close();
                CurrentProject = null;
                SqlStore = null;
            }

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

            Message = $"Project '{ProjectName}' opened.";
            LogBox.AppendLine(Message);
          
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

        // ● events
        static public event EventHandler ProjectClosed;
        static public event EventHandler ProjectOpened;


    }
}
