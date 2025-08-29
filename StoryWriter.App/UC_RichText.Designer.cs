namespace StoryWriter
{
    partial class UC_RichText
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            StatuBar = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            lblWords = new ToolStripStatusLabel();
            toolStripStatusLabel2 = new ToolStripStatusLabel();
            lblPages = new ToolStripStatusLabel();
            edtRichText = new RichTextBoxEx();
            pnlTop = new Panel();
            pnlFindAndReplace = new UC_RichTextFindAndReplace();
            ToolBar = new ToolStrip();
            btnBold = new ToolStripButton();
            btnItalic = new ToolStripButton();
            btnUnderline = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnFontColor = new ToolStripButton();
            btnBackColor = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnBullets = new ToolStripButton();
            btnNumbers = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            btnFind = new ToolStripButton();
            toolStripSeparator5 = new ToolStripSeparator();
            btnResetSelectionToDefault = new ToolStripButton();
            btnSearchForTerm = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            btnSave = new ToolStripButton();
            lblTitle = new ToolStripLabel();
            StatuBar.SuspendLayout();
            pnlTop.SuspendLayout();
            ToolBar.SuspendLayout();
            SuspendLayout();
            // 
            // StatuBar
            // 
            StatuBar.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1, lblWords, toolStripStatusLabel2, lblPages });
            StatuBar.Location = new Point(0, 546);
            StatuBar.Name = "StatuBar";
            StatuBar.Size = new Size(749, 22);
            StatuBar.TabIndex = 2;
            StatuBar.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(47, 17);
            toolStripStatusLabel1.Text = "Words: ";
            // 
            // lblWords
            // 
            lblWords.AutoSize = false;
            lblWords.Name = "lblWords";
            lblWords.Size = new Size(55, 17);
            lblWords.Text = "0";
            lblWords.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            toolStripStatusLabel2.Size = new Size(47, 17);
            toolStripStatusLabel2.Text = " Pages: ";
            // 
            // lblPages
            // 
            lblPages.AutoSize = false;
            lblPages.Name = "lblPages";
            lblPages.Size = new Size(55, 17);
            lblPages.Text = "0";
            lblPages.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // edtRichText
            // 
            edtRichText.Dock = DockStyle.Fill;
            edtRichText.Location = new Point(0, 68);
            edtRichText.Name = "edtRichText";
            edtRichText.Size = new Size(749, 478);
            edtRichText.TabIndex = 3;
            edtRichText.Text = "";
            // 
            // pnlTop
            // 
            pnlTop.Controls.Add(pnlFindAndReplace);
            pnlTop.Controls.Add(ToolBar);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(749, 68);
            pnlTop.TabIndex = 5;
            // 
            // pnlFindAndReplace
            // 
            pnlFindAndReplace.Dock = DockStyle.Top;
            pnlFindAndReplace.Location = new Point(0, 31);
            pnlFindAndReplace.Name = "pnlFindAndReplace";
            pnlFindAndReplace.Padding = new Padding(8, 6, 8, 6);
            pnlFindAndReplace.Size = new Size(749, 35);
            pnlFindAndReplace.TabIndex = 6;
            // 
            // ToolBar
            // 
            ToolBar.ImageScalingSize = new Size(24, 24);
            ToolBar.Items.AddRange(new ToolStripItem[] { btnBold, btnItalic, btnUnderline, toolStripSeparator1, btnFontColor, btnBackColor, toolStripSeparator2, btnBullets, btnNumbers, toolStripSeparator3, btnFind, toolStripSeparator5, btnResetSelectionToDefault, btnSearchForTerm, toolStripSeparator4, btnSave, lblTitle });
            ToolBar.Location = new Point(0, 0);
            ToolBar.Name = "ToolBar";
            ToolBar.Size = new Size(749, 31);
            ToolBar.TabIndex = 1;
            ToolBar.Text = "toolStrip1";
            // 
            // btnBold
            // 
            btnBold.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnBold.Image = Properties.Resources.text_bold;
            btnBold.ImageTransparentColor = Color.Magenta;
            btnBold.Name = "btnBold";
            btnBold.Size = new Size(28, 28);
            btnBold.Text = "Bold (Ctrl + B)";
            // 
            // btnItalic
            // 
            btnItalic.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnItalic.Image = Properties.Resources.text_italic;
            btnItalic.ImageTransparentColor = Color.Magenta;
            btnItalic.Name = "btnItalic";
            btnItalic.Size = new Size(28, 28);
            btnItalic.Text = "Italic (Ctrl + I)";
            // 
            // btnUnderline
            // 
            btnUnderline.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnUnderline.Image = Properties.Resources.text_underline;
            btnUnderline.ImageTransparentColor = Color.Magenta;
            btnUnderline.Name = "btnUnderline";
            btnUnderline.Size = new Size(28, 28);
            btnUnderline.Text = "Underline (Ctrl + U)";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 31);
            // 
            // btnFontColor
            // 
            btnFontColor.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnFontColor.Image = Properties.Resources.font_colors;
            btnFontColor.ImageTransparentColor = Color.Magenta;
            btnFontColor.Name = "btnFontColor";
            btnFontColor.Size = new Size(28, 28);
            btnFontColor.Text = "Font Color";
            // 
            // btnBackColor
            // 
            btnBackColor.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnBackColor.Image = Properties.Resources.color_wheel;
            btnBackColor.ImageTransparentColor = Color.Magenta;
            btnBackColor.Name = "btnBackColor";
            btnBackColor.Size = new Size(28, 28);
            btnBackColor.Text = "Background Color";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 31);
            // 
            // btnBullets
            // 
            btnBullets.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnBullets.Image = Properties.Resources.text_list_bullets;
            btnBullets.ImageTransparentColor = Color.Magenta;
            btnBullets.Name = "btnBullets";
            btnBullets.Size = new Size(28, 28);
            btnBullets.Text = "Start Bullet List";
            // 
            // btnNumbers
            // 
            btnNumbers.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnNumbers.Image = Properties.Resources.text_list_numbers;
            btnNumbers.ImageTransparentColor = Color.Magenta;
            btnNumbers.Name = "btnNumbers";
            btnNumbers.Size = new Size(28, 28);
            btnNumbers.Text = "Start Number List";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 31);
            // 
            // btnFind
            // 
            btnFind.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnFind.Image = Properties.Resources.page_find;
            btnFind.ImageTransparentColor = Color.Magenta;
            btnFind.Name = "btnFind";
            btnFind.Size = new Size(28, 28);
            btnFind.Text = "Find and Replace (Ctrl + F)";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 31);
            // 
            // btnResetSelectionToDefault
            // 
            btnResetSelectionToDefault.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnResetSelectionToDefault.Image = Properties.Resources.color_picker_default;
            btnResetSelectionToDefault.ImageTransparentColor = Color.Magenta;
            btnResetSelectionToDefault.Name = "btnResetSelectionToDefault";
            btnResetSelectionToDefault.Size = new Size(28, 28);
            btnResetSelectionToDefault.Text = "Reset formatting to default (Shift + Esc)";
            // 
            // btnSearchForTerm
            // 
            btnSearchForTerm.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSearchForTerm.Image = Properties.Resources.table_tab_search;
            btnSearchForTerm.ImageTransparentColor = Color.Magenta;
            btnSearchForTerm.Name = "btnSearchForTerm";
            btnSearchForTerm.Size = new Size(28, 28);
            btnSearchForTerm.Text = "Search for Term (Ctrl + T or Ctrl + LeftClick)";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 31);
            // 
            // btnSave
            // 
            btnSave.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSave.Image = Properties.Resources.disk;
            btnSave.ImageTransparentColor = Color.Magenta;
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(28, 28);
            btnSave.Text = "Save (Ctrl + S)";
            // 
            // lblTitle
            // 
            lblTitle.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 161);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(277, 28);
            lblTitle.Text = "Here goes the title of the item";
            // 
            // UC_RichText
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(edtRichText);
            Controls.Add(pnlTop);
            Controls.Add(StatuBar);
            Name = "UC_RichText";
            Size = new Size(749, 568);
            StatuBar.ResumeLayout(false);
            StatuBar.PerformLayout();
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ToolBar.ResumeLayout(false);
            ToolBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip StatuBar;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripStatusLabel lblWords;
        private ToolStripStatusLabel toolStripStatusLabel2;
        private ToolStripStatusLabel lblPages;
        private RichTextBoxEx edtRichText;
        private Panel pnlTop;
        private ToolStrip ToolBar;
        private ToolStripButton btnBold;
        private ToolStripButton btnItalic;
        private ToolStripButton btnUnderline;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnFontColor;
        private ToolStripButton btnBackColor;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnBullets;
        private ToolStripButton btnNumbers;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnFind;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton btnResetSelectionToDefault;
        private ToolStripButton btnSearchForTerm;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton btnSave;
        private ToolStripLabel lblTitle;
        private UC_RichTextFindAndReplace pnlFindAndReplace;
    }
}
