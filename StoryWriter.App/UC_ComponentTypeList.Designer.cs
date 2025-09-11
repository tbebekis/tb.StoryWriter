namespace StoryWriter
{
    partial class UC_ComponentTypeList
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
            btnAddComponentType = new ToolStripButton();
            btnEditComponentType = new ToolStripButton();
            btnDeleteComponentType = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            splitContainer1 = new SplitContainer();
            Grid = new DataGridView();
            coComponentType = new DataGridViewTextBoxColumn();
            lboComponents = new ListBox();
            ToolBar2 = new ToolStrip();
            toolStripLabel1 = new ToolStripLabel();
            toolStripSeparator2 = new ToolStripSeparator();
            btnAddToQuickView = new ToolStripButton();
            ToolBar.SuspendLayout();
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
            ToolBar.Items.AddRange(new ToolStripItem[] { btnAddComponentType, btnEditComponentType, btnDeleteComponentType, toolStripSeparator1 });
            ToolBar.Location = new Point(0, 0);
            ToolBar.Name = "ToolBar";
            ToolBar.Size = new Size(406, 31);
            ToolBar.TabIndex = 5;
            ToolBar.Text = "ToolBar";
            // 
            // btnAddComponentType
            // 
            btnAddComponentType.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddComponentType.Image = Properties.Resources.table_add;
            btnAddComponentType.ImageTransparentColor = Color.Magenta;
            btnAddComponentType.Name = "btnAddComponentType";
            btnAddComponentType.Size = new Size(28, 28);
            btnAddComponentType.Text = "Add";
            // 
            // btnEditComponentType
            // 
            btnEditComponentType.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEditComponentType.Image = Properties.Resources.table_edit;
            btnEditComponentType.ImageTransparentColor = Color.Magenta;
            btnEditComponentType.Name = "btnEditComponentType";
            btnEditComponentType.Size = new Size(28, 28);
            btnEditComponentType.Text = "Edit";
            // 
            // btnDeleteComponentType
            // 
            btnDeleteComponentType.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDeleteComponentType.Image = Properties.Resources.table_delete;
            btnDeleteComponentType.ImageTransparentColor = Color.Magenta;
            btnDeleteComponentType.Name = "btnDeleteComponentType";
            btnDeleteComponentType.Size = new Size(28, 28);
            btnDeleteComponentType.Text = "Remove";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 31);
            // 
            // splitContainer1
            // 
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
            splitContainer1.Panel2.Controls.Add(lboComponents);
            splitContainer1.Panel2.Controls.Add(ToolBar2);
            splitContainer1.Size = new Size(406, 670);
            splitContainer1.SplitterDistance = 334;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 9;
            // 
            // Grid
            // 
            Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Grid.Columns.AddRange(new DataGridViewColumn[] { coComponentType });
            Grid.Dock = DockStyle.Fill;
            Grid.Location = new Point(0, 0);
            Grid.Name = "Grid";
            Grid.Size = new Size(406, 334);
            Grid.TabIndex = 7;
            // 
            // coComponentType
            // 
            coComponentType.DataPropertyName = "Name";
            coComponentType.HeaderText = "Type";
            coComponentType.Name = "coComponentType";
            // 
            // lboComponents
            // 
            lboComponents.Dock = DockStyle.Fill;
            lboComponents.FormattingEnabled = true;
            lboComponents.Location = new Point(0, 31);
            lboComponents.Name = "lboComponents";
            lboComponents.Size = new Size(406, 299);
            lboComponents.TabIndex = 3;
            // 
            // ToolBar2
            // 
            ToolBar2.ImageScalingSize = new Size(24, 24);
            ToolBar2.Items.AddRange(new ToolStripItem[] { toolStripLabel1, toolStripSeparator2, btnAddToQuickView });
            ToolBar2.Location = new Point(0, 0);
            ToolBar2.Name = "ToolBar2";
            ToolBar2.Size = new Size(406, 31);
            ToolBar2.TabIndex = 2;
            ToolBar2.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(77, 28);
            toolStripLabel1.Text = "Components";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 31);
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
            // UC_ComponentTypeList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(ToolBar);
            Name = "UC_ComponentTypeList";
            Size = new Size(406, 701);
            ToolBar.ResumeLayout(false);
            ToolBar.PerformLayout();
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
        private ToolStripButton btnAddComponentType;
        private ToolStripButton btnEditComponentType;
        private ToolStripButton btnDeleteComponentType;
        private ToolStripSeparator toolStripSeparator1;
        private SplitContainer splitContainer1;
        private DataGridView Grid;
        private DataGridViewTextBoxColumn coComponentType;
        private ToolStrip ToolBar2;
        private ToolStripButton btnAddToQuickView;
        private ListBox lboComponents;
        private ToolStripLabel toolStripLabel1;
        private ToolStripSeparator toolStripSeparator2;
    }
}
