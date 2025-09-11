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
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripSeparator2 = new ToolStripSeparator();
            btnAddComponentsToTag = new ToolStripButton();
            panel1 = new Panel();
            edtFilter = new TextBox();
            label1 = new Label();
            splitContainer1 = new SplitContainer();
            Grid = new DataGridView();
            coTagName = new DataGridViewTextBoxColumn();
            lboComponents = new ListBox();
            ToolBar2 = new ToolStrip();
            btnAddToQuickView = new ToolStripButton();
            toolStripLabel1 = new ToolStripLabel();
            toolStripSeparator3 = new ToolStripSeparator();
            ToolBar.SuspendLayout();
            panel1.SuspendLayout();
            ((ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((ISupportInitialize)Grid).BeginInit();
            ToolBar2.SuspendLayout();
            SuspendLayout();
            // 
            // ToolBar
            // 
            ToolBar.ImageScalingSize = new Size(24, 24);
            ToolBar.Items.AddRange(new ToolStripItem[] { btnAddTag, btnDeleteTag, toolStripSeparator1, toolStripSeparator2, btnAddComponentsToTag });
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
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 31);
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 31);
            // 
            // btnAddComponentsToTag
            // 
            btnAddComponentsToTag.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddComponentsToTag.Image = Properties.Resources.to_do_list_cheked;
            btnAddComponentsToTag.ImageTransparentColor = Color.Magenta;
            btnAddComponentsToTag.Name = "btnAddComponentsToTag";
            btnAddComponentsToTag.Size = new Size(28, 28);
            btnAddComponentsToTag.Text = "Add Components to Tag";
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
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 63);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(Grid);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(lboComponents);
            splitContainer1.Panel2.Controls.Add(ToolBar2);
            splitContainer1.Size = new Size(350, 598);
            splitContainer1.SplitterDistance = 375;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 7;
            // 
            // Grid
            // 
            Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Grid.Columns.AddRange(new DataGridViewColumn[] { coTagName });
            Grid.Dock = DockStyle.Fill;
            Grid.Location = new Point(0, 0);
            Grid.Name = "Grid";
            Grid.Size = new Size(348, 373);
            Grid.TabIndex = 0;
            // 
            // coTagName
            // 
            coTagName.DataPropertyName = "Name";
            coTagName.HeaderText = "Tag";
            coTagName.Name = "coTagName";
            // 
            // lboComponents
            // 
            lboComponents.Dock = DockStyle.Fill;
            lboComponents.FormattingEnabled = true;
            lboComponents.Location = new Point(0, 31);
            lboComponents.Name = "lboComponents";
            lboComponents.Size = new Size(348, 184);
            lboComponents.TabIndex = 2;
            // 
            // ToolBar2
            // 
            ToolBar2.ImageScalingSize = new Size(24, 24);
            ToolBar2.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripSeparator3, btnAddToQuickView });
            ToolBar2.Location = new Point(0, 0);
            ToolBar2.Name = "ToolBar2";
            ToolBar2.Size = new Size(348, 31);
            ToolBar2.TabIndex = 1;
            ToolBar2.Text = "toolStrip1";
            // 
            // btnAddToQuickView
            // 
            btnAddToQuickView.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddToQuickView.Image = Properties.Resources.wishlist_add;
            btnAddToQuickView.ImageTransparentColor = Color.Magenta;
            btnAddToQuickView.Name = "btnAddToQuickView";
            btnAddToQuickView.Size = new Size(28, 28);
            btnAddToQuickView.Text = "Add selected item to Quick View List";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(77, 28);
            toolStripLabel1.Text = "Components";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 31);
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
            splitContainer1.Panel2.PerformLayout();
            ((ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((ISupportInitialize)Grid).EndInit();
            ToolBar2.ResumeLayout(false);
            ToolBar2.PerformLayout();
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
        private DataGridView Grid;
        private DataGridViewTextBoxColumn coTagName;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnAddComponentsToTag;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStrip ToolBar2;
        private ToolStripButton btnAddToQuickView;
        private ListBox lboComponents;
        private ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator3;
    }
}
