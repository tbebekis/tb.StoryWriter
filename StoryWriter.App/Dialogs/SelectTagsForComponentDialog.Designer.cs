namespace StoryWriter
{
    partial class SelectTagsForComponentDialog
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
            label1 = new Label();
            edtComponentName = new TextBox();
            label2 = new Label();
            lboTags = new CheckedListBox();
            btnOK = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 13);
            label1.Name = "label1";
            label1.Size = new Size(71, 15);
            label1.TabIndex = 0;
            label1.Text = "Component";
            // 
            // edtComponentName
            // 
            edtComponentName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtComponentName.Location = new Point(89, 10);
            edtComponentName.Name = "edtComponentName";
            edtComponentName.ReadOnly = true;
            edtComponentName.Size = new Size(320, 23);
            edtComponentName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(53, 39);
            label2.Name = "label2";
            label2.Size = new Size(30, 15);
            label2.TabIndex = 2;
            label2.Text = "Tags";
            // 
            // lboTags
            // 
            lboTags.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lboTags.FormattingEnabled = true;
            lboTags.Location = new Point(89, 39);
            lboTags.Name = "lboTags";
            lboTags.Size = new Size(320, 400);
            lboTags.TabIndex = 3;
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(258, 462);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 32);
            btnOK.TabIndex = 5;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(336, 462);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 32);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // SelectTagListDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(415, 498);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Controls.Add(lboTags);
            Controls.Add(label2);
            Controls.Add(edtComponentName);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectTagListDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Tags for Component";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox edtComponentName;
        private Label label2;
        private CheckedListBox lboTags;
        private Button btnOK;
        private Button btnCancel;
    }
}