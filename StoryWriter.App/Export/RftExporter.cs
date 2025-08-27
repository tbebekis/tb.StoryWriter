namespace StoryWriter
{
    public class RftExporter
    {
        ExportService Service;

        // ● construction
        public RftExporter(ExportService Service)
        {
            this.Service = Service;
        }

        // ● public
        public void Execute()
        {
            using var Editor = new RichTextBox { DetectUrls = false };
            Editor.Clear();

            for (int i = 0; i < Service.Project.ChapterList.Count; i++)
            {
                var ch = Service.Project.ChapterList[i];
                var title = string.IsNullOrWhiteSpace(ch?.Title) ? $"Chapter {i + 1}" : ch!.Title;

                // chapter title (bold)
                Editor.Select(Editor.TextLength, 0);
                Editor.SelectionFont = new Font("Arial", 15, FontStyle.Bold);
                Editor.AppendText(title + Environment.NewLine + Environment.NewLine);

                // chapter body (RTF)
                if (!string.IsNullOrWhiteSpace(ch?.BodyText))
                {
                    Editor.Select(Editor.TextLength, 0);
                    Editor.SelectedRtf = ch!.BodyText;    
                }

                // page break between chapters
                if (i < Service.Project.ChapterList.Count - 1)
                {
                    Editor.Select(Editor.TextLength, 0);
                   
                    // a tiny RTF fragment for page break
                    Editor.SelectedRtf = @"{\rtf1\ansi\pard\page\par}";
                    Editor.AppendText(Environment.NewLine);
                }
            }

            Editor.SaveFile(Service.FilePath, RichTextBoxStreamType.RichText);
            


        }
    }
}
