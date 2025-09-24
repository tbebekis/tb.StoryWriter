
namespace StoryWriter
{
    public class PlainTextExporter
    {
        ExportContext ExportContext;
        StringBuilder SB;        

        static readonly string[] NewLines = new[] { "\r\n", "\r", "\n" };
        static string[] ToLines(string Text) => Text.Split(NewLines, StringSplitOptions.None);

        string GetText(string PlainText)
        {
            if (!string.IsNullOrWhiteSpace(PlainText))
            {
                string[] Lines = ToLines(PlainText);
                PlainText = string.Join(Environment.NewLine, Lines);
            }

            return PlainText;
        }

        void ProcessComponents() 
        {
            SB.Clear();
 
            string BodyText; 

            foreach (var Component in ExportContext.Story.ComponentList)
            { 
                BodyText = GetText(Component.BodyText);
               
                SB.AppendLine($"● COMPONENT: {Component.Title}");
                SB.AppendLine($"Description: {Component.Description}");
                SB.AppendLine($"Type: {Component.TypeName}");
                SB.AppendLine($"Tags: {Component.GetTagsAsLine()}");
                SB.AppendLine();
                SB.AppendLine(BodyText);
                SB.AppendLine();
                SB.AppendLine();
            }

            string FilePath = Path.Combine(ExportContext.ExportFolderPath, "Components.txt");
            File.WriteAllText(FilePath, SB.ToString());
            App.WaitForFileAvailable(FilePath);
        }
        void ProcessCapters() 
        {
            SB.Clear();

            string BodyText; 

            foreach (var Chapter in ExportContext.Story.ChapterList)
            { 
                SB.AppendLine($"● CHAPTER: {Chapter.Title}");
                SB.AppendLine();

                foreach (var Scene in Chapter.SceneList)
                {
                    BodyText = GetText(Scene.BodyText);
                    SB.AppendLine($"● SCENE: {Scene.Title}");
                    SB.AppendLine(BodyText);
                    SB.AppendLine();
                    SB.AppendLine();
                } 
            }

            string FilePath = Path.Combine(ExportContext.ExportFolderPath, "Story.txt");
            File.WriteAllText(FilePath, SB.ToString());
            App.WaitForFileAvailable(FilePath);
        }
        void ProcessSynopsis()
        {
            SB.Clear();


            string SynopsisText;

            foreach (var Chapter in ExportContext.Story.ChapterList)
            {
                SB.AppendLine($"● CHAPTER: {Chapter.Title}");
                SB.AppendLine();

                foreach (var Scene in Chapter.SceneList)
                {
                    SynopsisText = GetText(Scene.Synopsis).Trim();
                    SB.AppendLine($"● SCENE: {Scene.Title}");
                    SB.AppendLine(SynopsisText);
                    SB.AppendLine();
                    SB.AppendLine();
                }
            }

            string FilePath = Path.Combine(ExportContext.ExportFolderPath, "Synopsis.txt");
            File.WriteAllText(FilePath, SB.ToString());
            App.WaitForFileAvailable(FilePath);
        }
        // ● construction
        public PlainTextExporter(ExportContext ExportContext)
        {
            this.ExportContext = ExportContext;
        }

        // ● public
        public void Execute()
        {
            ExportContext.InPlainText = true;

            SB = new();
            ProcessComponents();
            ProcessCapters();
            ProcessSynopsis();

        }
    }

}
