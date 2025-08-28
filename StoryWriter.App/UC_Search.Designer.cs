namespace StoryWriter
{
    partial class UC_Search
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
            panel1 = new Panel();
            edtSearch = new TextBox();
            label1 = new Label();
            Grid = new DataGridView();
            coType = new DataGridViewTextBoxColumn();
            coPlace = new DataGridViewTextBoxColumn();
            coName = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            ((ISupportInitialize)Grid).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(edtSearch);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(351, 31);
            panel1.TabIndex = 0;
            // 
            // edtSearch
            // 
            edtSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtSearch.Location = new Point(65, 4);
            edtSearch.Name = "edtSearch";
            edtSearch.Size = new Size(279, 23);
            edtSearch.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(17, 8);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 0;
            label1.Text = "Search";
            // 
            // Grid
            // 
            Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Grid.Columns.AddRange(new DataGridViewColumn[] { coType, coPlace, coName });
            Grid.Dock = DockStyle.Fill;
            Grid.Location = new Point(0, 31);
            Grid.Name = "Grid";
            Grid.Size = new Size(351, 596);
            Grid.TabIndex = 1;
            // 
            // coType
            // 
            coType.DataPropertyName = "Type";
            coType.HeaderText = "Type";
            coType.Name = "coType";
            // 
            // coPlace
            // 
            coPlace.DataPropertyName = "Place";
            coPlace.HeaderText = "Place";
            coPlace.Name = "coPlace";
            // 
            // coName
            // 
            coName.DataPropertyName = "Name";
            coName.HeaderText = "Name";
            coName.Name = "coName";
            // 
            // UC_Search
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Grid);
            Controls.Add(panel1);
            Name = "UC_Search";
            Size = new Size(351, 627);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((ISupportInitialize)Grid).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private TextBox edtSearch;
        private Label label1;
        private DataGridView Grid;
        private DataGridViewTextBoxColumn coType;
        private DataGridViewTextBoxColumn coPlace;
        private DataGridViewTextBoxColumn coName;
    }
}
