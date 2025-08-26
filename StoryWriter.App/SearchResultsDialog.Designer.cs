namespace StoryWriter
{
    partial class SearchResultsDialog
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
            Grid = new DataGridView();
            panel1 = new Panel();
            coType = new DataGridViewTextBoxColumn();
            coName = new DataGridViewTextBoxColumn();
            ((ISupportInitialize)Grid).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // btnOK
            // 
            btnOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnOK.Location = new Point(473, 4);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 32);
            btnOK.TabIndex = 7;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(551, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 32);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            // 
            // Grid
            // 
            Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Grid.Columns.AddRange(new DataGridViewColumn[] { coType, coName });
            Grid.Dock = DockStyle.Fill;
            Grid.Location = new Point(0, 0);
            Grid.Name = "Grid";
            Grid.Size = new Size(630, 372);
            Grid.TabIndex = 8;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnCancel);
            panel1.Controls.Add(btnOK);
            panel1.Dock = DockStyle.Bottom;
            panel1.Location = new Point(0, 372);
            panel1.Name = "panel1";
            panel1.Size = new Size(630, 39);
            panel1.TabIndex = 9;
            // 
            // coType
            // 
            coType.DataPropertyName = "Type";
            coType.HeaderText = "Type";
            coType.Name = "coType";
            // 
            // coName
            // 
            coName.DataPropertyName = "Name";
            coName.HeaderText = "Name";
            coName.Name = "coName";
            // 
            // SearchResultsDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(630, 411);
            Controls.Add(Grid);
            Controls.Add(panel1);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SearchResultsDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Search Results";
            ((ISupportInitialize)Grid).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button btnOK;
        private Button btnCancel;
        private DataGridView Grid;
        private Panel panel1;
        private DataGridViewTextBoxColumn coType;
        private DataGridViewTextBoxColumn coName;
    }
}