namespace StoryWriter
{
    partial class EditItemDialog
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
            label1 = new Label();
            label2 = new Label();
            edtParentName = new TextBox();
            edtName = new TextBox();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(252, 111);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 32);
            btnOK.TabIndex = 3;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(330, 111);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 32);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 32);
            label1.Name = "label1";
            label1.Size = new Size(41, 15);
            label1.TabIndex = 4;
            label1.Text = "Parent";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(18, 62);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 5;
            label2.Text = "Name";
            // 
            // edtParentName
            // 
            edtParentName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtParentName.Location = new Point(65, 29);
            edtParentName.Name = "edtParentName";
            edtParentName.ReadOnly = true;
            edtParentName.Size = new Size(333, 23);
            edtParentName.TabIndex = 6;
            // 
            // edtName
            // 
            edtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtName.Location = new Point(65, 58);
            edtName.Name = "edtName";
            edtName.Size = new Size(333, 23);
            edtName.TabIndex = 7;
            // 
            // EditItemDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(410, 148);
            Controls.Add(edtName);
            Controls.Add(edtParentName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditItemDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Add Item";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnOK;
        private Button btnCancel;
        private Label label1;
        private Label label2;
        private TextBox edtParentName;
        private TextBox edtName;
    }
}