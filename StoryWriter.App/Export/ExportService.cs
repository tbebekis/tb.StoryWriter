namespace StoryWriter
{
    public class ExportService
    { 
        void DisplayFileExplorer()
        {
            App.WaitForFileAvailable(FilePath);  
            string args = string.Format("/e, /select, \"{0}\"", FilePath);

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "explorer";
            info.Arguments = args;
            Process.Start(info);
        }

        // ● construction
        public ExportService( ) 
        {
            ExportFolder = Path.Combine(App.BinPath, "Exports");

            if (!Directory.Exists(ExportFolder)) 
                Directory.CreateDirectory(ExportFolder);
        }

        // ● public
        public bool Export()
        {
            if (ExportOptionsDialog.ShowModal(this))
            {                
                DoExport();

                DisplayFileExplorer();
                return true;
            }

            return false;
        }
        public void DoExport()
        {
            if (ExportMode == ExportMode.Markdown)
                this.RtfToPlainText = true;

            Project = new();
            Project.From(this);

            string ProjectName = App.CurrentProject.Name.Replace(' ', '_');

            switch (ExportMode)
            {
                case ExportMode.JSON:
                    new JsonExporter(this).Execute();
                    break;
                case ExportMode.RTF:
                    new RftExporter(this).Execute();
                    break;
                case ExportMode.DOCX:
                    new DocxExporter(this).Execute();
                    break;
                case ExportMode.ODT:
                    new OdtExporter(this).Execute();
                    break;
                case ExportMode.Markdown:                    
                    new MarkdownExporter(this).Execute();
                    break;
            }
        }
        
        /// <summary>
        /// Exports the current project to RTF and returns the path to the RTF file
        /// <para>The LibreOffice needs a RTF file always.</para>
        /// </summary>
        public string ExportToRtf()
        {
            ExportService Copy = new();
            Copy.ExportMode = ExportMode.RTF;
            Copy.DoExport();
            return Copy.FilePath;
        }

        /// <summary>
        /// Converts RTF to plain text if needed
        /// </summary>
        public string GetText(string RtfText) => RtfToPlainText ? App.ToPlainText(RtfText) : RtfText;
        /// <summary>
        /// Returns true if LibreOffice is installed.
        /// <para>Displays a message box if not.</para>
        /// </summary>
        public bool CheckLibreOfficeExists(bool LogIfNot = true)
        {
            if (!LibreOfficeExporter.LibreOfficeExists())
            {
                if (LogIfNot)
                {
                    string Message = "LibreOffice is not installed. Please install it and try again.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                }

                return false;
            }

            return true;
        }

        // ● properties
        public string ExportFolder { get; }
        public string FileName
        {
            get
            {
                string Ext = "dat";

                switch (ExportMode)
                {
                    case ExportMode.JSON:
                        Ext = "json";
                        break;
                    case ExportMode.RTF:
                        Ext = "rtf";
                        break;
                    case ExportMode.DOCX:
                        Ext = "docx";
                        break;
                    case ExportMode.ODT:
                        Ext = "odt";
                        break;
                    case ExportMode.Markdown:
                        Ext = "md";
                        break;
                }

                string ProjectName = App.CurrentProject.Name.Replace(' ', '_');
                string DT = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");

                return $"{ProjectName} {DT}.{Ext}";
            }
        }
        public string FilePath => Path.Combine(ExportFolder, FileName);
        public ExportMode ExportMode { get; set; }
        public bool RtfToPlainText { get; set; }
        public ProjectProxy Project { get; private set; }
    }
}
