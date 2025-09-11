namespace StoryWriter
{
    partial class UC_ChapterList
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
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(UC_ChapterList));
            ToolBar = new ToolStrip();
            btnAddItem = new ToolStripDropDownButton();
            mnuAddChapter = new ToolStripMenuItem();
            mnuAddScene = new ToolStripMenuItem();
            btnEditItem = new ToolStripButton();
            btnDeleteItem = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnEditRtfText = new ToolStripButton();
            btnAdjustSceneComponents = new ToolStripButton();
            btnUp = new ToolStripButton();
            btnDown = new ToolStripButton();
            Splitter = new SplitContainer();
            tv = new TreeView();
            tvImages = new ImageList(components);
            Pager = new TabControl();
            tabText = new TabPage();
            ucBodyText = new UC_RichText();
            tabSynopsis = new TabPage();
            ucSynopsis = new UC_RichText();
            tabComponents = new TabPage();
            lboComponents = new ListBox();
            panel1 = new Panel();
            lblTitle = new Label();
            ToolBar.SuspendLayout();
            ((ISupportInitialize)Splitter).BeginInit();
            Splitter.Panel1.SuspendLayout();
            Splitter.Panel2.SuspendLayout();
            Splitter.SuspendLayout();
            Pager.SuspendLayout();
            tabText.SuspendLayout();
            tabSynopsis.SuspendLayout();
            tabComponents.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ToolBar
            // 
            ToolBar.ImageScalingSize = new Size(24, 24);
            ToolBar.Items.AddRange(new ToolStripItem[] { btnAddItem, btnEditItem, btnDeleteItem, toolStripSeparator1, btnEditRtfText, btnAdjustSceneComponents, btnUp, btnDown });
            ToolBar.Location = new Point(0, 0);
            ToolBar.Name = "ToolBar";
            ToolBar.Size = new Size(430, 31);
            ToolBar.TabIndex = 5;
            ToolBar.Text = "ToolBar";
            // 
            // btnAddItem
            // 
            btnAddItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddItem.DropDownItems.AddRange(new ToolStripItem[] { mnuAddChapter, mnuAddScene });
            btnAddItem.Image = Properties.Resources.table_add;
            btnAddItem.ImageTransparentColor = Color.Magenta;
            btnAddItem.Name = "btnAddItem";
            btnAddItem.Size = new Size(37, 28);
            btnAddItem.Text = "Add Chapter or Scene";
            // 
            // mnuAddChapter
            // 
            mnuAddChapter.Name = "mnuAddChapter";
            mnuAddChapter.Size = new Size(141, 22);
            mnuAddChapter.Text = "Add Chapter";
            // 
            // mnuAddScene
            // 
            mnuAddScene.Name = "mnuAddScene";
            mnuAddScene.Size = new Size(141, 22);
            mnuAddScene.Text = "Add Scene";
            // 
            // btnEditItem
            // 
            btnEditItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEditItem.Image = Properties.Resources.table_edit;
            btnEditItem.ImageTransparentColor = Color.Magenta;
            btnEditItem.Name = "btnEditItem";
            btnEditItem.Size = new Size(28, 28);
            btnEditItem.Text = "Edit";
            // 
            // btnDeleteItem
            // 
            btnDeleteItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDeleteItem.Image = Properties.Resources.table_delete;
            btnDeleteItem.ImageTransparentColor = Color.Magenta;
            btnDeleteItem.Name = "btnDeleteItem";
            btnDeleteItem.Size = new Size(28, 28);
            btnDeleteItem.Text = "Remove";
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
            // btnAdjustSceneComponents
            // 
            btnAdjustSceneComponents.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAdjustSceneComponents.Image = Properties.Resources.to_do_list_cheked;
            btnAdjustSceneComponents.ImageTransparentColor = Color.Magenta;
            btnAdjustSceneComponents.Name = "btnAdjustSceneComponents";
            btnAdjustSceneComponents.Size = new Size(28, 28);
            btnAdjustSceneComponents.Text = "Adjust Scene Components";
            // 
            // btnUp
            // 
            btnUp.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnUp.Image = Properties.Resources.arrow_up;
            btnUp.ImageTransparentColor = Color.Magenta;
            btnUp.Name = "btnUp";
            btnUp.Size = new Size(28, 28);
            btnUp.Text = "Move Up";
            // 
            // btnDown
            // 
            btnDown.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDown.Image = Properties.Resources.arrow_down;
            btnDown.ImageTransparentColor = Color.Magenta;
            btnDown.Name = "btnDown";
            btnDown.Size = new Size(28, 28);
            btnDown.Text = "Move Down";
            // 
            // Splitter
            // 
            Splitter.Dock = DockStyle.Fill;
            Splitter.Location = new Point(0, 31);
            Splitter.Name = "Splitter";
            Splitter.Orientation = Orientation.Horizontal;
            // 
            // Splitter.Panel1
            // 
            Splitter.Panel1.Controls.Add(tv);
            // 
            // Splitter.Panel2
            // 
            Splitter.Panel2.Controls.Add(Pager);
            Splitter.Panel2.Controls.Add(panel1);
            Splitter.Size = new Size(430, 677);
            Splitter.SplitterDistance = 305;
            Splitter.SplitterWidth = 6;
            Splitter.TabIndex = 6;
            // 
            // tv
            // 
            tv.Dock = DockStyle.Fill;
            tv.HideSelection = false;
            tv.ImageIndex = 0;
            tv.ImageList = tvImages;
            tv.Location = new Point(0, 0);
            tv.Name = "tv";
            tv.SelectedImageIndex = 0;
            tv.Size = new Size(430, 305);
            tv.TabIndex = 0;
            // 
            // tvImages
            // 
            tvImages.ColorDepth = ColorDepth.Depth32Bit;
            tvImages.ImageStream = (ImageListStreamer)resources.GetObject("tvImages.ImageStream");
            tvImages.TransparentColor = Color.Transparent;
            tvImages.Images.SetKeyName(0, "Folder.png");
            tvImages.Images.SetKeyName(1, "Table.png");
            // 
            // Pager
            // 
            Pager.Controls.Add(tabText);
            Pager.Controls.Add(tabSynopsis);
            Pager.Controls.Add(tabComponents);
            Pager.Dock = DockStyle.Fill;
            Pager.Location = new Point(0, 24);
            Pager.Name = "Pager";
            Pager.SelectedIndex = 0;
            Pager.Size = new Size(430, 342);
            Pager.TabIndex = 1;
            // 
            // tabText
            // 
            tabText.Controls.Add(ucBodyText);
            tabText.Location = new Point(4, 24);
            tabText.Name = "tabText";
            tabText.Padding = new Padding(3);
            tabText.Size = new Size(422, 314);
            tabText.TabIndex = 0;
            tabText.Text = "Text";
            tabText.UseVisualStyleBackColor = true;
            // 
            // ucBodyText
            // 
            ucBodyText.Dock = DockStyle.Fill;
            ucBodyText.Location = new Point(3, 3);
            ucBodyText.Name = "ucBodyText";
            ucBodyText.Size = new Size(416, 308);
            ucBodyText.TabIndex = 0;
            // 
            // tabSynopsis
            // 
            tabSynopsis.Controls.Add(ucSynopsis);
            tabSynopsis.Location = new Point(4, 24);
            tabSynopsis.Name = "tabSynopsis";
            tabSynopsis.Padding = new Padding(3);
            tabSynopsis.Size = new Size(422, 314);
            tabSynopsis.TabIndex = 2;
            tabSynopsis.Text = "Synopsis";
            tabSynopsis.UseVisualStyleBackColor = true;
            // 
            // ucSynopsis
            // 
            ucSynopsis.Dock = DockStyle.Fill;
            ucSynopsis.Location = new Point(3, 3);
            ucSynopsis.Name = "ucSynopsis";
            ucSynopsis.Size = new Size(416, 308);
            ucSynopsis.TabIndex = 0;
            // 
            // tabComponents
            // 
            tabComponents.Controls.Add(lboComponents);
            tabComponents.Location = new Point(4, 24);
            tabComponents.Name = "tabComponents";
            tabComponents.Padding = new Padding(3);
            tabComponents.Size = new Size(422, 314);
            tabComponents.TabIndex = 1;
            tabComponents.Text = "Components";
            tabComponents.UseVisualStyleBackColor = true;
            // 
            // lboComponents
            // 
            lboComponents.Dock = DockStyle.Fill;
            lboComponents.FormattingEnabled = true;
            lboComponents.Location = new Point(3, 3);
            lboComponents.Name = "lboComponents";
            lboComponents.Size = new Size(416, 308);
            lboComponents.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblTitle);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(430, 24);
            panel1.TabIndex = 2;
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
            // UC_ChapterList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Splitter);
            Controls.Add(ToolBar);
            Name = "UC_ChapterList";
            Size = new Size(430, 708);
            ToolBar.ResumeLayout(false);
            ToolBar.PerformLayout();
            Splitter.Panel1.ResumeLayout(false);
            Splitter.Panel2.ResumeLayout(false);
            ((ISupportInitialize)Splitter).EndInit();
            Splitter.ResumeLayout(false);
            Pager.ResumeLayout(false);
            tabText.ResumeLayout(false);
            tabSynopsis.ResumeLayout(false);
            tabComponents.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip ToolBar;
        private ToolStripButton btnEditItem;
        private ToolStripButton btnDeleteItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnEditRtfText;
        private ToolStripButton btnAdjustSceneComponents;
        private SplitContainer Splitter;
        private TreeView tv;
        private ListBox lboComponents;
        private ImageList tvImages;
        private ToolStripButton btnUp;
        private ToolStripButton btnDown;
        private ToolStripDropDownButton btnAddItem;
        private ToolStripMenuItem mnuAddChapter;
        private ToolStripMenuItem mnuAddScene;
        private TabControl Pager;
        private TabPage tabText;
        private TabPage tabComponents;
        private UC_RichText ucBodyText;
        private Panel panel1;
        private Label lblTitle;
        private TabPage tabSynopsis;
        private UC_RichText ucSynopsis;
    }
}
