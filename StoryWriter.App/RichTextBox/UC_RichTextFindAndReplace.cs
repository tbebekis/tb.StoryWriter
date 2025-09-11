namespace StoryWriter
{
    /// <summary>
    /// Inline Find/Replace bar for a RichTextBox, with manual layout of child controls.
    /// </summary>
    public partial class UC_RichTextFindAndReplace : UserControl
    {
        /// <summary>
        /// Gets or sets the target RichTextBox to search/replace in.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RichTextBox Editor { get; set; }

        /// <summary>
        /// Initializes a new instance of the find/replace control.
        /// </summary>
        public UC_RichTextFindAndReplace()
        {
            InitializeComponent();
            HookEvents();
             
            DoubleBuffered = true;
        }


        /// <summary>
        /// Binds UI events.
        /// </summary>
        private void HookEvents()
        {
            btnNext.Click += (s, e) => FindNext();
            //btnPrev.Click += (s, e) => FindPrev();
            btnReplace.Click += (s, e) => ReplaceOnce();
            btnReplaceAll.Click += (s, e) => ReplaceAll();
            btnClose.Click += (s, e) => HideBar();

            txtFind.KeyDown += FindBarKeyDown;
            txtReplace.KeyDown += FindBarKeyDown;
        }

        /// <summary>
        /// Shows the bar and focuses the find box.
        /// </summary>
        public void ShowBar(string initialFindText = null)
        {
            FindAndReplaceVisibleChanged?.Invoke(this, true);

            if (!string.IsNullOrEmpty(initialFindText))
                txtFind.Text = initialFindText;

            ///if (!Visible)
            ///{
            ///    Visible = true;
            ///    BringToFront();
            ///    PerformLayout();
            ///}

            txtFind.Focus();
            txtFind.SelectAll();

            FindNext();
        }

        /// <summary>
        /// Hides the bar and returns focus to the editor.
        /// </summary>
        public void HideBar()
        {
            FindAndReplaceVisibleChanged?.Invoke(this, false);
            ///Visible = false;
            if (Editor != null && Editor.CanFocus)
                Editor.Focus();
        }

        /// <summary>
        /// Toggles the bar visibility.
        /// </summary>
        public void ToggleBar()
        {
            //if (Visible) HideBar(); else ShowBar(GetSelectedOrWord());
        }

        /// <summary>
        /// Handles Enter/Esc inside text boxes.
        /// </summary>
        private void FindBarKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (sender == txtFind) 
                        FindNext(); 
                    else 
                        ReplaceOnce();
                    e.Handled = true;
                    break;
                case Keys.Escape:
                    HideBar();
                    e.Handled = true;
                    break;
                case Keys.F3:
                    if (e.Shift)
                        FindPrev();
                    else
                        FindNext();
                    e.Handled = true;
                    break;
            }

        }

        /// <summary>
        /// Finds the next occurrence; selects it. If end is reached, prompts to wrap to the beginning.
        /// </summary>
        public void FindNext()
        {
            if (Editor == null) return;
            string query = txtFind.Text ?? string.Empty;
            if (query.Length == 0) return;

            var opts = RichTextBoxFinds.None;
            if (chkMatchCase.Checked) opts |= RichTextBoxFinds.MatchCase;
            if (chkWholeWord.Checked) opts |= RichTextBoxFinds.WholeWord;

            int start = Editor.SelectionStart + Editor.SelectionLength;
            int idx = Editor.Find(query, start, opts);

            if (idx < 0)
            {
                // Μόνο αν ήμασταν "κάπου στη μέση/τέλος" ρώτα για wrap·
                // αν ήδη ήμασταν στην αρχή (start == 0), μην ρωτήσεις — απλώς "not found".
                if (start > 0)
                {
                    var res = MessageBox.Show(this,
                        @"You have reached the end of the text. 
Do you want me to continue from the beginning?",
                        "Find", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (res == DialogResult.Yes)
                        idx = Editor.Find(query, 0, opts);
                    else
                        return;
                }
            }

            if (idx >= 0)
            {
                // Το RichTextBox.Find ήδη επιλέγει το match, αλλά το κάνουμε ρητά για σιγουριά.
                Editor.Select(idx, query.Length);
                Editor.ScrollToCaret();
                Editor.Focus();
            }
            else
            {
                MessageBox.Show(this, "Text not found.", "Find",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// Finds the previous occurrence with wrap-around.
        /// </summary>
        public void FindPrev_OLD()
        {
            if (Editor == null) return;
            var query = txtFind.Text ?? string.Empty;
            if (query.Length == 0) return;

            var opts = RichTextBoxFinds.Reverse;
            if (chkMatchCase.Checked) opts |= RichTextBoxFinds.MatchCase;
            if (chkWholeWord.Checked) opts |= RichTextBoxFinds.WholeWord;

            var start = Editor.SelectionStart;
            var idx = Editor.Find(query, 0, start, opts);
            if (idx < 0)
            {
                var end = Editor.TextLength;
                idx = Editor.Find(query, 0, end, opts);
            }

            if (idx >= 0)
                Editor.ScrollToCaret();
        }

        /// <summary>
        /// Finds the previous occurrence; selects it. If beginning is reached, prompts to wrap to the end.
        /// </summary>
        public void FindPrev()
        {
            if (Editor == null) return;
            string query = txtFind.Text ?? string.Empty;
            if (query.Length == 0) return;

            var opts = RichTextBoxFinds.Reverse;
            if (chkMatchCase.Checked) opts |= RichTextBoxFinds.MatchCase;
            if (chkWholeWord.Checked) opts |= RichTextBoxFinds.WholeWord;

            int start = Editor.SelectionStart; // search before current caret
            int idx = Editor.Find(query, 0, start, opts);

            if (idx < 0)
            {
                var res = MessageBox.Show(this,
                    @"You have reached the beginning of the text.
Do you want me to continue from the end?",
                    "Find", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (res == DialogResult.Yes)
                    idx = Editor.Find(query, 0, Editor.TextLength, opts); // reverse search whole text (selects the last match)
                else
                    return;
            }

            if (idx >= 0)
            {
                Editor.Select(idx, query.Length); // ensure selection
                Editor.ScrollToCaret();
                Editor.Focus();
            }
            else
            {
                MessageBox.Show(this, "Text not found.", "Find",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        /// <summary>
        /// Replaces the current selection if it matches and finds next.
        /// </summary>
        public void ReplaceOnce()
        {
            if (Editor == null) return;
            var query = txtFind.Text ?? string.Empty;
            var repl = txtReplace.Text ?? string.Empty;
            if (query.Length == 0) return;

            if (SelectionMatches(query, chkMatchCase.Checked, chkWholeWord.Checked))
                Editor.SelectedText = repl;

            FindNext();
        }

        /// <summary>
        /// Replaces all matches in the editor.
        /// </summary>
        public void ReplaceAll()
        {
            if (Editor == null) return;
            var query = txtFind.Text ?? string.Empty;
            var repl = txtReplace.Text ?? string.Empty;
            if (query.Length == 0) return;

            var opts = RichTextBoxFinds.None;
            if (chkMatchCase.Checked) opts |= RichTextBoxFinds.MatchCase;
            if (chkWholeWord.Checked) opts |= RichTextBoxFinds.WholeWord;

            var caret = Editor.SelectionStart;
            var count = 0;
            int pos = 0;

            Editor.SelectionLength = 0;

            while (true)
            {
                var idx = Editor.Find(query, pos, opts);
                if (idx < 0) break;
                Editor.SelectedText = repl;
                pos = idx + repl.Length;
                count++;
            }

            Editor.SelectionStart = Math.Min(caret, Editor.TextLength);
            Editor.SelectionLength = 0;
            Editor.ScrollToCaret();
        }

        /// <summary>
        /// Seeds the find box with selected text or current word.
        /// </summary>
        private string GetSelectedOrWord()
        {
            if (Editor == null) return string.Empty;
            if (!string.IsNullOrEmpty(Editor.SelectedText)) return Editor.SelectedText;

            var text = Editor.Text;
            var i = Editor.SelectionStart;
            if (string.IsNullOrEmpty(text) || i < 0 || i > text.Length) return string.Empty;

            int start = i, end = i;
            while (start > 0 && !char.IsWhiteSpace(text[start - 1])) start--;
            while (end < text.Length && !char.IsWhiteSpace(text[end])) end++;
            if (end > start) return text.Substring(start, end - start);
            return string.Empty;
        }

        /// <summary>
        /// Checks if current selection equals the query under the options.
        /// </summary>
        private bool SelectionMatches(string query, bool matchCase, bool wholeWord)
        {
            var sel = Editor.SelectedText ?? string.Empty;
            if (sel.Length == 0) return false;

            if (!matchCase)
            {
                sel = sel.ToLowerInvariant();
                query = query.ToLowerInvariant();
            }

            if (wholeWord)
                return sel.Equals(query);

            return sel.Equals(query);
        }

 

        /// <summary>
        /// Ensures manual layout runs when needed.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e); 
        }

        public event EventHandler<bool> FindAndReplaceVisibleChanged;
    }
}
