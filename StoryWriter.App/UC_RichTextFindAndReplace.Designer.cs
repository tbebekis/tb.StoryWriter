namespace StoryWriter
{
    partial class UC_RichTextFindAndReplace
    {
        private System.ComponentModel.IContainer components = null;
        private TextBox txtFind;
        private Button btnPrev;
        private Button btnNext;
        private CheckBox chkMatchCase;
        private CheckBox chkWholeWord;
        private TextBox txtReplace;
        private Button btnReplace;
        private Button btnReplaceAll;
        private Button btnClose;

        /// <summary>
        /// Disposes resources.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Designer initialization.
        /// </summary>
        private void InitializeComponent()
        {
            txtFind = new TextBox();
            btnPrev = new Button();
            btnNext = new Button();
            chkMatchCase = new CheckBox();
            chkWholeWord = new CheckBox();
            txtReplace = new TextBox();
            btnReplace = new Button();
            btnReplaceAll = new Button();
            btnClose = new Button();
            SuspendLayout();
            // 
            // txtFind
            // 
            txtFind.Location = new Point(5, 6);
            txtFind.Name = "txtFind";
            txtFind.Size = new Size(100, 23);
            txtFind.TabIndex = 1;
            // 
            // btnPrev
            // 
            btnPrev.AutoSize = true;
            btnPrev.Location = new Point(728, 5);
            btnPrev.Name = "btnPrev";
            btnPrev.Size = new Size(75, 25);
            btnPrev.TabIndex = 2;
            btnPrev.Text = "Prev";
            btnPrev.Visible = false;
            // 
            // btnNext
            // 
            btnNext.AutoSize = true;
            btnNext.Location = new Point(108, 5);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(75, 25);
            btnNext.TabIndex = 3;
            btnNext.Text = "Find";
            // 
            // chkMatchCase
            // 
            chkMatchCase.AutoSize = true;
            chkMatchCase.Location = new Point(191, 8);
            chkMatchCase.Name = "chkMatchCase";
            chkMatchCase.Size = new Size(86, 19);
            chkMatchCase.TabIndex = 4;
            chkMatchCase.Text = "Match case";
            // 
            // chkWholeWord
            // 
            chkWholeWord.AutoSize = true;
            chkWholeWord.Location = new Point(283, 8);
            chkWholeWord.Name = "chkWholeWord";
            chkWholeWord.Size = new Size(90, 19);
            chkWholeWord.TabIndex = 5;
            chkWholeWord.Text = "Whole word";
            // 
            // txtReplace
            // 
            txtReplace.Location = new Point(379, 6);
            txtReplace.Name = "txtReplace";
            txtReplace.Size = new Size(100, 23);
            txtReplace.TabIndex = 7;
            // 
            // btnReplace
            // 
            btnReplace.AutoSize = true;
            btnReplace.Location = new Point(485, 5);
            btnReplace.Name = "btnReplace";
            btnReplace.Size = new Size(75, 25);
            btnReplace.TabIndex = 8;
            btnReplace.Text = "Replace";
            // 
            // btnReplaceAll
            // 
            btnReplaceAll.AutoSize = true;
            btnReplaceAll.Location = new Point(566, 5);
            btnReplaceAll.Name = "btnReplaceAll";
            btnReplaceAll.Size = new Size(75, 25);
            btnReplaceAll.TabIndex = 9;
            btnReplaceAll.Text = "Replace All";
            // 
            // btnClose
            // 
            btnClose.Location = new Point(647, 5);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(32, 25);
            btnClose.TabIndex = 10;
            btnClose.Text = "✕";
            // 
            // UC_RichTextFindAndReplace
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(txtFind);
            Controls.Add(btnPrev);
            Controls.Add(btnNext);
            Controls.Add(chkMatchCase);
            Controls.Add(chkWholeWord);
            Controls.Add(txtReplace);
            Controls.Add(btnReplace);
            Controls.Add(btnReplaceAll);
            Controls.Add(btnClose);
            Name = "UC_RichTextFindAndReplace";
            Padding = new Padding(8, 6, 8, 6);
            Size = new Size(1465, 35);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
