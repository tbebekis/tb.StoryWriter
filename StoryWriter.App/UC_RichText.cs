namespace StoryWriter
{
    public partial class UC_RichText : UserControl
    {
        // ● fields
        FindReplaceForm _findReplaceForm;

        bool _bulletMode = false;       // List state (simple, per-editor)
        bool _numberMode = false;
        int _currentNumber = 0;        
        int _listIndent = 20;           // base indent for bullets (pixels)

        // ● private
        void ControlInitialize()
        {
            Editor.DetectUrls = true;

            btnBold.Click += (s, e) => ToggleBold();
            btnItalic.Click += (s, e) => ToggleItalic();
            btnUnderline.Click += (s, e) => ToggleUnderline();
            btnLink.Click += (s, e) => GoToLink();
            btnFind.Click += (s, e) => ShowFindReplaceDialog();
            btnReplace.Click += (s, e) => ShowFindReplaceDialog();
            btnSave.Click += (s, e) => SaveText();

            // Menu item clicks
            btnBullets.Click += btnBullets_Click;
            btnNumbers.Click += btnNumbers_Click;
            
            Editor.KeyDown += Editor_KeyDown; 
            Editor.MouseDown += Editor_MouseDown;

            btnBullets.CheckOnClick = true;
            btnNumbers.CheckOnClick = true;

            Ui.RunOnce((Info) => {
               SetEditorFont();
            }, 1000, null);      

        }
        void SetEditorFont()
        {
            string family = string.IsNullOrWhiteSpace(App.Settings.FontFamily) ? "Times New Roman" : App.Settings.FontFamily;
            float size = Math.Max(12, App.Settings.FontSize);
            
            //int selStart = Editor.SelectionStart;
            //int selLen = Editor.SelectionLength;

            //Editor.SelectAll();
            //Editor.SelectionFont = new Font(family, size, FontStyle.Regular);
            //Editor.Select(selStart, selLen);
            Editor.Font = new Font(family, size, FontStyle.Regular);
            Editor.Update();
        }

        // ● event handlers
        void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.B:
                        ToggleBold();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.I:
                        ToggleItalic();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.U:
                        ToggleUnderline();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.S:
                        SaveText();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.L:
                        GoToLink();
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.F:
                        ShowFindReplaceDialog(); 
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.H:
                        ShowFindReplaceDialog(); 
                        e.SuppressKeyPress = true;
                        break;
                }
            }

 

            // ● bullets and numbers
 
            // Continue numbering on Enter
            if (_numberMode && e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                // If current line contains only "N. " → stop numbering
                if (IsCurrentLineOnlyPrefix(Editor))
                {
                    RemoveCurrentLineIfOnlyPrefix(Editor);
                    StopNumbering(updateButtons: true);
                    Editor.SelectedText = Environment.NewLine; // blank line
                    return;
                }

                _currentNumber++;
                Editor.SelectedText = Environment.NewLine;
                InsertNumberPrefix(_currentNumber);
                return;
            }

            // Backspace at start-of-line while in bullet mode → stop bullets
            if (_bulletMode && e.KeyCode == Keys.Back && IsAtLineStart(Editor))
            {
                e.SuppressKeyPress = true;
                ApplyBullets(false);
                _bulletMode = false;
                btnBullets.Checked = false;
                return;
            }
        }
        void Editor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) 
                return;

            bool ctrl = (ModifierKeys & Keys.Control) == Keys.Control;
            if (!ctrl) 
                return;

            GoToLink();
        }
        void btnBullets_Click(object sender, EventArgs e)
        {
            // toggle bullets
            _bulletMode = !_bulletMode;

            // numbering is mutually exclusive
            if (_bulletMode)
            {
                StopNumbering(updateButtons: false);
                ApplyBullets(true);
                btnBullets.Checked = true;
            }
            else
            {
                ApplyBullets(false);
                btnBullets.Checked = false;
            }

            Editor.Focus();
        }
        void btnNumbers_Click(object sender, EventArgs e)
        {
            // toggle numbering
            if (_numberMode)
            {
                StopNumbering(updateButtons: true);
                return;
            }

            _numberMode = true;
            _currentNumber = 1;

            // Bullets are mutually exclusive
            if (_bulletMode)
            {
                ApplyBullets(false);
                _bulletMode = false;
                btnBullets.Checked = false;
            }

            btnNumbers.Checked = true;

            // Optional UX: start on a new line if not at line-start
            if (!IsAtLineStart(Editor))
                Editor.SelectedText = Environment.NewLine;

            InsertNumberPrefix(_currentNumber);
            Editor.Focus();
        }

        void SaveText()
        {
            if (EditorHandler != null)
                EditorHandler.SaveEditorText(this.Editor);
        }

        // ● formatting, find/replace and links
        void ToggleBold()
        {
            FontStyle NewStyle = Editor.SelectionFont.Bold? Editor.SelectionFont.Style ^ FontStyle.Bold: Editor.SelectionFont.Style | FontStyle.Bold;
            Editor.SelectionFont = new Font(Editor.SelectionFont, NewStyle);
        }
        void ToggleItalic()
        {
            FontStyle NewStyle = Editor.SelectionFont.Italic ? Editor.SelectionFont.Style ^ FontStyle.Italic : Editor.SelectionFont.Style | FontStyle.Italic;
            Editor.SelectionFont = new Font(Editor.SelectionFont, NewStyle);
        }
        void ToggleUnderline()
        {
            FontStyle NewStyle = Editor.SelectionFont.Underline ? Editor.SelectionFont.Style ^ FontStyle.Underline : Editor.SelectionFont.Style | FontStyle.Underline;
            Editor.SelectionFont = new Font(Editor.SelectionFont, NewStyle);
        }
        void GoToLink()
        {
            string Term = Editor.SelectedText;
            if (string.IsNullOrWhiteSpace(Term))
            {
                Point clientPt = Editor.PointToClient(Cursor.Position);
                int index = Editor.GetCharIndexFromPosition(clientPt);      //int index = Editor.GetCharIndexFromPosition(e.Location);

                Term = GetWordAt(Editor.Text, index);
            }

            if (string.IsNullOrWhiteSpace(Term))
                return;
           
            App.CurrentProject.OpenPageByTerm(Term);     //LogBox.AppendLine($"GoToLink(\"{Term}\")");
        }
        void ShowFindReplaceDialog()
        {
            if (_findReplaceForm == null || _findReplaceForm.IsDisposed)
            {
                _findReplaceForm = new FindReplaceForm(Editor);
                _findReplaceForm.Owner = this.FindForm();

                // Υπολογισμός θέσης: πάνω–δεξιά του RichTextBox
                Rectangle rtbRect = Editor.RectangleToScreen(Editor.ClientRectangle);
                int x = rtbRect.Right - _findReplaceForm.Width;
                int y = rtbRect.Top;

                _findReplaceForm.StartPosition = FormStartPosition.Manual;
                _findReplaceForm.Location = new Point(x, y);

                _findReplaceForm.Show(this);
            }
            else
            {
                _findReplaceForm.Activate();
            }
        }

        // ● helpers for links  
        static string GetWordAt(string text, int index)
        {
            if (index < 0 || index >= text.Length) return string.Empty;

            // Αν ο cursor είναι πάνω σε space/punct, μετακινήσου αριστερά στην πλησιέστερη λέξη
            int i = index;
            while (i > 0 && !IsWordChar(text[i])) i--;
            int end = i;
            while (end < text.Length && IsWordChar(text[end])) end++;
            int start = i;
            while (start > 0 && IsWordChar(text[start - 1])) start--;

            var len = end - start;
            if (len <= 0) return string.Empty;
            return text.Substring(start, len);
        }
        static bool IsWordChar(char c)
        {
            // Γράμματα/Ψήφια από όλες τις γλώσσες + underscore
            var cat = char.GetUnicodeCategory(c);
            return char.IsLetterOrDigit(c) || c == '_' ||
                   cat == System.Globalization.UnicodeCategory.NonSpacingMark ||
                   cat == System.Globalization.UnicodeCategory.SpacingCombiningMark;
        }

        // ● helpers for bullets and numbers
        void ApplyBullets(bool enable)
        {
            if (enable)
            {
                Editor.SelectionIndent = _listIndent;
                Editor.BulletIndent = _listIndent / 2;
                Editor.SelectionBullet = true;
            }
            else
            {
                Editor.SelectionBullet = false;
                Editor.SelectionIndent = 0;
                Editor.BulletIndent = 0;
            }
        }
        void StopNumbering(bool updateButtons)
        {
            _numberMode = false;
            _currentNumber = 0;
            if (updateButtons) btnNumbers.Checked = false;
        }
        void InsertNumberPrefix(int n)
        {
            Editor.SelectedText = $"{n}. ";
        }
        bool IsAtLineStart(RichTextBox rtb)
        {
            int lineIndex = rtb.GetLineFromCharIndex(rtb.SelectionStart);
            int lineStart = rtb.GetFirstCharIndexFromLine(lineIndex);
            return rtb.SelectionStart <= lineStart;
        }
        static string GetCurrentLineText(RichTextBox rtb, out int lineStart, out int lineLength)
        {
            int lineIndex = rtb.GetLineFromCharIndex(rtb.SelectionStart);
            lineStart = rtb.GetFirstCharIndexFromLine(lineIndex);
            int nextStart = (lineIndex + 1 < rtb.Lines.Length)
                                ? rtb.GetFirstCharIndexFromLine(lineIndex + 1)
                                : rtb.TextLength;
            lineLength = Math.Max(0, nextStart - lineStart);
            if (lineLength == 0) return string.Empty;
            return rtb.Text.Substring(lineStart, lineLength);
        }
        // "N. " and nothing else on the line
        bool IsCurrentLineOnlyPrefix(RichTextBox rtb)
        {
            string line = GetCurrentLineText(rtb, out _, out _).TrimEnd('\r', '\n');
            if (string.IsNullOrEmpty(line)) return false;

            int dotIdx = line.IndexOf('.');
            if (dotIdx <= 0) return false;

            string digits = line.Substring(0, dotIdx);
            if (!int.TryParse(digits, out _)) return false;

            string after = line.Substring(dotIdx + 1);
            return after == " ";
        }
        void RemoveCurrentLineIfOnlyPrefix(RichTextBox rtb)
        {
            string line = GetCurrentLineText(rtb, out int start, out int len).TrimEnd('\r', '\n');

            int dotIdx = line.IndexOf('.');
            if (dotIdx <= 0) return;

            string digits = line.Substring(0, dotIdx);
            if (!int.TryParse(digits, out _)) return;

            string after = line.Substring(dotIdx + 1);
            if (after != " ") return;

            // Delete the entire line (prefix-only)
            rtb.Select(start, len);
            rtb.SelectedText = string.Empty;
        }
 
        // ● overrides  
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!DesignMode)
                ControlInitialize();
        }


        // ● construction
        public UC_RichText()
        {
            InitializeComponent();            
        }
 
        // ● properties
        public RichTextBox Editor => edtRichText;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string RtfText
        {
            get => Editor.Rtf;
            set => Editor.Rtf = value;
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEditorHandler EditorHandler { get; set; }
 
    }

    /// <summary>
    /// Extensions
    /// </summary>
    static public class RichTextBoxExtensions
    {
        /// <summary>
        /// Returns the caret position in RichTextBox
        /// </summary>
        static public Point GetCaretPosition(this RichTextBox RichTextBox)
        {
            Point P = new ();
            P.Y = (RichTextBox.GetLineFromCharIndex(RichTextBox.SelectionStart));
            P.X = (RichTextBox.SelectionStart - RichTextBox.GetFirstCharIndexOfCurrentLine());
            return P;
        }
    }

}
