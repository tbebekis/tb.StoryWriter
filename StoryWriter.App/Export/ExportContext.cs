namespace StoryWriter
{
    public class ExportContext
    {
        // ● private
        string fExportFolderPath;

        void DisplayFileExplorer()
        {
            string args = string.Format("/e, /select, \"{0}\"", ExportFolderPath);
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "explorer";
            info.Arguments = args;
            Process.Start(info);
        }
        
        


        // ● construction
        /// <summary>
        /// Constructor
        /// </summary>
        public ExportContext()
        {
        }

        public void Execute()
        {
            string DatabaseFileName = Path.GetFileName(App.StoryDatabaseFilePath);
            string DatabaseFolderPath = Path.Combine(ExportFolderPath, DatabaseFileName);

            File.Copy(App.StoryDatabaseFilePath, DatabaseFolderPath);
            App.WaitForFileAvailable(DatabaseFolderPath);


            Story = new StoryProxy();
            Story.Export(this);

            JsonExporter JsonExporter = new JsonExporter(this);
            JsonExporter.Execute();

            PlainTextExporter PlainTextExporter = new PlainTextExporter(this);
            PlainTextExporter.Execute();

            OdtExporter OdtExporter = new OdtExporter(this);
            OdtExporter.Execute();

            MarkdownExporter MarkdownExporter = new MarkdownExporter(this);
            MarkdownExporter.Execute();

            WikiExporter WikiExporter = new WikiExporter(this);
            WikiExporter.Execute();

            DisplayFileExplorer();
        }

        // ● properties
        /// <summary>
        /// Returns the export folder path
        /// </summary>
        public string ExportFolderPath
        {
            get
            {   
                if (fExportFolderPath == null)
                {
                    string StoryFolderPath = App.StoryNameToStoryFolderPath(App.CurrentStory.Name);
                    fExportFolderPath = Path.Combine(StoryFolderPath, "Exports");

                    string DT = DateTime.Now.ToString("yyyy-MM-dd HH_mm_ss");
                    fExportFolderPath = Path.Combine(fExportFolderPath, DT);

                    if (!Directory.Exists(fExportFolderPath))
                        Directory.CreateDirectory(fExportFolderPath);
                }


                return fExportFolderPath;
            }
        }
        public bool InPlainText { get; set; }
        public StoryProxy Story { get; private set; }
    }


    public class ImportService
    {
        // ● construction
        /// <summary>
        /// Constructor
        /// </summary>
        public ImportService()
        {
        }
    }


}
