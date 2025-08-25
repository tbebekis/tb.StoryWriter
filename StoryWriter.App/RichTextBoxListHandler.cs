using System.Text.RegularExpressions;

namespace StoryWriter
{
    /// <summary>
    /// Handles the list features of a RichTextBox for bulleted and numbered lists
    /// </summary>
    public class RichTextBoxListHandler
    {
        RichTextBox Editor;

        ToolStripButton btnBullets;
        ToolStripButton btnNumbers;

        int fListIndent = 15;
        bool BulletMode;
        bool NumberMode;
        int CurrentNumber;

        // ● event handlers
        void btnBullets_Click(object sender, EventArgs e)
        {
            BulletMode = !BulletMode;

            if (BulletMode)
            {
                StopNumbering(updateButtons: false);
                ApplyBullets(true);
                if (btnBullets != null) btnBullets.Checked = true;
            }
            else
            {
                ApplyBullets(false);
                if (btnBullets != null) btnBullets.Checked = false;
            }
            Editor.Focus();
        }
        void btnNumbers_Click(object sender, EventArgs e)
        {
            // toggle numbering
            if (NumberMode)
            {
                StopNumbering(updateButtons: true);
                return;
            }

            // mutually exclusive with bullets
            if (BulletMode)
            {
                ApplyBullets(false);
                BulletMode = false;
                if (btnBullets != null) btnBullets.Checked = false;
            }

            // Αν υπάρχει επιλογή, κάνε batch-numbering των γραμμών και ΜΗΝ μένεις σε "live" numbering
            if (Editor.SelectionLength > 0)
            {
                ApplyNumberingToSelection();
                // μετά το batch numbering, ξε-πατάμε το κουμπί για να μην συνεχίσει αθέλητα
                StopNumbering(updateButtons: true);
                Editor.Focus();
                return;
            }

            // Κατά τα άλλα, ενεργοποίησε "live" numbering για caret-only
            NumberMode = true;
            CurrentNumber = 1;
            if (btnNumbers != null) btnNumbers.Checked = true;

            if (!Editor.IsAtLineStart())
                Editor.SelectedText = Environment.NewLine;

            InsertNumberPrefix(CurrentNumber);
            Editor.Focus();
        }
        void Editor_SelectionChanged(object sender, EventArgs e)
        {
            // Bullets: δείξε την πραγματική κατάσταση της παραγράφου όπου είναι το caret
            bool isBulleted = false;
            try { isBulleted = Editor.SelectionBullet; } catch { }
            BulletMode = isBulleted;
            if (btnBullets != null) btnBullets.Checked = isBulleted;

            // Numbers: κουμπί πατημένο ΜΟΝΟ όταν είμαστε σε live-numbering
            if (btnNumbers != null) btnNumbers.Checked = NumberMode;
        }

