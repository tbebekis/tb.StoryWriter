namespace StoryWriter
{
    partial class EditComponentDialog
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
            edtName = new TextBox();
            label2 = new Label();
            btnOK = new Button();
            btnCancel = new Button();
            label1 = new Label();
            lboComponentTypes = new ListBox();
            label3 = new Label();
            edtDescription = new TextBox();
            SuspendLayout();
            // 
            // edtName
            // 
            edtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtName.Location = new Point(89, 22);
            edtName.Name = "edtName";
            edtName.Size = new Size(358, 23);
            edtName.TabIndex = 11;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(42, 26);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 10;
            label2.Text = "Name";
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(303, 521);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 32);
            btnOK.TabIndex = 9;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(381, 521);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 32);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(52, 155);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 12;
            label1.Text = "Type";
            // 
            // lboComponentTypes
            // 
            lboComponentTypes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lboComponentTypes.FormattingEnabled = true;
            lboComponentTypes.Location = new Point(89, 155);
            lboComponentTypes.Name = "lboComponentTypes";
            lboComponentTypes.Size = new Size(358, 349);
            lboComponentTypes.TabIndex = 13;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 51);
            label3.Name = "label3";
            label3.Size = new Size(67, 15);
            label3.TabIndex = 14;
            label3.Text = "Description";
            // 
            // edtDescription
            // 
            edtDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtDescription.Location = new Point(89, 51);
            edtDescription.MaxLength = 250;
            edtDescription.Multiline = true;
            edtDescription.Name = "edtDescription";
            edtDescription.ScrollBars = ScrollBars.Both;
            edtDescription.Size = new Size(358, 97);
            edtDescription.TabIndex = 15;
            // 
            // EditComponentDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(459, 557);
            Controls.Add(edtDescription);
            Controls.Add(label3);
            Controls.Add(lboComponentTypes);
            Controls.Add(label1);
            Controls.Add(edtName);
            Controls.Add(label2);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditComponentDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Component";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox edtName;
        private Label label2;
        private Button btnOK;
        private Button btnCancel;
        private Label label1;
        private ListBox lboComponentTypes;
        private Label label3;
        private TextBox edtDescription;
    }
}