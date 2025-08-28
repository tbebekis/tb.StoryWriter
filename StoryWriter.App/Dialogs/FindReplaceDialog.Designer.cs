namespace StoryWriter
{
    partial class FindReplaceDialog
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblFind;
        private System.Windows.Forms.Label lblReplace;
        private System.Windows.Forms.TextBox txtFind;
        private System.Windows.Forms.TextBox txtReplace;
        private System.Windows.Forms.CheckBox chkMatchCase;
        private System.Windows.Forms.CheckBox chkWholeWord;
        private System.Windows.Forms.CheckBox chkSearchUp;
        private System.Windows.Forms.Button btnFindNext;
        private System.Windows.Forms.Button btnReplace;
        private System.Windows.Forms.Button btnReplaceAll;
        private System.Windows.Forms.Button btnClose;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            lblFind = new Label();
            lblReplace = new Label();
            txtFind = new TextBox();
            txtReplace = new TextBox();
            chkMatchCase = new CheckBox();
            chkWholeWord = new CheckBox();
            chkSearchUp = new CheckBox();
            btnFindNext = new Button();
            btnReplace = new Button();
            btnReplaceAll = new Button();
            btnClose = new Button();
            SuspendLayout();
            // 
            // lblFind
            // 
            lblFind.AutoSize = true;
            lblFind.Location = new Point(34, 10);
            lblFind.Name = "lblFind";
            lblFind.Size = new Size(30, 15);
            lblFind.TabIndex = 0;
            lblFind.Text = "Find";
            // 
            // lblReplace
            // 
            lblReplace.AutoSize = true;
            lblReplace.Location = new Point(16, 38);
            lblReplace.Name = "lblReplace";
            lblReplace.Size = new Size(48, 15);
            lblReplace.TabIndex = 1;
            lblReplace.Text = "Replace";
            // 
            // txtFind
            // 
            txtFind.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtFind.Location = new Point(67, 7);
            txtFind.Name = "txtFind";
            txtFind.Size = new Size(331, 23);
            txtFind.TabIndex = 2;
            // 
            // txtReplace
            // 
            txtReplace.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtReplace.Location = new Point(67, 34);
            txtReplace.Name = "txtReplace";
            txtReplace.Size = new Size(331, 23);
            txtReplace.TabIndex = 3;
            // 
            // chkMatchCase
            // 
            chkMatchCase.AutoSize = true;
            chkMatchCase.Location = new Point(80, 61);
            chkMatchCase.Name = "chkMatchCase";
            chkMatchCase.Size = new Size(86, 19);
            chkMatchCase.TabIndex = 4;
            chkMatchCase.Text = "Match case";
            chkMatchCase.UseVisualStyleBackColor = true;
            // 
            // chkWholeWord
            // 
            chkWholeWord.AutoSize = true;
            chkWholeWord.Location = new Point(200, 61);
            chkWholeWord.Name = "chkWholeWord";
            chkWholeWord.Size = new Size(90, 19);
            chkWholeWord.TabIndex = 5;
            chkWholeWord.Text = "Whole word";
            chkWholeWord.UseVisualStyleBackColor = true;
            // 
            // chkSearchUp
            // 
            chkSearchUp.AutoSize = true;
            chkSearchUp.Location = new Point(320, 61);
            chkSearchUp.Name = "chkSearchUp";
            chkSearchUp.Size = new Size(78, 19);
            chkSearchUp.TabIndex = 6;
            chkSearchUp.Text = "Search up";
            chkSearchUp.UseVisualStyleBackColor = true;
            // 
            // btnFindNext
            // 
            btnFindNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnFindNext.Location = new Point(67, 93);
            btnFindNext.Name = "btnFindNext";
            btnFindNext.Size = new Size(75, 27);
            btnFindNext.TabIndex = 7;
            btnFindNext.Text = "Find Next";
            btnFindNext.UseVisualStyleBackColor = true;
            btnFindNext.Click += btnFindNext_Click;
            // 
            // btnReplace
            // 
            btnReplace.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnReplace.Location = new Point(144, 93);
            btnReplace.Name = "btnReplace";
            btnReplace.Size = new Size(75, 27);
            btnReplace.TabIndex = 8;
            btnReplace.Text = "Replace";
            btnReplace.UseVisualStyleBackColor = true;
            btnReplace.Click += btnReplace_Click;
            // 
            // btnReplaceAll
            // 
            btnReplaceAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnReplaceAll.Location = new Point(220, 93);
            btnReplaceAll.Name = "btnReplaceAll";
            btnReplaceAll.Size = new Size(75, 27);
            btnReplaceAll.TabIndex = 9;
            btnReplaceAll.Text = "Replace All";
            btnReplaceAll.UseVisualStyleBackColor = true;
            btnReplaceAll.Click += btnReplaceAll_Click;
            // 
            // btnClose
            // 
            btnClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClose.Location = new Point(323, 93);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(75, 27);
            btnClose.TabIndex = 10;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // FindReplaceDialog
            // 
            ClientSize = new Size(404, 123);
            Controls.Add(btnClose);
            Controls.Add(btnReplaceAll);
            Controls.Add(btnReplace);
            Controls.Add(btnFindNext);
            Controls.Add(chkSearchUp);
            Controls.Add(chkWholeWord);
            Controls.Add(chkMatchCase);
            Controls.Add(txtReplace);
            Controls.Add(txtFind);
            Controls.Add(lblReplace);
            Controls.Add(lblFind);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FindReplaceDialog";
            Text = "Find / Replace";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}