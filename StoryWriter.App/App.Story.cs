namespace StoryWriter
{
    static public partial class App
    {
        // ● filenames and paths
        /// <summary>
        /// Returns true if the provided story name is valid (not empty, no invalid characters, does not start with a digit).
        /// </summary>
        static public bool IsValidStoryName(string StoryName)
        {
            if (string.IsNullOrWhiteSpace(StoryName))
                return false;

            char[] InvalidChars = System.IO.Path.GetInvalidFileNameChars();

            foreach (char c in InvalidChars)
            {
                if (StoryName.Contains(c))
                    return false;
            }

            if (char.IsDigit(StoryName[0]))
                return false;

            return true;
        }
        /// <summary>
        /// Returns the story name derived from the given story file name by removing the extension and replacing underscores with spaces.
        /// </summary>
        static public string StoryFileNameToStoryName(string StoryFileName)
        {
            string Result = Path.GetFileNameWithoutExtension(StoryFileName).TrimEnd(Path.DirectorySeparatorChar);
            Result = Result.Replace('_', ' ');
            return Result;
        }
        /// <summary>
        /// Returns a valid story file or folder name derived from the given story name by replacing spaces with underscores.
        /// </summary>
        static public string StoryNameToStoryFileName(string StoryName)
        {
            string Result = StoryName.Replace(' ', '_');
            return Result;
        }
        /// <summary>
        /// Returns a valid story database file name derived from the given story name by replacing spaces with underscores and appending the .db3 extension.
        /// </summary>
        static public string StoryNameToStoryDatabaseFileName(string StoryName)
        {
            string Result = StoryNameToStoryFileName(StoryName);
            Result += ".db3";
            return Result;
        }
        /// <summary>
        /// Returns a valid story folder path derived from the given story name by replacing spaces with underscores.
        /// </summary>
        static public string StoryNameToStoryFolderPath(string StoryName)
        {
            string StoryFolderName = StoryNameToStoryFileName(StoryName);
            string Result = Path.Combine(StoriesFolderPath, StoryFolderName);
            return Result;
        }
        /// <summary>
        /// Returns a valid story database file path derived from the given story name by replacing spaces with underscores and appending the .db3 extension.
        /// </summary>
        static public string StoryNameToStoryDatabaseFilePath(string StoryName)
        {
            string FolderPath = StoryNameToStoryFolderPath(StoryName);
            string FileName = StoryNameToStoryDatabaseFileName(StoryName);
            string Result = Path.Combine(FolderPath, FileName);
            return Result;
        }

        // ● 
        /// <summary>
        /// Returns a list of all story names found in the Storys folder
        /// </summary>
        static public List<string> GetStoryNameList()
        {
            List<string> StoryNames = new();
            string[] StoryFolders = Directory.GetDirectories(StoriesFolderPath);

            foreach (string StoryFolder in StoryFolders)
            {
                string StoryName = StoryFileNameToStoryName(StoryFolder);
                StoryNames.Add(StoryName);
            }

            return StoryNames;
        }
        /// <summary>
        /// Returns true if a story with the given name exists
        /// </summary>
        static public bool StoryExists(string StoryName)
        {
            List<string> StoryNamesList = GetStoryNameList();
            return StoryNamesList.Contains(StoryName);
        }

        /// <summary>
        /// Loads the last opened story
        /// </summary>
        static public void LoadLastStory()
        {
            CloseAllUi();

            if (Settings.LoadLastStoryOnStartup && !string.IsNullOrWhiteSpace(Settings.LastStory) && StoryExists(Settings.LastStory))
            {
                LoadStory(Settings.LastStory, CloseCurrentStory: true);
            }
        }
        /// <summary>
        /// Loads a story by name
        /// </summary>
        static public void LoadStory(string StoryName, bool CloseCurrentStory)
        {
            string Message;

            if (CloseCurrentStory)
                CloseStory();

            if (!StoryExists(StoryName))
            {
                Message = $"Story '{StoryName}' does not exist.";
                ErrorBox(Message);
                LogBox.AppendLine(Message);
                return;
            }

            string StoryDatabaseFilePath = StoryNameToStoryDatabaseFilePath(StoryName);
            SqlConnectionInfo CI = new(StoryName, SqlProvider.Name, "", StoryDatabaseFilePath, "", "");
            SqlStore = new SqlStore(CI);

            CurrentStory = new Story(StoryName);
            CurrentStory.Open();

            StoryOpened?.Invoke(null, EventArgs.Empty);

            Settings.LastStory = StoryName;
            Settings.Save();

            ShowSideBarPages();

            Message = $"Story '{StoryName}' opened.";
            LogBox.AppendLine(Message);

        }
        /// <summary>
        /// Closes the current story. Closes all open UI too.
        /// </summary>
        static public void CloseStory()
        {
            if (CurrentStory != null)
            {
                StoryClosed?.Invoke(null, EventArgs.Empty);

                string StoryName = CurrentStory.Name;

                CloseAllUi();
                CurrentStory.Close();
                CurrentStory = null;
                SqlStore = null;

                string Message = $"Story '{StoryName}' closed.";
                LogBox.AppendLine(Message);
            }
        }

        /// <summary>
        /// Creates a new story and makes it the current story
        /// </summary>
        static public void CreateNewStory(bool LoadToo)
        {
            if (NewStoryDialog.ShowModal(out string StoryName))
            {
                string Message;

                if (StoryExists(StoryName))
                {
                    Message = $"Story '{StoryName}' already exists. Please choose another name.";
                    ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
                }

                CloseStory();

                string StoryFolderPath = StoryNameToStoryFolderPath(StoryName);
                Directory.CreateDirectory(StoryFolderPath);

                string StoryDatabaseFilePath = StoryNameToStoryDatabaseFilePath(StoryName);

                SqlConnectionInfo CI = new(Sys.DEFAULT, SqlProvider.Name, "", StoryDatabaseFilePath, "", "");
                SqlProvider.CreateDatabase(CI.ConnectionString);

                WaitForFileAvailable(StoryDatabaseFilePath);

                Db.Connections = null;
                Db.Connections = new List<SqlConnectionInfo>() { CI };

                //Db.Connections.Add(CI); // add the connection info to the global connections list 

                SqlStore = new SqlStore(CI);

                CurrentStory = new Story(StoryName);
                CurrentStory.CreateSchema();
                CurrentStory.Close();

                /// CurrentStory.Open();
                /// StoryOpened?.Invoke(null, EventArgs.Empty);
                /// 
                /// Settings.LastStory = StoryName;
                /// Settings.Save();

                Message = $"Story '{StoryName}' created.";
                LogBox.AppendLine(Message);

                if (LoadToo)
                    LoadStory(StoryName, CloseCurrentStory: false);

            }
        }


        static public string CurrentStoryFolderPath
        {
            get
            {
                if (CurrentStory == null)
                    return string.Empty;

                string FolderPath = App.StoryNameToStoryFolderPath(App.CurrentStory.Name);
                return FolderPath;
            }
        }
        static public string QuickViewListFilePath
        {
            get
            {
                if (CurrentStory == null)
                    return string.Empty;

                string FileName = App.StoryNameToStoryFileName(App.CurrentStory.Name);
                FileName = "QuickViewList_" + FileName;
                FileName = Path.ChangeExtension(FileName, ".json");
 
                string QuickViewListFilePath = Path.Combine(CurrentStoryFolderPath, FileName);
                return QuickViewListFilePath;
            }
        }
        static public string CurrentStoryTempDocFilePath
        {
            get
            {
                if (CurrentStory == null)
                    return string.Empty;

                string QuickViewListFilePath = Path.Combine(CurrentStoryFolderPath, "TempDoc.rtf");
                return QuickViewListFilePath;
            }
        }

    }
}
