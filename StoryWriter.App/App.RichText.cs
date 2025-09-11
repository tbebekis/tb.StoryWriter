namespace StoryWriter
{
    static public partial class App
    {
        static RichTextBox Editor;

        /// <summary>
        /// Converts RTF to plain text
        /// </summary>
        static public string ToPlainText(string RtfText)
        {
            if (Editor == null)
                Editor = new RichTextBox { DetectUrls = false };

            Editor.Clear();
            Editor.Rtf = RtfText;
            return Editor.Text;
        }
        /// <summary>
        /// Converts plain text to RTF
        /// </summary>
        static public string ToRtfText(string PlainText)
        {
            if (IsRtf(PlainText))
                return PlainText;

            if (Editor == null)
                Editor = new RichTextBox { DetectUrls = false };

            Editor.Clear();
            Editor.Text = PlainText;

            return Editor.Rtf;
        }
        /// <summary>
        /// Checks if the given string is an RTF text.
        /// </summary>
        static public bool IsRtf(string PlainText)
        {
            if (string.IsNullOrWhiteSpace(PlainText))
                return true;

            return PlainText.TrimStart().StartsWith(@"{\rtf", StringComparison.Ordinal);
        }
        /// <summary>
        /// Returns true if the given RTF text contains the given term
        /// </summary>
        static public bool RichTextContainsTerm(string RtfText, string PlainTextTerm)
        {
            if (string.IsNullOrWhiteSpace(RtfText))
                return false;
            string PlainText = ToPlainText(RtfText);
            return PlainText.Contains(PlainTextTerm, StringComparison.OrdinalIgnoreCase);
        }
    }
}