        // ● numbering
        /// <summary>
        /// πιο ανεκτικός έλεγχος: "^[space]*\d+\.\s*$"
        /// </summary>
        void ApplyNumberingToSelection()
        {
            int selStart = Editor.SelectionStart;
            int selEnd = selStart + Editor.SelectionLength;

            int startLine = Editor.GetLineFromCharIndex(selStart);
            int endLine = Editor.GetLineFromCharIndex(Math.Max(selEnd - 1, selStart));

            int blockStart = Editor.GetFirstCharIndexFromLine(startLine);
            int blockEnd = (endLine + 1 < Editor.Lines.Length)
                             ? Editor.GetFirstCharIndexFromLine(endLine + 1)
                             : Editor.TextLength;

            int blockLen = Math.Max(0, blockEnd - blockStart);
            if (blockLen == 0) return;

            string block = Editor.Text.Substring(blockStart, blockLen);

            // Regex που “σκανάρει” γραμμή-γραμμή ΚΑΙ κρατά τον αρχικό separator:
            //  group1 = leading whitespace (spaces/tabs)
            //  group2 = περιεχόμενο γραμμής (ό,τι μέχρι το newline)
            //  group3 = ο αρχικός separator (\r\n ή \n ή "")
            var matches = Regex.Matches(block, @"([ \t]*)(.*?)(\r\n|\n|$)", RegexOptions.Singleline);

            var sb = new StringBuilder(block.Length + matches.Count * 4);

            // Μέτρα πόσες γραμμές θα αριθμηθούν (μη κενές μετά το trimming)
            int linesToNumber = 0;
            foreach (Match m in matches)
            {
                string content = m.Groups[2].Value;
                if (!string.IsNullOrWhiteSpace(content))
                    linesToNumber++;
            }
            int digits = Math.Max(1, (int)Math.Floor(Math.Log10(Math.Max(1, linesToNumber))) + 1);

            int n = 1;
            foreach (Match m in matches)
            {
                string leading = m.Groups[1].Value;  // π.χ. indentation της γραμμής
                string content = m.Groups[2].Value;  // το κείμενο της γραμμής (χωρίς το \r\n/\n)
                string sep = m.Groups[3].Value;  // ο αρχικός separator (μπορεί να είναι "" στην τελευταία)

                if (string.IsNullOrWhiteSpace(content))
                {
                    // Κενή γραμμή: διατήρησε τον ΑΡΧΙΚΟ separator — δεν προσθέτουμε τίποτα έξτρα
                    sb.Append(sep);
                    continue;
                }

                // Μη κενή γραμμή → αριθμημένη με σεβασμό στο leading whitespace
                sb.Append(leading);
                sb.Append(n);
                sb.Append(". ");
                sb.Append(content);
                sb.Append(sep);

                n++;
            }

            // Αντικατάσταση ολόκληρου του block με το νέο περιεχόμενο
            Editor.Select(blockStart, blockLen);
            Editor.SelectedText = sb.ToString();

            // Εφάρμοσε paragraph format (ListIndent + hanging indent βάσει digits)
            Editor.Select(blockStart, sb.Length);
            ApplyNumberingParagraphFormatForDigits(digits);

            // caret στο τέλος του block
            Editor.SelectionStart = blockStart + sb.Length;
            Editor.SelectionLength = 0;
        }
        bool IsCurrentLineOnlyPrefix()
        {
            string line = Editor.GetCurrentLineText(out _, out _);
            if (string.IsNullOrEmpty(line)) return false;

            int dotIdx = line.IndexOf('.');
            if (dotIdx <= 0) return false;

            string digits = line.Substring(0, dotIdx).Trim();
            if (!int.TryParse(digits, out _)) return false;

            string after = line.Substring(dotIdx + 1); // ό,τι υπάρχει μετά την τελεία
            return string.IsNullOrWhiteSpace(after);   // ← χαλαρός έλεγχος
        }
        void RemoveCurrentLineIfOnlyPrefix()
        {
            // Σβήσε το περιεχόμενο της γραμμής αλλά κράτα ένα newline για να μην κολλήσουν οι γραμμές
            //string raw = Editor.GetCurrentLineText(out int start, out int len);

            // Το GetCurrentLineText() σου δίνει χωρίς CR/LF, οπότε πρέπει να περιλάβουμε το newline με προσοχή.
            // Υπολόγισε ξανά μέχρι το τέλος γραμμής στο raw + CRLF (αν υπάρχει).
            int lineIndex = Editor.GetLineFromCharIndex(Editor.SelectionStart);
            int lineStart = Editor.GetFirstCharIndexFromLine(lineIndex);
            int nextStart = (lineIndex + 1 < Editor.Lines.Length)
                                ? Editor.GetFirstCharIndexFromLine(lineIndex + 1)
                                : Editor.TextLength;

            int fullLen = Math.Max(0, nextStart - lineStart); // περιλαμβάνει CR/LF αν υπάρχει άλλη γραμμή
            Editor.Select(lineStart, fullLen);
            Editor.SelectedText = Environment.NewLine; // άφησε κενή γραμμή αντί για πλήρη διαγραφή
            Editor.SelectionStart = lineStart; // θέση caret στην αρχή της κενής
        }
        void StopNumbering(bool updateButtons)
        {
            NumberMode = false;
            CurrentNumber = 0;
            if (updateButtons && btnNumbers != null)
                btnNumbers.Checked = false;

            // ΝΕΟ: επαναφορά paragraph format
            ResetNumberingParagraphFormat();
        }
        void InsertNumberPrefix(int n)
        {
            // σεβασμός υπάρχοντος indentation της γραμμής (spaces/tabs)
            int lineStart = Editor.GetFirstCharIndexOfCurrentLine();
            int i = lineStart;
            while (i < Editor.TextLength)
            {
                char ch = Editor.Text[i];
                if (ch != ' ' && ch != '\t') break;
                i++;
            }
            if (Editor.SelectionStart < i)
            {
                Editor.SelectionStart = i;
                Editor.SelectionLength = 0;
            }

            // ΝΕΟ: paragraph format σύμφωνα με ListIndent + πλάτος prefix
            int digits = Math.Max(1, (int)Math.Floor(Math.Log10(Math.Max(1, n))) + 1);
            ApplyNumberingParagraphFormatForDigits(digits);

            Editor.SelectedText = $"{n}. ";
        }
        /// <summary>
        /// υπολογίζει σε pixels το πλάτος ενός prefix (π.χ. "99. ")
        /// </summary>
        int MeasurePrefixWidthPx(string prefix)
        {
            // TextRenderer δίνει πλάτος σε pixels με το τρέχον font του Editor
            var sz = TextRenderer.MeasureText(prefix, Editor.Font);
            // μικρό safety margin
            return Math.Max(0, sz.Width - 6);
        }
        /// <summary>
        /// ορίζει paragraph format για αριθμημένες παραγράφους
        /// </summary>
        /// <param name="digits"></param>
        void ApplyNumberingParagraphFormatForDigits(int digits)
        {
            // παράδειγμα χειρότερης περίπτωσης για σταθερότητα στο alignment
            string sample = new string('9', Math.Max(1, digits)) + ". ";
            int hanging = MeasurePrefixWidthPx(sample);

            Editor.SelectionIndent = ListIndent;            // 1η γραμμή ξεκινά στο ListIndent
            Editor.SelectionHangingIndent = hanging;        // οι επόμενες πάνε ListIndent + hanging
        }
        /// <summary>
        /// καθαρίζει paragraph format όταν σταματά η αρίθμηση
        /// </summary>
        void ResetNumberingParagraphFormat()
        {
            Editor.SelectionHangingIndent = 0;
            Editor.SelectionIndent = 0;
        }
 
