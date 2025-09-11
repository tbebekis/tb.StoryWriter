namespace StoryWriter
{
    partial class SelectItemsForItemDialog
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
            edtItemName = new TextBox();
            label2 = new Label();
            btnOK = new Button();
            btnCancel = new Button();
            lboAvail = new ListBox();
            btnSelectAll = new Button();
            btnSelectOne = new Button();
            btnUnselectOne = new Button();
            btnUnselectAll = new Button();
            lboSelected = new ListBox();
            label3 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 13);
            label1.Name = "label1";
            label1.Size = new Size(24, 15);
            label1.TabIndex = 0;
            label1.Text = "For";
            // 
            // edtItemName
            // 
            edtItemName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtItemName.Location = new Point(36, 10);
            edtItemName.Name = "edtItemName";
            edtItemName.ReadOnly = true;
            edtItemName.Size = new Size(569, 23);
            edtItemName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 42);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 2;
            label2.Text = "Available";
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(460, 450);
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
            btnCancel.Location = new Point(538, 450);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 32);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // lboAvail
            // 
            lboAvail.FormattingEnabled = true;
            lboAvail.HorizontalScrollbar = true;
            lboAvail.Location = new Point(36, 63);
            lboAvail.Name = "lboAvail";
            lboAvail.Size = new Size(260, 379);
            lboAvail.Sorted = true;
            lboAvail.TabIndex = 6;
            // 
            // btnSelectAll
            // 
            btnSelectAll.Font = new Font("Segoe UI", 9F);
            btnSelectAll.Location = new Point(302, 113);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Size = new Size(40, 32);
            btnSelectAll.TabIndex = 7;
            btnSelectAll.Text = " >>";
            btnSelectAll.UseVisualStyleBackColor = true;
            // 
            // btnSelectOne
            // 
            btnSelectOne.Location = new Point(302, 151);
            btnSelectOne.Name = "btnSelectOne";
            btnSelectOne.Size = new Size(40, 32);
            btnSelectOne.TabIndex = 8;
            btnSelectOne.Text = ">";
            btnSelectOne.UseVisualStyleBackColor = true;
            // 
            // btnUnselectOne
            // 
            btnUnselectOne.Location = new Point(302, 189);
            btnUnselectOne.Name = "btnUnselectOne";
            btnUnselectOne.Size = new Size(40, 32);
            btnUnselectOne.TabIndex = 9;
            btnUnselectOne.Text = "<";
            btnUnselectOne.UseVisualStyleBackColor = true;
            // 
            // btnUnselectAll
            // 
            btnUnselectAll.Location = new Point(302, 227);
            btnUnselectAll.Name = "btnUnselectAll";
            btnUnselectAll.Size = new Size(40, 32);
            btnUnselectAll.TabIndex = 10;
            btnUnselectAll.Text = "<<";
            btnUnselectAll.UseVisualStyleBackColor = true;
            // 
            // lboSelected
            // 
            lboSelected.FormattingEnabled = true;
            lboSelected.HorizontalScrollbar = true;
            lboSelected.Location = new Point(348, 63);
            lboSelected.Name = "lboSelected";
            lboSelected.Size = new Size(260, 379);
            lboSelected.Sorted = true;
            lboSelected.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(348, 42);
            label3.Name = "label3";
            label3.Size = new Size(51, 15);
            label3.TabIndex = 12;
            label3.Text = "Selected";
            // 
            // SelectItemsForItemDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(617, 486);
            Controls.Add(label3);
            Controls.Add(lboSelected);
            Controls.Add(btnUnselectAll);
            Controls.Add(btnUnselectOne);
            Controls.Add(btnSelectOne);
            Controls.Add(btnSelectAll);
            Controls.Add(lboAvail);
            Controls.Add(btnOK);
            Controls.Add(btnCancel);
            Controls.Add(label2);
            Controls.Add(edtItemName);
            Controls.Add(label1);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SelectItemsForItemDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Select Items For Item";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox edtItemName;
        private Label label2;
        private Button btnOK;
        private Button btnCancel;
        private ListBox lboAvail;
        private Button btnSelectAll;
        private Button btnSelectOne;
        private Button btnUnselectOne;
        private Button btnUnselectAll;
        private ListBox lboSelected;
        private Label label3;
    }
}