namespace StoryWriter
{
    partial class UC_ComponentTree
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
            tvComponents = new TreeView();
            mnuComponents = new ContextMenuStrip(components);
            mnuAddTopGroup = new ToolStripMenuItem();
            mnuAddSubGroup = new ToolStripMenuItem();
            mnuAddComponent = new ToolStripMenuItem();
            mnuEditComponent = new ToolStripMenuItem();
            mnuDeleteComponent = new ToolStripMenuItem();
            ToolBar = new ToolStrip();
            btnAddTopGroup = new ToolStripButton();
            btnAddSubGroup = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnAddComponent = new ToolStripButton();
            btnEditComponent = new ToolStripButton();
            btnDeleteComponent = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnEditComponentText = new ToolStripButton();
            mnuComponents.SuspendLayout();
            ToolBar.SuspendLayout();
            SuspendLayout();
            // 
            // tvComponents
            // 
            tvComponents.ContextMenuStrip = mnuComponents;
            tvComponents.Dock = DockStyle.Fill;
            tvComponents.Location = new Point(0, 31);
            tvComponents.Name = "tvComponents";
            tvComponents.Size = new Size(217, 590);
            tvComponents.TabIndex = 2;
            // 
            // mnuComponents
            // 
            mnuComponents.Items.AddRange(new ToolStripItem[] { mnuAddTopGroup, mnuAddSubGroup, mnuAddComponent, mnuEditComponent, mnuDeleteComponent });
            mnuComponents.Name = "mnuComponents";
            mnuComponents.Size = new Size(155, 114);
            // 
            // mnuAddTopGroup
            // 
            mnuAddTopGroup.Name = "mnuAddTopGroup";
            mnuAddTopGroup.Size = new Size(154, 22);
            mnuAddTopGroup.Text = "Add Top Group";
            // 
            // mnuAddSubGroup
            // 
            mnuAddSubGroup.Name = "mnuAddSubGroup";
            mnuAddSubGroup.Size = new Size(154, 22);
            mnuAddSubGroup.Text = "Add Group";
            // 
            // mnuAddComponent
            // 
            mnuAddComponent.Name = "mnuAddComponent";
            mnuAddComponent.Size = new Size(154, 22);
            mnuAddComponent.Text = "Add";
            // 
            // mnuEditComponent
            // 
            mnuEditComponent.Name = "mnuEditComponent";
            mnuEditComponent.Size = new Size(154, 22);
            mnuEditComponent.Text = "Edit";
            // 
            // mnuDeleteComponent
            // 
            mnuDeleteComponent.Name = "mnuDeleteComponent";
            mnuDeleteComponent.Size = new Size(154, 22);
            mnuDeleteComponent.Text = "Delete";
            // 
            // ToolBar
            // 
            ToolBar.ImageScalingSize = new Size(24, 24);
            ToolBar.Items.AddRange(new ToolStripItem[] { btnAddTopGroup, btnAddSubGroup, toolStripSeparator1, btnAddComponent, btnEditComponent, btnDeleteComponent, toolStripSeparator2, btnEditComponentText });
            ToolBar.Location = new Point(0, 0);
            ToolBar.Name = "ToolBar";
            ToolBar.Size = new Size(217, 31);
            ToolBar.TabIndex = 3;
            ToolBar.Text = "ToolBar";
            // 
            // btnAddTopGroup
            // 
            btnAddTopGroup.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddTopGroup.Image = Properties.Resources.table_insert;
            btnAddTopGroup.ImageTransparentColor = Color.Magenta;
            btnAddTopGroup.Name = "btnAddTopGroup";
            btnAddTopGroup.Size = new Size(28, 28);
            btnAddTopGroup.Text = "Add Top Group";
            // 
            // btnAddSubGroup
            // 
            btnAddSubGroup.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddSubGroup.Image = Properties.Resources.table_replace;
            btnAddSubGroup.ImageTransparentColor = Color.Magenta;
            btnAddSubGroup.Name = "btnAddSubGroup";
            btnAddSubGroup.Size = new Size(28, 28);
            btnAddSubGroup.Text = "Add SubGroup";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 31);
            // 
            // btnAddComponent
            // 
            btnAddComponent.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnAddComponent.Image = Properties.Resources.table_add;
            btnAddComponent.ImageTransparentColor = Color.Magenta;
            btnAddComponent.Name = "btnAddComponent";
            btnAddComponent.Size = new Size(28, 28);
            btnAddComponent.Text = "Add Component";
            // 
            // btnEditComponent
            // 
            btnEditComponent.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEditComponent.Image = Properties.Resources.table_edit;
            btnEditComponent.ImageTransparentColor = Color.Magenta;
            btnEditComponent.Name = "btnEditComponent";
            btnEditComponent.Size = new Size(28, 28);
            btnEditComponent.Text = "Edit Component";
            // 
            // btnDeleteComponent
            // 
            btnDeleteComponent.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnDeleteComponent.Image = Properties.Resources.table_delete;
            btnDeleteComponent.ImageTransparentColor = Color.Magenta;
            btnDeleteComponent.Name = "btnDeleteComponent";
            btnDeleteComponent.Size = new Size(28, 28);
            btnDeleteComponent.Text = "Delete Component";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 31);
            // 
            // btnEditComponentText
            // 
            btnEditComponentText.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnEditComponentText.Image = Properties.Resources.page_edit;
            btnEditComponentText.ImageTransparentColor = Color.Magenta;
            btnEditComponentText.Name = "btnEditComponentText";
            btnEditComponentText.Size = new Size(28, 28);
            btnEditComponentText.Text = "Edit Component Text";
            // 
            // UC_ComponentTree
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tvComponents);
            Controls.Add(ToolBar);
            Name = "UC_ComponentTree";
            Size = new Size(217, 621);
            mnuComponents.ResumeLayout(false);
            ToolBar.ResumeLayout(false);
            ToolBar.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TreeView tvComponents;
        private ContextMenuStrip mnuComponents;
        private ToolStripMenuItem mnuAddTopGroup;
        private ToolStripMenuItem mnuAddSubGroup;
        private ToolStripMenuItem mnuAddComponent;
        private ToolStripMenuItem mnuEditComponent;
        private ToolStripMenuItem mnuDeleteComponent;
        private ToolStrip ToolBar;
        private ToolStripButton btnAddTopGroup;
        private ToolStripButton btnAddSubGroup;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnAddComponent;
        private ToolStripButton btnEditComponent;
        private ToolStripButton btnDeleteComponent;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnEditComponentText;
    }
}
