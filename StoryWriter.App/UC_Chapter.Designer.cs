namespace StoryWriter
{
    partial class UC_Chapter
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
            ucBodyText = new UC_RichText();
            Pager = new TabControl();
            tabBodyText = new TabPage();
            tabSynopsis = new TabPage();
            ucSynopsis = new UC_RichText();
            Pager.SuspendLayout();
            tabBodyText.SuspendLayout();
            tabSynopsis.SuspendLayout();
            SuspendLayout();
            // 
            // ucBodyText
            // 
            ucBodyText.Dock = DockStyle.Fill;
            ucBodyText.Location = new Point(3, 3);
            ucBodyText.Name = "ucBodyText";
            ucBodyText.Size = new Size(776, 569);
            ucBodyText.TabIndex = 0;
            // 
            // Pager
            // 
            Pager.Controls.Add(tabBodyText);
            Pager.Controls.Add(tabSynopsis);
            Pager.Dock = DockStyle.Fill;
            Pager.Location = new Point(0, 0);
            Pager.Name = "Pager";
            Pager.SelectedIndex = 0;
            Pager.Size = new Size(790, 603);
            Pager.TabIndex = 1;
            // 
            // tabBodyText
            // 
            tabBodyText.Controls.Add(ucBodyText);
            tabBodyText.Location = new Point(4, 24);
            tabBodyText.Name = "tabBodyText";
            tabBodyText.Padding = new Padding(3);
            tabBodyText.Size = new Size(782, 575);
            tabBodyText.TabIndex = 0;
            tabBodyText.Text = "Text";
            tabBodyText.UseVisualStyleBackColor = true;
            // 
            // tabSynopsis
            // 
            tabSynopsis.Controls.Add(ucSynopsis);
            tabSynopsis.Location = new Point(4, 24);
            tabSynopsis.Name = "tabSynopsis";
            tabSynopsis.Padding = new Padding(3);
            tabSynopsis.Size = new Size(782, 575);
            tabSynopsis.TabIndex = 1;
            tabSynopsis.Text = "Synopsis";
            tabSynopsis.UseVisualStyleBackColor = true;
            // 
            // ucSynopsis
            // 
            ucSynopsis.Dock = DockStyle.Fill;
            ucSynopsis.Location = new Point(3, 3);
            ucSynopsis.Name = "ucSynopsis";
            ucSynopsis.Size = new Size(776, 569);
            ucSynopsis.TabIndex = 0;
            // 
            // UC_Chapter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Pager);
            Name = "UC_Chapter";
            Size = new Size(790, 603);
            Pager.ResumeLayout(false);
            tabBodyText.ResumeLayout(false);
            tabSynopsis.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private UC_RichText ucBodyText;
        private TabControl Pager;
        private TabPage tabBodyText;
        private TabPage tabSynopsis;
        private UC_RichText ucSynopsis;
    }
}
