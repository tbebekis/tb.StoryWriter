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
            panel1 = new Panel();
            comboBox1 = new ComboBox();
            label1 = new Label();
            panel2 = new Panel();
            comboBox2 = new ComboBox();
            label2 = new Label();
            Grid = new DataGridView();
            coGroup = new DataGridViewTextBoxColumn();
            coSubGroup = new DataGridViewTextBoxColumn();
            coName = new DataGridViewTextBoxColumn();
            ToolBar.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((ISupportInitialize)Grid).BeginInit();
            SuspendLayout();
            // 
            // ToolBar
            // 
            ToolBar.ImageScalingSize = new Size(24, 24);
            ToolBar.Items.AddRange(new ToolStripItem[] { btnAddComponent, btnEditComponent, btnDeleteComponent });
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
            // panel1
            // 
            panel1.Controls.Add(comboBox1);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 31);
            panel1.Name = "panel1";
            panel1.Size = new Size(351, 32);
            panel1.TabIndex = 5;
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(83, 5);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(260, 23);
            comboBox1.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 9);
            label1.Name = "label1";
            label1.Size = new Size(40, 15);
            label1.TabIndex = 0;
            label1.Text = "Group";
            // 
            // panel2
            // 
            panel2.Controls.Add(comboBox2);
            panel2.Controls.Add(label2);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 63);
            panel2.Name = "panel2";
            panel2.Size = new Size(351, 33);
            panel2.TabIndex = 6;
            // 
            // comboBox2
            // 
            comboBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(83, 5);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(259, 23);
            comboBox2.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 9);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 0;
            label2.Text = "Sub-Group";
            // 
            // Grid
            // 
            Grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Grid.Columns.AddRange(new DataGridViewColumn[] { coGroup, coSubGroup, coName });
            Grid.Dock = DockStyle.Fill;
            Grid.Location = new Point(0, 96);
            Grid.Name = "Grid";
            Grid.Size = new Size(351, 531);
            Grid.TabIndex = 7;
            // 
            // coGroup
            // 
            coGroup.DataPropertyName = "Group";
            coGroup.HeaderText = "Group";
            coGroup.Name = "coGroup";
            // 
            // coSubGroup
            // 
            coSubGroup.DataPropertyName = "SubGroup";
            coSubGroup.HeaderText = "SubGroup";
            coSubGroup.Name = "coSubGroup";
            // 
            // coName
            // 
            coName.DataPropertyName = "Name";
            coName.HeaderText = "Name";
            coName.Name = "coName";
            // 
            // UC_ComponentList
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(Grid);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(ToolBar);
            Name = "UC_ComponentList";
            Size = new Size(351, 627);
            ToolBar.ResumeLayout(false);
            ToolBar.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((ISupportInitialize)Grid).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip ToolBar;
        private ToolStripButton btnAddComponent;
        private ToolStripButton btnEditComponent;
        private ToolStripButton btnDeleteComponent;
        private Panel panel1;
        private ComboBox comboBox1;
        private Label label1;
        private Panel panel2;
        private ComboBox comboBox2;
        private Label label2;
        private DataGridView Grid;
        private DataGridViewTextBoxColumn coGroup;
        private DataGridViewTextBoxColumn coSubGroup;
        private DataGridViewTextBoxColumn coName;
    }
}
