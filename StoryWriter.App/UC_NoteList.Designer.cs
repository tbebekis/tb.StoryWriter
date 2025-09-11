namespace StoryWriter
{
    partial class UC_NoteList
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
            btnAddNote = new ToolStripButton();
            btnEditNote = new ToolStripButton();
            btnDeleteNote = new ToolStripButton();
            panel2 = new Panel();
            edtFilter = new TextBox();
            label2 = new Label();
            splitContainer1 = new SplitContainer();
            Grid = new DataGridView();
            ucRichText = new UC_RichText();
            panel1 = new Panel();
            lblItemTitle = new Label();
            coName = new DataGridViewTextBoxColumn();
            toolStripSeparator1 = new ToolStripSeparator();
            btnAddToQuickView = new ToolStripButton();
            btnEditRtfText = new ToolStripButton();
            ToolBar.SuspendLayout();
            panel2.SuspendLayout();
            ((ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((ISupportInitialize)Grid).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // ToolBar
            // 
            ToolBar.ImageScalingSize = new Size(24, 24);
            ToolBar.Items.AddRange(new ToolStripItem[] { btnAddNote, btnEditNote, btnDeleteNote, toolStripSeparator1, btnEditRtfText, btnAddToQuickView });
            ToolBar.Location = new Point(0, 0);
            ToolBar.Name = "ToolBar";
            ToolBar.Size = new Size(415, 31);
            ToolBar.TabIndex = 5;
            ToolBar.Text = "ToolBar";
            // 
            // btnAddComponent
            // 
            btnAddNote.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddNote.Image = Properties.Resources.table_add;
            btnAddNote.ImageTransparentColor = Color.Magenta;
            btnAddNote.Name = "btnAddComponent";
            btnAddNote.Size = new Size(28, 28);
            btnAddNote.Text = "Add";
            // 
            // btnEditComponent
            // 
            btnEditNote.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEditNote.Image = Properties.Resources.table_edit;
            btnEditNote.ImageTransparentColor = Color.Magenta;
            btnEditNote.Name = "btnEditComponent";
            btnEditNote.Size = new Size(28, 28);
            btnEditNote.Text = "Edit";
            // 
            // btnDeleteComponent
            // 
            btnDeleteNote.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDeleteNote.Image = Properties.Resources.table_delete;
            btnDeleteNote.ImageTransparentColor = Color.Magenta;
            btnDeleteNote.Name = "btnDeleteComponent";
            btnDeleteNote.Size = new Size(28, 28);
            btnDeleteNote.Text = "Remove";
            // 
            // panel2
            // 
            panel2.Controls.Add(edtFilter);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 31);
            panel2.Name = "panel2";
            panel2.Size = new Size(415, 33);
            panel2.TabIndex = 7;
            // 
            // edtFilter
            // 
            edtFilter.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            edtFilter.Location = new Point(56, 5);
            edtFilter.Name = "edtFilter";
            edtFilter.Size = new Size(351, 23);
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
            // splitContainer1
            // 
            splitContainer1.BackColor = SystemColors.Control;
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 64);
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
            splitContainer1.Panel2.Controls.Add(panel1);
            splitContainer1.Size = new Size(415, 563);
            splitContainer1.SplitterDistance = 295;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 8;
            // 
            // Grid
            // 
            Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Grid.Columns.AddRange(new DataGridViewColumn[] { coName });
            Grid.Dock = DockStyle.Fill;
            Grid.Location = new Point(0, 0);
            Grid.Name = "Grid";
            Grid.Size = new Size(413, 293);
            Grid.TabIndex = 1;
            // 
            // ucRichText
            // 
            ucRichText.Dock = DockStyle.Fill;
            ucRichText.Location = new Point(0, 25);
            ucRichText.Name = "ucRichText";
            ucRichText.Size = new Size(413, 235);
            ucRichText.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblItemTitle);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(413, 25);
            panel1.TabIndex = 1;
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
            // coName
            // 
            coName.DataPropertyName = "Name";
            coName.HeaderText = "Name";
            coName.Name = "coName";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 31);
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
            // btnEditRtfText
            // 
            btnEditRtfText.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEditRtfText.Image = Properties.Resources.page_edit;
            btnEditRtfText.ImageTransparentColor = Color.Magenta;
            btnEditRtfText.Name = "btnEditRtfText";
            btnEditRtfText.Size = new Size(28, 28);
            btnEditRtfText.Text = "Edit Text";
            // 
            // UC_NoteList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(panel2);
            Controls.Add(ToolBar);
            Name = "UC_NoteList";
            Size = new Size(415, 627);
            ToolBar.ResumeLayout(false);
            ToolBar.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((ISupportInitialize)Grid).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip ToolBar;
        private ToolStripButton btnAddNote;
        private ToolStripButton btnEditNote;
        private ToolStripButton btnDeleteNote;
        private Panel panel2;
        private TextBox edtFilter;
        private Label label2;
        private SplitContainer splitContainer1;
        private DataGridView Grid;
        private UC_RichText ucRichText;
        private Panel panel1;
        private Label lblItemTitle;
        private DataGridViewTextBoxColumn coName;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnAddToQuickView;
        private ToolStripButton btnEditRtfText;
    }
}
