namespace StoryWriter
{
    public class OdtExporter
    {
        ExportContext ExportContext;
        StringBuilder SB;


        /// <summary>
        /// Returns true if LibreOffice is installed.
        /// <para>Displays a message box if not.</para>
        /// </summary>
        static bool CheckLibreOfficeExists(bool LogIfNot = true)
        {
            if (!LibreOfficeExists())
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
        static string FindSoffice()
        {
            // environment variable
            var envPath = Environment.GetEnvironmentVariable("PATH") ?? "";

            foreach (var dir in envPath.Split(Path.PathSeparator))
            {
                var exe = Path.Combine(dir, "soffice.exe");
                if (File.Exists(exe)) return exe;
                exe = Path.Combine(dir, "soffice");
                if (File.Exists(exe)) return exe;
                exe = Path.Combine(dir, "libreoffice");
                if (File.Exists(exe)) return exe;
            }

            // application path
            var candidates = new[]
            {
                @"C:\Program Files\LibreOffice\program\soffice.exe",
                @"C:\Program Files (x86)\LibreOffice\program\soffice.exe"
            };

            foreach (var c in candidates)
                if (File.Exists(c)) return c;

            return null;
        }
        /// <summary>
        /// Returns true if LibreOffice exists
        /// </summary>
        static bool LibreOfficeExists() => FindSoffice() != null;

        static readonly string[] NewLines = new[] { "\r\n", "\r", "\n" };
        static string[] ToLines(string Text) => Text.Split(NewLines, StringSplitOptions.None);

        string GetText(string PlainText)
        {
            if (!string.IsNullOrWhiteSpace(PlainText))
            {
                string[] Lines = ToLines(PlainText);
                string Line;
                for (int i = 0; i < Lines.Length; i++)
                {
                    Line = Lines[i];
                    if (string.IsNullOrWhiteSpace(Line))
                        Line = "&nbsp;";

                    Line = Line.Replace("<", "&lt;");
                    Line = Line.Replace(">", "&gt;");
                    Lines[i] = $"<p>{Line}</p>";
                }

                PlainText = string.Join(Environment.NewLine, Lines);
            }
            
            return PlainText;
        }

        string ExportToHtml()
        {
            SB = new();

            string BodyText;

            foreach (var Chapter in ExportContext.Story.ChapterList)
            {               
 
                SB.AppendLine($"<h1>{Chapter.Title}</h1>");
                SB.AppendLine();

                foreach (var Scene in Chapter.SceneList)
                {
                    BodyText = GetText(Scene.BodyText);
                    SB.AppendLine($"<h2>{Scene.Title}</h2>");
                    SB.AppendLine(BodyText);
                    SB.AppendLine();
                    SB.AppendLine();
                }
            }

            string HtmlText = $@"
<!DOCTYPE html>
<html>
<head>
<title>Page Title</title>
</head>
<body> 
{SB.ToString()}
</body>
</html>
";

            string FilePath = Path.Combine(ExportContext.ExportFolderPath, "Story.html");
            File.WriteAllText(FilePath, HtmlText);
            App.WaitForFileAvailable(FilePath);

            return FilePath;
        }
        void ExportToOdt(string HtmlFilePath, string TargetFormat = "odt", int TimeoutMsecs = 300000)
        {
            string SOfficePath = FindSoffice();

            var PSI = new ProcessStartInfo
            {
                FileName = SOfficePath,
                Arguments = $"--headless --convert-to \"{TargetFormat}\" --outdir \"{ExportContext.ExportFolderPath}\" \"{HtmlFilePath}\"",  
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var OfficeProcess = Process.Start(PSI) ?? throw new Exception($"Cannot convert source file {HtmlFilePath} to {TargetFormat}. Cannot start LibreOffice.");
            if (!OfficeProcess.WaitForExit(TimeoutMsecs))
            {
                try
                {
                    OfficeProcess.Kill();
                }
                catch
                {
                }

                throw new Exception($"Cannot convert source file {HtmlFilePath} to {TargetFormat}. Timeout of {TimeoutMsecs} ms exceeded.");
            }
        }


        // ● construction
        public OdtExporter(ExportContext ExportContext)
        {
            this.ExportContext = ExportContext;
        }

        // ● public
        public void Execute()
        {
            ExportContext.InPlainText = true;

            string HtmlFilePath = ExportToHtml();

            if (!CheckLibreOfficeExists(LogIfNot: false))
                return;

            ExportToOdt(HtmlFilePath);
        }
    }
}
