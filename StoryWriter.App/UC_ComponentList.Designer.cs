namespace StoryWriter
{
    partial class UC_ComponentList
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
            btnAddComponent = new ToolStripButton();
            btnEditComponent = new ToolStripButton();
            btnDeleteComponent = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnEditRtfText = new ToolStripButton();
            btnAdjustComponentTags = new ToolStripButton();
            btnAddToQuickView = new ToolStripButton();
            panel2 = new Panel();
            edtFilter = new TextBox();
            label2 = new Label();
            gridComponents = new DataGridView();
            coComponent = new DataGridViewTextBoxColumn();
            coCategory = new DataGridViewTextBoxColumn();
            splitContainer1 = new SplitContainer();
            lboTags = new ListBox();
            Pager = new TabControl();
            tabText = new TabPage();
            tabTags = new TabPage();
            panel1 = new Panel();
            lblTitle = new Label();
            ucRichText = new UC_RichText();
            ToolBar.SuspendLayout();
            panel2.SuspendLayout();
            ((ISupportInitialize)gridComponents).BeginInit();
            ((ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            Pager.SuspendLayout();
            tabText.SuspendLayout();
            tabTags.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ToolBar
            // 
            ToolBar.ImageScalingSize = new Size(24, 24);
            ToolBar.Items.AddRange(new ToolStripItem[] { btnAddComponent, btnEditComponent, btnDeleteComponent, toolStripSeparator1, btnEditRtfText, btnAdjustComponentTags, btnAddToQuickView });
            ToolBar.Location = new Point(0, 0);
            ToolBar.Name = "ToolBar";
            ToolBar.Size = new Size(351, 31);
            ToolBar.TabIndex = 4;
            ToolBar.Text = "ToolBar";
            // 
            // btnAddComponent
            // 
            btnAddComponent.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddComponent.Image = Properties.Resources.table_add;
            btnAddComponent.ImageTransparentColor = Color.Magenta;
            btnAddComponent.Name = "btnAddComponent";
            btnAddComponent.Size = new Size(28, 28);
            btnAddComponent.Text = "Add";
            // 
            // btnEditComponent
            // 
            btnEditComponent.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEditComponent.Image = Properties.Resources.table_edit;
            btnEditComponent.ImageTransparentColor = Color.Magenta;
            btnEditComponent.Name = "btnEditComponent";
            btnEditComponent.Size = new Size(28, 28);
            btnEditComponent.Text = "Edit";
            // 
            // btnDeleteComponent
            // 
            btnDeleteComponent.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDeleteComponent.Image = Properties.Resources.table_delete;
            btnDeleteComponent.ImageTransparentColor = Color.Magenta;
            btnDeleteComponent.Name = "btnDeleteComponent";
            btnDeleteComponent.Size = new Size(28, 28);
            btnDeleteComponent.Text = "Remove";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 31);
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
            // btnAdjustComponentTags
            // 
            btnAdjustComponentTags.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAdjustComponentTags.Image = Properties.Resources.to_do_list_cheked;
            btnAdjustComponentTags.ImageTransparentColor = Color.Magenta;
            btnAdjustComponentTags.Name = "btnAdjustComponentTags";
            btnAdjustComponentTags.Size = new Size(28, 28);
            btnAdjustComponentTags.Text = "Adjust Component Tags";
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
            // panel2
            // 
            panel2.Controls.Add(edtFilter);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 31);
            panel2.Name = "panel2";
            panel2.Size = new Size(351, 33);
            panel2.TabIndex = 6;
            // 
            // edtFilter
            // 
            edtFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtFilter.Location = new Point(56, 5);
            edtFilter.Name = "edtFilter";
            edtFilter.Size = new Size(290, 23);
            edtFilter.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 0;
            label2.Text = "Filter";
            // 
            // gridComponents
            // 
            gridComponents.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridComponents.Columns.AddRange(new DataGridViewColumn[] { coComponent, coCategory });
            gridComponents.Dock = DockStyle.Fill;
            gridComponents.Location = new Point(0, 0);
            gridComponents.Name = "gridComponents";
            gridComponents.Size = new Size(351, 281);
            gridComponents.TabIndex = 7;
            // 
            // coComponent
            // 
            coComponent.DataPropertyName = "Name";
            coComponent.HeaderText = "Component";
            coComponent.Name = "coComponent";
            // 
            // coCategory
            // 
            coCategory.DataPropertyName = "Category";
            coCategory.HeaderText = "Category";
            coCategory.Name = "coCategory";
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 64);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(gridComponents);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(Pager);
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Size = new Size(351, 563);
            splitContainer1.SplitterDistance = 281;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 8;
            // 
            // lboTags
            // 
            lboTags.Dock = DockStyle.Fill;
            lboTags.FormattingEnabled = true;
            lboTags.Location = new Point(3, 3);
            lboTags.Name = "lboTags";
            lboTags.Size = new Size(337, 218);
            lboTags.TabIndex = 4;
            // 
            // Pager
            // 
            Pager.Controls.Add(tabText);
            Pager.Controls.Add(tabTags);
            Pager.Dock = DockStyle.Fill;
            Pager.Location = new Point(0, 24);
            Pager.Name = "Pager";
            Pager.SelectedIndex = 0;
            Pager.Size = new Size(351, 252);
            Pager.TabIndex = 5;
            // 
            // tabText
            // 
            tabText.Controls.Add(ucRichText);
            tabText.Location = new Point(4, 24);
            tabText.Name = "tabText";
            tabText.Padding = new Padding(3);
            tabText.Size = new Size(343, 224);
            tabText.TabIndex = 0;
            tabText.Text = "Text";
            tabText.UseVisualStyleBackColor = true;
            // 
            // tabTags
            // 
            tabTags.Controls.Add(lboTags);
            tabTags.Location = new Point(4, 24);
            tabTags.Name = "tabTags";
            tabTags.Padding = new Padding(3);
            tabTags.Size = new Size(343, 224);
            tabTags.TabIndex = 1;
            tabTags.Text = "Tags";
            tabTags.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblTitle);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(351, 24);
            panel1.TabIndex = 6;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblTitle.Location = new Point(2, 2);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(97, 20);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "No Selection";
            // 
            // ucRichText
            // 
            ucRichText.Dock = DockStyle.Fill;
            ucRichText.Location = new Point(3, 3);
            ucRichText.Name = "ucRichText";
            ucRichText.Size = new Size(337, 218);
            ucRichText.TabIndex = 0;
            // 
            // UC_ComponentList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(panel2);
            Controls.Add(ToolBar);
            Name = "UC_ComponentList";
            Size = new Size(351, 627);
            ToolBar.ResumeLayout(false);
            ToolBar.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((ISupportInitialize)gridComponents).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            Pager.ResumeLayout(false);
            tabText.ResumeLayout(false);
            tabTags.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip ToolBar;
        private ToolStripButton btnAddComponent;
        private ToolStripButton btnEditComponent;
        private ToolStripButton btnDeleteComponent;
        private Panel panel2;
        private Label label2;
        private DataGridView gridComponents;
        private TextBox edtFilter;
        private SplitContainer splitContainer1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnEditRtfText;
        private ToolStripButton btnAdjustComponentTags;
        private ToolStripButton btnAddToQuickView;
        private DataGridViewTextBoxColumn coComponent;
        private DataGridViewTextBoxColumn coCategory;
        private ListBox lboTags;
        private TabControl Pager;
        private TabPage tabText;
        private TabPage tabTags;
        private Panel panel1;
        private Label lblTitle;
        private UC_RichText ucRichText;
    }
}
