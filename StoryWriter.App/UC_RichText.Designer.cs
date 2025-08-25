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
            ToolBar = new ToolStrip();
            btnBold = new ToolStripButton();
            btnItalic = new ToolStripButton();
            btnUnderline = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnResetSelectionToDefault = new ToolStripButton();
            btnLink = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnFind = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            btnBullets = new ToolStripButton();
            btnNumbers = new ToolStripButton();
            btnFontColor = new ToolStripButton();
            btnBackColor = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            btnSave = new ToolStripButton();
            edtRichText = new RichTextBox();
            toolStripSeparator5 = new ToolStripSeparator();
            ToolBar.SuspendLayout();
            SuspendLayout();
            // 
            // ToolBar
            // 
            ToolBar.ImageScalingSize = new Size(24, 24);
            ToolBar.Items.AddRange(new ToolStripItem[] { btnBold, btnItalic, btnUnderline, toolStripSeparator1, btnFontColor, btnBackColor, toolStripSeparator2, btnBullets, btnNumbers, toolStripSeparator3, btnFind, toolStripSeparator5, btnResetSelectionToDefault, btnLink, toolStripSeparator4, btnSave });
            ToolBar.Location = new Point(0, 0);
            ToolBar.Name = "ToolBar";
            ToolBar.Size = new Size(535, 31);
            ToolBar.TabIndex = 0;
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
            // btnResetSelectionToDefault
            // 
            btnResetSelectionToDefault.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnResetSelectionToDefault.Image = Properties.Resources.color_picker_default;
            btnResetSelectionToDefault.ImageTransparentColor = Color.Magenta;
            btnResetSelectionToDefault.Name = "btnResetSelectionToDefault";
            btnResetSelectionToDefault.Size = new Size(28, 28);
            btnResetSelectionToDefault.Text = "Reset formatting to default (Shift + Esc)";
            // 
            // btnLink
            // 
            btnLink.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnLink.Image = Properties.Resources.link;
            btnLink.ImageTransparentColor = Color.Magenta;
            btnLink.Name = "btnLink";
            btnLink.Size = new Size(28, 28);
            btnLink.Text = "Go to link (Ctrl + L or Ctrl + LeftClick)";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 31);
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
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 31);
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
            btnSave.Text = "Save";
            // 
            // edtRichText
            // 
            edtRichText.DetectUrls = false;
            edtRichText.Dock = DockStyle.Fill;
            edtRichText.Location = new Point(0, 31);
            edtRichText.Name = "edtRichText";
            edtRichText.Size = new Size(535, 170);
            edtRichText.TabIndex = 1;
            edtRichText.Text = "";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(6, 31);
            // 
            // UC_RichText
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(edtRichText);
            Controls.Add(ToolBar);
            Name = "UC_RichText";
            Size = new Size(535, 201);
            ToolBar.ResumeLayout(false);
            ToolBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip ToolBar;
        private ToolStripButton btnBold;
        private ToolStripButton btnItalic;
        private ToolStripButton btnUnderline;
        private RichTextBox edtRichText;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnLink;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnFind;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnBullets;
        private ToolStripButton btnNumbers;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton btnSave;
        private ToolStripButton btnFontColor;
        private ToolStripButton btnBackColor;
        private ToolStripButton btnResetSelectionToDefault;
        private ToolStripSeparator toolStripSeparator5;
    }
}
