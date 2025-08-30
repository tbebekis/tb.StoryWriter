namespace StoryWriter
{
    partial class UC_QuickViewList
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
            splitContainer1 = new SplitContainer();
            Grid = new DataGridView();
            coType = new DataGridViewTextBoxColumn();
            coPlace = new DataGridViewTextBoxColumn();
            coName = new DataGridViewTextBoxColumn();
            ucRichText = new UC_RichText();
            panel2 = new Panel();
            lblItemTitle = new Label();
            ToolBar = new ToolStrip();
            btnEditRtfText = new ToolStripButton();
            btnRemoveItem = new ToolStripButton();
            btnRemoveAll = new ToolStripButton();
            ((ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((ISupportInitialize)Grid).BeginInit();
            panel2.SuspendLayout();
            ToolBar.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = SystemColors.Control;
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 31);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(Grid);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(ucRichText);
            splitContainer1.Panel2.Controls.Add(panel2);
            splitContainer1.Size = new Size(351, 596);
            splitContainer1.SplitterDistance = 313;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 3;
            // 
            // Grid
            // 
            Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Grid.Columns.AddRange(new DataGridViewColumn[] { coType, coPlace, coName });
            Grid.Dock = DockStyle.Fill;
            Grid.Location = new Point(0, 0);
            Grid.Name = "Grid";
            Grid.Size = new Size(349, 311);
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
            // ucRichText
            // 
            ucRichText.Dock = DockStyle.Fill;
            ucRichText.Location = new Point(0, 25);
            ucRichText.Name = "ucRichText";
            ucRichText.Size = new Size(349, 250);
            ucRichText.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(lblItemTitle);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(349, 25);
            panel2.TabIndex = 1;
            // 
            // lblItemTitle
            // 
            lblItemTitle.AutoSize = true;
            lblItemTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblItemTitle.Location = new Point(1, 1);
            lblItemTitle.Name = "lblItemTitle";
            lblItemTitle.Size = new Size(99, 21);
            lblItemTitle.TabIndex = 0;
            lblItemTitle.Text = "lblItemTitle";
            // 
            // ToolBar
            // 
            ToolBar.ImageScalingSize = new Size(24, 24);
            ToolBar.Items.AddRange(new ToolStripItem[] { btnRemoveItem, btnRemoveAll, btnEditRtfText });
            ToolBar.Location = new Point(0, 0);
            ToolBar.Name = "ToolBar";
            ToolBar.Size = new Size(351, 31);
            ToolBar.TabIndex = 4;
            ToolBar.Text = "toolStrip1";
            // 
            // btnEditRtfText
            // 
            btnEditRtfText.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEditRtfText.Image = Properties.Resources.page_edit;
            btnEditRtfText.ImageTransparentColor = Color.Magenta;
            btnEditRtfText.Name = "btnEditRtfText";
            btnEditRtfText.Size = new Size(28, 28);
            btnEditRtfText.Text = "Edit Text";
            // 
            // btnRemoveItem
            // 
            btnRemoveItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnRemoveItem.Image = Properties.Resources.table_delete;
            btnRemoveItem.ImageTransparentColor = Color.Magenta;
            btnRemoveItem.Name = "btnRemoveItem";
            btnRemoveItem.Size = new Size(28, 28);
            btnRemoveItem.Text = "Remove Item";
            // 
            // btnRemoveAll
            // 
            btnRemoveAll.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnRemoveAll.Image = Properties.Resources.shape_square_delete;
            btnRemoveAll.ImageTransparentColor = Color.Magenta;
            btnRemoveAll.Name = "btnRemoveAll";
            btnRemoveAll.Size = new Size(28, 28);
            btnRemoveAll.Text = "Remove All";
            // 
            // UC_QuickViewList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(ToolBar);
            Name = "UC_QuickViewList";
            Size = new Size(351, 627);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((ISupportInitialize)Grid).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ToolBar.ResumeLayout(false);
            ToolBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splitContainer1;
        private DataGridView Grid;
        private DataGridViewTextBoxColumn coType;
        private DataGridViewTextBoxColumn coPlace;
        private DataGridViewTextBoxColumn coName;
        private UC_RichText ucRichText;
        private Panel panel2;
        private Label lblItemTitle;
        private ToolStrip ToolBar;
        private ToolStripButton btnEditRtfText;
        private ToolStripButton btnRemoveItem;
        private ToolStripButton btnRemoveAll;
    }
}
