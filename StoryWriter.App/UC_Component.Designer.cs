namespace StoryWriter
{
    partial class UC_Component
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
            ucComponentText = new UC_RichText();
            SuspendLayout();
            // 
            // ucComponentText
            // 
            ucComponentText.Dock = DockStyle.Fill;
            ucComponentText.Location = new Point(0, 0);
            ucComponentText.Name = "ucComponentText";
            ucComponentText.Size = new Size(743, 609);
            ucComponentText.TabIndex = 0;
            // 
            // UC_Component
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(ucComponentText);
            Name = "UC_Component";
            Size = new Size(743, 609);
            ResumeLayout(false);
        }

        #endregion

        private UC_RichText ucComponentText;
    }
}
