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
            ToolBar = new ToolStrip();
            btnAdd = new ToolStripButton();
            btnEdit = new ToolStripButton();
            btnDelete = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnUp = new ToolStripButton();
            btnDown = new ToolStripButton();
            btnEditRtfText = new ToolStripButton();
            lboChapters = new ListBox();
            ToolBar.SuspendLayout();
            SuspendLayout();
            // 
            // ToolBar
            // 
            ToolBar.ImageScalingSize = new Size(24, 24);
            ToolBar.Items.AddRange(new ToolStripItem[] { btnAdd, btnEdit, btnDelete, toolStripSeparator1, btnUp, btnDown, btnEditRtfText });
            ToolBar.Location = new Point(0, 0);
            ToolBar.Name = "ToolBar";
            ToolBar.Size = new Size(217, 31);
            ToolBar.TabIndex = 0;
            ToolBar.Text = "toolStrip1";
            // 
            // btnAdd
            // 
            btnAdd.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAdd.Image = Properties.Resources.table_add;
            btnAdd.ImageTransparentColor = Color.Magenta;
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(28, 28);
            btnAdd.Text = "Add";
            // 
            // btnEdit
            // 
            btnEdit.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEdit.Image = Properties.Resources.table_edit;
            btnEdit.ImageTransparentColor = Color.Magenta;
            btnEdit.Name = "btnEdit";
            btnEdit.Size = new Size(28, 28);
            btnEdit.Text = "Edit";
            // 
            // btnDelete
            // 
            btnDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDelete.Image = Properties.Resources.table_delete;
            btnDelete.ImageTransparentColor = Color.Magenta;
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(28, 28);
            btnDelete.Text = "Remove";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 31);
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
            // btnEditRtfText
            // 
            btnEditRtfText.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEditRtfText.Image = Properties.Resources.page_edit;
            btnEditRtfText.ImageTransparentColor = Color.Magenta;
            btnEditRtfText.Name = "btnEditRtfText";
            btnEditRtfText.Size = new Size(28, 28);
            btnEditRtfText.Text = "Edit Text";
            // 
            // lboChapters
            // 
            lboChapters.Dock = DockStyle.Fill;
            lboChapters.FormattingEnabled = true;
            lboChapters.Location = new Point(0, 31);
            lboChapters.Name = "lboChapters";
            lboChapters.Size = new Size(217, 590);
            lboChapters.TabIndex = 1;
            // 
            // UC_ChapterList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lboChapters);
            Controls.Add(ToolBar);
            Name = "UC_ChapterList";
            Size = new Size(217, 621);
            ToolBar.ResumeLayout(false);
            ToolBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip ToolBar;
        private ToolStripButton btnAdd;
        private ToolStripButton btnEdit;
        private ToolStripButton btnDelete;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnUp;
        private ToolStripButton btnDown;
        private ListBox lboChapters;
        private ToolStripButton btnEditRtfText;
    }
}
