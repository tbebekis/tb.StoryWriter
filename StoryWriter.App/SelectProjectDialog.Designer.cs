namespace StoryWriter
{
    partial class SelectProjectDialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnCancel = new Button();
            btnOK = new Button();
            lboProjectNames = new ListBox();
            SuspendLayout();
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(324, 295);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 32);
            btnCancel.TabIndex = 0;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(246, 295);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 32);
            btnOK.TabIndex = 1;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // lboProjectNames
            // 
            lboProjectNames.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lboProjectNames.FormattingEnabled = true;
            lboProjectNames.Location = new Point(12, 12);
            lboProjectNames.Name = "lboProjectNames";
            lboProjectNames.Size = new Size(386, 274);
            lboProjectNames.TabIndex = 2;
            // 
            // SelectProjectDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 329);
            Controls.Add(lboProjectNames);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectProjectDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Project";
            ResumeLayout(false);
        }

        #endregion

        private Button btnCancel;
        private Button btnOK;
        private ListBox lboProjectNames;
    }
}