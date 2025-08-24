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
            toolStrip1 = new ToolStrip();
            btnBold = new ToolStripButton();
            btnItalic = new ToolStripButton();
            btnUnderline = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnLink = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnFind = new ToolStripButton();
            btnReplace = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            btnBullets = new ToolStripButton();
            btnNumbers = new ToolStripButton();
            edtRichText = new RichTextBox();
            toolStripSeparator4 = new ToolStripSeparator();
            btnSave = new ToolStripButton();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { btnBold, btnItalic, btnUnderline, toolStripSeparator1, btnLink, toolStripSeparator2, btnFind, btnReplace, toolStripSeparator3, btnBullets, btnNumbers, toolStripSeparator4, btnSave });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(440, 31);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // btnBold
            // 
            btnBold.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnBold.Image = Properties.Resources.text_bold;
            btnBold.ImageTransparentColor = Color.Magenta;
            btnBold.Name = "btnBold";
            btnBold.Size = new Size(28, 28);
            btnBold.Text = "Bold";
            // 
            // btnItalic
            // 
            btnItalic.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnItalic.Image = Properties.Resources.text_italic;
            btnItalic.ImageTransparentColor = Color.Magenta;
            btnItalic.Name = "btnItalic";
            btnItalic.Size = new Size(28, 28);
            btnItalic.Text = "Italic";
            // 
            // btnUnderline
            // 
            btnUnderline.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnUnderline.Image = Properties.Resources.text_underline;
            btnUnderline.ImageTransparentColor = Color.Magenta;
            btnUnderline.Name = "btnUnderline";
            btnUnderline.Size = new Size(28, 28);
            btnUnderline.Text = "Underline";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 31);
            // 
            // btnLink
            // 
            btnLink.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnLink.Image = Properties.Resources.link;
            btnLink.ImageTransparentColor = Color.Magenta;
            btnLink.Name = "btnLink";
            btnLink.Size = new Size(28, 28);
            btnLink.Text = "Go to link";
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
            btnFind.Text = "Find";
            // 
            // btnReplace
            // 
            btnReplace.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnReplace.Image = Properties.Resources.text_replace;
            btnReplace.ImageTransparentColor = Color.Magenta;
            btnReplace.Name = "btnReplace";
            btnReplace.Size = new Size(28, 28);
            btnReplace.Text = "Replace";
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
            // edtRichText
            // 
            edtRichText.Dock = DockStyle.Fill;
            edtRichText.Location = new Point(0, 31);
            edtRichText.Name = "edtRichText";
            edtRichText.Size = new Size(440, 170);
            edtRichText.TabIndex = 1;
            edtRichText.Text = "";
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
            // UC_RichText
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(edtRichText);
            Controls.Add(toolStrip1);
            Name = "UC_RichText";
            Size = new Size(440, 201);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton btnBold;
        private ToolStripButton btnItalic;
        private ToolStripButton btnUnderline;
        private RichTextBox edtRichText;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnLink;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnFind;
        private ToolStripButton btnReplace;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnBullets;
        private ToolStripButton btnNumbers;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton btnSave;
    }
}
