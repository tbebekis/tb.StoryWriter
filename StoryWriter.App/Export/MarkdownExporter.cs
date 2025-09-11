namespace StoryWriter
{
    public class MarkdownExporter
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
                string Line;
                bool LastStartsWithDash = false;
                bool StartsWithDash = false;
                for (int i = 0; i < Lines.Length; i++)
                {
                    Line = Lines[i].TrimStart();

                    StartsWithDash = Line.StartsWith("-");
                    if (StartsWithDash)
                        Line = Line.Replace("-", "—");

                    Lines[i] = Line + Environment.NewLine;

                    LastStartsWithDash = StartsWithDash;
   
                }

                PlainText = string.Join(Environment.NewLine, Lines);

                // —
            }

            return PlainText;
        }

        // ● construction
        public MarkdownExporter(ExportContext ExportContext)
        {
            this.ExportContext = ExportContext;
        }

        // ● public
        public void Execute()
        {
            ExportContext.InPlainText = true;        

            SB = new();

            string BodyText;

            foreach (var Chapter in ExportContext.Story.ChapterList)
            {
                SB.AppendLine($"# {Chapter.Title}");
                SB.AppendLine();

                foreach (var Scene in Chapter.SceneList)
                {
                    BodyText = GetText(Scene.BodyText);
                    SB.AppendLine($"## {Scene.Title}");
                    SB.AppendLine(BodyText);
                    SB.AppendLine();
                    SB.AppendLine();
                }
            }

            string FilePath = Path.Combine(ExportContext.ExportFolderPath, "Story.md");
            File.WriteAllText(FilePath, SB.ToString());
            App.WaitForFileAvailable(FilePath);

 
        }
    }
}
