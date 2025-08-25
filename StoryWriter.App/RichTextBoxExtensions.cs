namespace StoryWriter
{
    static public class RichTextBoxExtensions
    {
        static bool IsWordChar(char c)
        {
            var cat = char.GetUnicodeCategory(c);
            return char.IsLetterOrDigit(c) || c == '_' ||
                   cat == System.Globalization.UnicodeCategory.NonSpacingMark ||
                   cat == System.Globalization.UnicodeCategory.SpacingCombiningMark;
        }

        static public Point GetCaretPosition(this RichTextBox Editor)
        {
            Point P = new();
            P.Y = Editor.GetLineFromCharIndex(Editor.SelectionStart);
            P.X = Editor.SelectionStart - Editor.GetFirstCharIndexOfCurrentLine();
            return P;
        }

        static public bool IsAtLineStart(this RichTextBox Editor)
        {
            int lineIndex = Editor.GetLineFromCharIndex(Editor.SelectionStart);
            int lineStart = Editor.GetFirstCharIndexFromLine(lineIndex);
            return Editor.SelectionStart == lineStart; // πιο αυστηρό & ασφαλές
        }

        static public string GetCurrentLineText(this RichTextBox Editor, out int lineStart, out int lineLength)
        {
            int lineIndex = Editor.GetLineFromCharIndex(Editor.SelectionStart);
            lineStart = Editor.GetFirstCharIndexFromLine(lineIndex);
            int nextStart = (lineIndex + 1 < Editor.Lines.Length)
                                ? Editor.GetFirstCharIndexFromLine(lineIndex + 1)
                                : Editor.TextLength;

            lineLength = Math.Max(0, nextStart - lineStart);
            if (lineLength <= 0) return string.Empty;

            // Πάρε το raw και αφαίρεσε τελικά CR/LF για να γλιτώσεις TrimEnd downstream
            string raw = Editor.Text.Substring(lineStart, lineLength);
            return raw.TrimEnd('\r', '\n');
        }

        static public string GetWordAtIndex(this RichTextBox Editor, int index)
        {
            string text = Editor.Text;
            if (index < 0 || index >= text.Length) return string.Empty;

            int i = index;
            // Αν βρισκόμαστε πάνω σε μη-λέξη, κινήσου αριστερά μέχρι να βρεις λέξη
            while (i > 0 && !IsWordChar(text[i])) i--;

            // Αν ακόμα δεν είμαστε σε char λέξης, δεν υπάρχει λέξη αριστερά
            if (!IsWordChar(text[i])) return string.Empty;

            int end = i;
            while (end < text.Length && IsWordChar(text[end])) end++;

            int start = i;
            while (start > 0 && IsWordChar(text[start - 1])) start--;

            int len = end - start;
            return len > 0 ? text.Substring(start, len) : string.Empty;
        }
    }

}
