using System.Windows.Forms;

namespace StoryWriter
{
    public partial class UC_RichText : UserControl
    {
        // ● fields
        FindReplaceForm _findReplaceForm;

        
        NumericUpDown nudFontSize;
        ToolStripControlHost hostFontSize;

        NumericUpDown nudZoom;
        ToolStripControlHost hostZoom;

        RichTextBoxListHandler ListHandler;

        bool _syncingFromCode;

        CancellationTokenSource _metricsCts;
        int _lastLength = -1;
        const int WordsPerPage = 400;

        System.Windows.Forms.Timer timerWordCounter;


        // ● private
        void ControlInitialize()
        {
            Editor.DetectUrls = true;

            btnBold.Click += (s, e) => ToggleBold();
            btnItalic.Click += (s, e) => ToggleItalic();
            btnUnderline.Click += (s, e) => ToggleUnderline();
            btnResetSelectionToDefault.Click += (s, e) => ResetSelectionToDefault();
            btnLink.Click += (s, e) => GoToLink();
            btnFind.Click += (s, e) => ShowFindReplaceDialog();            
            btnSave.Click += (s, e) => SaveText();
            //btnBullets.Click += btnBullets_Click;
           // btnNumbers.Click += btnNumbers_Click;
            btnFontColor.Click += btnFontColor_Click;
            btnBackColor.Click += btnBackColor_Click;
            
            //Editor.KeyDown += Editor_KeyDown; 
            //Editor.MouseDown += Editor_MouseDown;
            //Editor.TextChanged += (s, e) => Editor.Modified = true;
            Editor.SelectionChanged += Editor_SelectionChanged;

            btnBullets.CheckOnClick = true;
            btnNumbers.CheckOnClick = true;

            ListHandler = new(Editor, btnBullets, btnNumbers);  // bullet/number list handler

            timerWordCounter = new ();
            timerWordCounter.Interval = 3 * 1000;
            timerWordCounter.Tick += async (s, e) => await UpdateWordCounter();
            timerWordCounter.Start();

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
            Editor.Font = new System.Drawing.Font(family, size, FontStyle.Regular);
            Editor.Update();
        }
        void ApplySelectionFontSize(float size)
        {
            var rtb = Editor;

            // Αν υπάρχουν mixed fonts στο selection, SelectionFont == null.
            // Τότε χρησιμοποιούμε το τρέχον Font του RichTextBox ως βάση.
            var baseFont = rtb.SelectionFont ?? rtb.Font;
            var newFont = new Font(baseFont.FontFamily, size, baseFont.Style, GraphicsUnit.Point);

            rtb.SelectionFont = newFont;
            rtb.Focus();
        }
        void AddToolBarControls()
        {
            // ● Font Size
            nudFontSize = new NumericUpDown
            {
                Minimum = 6,
                Maximum = 96,
                DecimalPlaces = 0,
                Increment = 1,
                Value = (decimal)App.Settings.FontSize, // προεπιλογή
                BorderStyle = BorderStyle.FixedSingle
            };
            nudFontSize.ValueChanged += (s, e) => ApplySelectionFontSize((float)nudFontSize.Value);

            hostFontSize = new ToolStripControlHost(nudFontSize)
            {              
                AutoSize = false,
                Width = 50
            };
 
            int Index = ToolBar.Items.IndexOf(btnBullets);

            Index--;
            ToolStripSeparator sepFontSize = new ();
            ToolBar.Items.Insert(Index, sepFontSize);

            Index++;
            ToolBar.Items.Insert(Index, new ToolStripLabel("Font Size"));

            Index++;
            ToolBar.Items.Insert(Index, hostFontSize);

            // ● Zoom Factor
            nudZoom = new NumericUpDown
            {
                Minimum = 0.5M,
                Maximum = 5.0M,
                DecimalPlaces = 2,
                Increment = 0.05M,
                Value = 1.00M,
                BorderStyle = BorderStyle.FixedSingle,

            };
            nudZoom.ValueChanged += (s, e) => Editor.ZoomFactor = (float)nudZoom.Value;

            hostZoom = new ToolStripControlHost(nudZoom)
            {
                AutoSize = false,
                Width = 50
            };

            Index = ToolBar.Items.IndexOf(btnSave);
            
            Index--;
            ToolStripSeparator sepZoom = new ();
            ToolBar.Items.Insert(Index, sepZoom);

            Index++;
            ToolBar.Items.Insert(Index, new ToolStripLabel("Zoom"));

            Index++;
            ToolBar.Items.Insert(Index, hostZoom);

 

            // συγχρονισμός όταν αλλάζει το zoom με άλλο τρόπο
            //richTextBoxNotes.ZoomFactorChanged += RichTextBoxNotes_ZoomFactorChanged;



            //ToolBar.Items.Add(new ToolStripLabel("Size"));

        }
        void ResetSelectionToDefault()
        {
            var defaultSize = App.Settings.FontSize;
            var defaultFont = new Font(Editor.Font.FontFamily, defaultSize, FontStyle.Regular);

            Editor.SelectionFont = defaultFont;
            Editor.SelectionColor = Color.Black;

            try
            {
                Editor.SelectionBackColor = Color.White;
            }
            catch
            {
            }

            Editor.Focus();
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
 
                }
            }

