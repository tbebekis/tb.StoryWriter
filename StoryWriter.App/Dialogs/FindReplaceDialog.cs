namespace StoryWriter
{
    public partial class FindReplaceDialog : Form
    {
        private readonly RichTextBox _rtb;

        public FindReplaceDialog(RichTextBox rtb)
        {
            InitializeComponent();
            _rtb = rtb;
            this.CancelButton = btnClose;    

            string TextToFind = rtb.SelectedText;
            if (!string.IsNullOrWhiteSpace(TextToFind)) 
                txtFind.Text = TextToFind;
        }

        private void btnFindNext_Click(object sender, EventArgs e)
        {
            var hit = RichTextBoxFindReplace.FindNext(
                _rtb,
                txtFind.Text,
                matchCase: chkMatchCase.Checked,
                wholeWord: chkWholeWord.Checked,
                searchUp: chkSearchUp.Checked);

            if (hit < 0)
                System.Media.SystemSounds.Beep.Play();
            else
                _rtb.Focus();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (_rtb.SelectionLength == 0 || !IsSelectionEqualToFind())
            {
                btnFindNext.PerformClick();
                return;
            }

            if (RichTextBoxFindReplace.ReplaceSelection(_rtb, txtReplace.Text))
            {
                btnFindNext.PerformClick();
            }
        }

        private bool IsSelectionEqualToFind()
        {
            if (string.IsNullOrEmpty(txtFind.Text) || _rtb.SelectionLength == 0) return false;

            var sel = _rtb.SelectedText;
            if (!chkMatchCase.Checked)
            {
                return sel.Equals(txtFind.Text, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                return sel.Equals(txtFind.Text, StringComparison.Ordinal);
            }
        }

        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            int n = RichTextBoxFindReplace.ReplaceAll(
                _rtb,
                txtFind.Text,
                txtReplace.Text,
                matchCase: chkMatchCase.Checked,
                wholeWord: chkWholeWord.Checked);

            MessageBox.Show($"Replaced {n} occurrence(s).", "Replace All",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            _rtb.Focus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
