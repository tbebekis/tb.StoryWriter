namespace StoryWriter
{
    public class PlainTextExporter
    {
        ExportService Service;
        StringBuilder SB;
        ProjectProxy Project;
        string ExportFolderPath;


        static readonly string[] NewLines = new[] { "\r\n", "\r", "\n" };

        ///void AppendLine(string Text) => SB.AppendLine(Text);
        ///void AppendHeader(string Text) => AppendLine($"# {Text}");
        ///void AppendHeader2(string Text) => AppendLine($"## {Text}");
        ///void AppendHeader3(string Text) => AppendLine($"### {Text}");
        ///void EmptyLine() => AppendLine("");

        string[] ToLines(string Text) => Text.Split(NewLines, StringSplitOptions.None);
        
        string GetText(string PlainText)
        {
            if (!string.IsNullOrWhiteSpace(PlainText))
            {
                string[] Lines = ToLines(PlainText);
                PlainText = string.Join(Environment.NewLine, Lines);  
            }

            return PlainText;
        }
        

        public void CreateExportFolder()
        {
            string DT = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            ExportFolderPath = Path.Combine(Service.ExportFolder, $"Text {DT}");

            if (!Directory.Exists(ExportFolderPath))
                Directory.CreateDirectory(ExportFolderPath);
        }
        public void ProcessComponents()
        {
            SB.Clear();

            string FileName;
            string FilePath;
            string Title;
            string BodyText;

            StringBuilder sbText = new StringBuilder();

            foreach (var Component in Project.ComponentList)
            {
                Title = Component.Title;
                BodyText = GetText(Component.BodyText);

                sbText.Clear();
                sbText.AppendLine($"● COMPONENT: {Title}");
                sbText.AppendLine($"Tags: {Component.GetTagsAsLine()}");
                sbText.AppendLine();
                sbText.AppendLine(BodyText);

                FileName = App.NormalizeFileName(Title);
                FilePath = Path.Combine(ExportFolderPath, $"Component - {FileName}.txt");

                File.WriteAllText(FilePath, sbText.ToString());
                App.WaitForFileAvailable(FilePath);

                SB.AppendLine(sbText.ToString());
                SB.AppendLine();
                SB.AppendLine();
            }

            FileName = $"Components for {Project.Title}.txt";
            FilePath = Path.Combine(ExportFolderPath, FileName);
            File.WriteAllText(FilePath, SB.ToString());
            App.WaitForFileAvailable(FilePath);
        }
        public void ProcessCapters()
        {
            SB.Clear();

            string FileName;
            string FilePath;
            string Title;
            string BodyText;

            StringBuilder sbText = new StringBuilder();
            int Counter = 0;

            foreach (var Chapter in Project.ChapterList)
            {
                Counter++;                

                Title = Chapter.Title;
                BodyText = GetText(Chapter.BodyText);

                sbText.Clear();
                sbText.AppendLine($"● CHAPTER {Counter}: {Title}");
                sbText.AppendLine();
                sbText.AppendLine(BodyText);

                FileName = App.NormalizeFileName(Title);
                FilePath = Path.Combine(ExportFolderPath, $"Chapter {Counter} - {FileName}.txt");

                File.WriteAllText(FilePath, sbText.ToString());
                App.WaitForFileAvailable(FilePath);

                SB.AppendLine(sbText.ToString());
                SB.AppendLine();
                SB.AppendLine();
            }

            FileName = $"Book {Project.Title}.txt";
            FilePath = Path.Combine(ExportFolderPath, FileName);
            File.WriteAllText(FilePath, SB.ToString());
            App.WaitForFileAvailable(FilePath);
        }
 


        // ● construction
        public PlainTextExporter(ExportService Service)
        {
            this.Service = Service;
        }

        // ● public
        public void Execute()
        {
            this.Project = Service.Project;
            this.SB = new StringBuilder();

            CreateExportFolder();

            ProcessComponents();
            ProcessCapters();

            Service.ExportFolder = ExportFolderPath;


        }
    }
}