            // ● reset any formatting (bold, italic, underline, colors, etc.) to default on current selection, if any.
            if (e.Shift)
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        ResetSelectionToDefault();
                        e.SuppressKeyPress = true;
                        break;
                }
            }
 

            // ● bullets and numbers
            ListHandler.HandleKeyDown(e);
 
            /*
            // Continue numbering on Enter
            if (ListHandler.NumberMode && e.KeyCode == Keys.Enter)
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
            if (_bulletMode && e.KeyCode == Keys.Back && Editor.IsAtLineStart())
            {
                e.SuppressKeyPress = true;
                Editor.ApplyBullets(false);
                _bulletMode = false;
                btnBullets.Checked = false;
                return;
            }
            */
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
        void Editor_SelectionChanged(object sender, EventArgs e)
        {
            if (_syncingFromCode) return;

            var rtb = Editor;
            try
            {
                _syncingFromCode = true;

                var f = rtb.SelectionFont ?? rtb.Font;
                if (nudFontSize.Value != (decimal)f.Size)
                    nudFontSize.Value = (decimal)f.Size;

                // Optional: θα μπορούσες να ενημερώνεις και κατάσταση bold/italic/underline κουμπιών εδώ.
                // Optional: ενημέρωσε tooltip/preview χρωμάτων αν θέλεις.

                // Αν η επιλογή είναι "μικτή" (διαφορετικά styles), το SelectionFont == null

                // ● sync bold/italic/underline buttons
                var selFont = rtb.SelectionFont;

                if (selFont != null)
                {
                    btnBold.Checked = selFont.Bold;
                    btnItalic.Checked = selFont.Italic;
                    btnUnderline.Checked = selFont.Underline;

                    // Αν θες και το NumericUpDown να συγχρονίζεται
                    if (nudFontSize.Value != (decimal)selFont.Size)
                        nudFontSize.Value = (decimal)selFont.Size;
                }
                else
                {
                    // Αν η επιλογή έχει μικτά styles, μπορείς να βάλεις "indeterminate" λογική.
                    // Εδώ απλά ξετσεκάρω τα πάντα:
                    btnBold.Checked = false;
                    btnItalic.Checked = false;
                    btnUnderline.Checked = false;
                }
            }
            finally
            {
                _syncingFromCode = false;
            }
        }
 
        void btnFontColor_Click(object sender, EventArgs e)
        {
            using var cd = new ColorDialog
            {
                AllowFullOpen = true,
                FullOpen = true,
                Color = Editor.SelectionColor.IsEmpty
                ? Editor.ForeColor
                : Editor.SelectionColor
            };

            if (cd.ShowDialog(this) == DialogResult.OK)
            {
                Editor.SelectionColor = cd.Color;
                Editor.Focus();
            }
        }
        void btnBackColor_Click(object sender, EventArgs e)
        {
            using var cd = new ColorDialog
            {
                AllowFullOpen = true,
                FullOpen = true,
                Color = Editor.SelectionBackColor.IsEmpty
                ? Editor.BackColor
                : Editor.SelectionBackColor
            };

            if (cd.ShowDialog(this) == DialogResult.OK)
            {
                // Απαιτεί .NET που υποστηρίζει SelectionBackColor (σύγχρονα WinForms το έχουν)
                Editor.SelectionBackColor = cd.Color;
                Editor.Focus();
            }
        }

        async Task UpdateWordCounter()
        {
            // Πάρε το κείμενο ΜΙΑ ΦΟΡΑ στο UI thread
            string txt = Editor.Text;

            // Μικρό optimization: αν δεν άλλαξε μήκος, μην κάνεις τίποτα
            if (txt.Length == _lastLength) return;
            _lastLength = txt.Length;

            // Ακύρωσε προηγούμενη μέτρηση (αν τρέχει)
            _metricsCts?.Cancel();
            _metricsCts?.Dispose();
            _metricsCts = new CancellationTokenSource();
            var token = _metricsCts.Token;

            try
            {
                var stats = await TextMetrics.ComputeAsync(txt, WordsPerPage, token);
                if (token.IsCancellationRequested) return; // προληπτικό

                lblWords.Text = $"{stats.WordCount}";
                lblPages.Text = $"~{stats.EstimatedPages:0.0}";
            }
            catch (OperationCanceledException)
            {
                // αθόρυβα — ξεκίνησε νέα μέτρηση εν τω μεταξύ
            }
            catch (Exception ex)
            {
                // προαιρετικά log
                /// toolStripStatusLabel1.Text = "—";
                System.Diagnostics.Debug.WriteLine(ex);
            }
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

                Term = Editor.GetWordAtIndex(index);
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

            SetEditorFont();
            AddToolBarControls();
        }

        // ● public        
        public void InitializeEditor(bool NotesFormattingEnabled)
        {
            Editor.KeyDown += Editor_KeyDown;
            Editor.MouseDown += Editor_MouseDown;
            Editor.TextChanged += (s, e) => Editor.Modified = true;

            hostFontSize.Enabled = NotesFormattingEnabled;
            btnFontColor.Enabled = NotesFormattingEnabled;
            btnBackColor.Enabled = NotesFormattingEnabled;
        }
        public void SaveText()
        {
            if (EditorHandler != null)
                EditorHandler.SaveEditorText(this.Editor);
        }
 

        // ● properties
        public RichTextBoxEx Editor => edtRichText;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string RtfText
        {
            get { return Editor.Rtf; }
            set
            {               
                Editor.Rtf = value;
                Editor.Modified = false;
                Editor.ZoomFactor = 1;
                Editor.ZoomFactor = (float)nudZoom.Value;

                Editor.SelectAll();
                Editor.SetSelectionParagraphSpacingBefore();
                Editor.SetSelectionParagraphSpacingAfter();
                Editor.DeselectAll();

                Editor.Refresh();
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IEditorHandler EditorHandler { get; set; }
 
    }



}
