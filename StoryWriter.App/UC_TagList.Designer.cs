namespace StoryWriter
{
    partial class UC_TagList
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
            ToolBar = new ToolStrip();
            btnAddTag = new ToolStripButton();
            btnDeleteTag = new ToolStripButton();
            panel1 = new Panel();
            edtFilter = new TextBox();
            label1 = new Label();
            splitContainer1 = new SplitContainer();
            gridTags = new DataGridView();
            coTagName = new DataGridViewTextBoxColumn();
            gridComponents = new DataGridView();
            coComponent = new DataGridViewTextBoxColumn();
            ToolBar.SuspendLayout();
            panel1.SuspendLayout();
            ((ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((ISupportInitialize)gridTags).BeginInit();
            ((ISupportInitialize)gridComponents).BeginInit();
            SuspendLayout();
            // 
            // ToolBar
            // 
            ToolBar.ImageScalingSize = new Size(24, 24);
            ToolBar.Items.AddRange(new ToolStripItem[] { btnAddTag, btnDeleteTag });
            ToolBar.Location = new Point(0, 0);
            ToolBar.Name = "ToolBar";
            ToolBar.Size = new Size(350, 31);
            ToolBar.TabIndex = 5;
            ToolBar.Text = "ToolBar";
            // 
            // btnAddTag
            // 
            btnAddTag.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddTag.Image = Properties.Resources.table_add;
            btnAddTag.ImageTransparentColor = Color.Magenta;
            btnAddTag.Name = "btnAddTag";
            btnAddTag.Size = new Size(28, 28);
            btnAddTag.Text = "Add";
            // 
            // btnDeleteTag
            // 
            btnDeleteTag.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDeleteTag.Image = Properties.Resources.table_delete;
            btnDeleteTag.ImageTransparentColor = Color.Magenta;
            btnDeleteTag.Name = "btnDeleteTag";
            btnDeleteTag.Size = new Size(28, 28);
            btnDeleteTag.Text = "Remove";
            // 
            // panel1
            // 
            panel1.Controls.Add(edtFilter);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 31);
            panel1.Name = "panel1";
            panel1.Size = new Size(350, 32);
            panel1.TabIndex = 6;
            // 
            // edtFilter
            // 
            edtFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtFilter.Location = new Point(49, 4);
            edtFilter.Name = "edtFilter";
            edtFilter.Size = new Size(295, 23);
            edtFilter.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 7);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 0;
            label1.Text = "Filter";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 63);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(gridTags);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(gridComponents);
            splitContainer1.Size = new Size(350, 598);
            splitContainer1.SplitterDistance = 375;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 7;
            // 
            // gridTags
            // 
            gridTags.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridTags.Columns.AddRange(new DataGridViewColumn[] { coTagName });
            gridTags.Dock = DockStyle.Fill;
            gridTags.Location = new Point(0, 0);
            gridTags.Name = "gridTags";
            gridTags.Size = new Size(350, 375);
            gridTags.TabIndex = 0;
            // 
            // coTagName
            // 
            coTagName.DataPropertyName = "Name";
            coTagName.HeaderText = "Tag";
            coTagName.Name = "coTagName";
            // 
            // gridComponents
            // 
            gridComponents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridComponents.Columns.AddRange(new DataGridViewColumn[] { coComponent });
            gridComponents.Dock = DockStyle.Fill;
            gridComponents.Location = new Point(0, 0);
            gridComponents.Name = "gridComponents";
            gridComponents.Size = new Size(350, 217);
            gridComponents.TabIndex = 0;
            // 
            // coComponent
            // 
            coComponent.DataPropertyName = "Name";
            coComponent.HeaderText = "Component";
            coComponent.Name = "coComponent";
            // 
            // UC_TagList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(panel1);
            Controls.Add(ToolBar);
            Name = "UC_TagList";
            Size = new Size(350, 661);
            ToolBar.ResumeLayout(false);
            ToolBar.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((ISupportInitialize)gridTags).EndInit();
            ((ISupportInitialize)gridComponents).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip ToolBar;
        private ToolStripButton btnAddTag;
        private ToolStripButton btnDeleteTag;
        private Panel panel1;
        private Label label1;
        private TextBox edtFilter;
        private SplitContainer splitContainer1;
        private DataGridView gridTags;
        private DataGridView gridComponents;
        private DataGridViewTextBoxColumn coTagName;
        private DataGridViewTextBoxColumn coComponent;
    }
}
