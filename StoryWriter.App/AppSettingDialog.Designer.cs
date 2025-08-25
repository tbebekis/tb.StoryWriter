namespace StoryWriter
{
    partial class AppSettingDialog
    {
        private System.ComponentModel.IContainer components = null;

        private CheckBox chkLoadLast;
        private CheckBox chkAutoSave;
        private Label lblFontFamily;
        private ComboBox cboFontFamily;
        private Label lblFontSize;
        private NumericUpDown nudFontSize;
        private Button btnOK;
        private Button btnCancel;

        /// <summary>
        /// Καθαρισμός πόρων.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            chkLoadLast = new CheckBox();
            chkAutoSave = new CheckBox();
            lblFontFamily = new Label();
            cboFontFamily = new ComboBox();
            lblFontSize = new Label();
            nudFontSize = new NumericUpDown();
            btnOK = new Button();
            btnCancel = new Button();
            label1 = new Label();
            edtAutoSaveSecondsInterval = new NumericUpDown();
            ((ISupportInitialize)nudFontSize).BeginInit();
            ((ISupportInitialize)edtAutoSaveSecondsInterval).BeginInit();
            SuspendLayout();
            // 
            // chkLoadLast
            // 
            chkLoadLast.AutoSize = true;
            chkLoadLast.Location = new Point(11, 12);
            chkLoadLast.Name = "chkLoadLast";
            chkLoadLast.Size = new Size(170, 19);
            chkLoadLast.TabIndex = 0;
            chkLoadLast.Text = "Load last project on startup";
            chkLoadLast.UseVisualStyleBackColor = true;
            // 
            // chkAutoSave
            // 
            chkAutoSave.AutoSize = true;
            chkAutoSave.Location = new Point(11, 37);
            chkAutoSave.Name = "chkAutoSave";
            chkAutoSave.Size = new Size(206, 19);
            chkAutoSave.TabIndex = 4;
            chkAutoSave.Text = "Save current project automatically";
            chkAutoSave.UseVisualStyleBackColor = true;
            // 
            // lblFontFamily
            // 
            lblFontFamily.AutoSize = true;
            lblFontFamily.Location = new Point(142, 107);
            lblFontFamily.Name = "lblFontFamily";
            lblFontFamily.Size = new Size(31, 15);
            lblFontFamily.TabIndex = 5;
            lblFontFamily.Text = "Font";
            // 
            // cboFontFamily
            // 
            cboFontFamily.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboFontFamily.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboFontFamily.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFontFamily.FormattingEnabled = true;
            cboFontFamily.Location = new Point(179, 104);
            cboFontFamily.Name = "cboFontFamily";
            cboFontFamily.Size = new Size(255, 23);
            cboFontFamily.TabIndex = 6;
            // 
            // lblFontSize
            // 
            lblFontSize.AutoSize = true;
            lblFontSize.Location = new Point(451, 106);
            lblFontSize.Name = "lblFontSize";
            lblFontSize.Size = new Size(27, 15);
            lblFontSize.TabIndex = 7;
            lblFontSize.Text = "Size";
            // 
            // nudFontSize
            // 
            nudFontSize.Location = new Point(488, 104);
            nudFontSize.Maximum = new decimal(new int[] { 72, 0, 0, 0 });
            nudFontSize.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            nudFontSize.Name = "nudFontSize";
            nudFontSize.Size = new Size(80, 23);
            nudFontSize.TabIndex = 8;
            nudFontSize.Value = new decimal(new int[] { 13, 0, 0, 0 });
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(427, 156);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(80, 32);
            btnOK.TabIndex = 9;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(509, 156);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(80, 32);
            btnCancel.TabIndex = 10;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 77);
            label1.Name = "label1";
            label1.Size = new Size(163, 15);
            label1.TabIndex = 11;
            label1.Text = "Auto-Save Interval in seconds";
            // 
            // edtAutoSaveSecondsInterval
            // 
            edtAutoSaveSecondsInterval.Location = new Point(179, 73);
            edtAutoSaveSecondsInterval.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            edtAutoSaveSecondsInterval.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            edtAutoSaveSecondsInterval.Name = "edtAutoSaveSecondsInterval";
            edtAutoSaveSecondsInterval.Size = new Size(68, 23);
            edtAutoSaveSecondsInterval.TabIndex = 12;
            edtAutoSaveSecondsInterval.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // AppSettingDialog
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(595, 194);
            Controls.Add(edtAutoSaveSecondsInterval);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnOK);
            Controls.Add(nudFontSize);
            Controls.Add(lblFontSize);
            Controls.Add(cboFontFamily);
            Controls.Add(lblFontFamily);
            Controls.Add(chkAutoSave);
            Controls.Add(chkLoadLast);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "AppSettingDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Application Settings";
            ((ISupportInitialize)nudFontSize).EndInit();
            ((ISupportInitialize)edtAutoSaveSecondsInterval).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private NumericUpDown edtAutoSaveSecondsInterval;
    }
}
