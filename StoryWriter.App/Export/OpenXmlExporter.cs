namespace StoryWriter
{
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Wordprocessing;
    using Google.Protobuf;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Text;
    using static System.Net.Mime.MediaTypeNames;
    using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;


    public enum ChapterExportMode
    {
        AltChunkRtf,   // Διατήρηση RTF formatting (Word θα το “λύσει” στο άνοιγμα)
        PlainText      // Χωρίς formatting (φορητό σε Word/Libre)
    }

    public sealed class DocxExportOptions
    {
        public bool IncludeToc { get; set; } = true;
        public bool PageBreakBetweenChapters { get; set; } = true;
        public ChapterExportMode Mode { get; set; } = ChapterExportMode.AltChunkRtf;
        public Func<int, Chapter, string> ChapterTitleResolver { get; set; } =
            (i, ch) => string.IsNullOrWhiteSpace(ch?.Name) ? $"Chapter {i + 1}" : ch!.Name;
    }

    static public class OpenXmlExporter
    {
        static readonly char[] DoubleNewlineChars = new[] { '\r', '\n' };

        public static void ExportProjectToDocx(Project project, string filePath, DocxExportOptions options = null)
        {
            ArgumentNullException.ThrowIfNull(project);
            options ??= new DocxExportOptions();

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

            using var doc = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document);
            var main = doc.AddMainDocumentPart();
            main.Document = new DocumentFormat.OpenXml.Wordprocessing.Document(new DocumentFormat.OpenXml.Wordprocessing.Body());

            EnsureBasicStyles(main);
            DocumentFormat.OpenXml.Wordprocessing.Body body = main.Document.Body!;

            // Τίτλος έργου
            if (!string.IsNullOrWhiteSpace(project.Name))
                body.Append(Heading("Title", project.Name));

            // TOC placeholder (ο χρήστης κάνει Update Field στο Word)
            if (options.IncludeToc)
                InsertToc(body);

            // Κεφάλαια
            for (int i = 0; i < project.ChapterList.Count; i++)
            {
                var ch = project.ChapterList[i];

                if (i > 0 && options.PageBreakBetweenChapters)
                    body.Append(PageBreak());

                var chapterTitle = options.ChapterTitleResolver(i, ch);
                body.Append(Heading("Heading1", chapterTitle));

                if (!string.IsNullOrWhiteSpace(ch?.BodyText))
                {
                    if (options.Mode == ChapterExportMode.AltChunkRtf)
                    {
                        // === AltChunk RTF ===
                        var afPart = main.AddAlternativeFormatImportPart(AlternativeFormatImportPartType.Rtf);
                        using (var s = afPart.GetStream(FileMode.Create, FileAccess.Write))
                        using (var w = new StreamWriter(s, new UTF8Encoding(false)))
                            w.Write(ch!.BodyText);

                        var relId = main.GetIdOfPart(afPart);
                        body.Append(new AltChunk { Id = relId });
                    }
                    else
                    {
                        // === Plain Text === (χωρίς rich formatting)
                        AppendPlainText(body, ch!.BodyText);
                    }
                }
            }

            main.Document.Save();
        }

        // ---------- Helpers ----------

        private static Paragraph Heading(string styleId, string text) =>
            new (
                new ParagraphProperties(new ParagraphStyleId { Val = styleId }),
                new Run(new DocumentFormat.OpenXml.Wordprocessing.Text(text) { Space = SpaceProcessingModeValues.Preserve })
            );

        private static Paragraph PageBreak() =>
            new (new Run(new Break { Type = BreakValues.Page }));

        private static void InsertToc(DocumentFormat.OpenXml.Wordprocessing.Body body)
        {
            body.Append(Heading("Heading2", "Table of Contents"));

            // Field: TOC για Heading1–3, με hyperlinks
            var p = new Paragraph();
            p.Append(
                new Run(new FieldChar { FieldCharType = FieldCharValues.Begin }),
                new Run(new FieldCode(" TOC \\o \"1-3\" \\h \\z \\u ")),
                new Run(new FieldChar { FieldCharType = FieldCharValues.Separate }),
                new Run(new DocumentFormat.OpenXml.Wordprocessing.Text("Right-click → Update Field in Word…")),
                new Run(new FieldChar { FieldCharType = FieldCharValues.End })
            );
            body.Append(p);
        }

        private static void EnsureBasicStyles(MainDocumentPart main)
        {
            var stylesPart = main.StyleDefinitionsPart ?? main.AddNewPart<StyleDefinitionsPart>();
            stylesPart.Styles ??= new Styles();

            // Title
            if (stylesPart.Styles.Elements<Style>().FirstOrDefault(s => s.StyleId == "Title") == null)
                stylesPart.Styles.Append(CreateStyle("Title", "Title", outlineLevel: null, bold: true, fontSizeHalfPoints: 48));

            // Heading1
            if (stylesPart.Styles.Elements<Style>().FirstOrDefault(s => s.StyleId == "Heading1") == null)
                stylesPart.Styles.Append(CreateStyle("Heading1", "heading 1", outlineLevel: 0, bold: true, fontSizeHalfPoints: 32));

            // Heading2 (για TOC heading)
            if (stylesPart.Styles.Elements<Style>().FirstOrDefault(s => s.StyleId == "Heading2") == null)
                stylesPart.Styles.Append(CreateStyle("Heading2", "heading 2", outlineLevel: 1, bold: true, fontSizeHalfPoints: 24));
        }

        private static Style CreateStyle(string id, string name, int? outlineLevel, bool bold, int fontSizeHalfPoints)
        {
            var s = new Style
            {
                Type = StyleValues.Paragraph,
                StyleId = id,
                CustomStyle = OnOffValue.FromBoolean(true)
            };
            s.Append(new StyleName { Val = name });
            s.Append(new BasedOn { Val = "Normal" });
            s.Append(new UIPriority { Val = 9 });
            s.Append(new PrimaryStyle());

            var pPr = new StyleParagraphProperties();
            if (outlineLevel.HasValue)
                pPr.Append(new OutlineLevel { Val = outlineLevel.Value });

            var rPr = new StyleRunProperties();
            if (bold) rPr.Append(new Bold());
            rPr.Append(new FontSize { Val = fontSizeHalfPoints.ToString() });

            s.Append(pPr);
            s.Append(rPr);
            return s;
        }

        /// <summary>
        /// Μετατρέπει το RTF σε απλό κείμενο (χάνοντας formatting) και το εισάγει ως παραγράφους.
        /// Χρησιμοποιεί απλό split σε blocks με διπλά newline.
        /// </summary>
        private static void AppendPlainText(DocumentFormat.OpenXml.Wordprocessing.Body body, string rtf)
        {
            // Βγάζουμε το plain text από RTF με έναν πολύ απλό τρόπο:
            // - Αν έχεις WinForms reference, μπορείς να το κάνεις και με RichTextBox για καλύτερη ακρίβεια.
            string plain = RtfToPlainTextNaive(rtf);

            var blocks = plain.Replace("\r\n", "\n").Split(DoubleNewlineChars, StringSplitOptions.None);
            foreach (var block in blocks)
            {
                var lines = block.Split('\n');
                var p = new Paragraph();
                var run = new Run();
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i > 0) run.Append(new Break());
                    run.Append(new DocumentFormat.OpenXml.Wordprocessing.Text(lines[i]) { Space = SpaceProcessingModeValues.Preserve });
                }
                p.Append(run);
                body.Append(p);
            }
        }

        // Πολύ απλός απο-RTF-οποιητής. Για καλύτερο αποτέλεσμα, αν μπορείς να κάνεις reference WinForms:
        //   using(var rtb=new System.Windows.Forms.RichTextBox()){ rtb.Rtf=rtf; return rtb.Text; }
        private static string RtfToPlainTextNaive(string rtf)
        {
            if (string.IsNullOrEmpty(rtf)) return string.Empty;

            // 1) πετάμε RTF control words/ groups όσο πιο ήπια γίνεται
            var sb = new StringBuilder(rtf.Length);
            bool inCtrl = false;
            int braceDepth = 0;
            for (int i = 0; i < rtf.Length; i++)
            {
                char c = rtf[i];
                if (c == '{') { braceDepth++; inCtrl = false; continue; }
                if (c == '}') { braceDepth = Math.Max(0, braceDepth - 1); inCtrl = false; continue; }
                if (c == '\\') { inCtrl = true; continue; }
                if (inCtrl)
                {
                    // προσπέρασε control word/char μέχρι να βγούμε σε space ή μη-αλφαριθμ.
                    while (i < rtf.Length && !char.IsWhiteSpace(rtf[i]) && rtf[i] != '\\' && rtf[i] != '{' && rtf[i] != '}')
                        i++;
                    inCtrl = false;
                    continue;
                }
                sb.Append(c);
            }

            // 2) καθάρισμα διπλών κενών
            var text = sb.ToString();
            return text.Replace("\r\n", "\n").Replace("\r", "\n");
        }
    }
}