        // ● bullets
        /// <summary>
        /// Applies or removes bullet formatting
        /// </summary>
        void ApplyBullets(bool Value)
        {
            if (Value)
            {
                Editor.SelectionIndent = ListIndent;
                Editor.BulletIndent = Math.Max(0, ListIndent / 2);
                Editor.SelectionBullet = true;
            }
            else
            {
                Editor.SelectionBullet = false;
                Editor.SelectionIndent = 0;
                Editor.BulletIndent = 0;
            }
        }
 
        // ● construction
        /// <summary>
        /// Constructor
        /// </summary>
        public RichTextBoxListHandler(RichTextBox Editor, ToolStripButton btnBullets, ToolStripButton btnNumbers)
        {
            this.Editor = Editor;
            this.btnBullets = btnBullets;
            this.btnNumbers = btnNumbers;

            if (btnBullets != null) btnBullets.Click += btnBullets_Click;
            if (btnNumbers != null) btnNumbers.Click += btnNumbers_Click;

            this.Editor.SelectionChanged += Editor_SelectionChanged;
        }

        // ● public
        /// <summary>
        /// Handles the KeyDown event
        /// </summary>
        public void HandleKeyDown(KeyEventArgs e)
        {
            // Αρίθμηση: Enter συνεχίζει ή σταματάει αν είναι "prefix-only"
            if (NumberMode && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (IsCurrentLineOnlyPrefix())
                {
                    RemoveCurrentLineIfOnlyPrefix();
                    StopNumbering(updateButtons: true);
                    return;
                }

                CurrentNumber++;
                Editor.SelectedText = Environment.NewLine;
                InsertNumberPrefix(CurrentNumber);
                return;
            }

            // Backspace στην αρχή γραμμής: βγες από bullets
            if (BulletMode && e.KeyCode == Keys.Back && Editor.IsAtLineStart())
            {
                e.SuppressKeyPress = true;
                ApplyBullets(false);
                BulletMode = false;
                if (btnBullets != null) btnBullets.Checked = false;
                return;
            }

            // Προαιρετικό quality-of-life:
            // Αν είσαι σε NumberMode και πατήσεις Backspace στην αρχή γραμμής με μόνο "N. ",
            // σταμάτα την αρίθμηση και καθάρισε τη γραμμή.
            if (NumberMode && e.KeyCode == Keys.Back && Editor.IsAtLineStart())
            {
                if (IsCurrentLineOnlyPrefix())
                {
                    e.SuppressKeyPress = true;
                    RemoveCurrentLineIfOnlyPrefix();
                    StopNumbering(updateButtons: true);
                    return;
                }
            }
        }

        // ● properties
        /// <summary>
        /// The identation a list may have from the left side, in pixels
        /// </summary>
        public int ListIndent
        {
            get => Math.Max(fListIndent, 5);
            set => fListIndent = value;
        }
    }

}
