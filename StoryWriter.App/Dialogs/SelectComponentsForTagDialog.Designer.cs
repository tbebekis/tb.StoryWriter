namespace StoryWriter
{
    partial class SelectComponentsForTagDialog
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
            btnOK = new Button();
            btnCancel = new Button();
            lboComponents = new CheckedListBox();
            label2 = new Label();
            edtTagName = new TextBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(254, 459);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 32);
            btnOK.TabIndex = 11;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(332, 459);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 32);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // lboComponents
            // 
            lboComponents.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lboComponents.FormattingEnabled = true;
            lboComponents.Location = new Point(85, 36);
            lboComponents.Name = "lboComponents";
            lboComponents.Size = new Size(320, 400);
            lboComponents.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(3, 36);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 8;
            label2.Text = "Components";
            // 
            // edtTagName
            // 
            edtTagName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtTagName.Location = new Point(85, 7);
            edtTagName.Name = "edtTagName";
            edtTagName.ReadOnly = true;
            edtTagName.Size = new Size(320, 23);
            edtTagName.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(54, 10);
            label1.Name = "label1";
            label1.Size = new Size(25, 15);
            label1.TabIndex = 6;
            label1.Text = "Tag";
            // 
            // SelectComponentsForTagDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(415, 498);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Controls.Add(lboComponents);
            Controls.Add(label2);
            Controls.Add(edtTagName);
            Controls.Add(label1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectComponentsForTagDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Components for Tag";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOK;
        private Button btnCancel;
        private CheckedListBox lboComponents;
        private Label label2;
        private TextBox edtTagName;
        private Label label1;
    }
}