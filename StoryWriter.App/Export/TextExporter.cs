using DocumentFormat.OpenXml.Bibliography;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryWriter.Export
{
    static public class TextExporter
    {
        class ExportContext
        {
            Project Project;
            RichTextBox Editor;
            StringBuilder SB;

            void AppendLine(string Text) => SB.AppendLine(Text);
            void AppendHeader(string Text) => AppendLine($"# {Text}");
            void AppendHeader2(string Text) => AppendLine($"## {Text}");
            void AppendHeader3(string Text) => AppendLine($"### {Text}");
            void EmptyLine() => AppendLine("");
            string ToPlainText(string RtfText)
            {
                Editor.Rtf = RtfText;
                return Editor.Text;
            }
            string[] ToLines(string Text) => Text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            public string ReplaceStartingBullet(string Text)
            {
                string S = Text.TrimStart();
                if (S.StartsWith("●"))
                {
                    int i = Text.IndexOf("●");
                    Text = Text.Remove(i, "●".Length);
                    Text = Text.Insert(i, "-");
                }
                else if (S.StartsWith("•"))
                {
                    int i = Text.IndexOf("•");
                    Text = Text.Remove(i, "•".Length);
                    Text = Text.Insert(i, "-");
                }
                    
                return Text;
            }
            public void ProcessRtf(string RtfText)
            {
                string PlainText = ToPlainText(RtfText);

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
                    ProcessRtf(Chapter.BodyText);
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
                    AppendHeader2($"{Chapter.ToString()} - Synopsis");
                    EmptyLine();
                    ProcessRtf(Chapter.Synopsis);

                    AppendHeader2($"{Chapter.ToString()} - Concept");
                    EmptyLine();
                    ProcessRtf(Chapter.Concept);

                    AppendHeader2($"{Chapter.ToString()} - Outcome");
                    EmptyLine();
                    ProcessRtf(Chapter.Outcome); 
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
                    AppendHeader2($"{Chapter.ToString()} - SCENES");
                    EmptyLine();

                    foreach (var Scene in Chapter.SceneList)
                    {
                        AppendHeader3($"{Scene.ToString()}");
                        EmptyLine();
                        ProcessRtf(Scene.BodyText);
                    }
                }

                EmptyLine();
                EmptyLine();
            }
            public void ProcessComponents()
            {
                AppendHeader("COMPONENTS");
                EmptyLine();
                EmptyLine();

                foreach (Component Component in Project.FlatComponentList)
                {
                    if (!Component.IsGroup)
                    {
                        AppendHeader2(Component.Title);
                        EmptyLine();
                        ProcessRtf(Component.Notes);
                    }
                }

                EmptyLine();
                EmptyLine();
            }

            public StringBuilder Execute(Project Project)
            {
                SB = new StringBuilder();

                this.Project = Project;

                using (RichTextBox RTB = new RichTextBox())
                {
                    Editor = RTB;

                    AppendHeader(Project.Name);
                    EmptyLine();
                    EmptyLine();

                    ProcessComponents();
                    ProcessCapters();
                    ProcessChapterProperties();
                    ProcessScenes();
                }

                return SB;
            }
        }



        static public void Export(Project Project, string FilePath)
        {
            using (RichTextBox Editor = new RichTextBox())
            {
                ExportContext Context = new ();
                StringBuilder SB = Context.Execute(Project);

                File.WriteAllText(FilePath, SB.ToString());
            }
        }

    }
}
