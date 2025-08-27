namespace StoryWriter
{
 
    public class MarkdownExporter
    {
        ExportService Service;
        StringBuilder SB;
        ProjectProxy Project;
 

        static readonly string[] NewLines = new[] { "\r\n", "\r", "\n" };

        void AppendLine(string Text) => SB.AppendLine(Text);
        void AppendHeader(string Text) => AppendLine($"# {Text}");
        void AppendHeader2(string Text) => AppendLine($"## {Text}");
        void AppendHeader3(string Text) => AppendLine($"### {Text}");
        void EmptyLine() => AppendLine("");
 
        string[] ToLines(string Text) => Text.Split(NewLines, StringSplitOptions.None);
        string ReplaceStartingBullet(string Text)
        {
            string S = Text.TrimStart();
            if (S.StartsWith('●'))
            {
                int i = Text.IndexOf('●');
                Text = Text.Remove(i, "●".Length);
                Text = Text.Insert(i, "-");
            }
            else if (S.StartsWith('•'))
            {
                int i = Text.IndexOf('•');
                Text = Text.Remove(i, "•".Length);
                Text = Text.Insert(i, "-");
            }

            return Text;
        }
        void ProcessText(string PlainText)
        {
            if (!string.IsNullOrWhiteSpace(PlainText))
            {
                string[] Lines = ToLines(PlainText);

                string Line;
                for (int i = 0; i < Lines.Length; i++)
                {
                    Line = ReplaceStartingBullet(Lines[i]);
                    AppendLine(Line);
                }
            }
            else
            {
                EmptyLine();
            }
        }

        void ProcessCapters()
        {
            AppendHeader("CHAPTERS");
            EmptyLine();
            EmptyLine();

            foreach (var Chapter in Project.ChapterList)
            {
                AppendHeader2(Chapter.ToString());
                EmptyLine();
                ProcessText(Chapter.BodyText);
            }

            EmptyLine();
            EmptyLine();
        }

        void ProcessChapterProperties()
        {
            AppendHeader("CHAPTER PROPERTIES");
            EmptyLine();
            EmptyLine();

            foreach (var Chapter in Project.ChapterList)
            {
                AppendHeader2($"{Chapter} - Synopsis");
                EmptyLine();
                ProcessText(Chapter.Synopsis);

                AppendHeader2($"{Chapter} - Concept");
                EmptyLine();
                ProcessText(Chapter.Concept);

                AppendHeader2($"{Chapter} - Outcome");
                EmptyLine();
                ProcessText(Chapter.Outcome);
            }

            EmptyLine();
            EmptyLine();
        }
        void ProcessScenes()
        {
            AppendHeader("CHAPTER SCENES");
            EmptyLine();
            EmptyLine();

            foreach (var Chapter in Project.ChapterList)
            {
                AppendHeader2($"{Chapter} - SCENES");
                EmptyLine();

                foreach (var Scene in Chapter.SceneList)
                {
                    AppendHeader3($"{Scene}");
                    EmptyLine();
                    ProcessText(Scene.BodyText);
                }
            }

            EmptyLine();
            EmptyLine();
        }
        void ProcessComponents()
        {
            AppendHeader("COMPONENTS");
            EmptyLine();
            EmptyLine();

            foreach (var Component in Project.ComponentList)
            {
                AppendHeader2(Component.Title);
                EmptyLine();
                ProcessText(Component.BodyText);
            }

            EmptyLine();
            EmptyLine();
        }


        // ● construction
        public MarkdownExporter(ExportService Service)
        {
            this.Service = Service;
        }


        // ● public
        public void Execute()
        {
            SB = new StringBuilder();

            this.Project = Service.Project;

            AppendHeader(Project.Title);
            EmptyLine();
            EmptyLine();

            ProcessComponents();
            ProcessCapters();
            ProcessChapterProperties();
            ProcessScenes();

            File.WriteAllText(Service.FilePath, SB.ToString());
        }
    }
}
