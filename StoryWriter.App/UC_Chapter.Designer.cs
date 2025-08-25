namespace StoryWriter
{
    partial class UC_Chapter
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
            pagerChapter = new TabControl();
            tabBodyText = new TabPage();
            ucBodyText = new UC_RichText();
            tabScenes = new TabPage();
            splitContainer1 = new SplitContainer();
            lboScenes = new ListBox();
            ToolBarScenes = new ToolStrip();
            btnAdd = new ToolStripButton();
            btnEdit = new ToolStripButton();
            btnDelete = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnUp = new ToolStripButton();
            btnDown = new ToolStripButton();
            ucSceneText = new UC_RichText();
            tabSynopsis = new TabPage();
            ucSynopsisText = new UC_RichText();
            tabConcept = new TabPage();
            ucConceptText = new UC_RichText();
            tabOutcome = new TabPage();
            ucOutcomeText = new UC_RichText();
            pagerChapter.SuspendLayout();
            tabBodyText.SuspendLayout();
            tabScenes.SuspendLayout();
            ((ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ToolBarScenes.SuspendLayout();
            tabSynopsis.SuspendLayout();
            tabConcept.SuspendLayout();
            tabOutcome.SuspendLayout();
            SuspendLayout();
            // 
            // pagerChapter
            // 
            pagerChapter.Controls.Add(tabBodyText);
            pagerChapter.Controls.Add(tabScenes);
            pagerChapter.Controls.Add(tabSynopsis);
            pagerChapter.Controls.Add(tabConcept);
            pagerChapter.Controls.Add(tabOutcome);
            pagerChapter.Dock = DockStyle.Fill;
            pagerChapter.Location = new Point(0, 0);
            pagerChapter.Name = "pagerChapter";
            pagerChapter.SelectedIndex = 0;
            pagerChapter.Size = new Size(863, 727);
            pagerChapter.TabIndex = 1;
            // 
            // tabBodyText
            // 
            tabBodyText.Controls.Add(ucBodyText);
            tabBodyText.Location = new Point(4, 24);
            tabBodyText.Name = "tabBodyText";
            tabBodyText.Padding = new Padding(3);
            tabBodyText.Size = new Size(855, 699);
            tabBodyText.TabIndex = 0;
            tabBodyText.Text = "Text";
            tabBodyText.UseVisualStyleBackColor = true;
            // 
            // ucBodyText
            // 
            ucBodyText.Dock = DockStyle.Fill;
            ucBodyText.Location = new Point(3, 3);
            ucBodyText.Name = "ucBodyText";
            ucBodyText.Size = new Size(849, 693);
            ucBodyText.TabIndex = 0;
            // 
            // tabScenes
            // 
            tabScenes.Controls.Add(splitContainer1);
            tabScenes.Location = new Point(4, 24);
            tabScenes.Name = "tabScenes";
            tabScenes.Padding = new Padding(3);
            tabScenes.Size = new Size(855, 699);
            tabScenes.TabIndex = 1;
            tabScenes.Text = "Scenes";
            tabScenes.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(3, 3);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(lboScenes);
            splitContainer1.Panel1.Controls.Add(ToolBarScenes);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(ucSceneText);
            splitContainer1.Size = new Size(849, 693);
            splitContainer1.SplitterDistance = 160;
            splitContainer1.SplitterWidth = 6;
            splitContainer1.TabIndex = 0;
            // 
            // lboScenes
            // 
            lboScenes.Dock = DockStyle.Fill;
            lboScenes.FormattingEnabled = true;
            lboScenes.Location = new Point(0, 31);
            lboScenes.Name = "lboScenes";
            lboScenes.Size = new Size(160, 662);
            lboScenes.TabIndex = 1;
            // 
            // ToolBarScenes
            // 
            ToolBarScenes.ImageScalingSize = new Size(24, 24);
            ToolBarScenes.Items.AddRange(new ToolStripItem[] { btnAdd, btnEdit, btnDelete, toolStripSeparator1, btnUp, btnDown });
            ToolBarScenes.Location = new Point(0, 0);
            ToolBarScenes.Name = "ToolBarScenes";
            ToolBarScenes.Size = new Size(160, 31);
            ToolBarScenes.TabIndex = 2;
            ToolBarScenes.Text = "toolStrip1";
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
            // ucSceneText
            // 
            ucSceneText.Dock = DockStyle.Fill;
            ucSceneText.Location = new Point(0, 0);
            ucSceneText.Name = "ucSceneText";
            ucSceneText.Size = new Size(683, 693);
            ucSceneText.TabIndex = 0;
            // 
            // tabSynopsis
            // 
            tabSynopsis.Controls.Add(ucSynopsisText);
            tabSynopsis.Location = new Point(4, 24);
            tabSynopsis.Name = "tabSynopsis";
            tabSynopsis.Padding = new Padding(3);
            tabSynopsis.Size = new Size(855, 699);
            tabSynopsis.TabIndex = 2;
            tabSynopsis.Text = "Synopsis";
            tabSynopsis.UseVisualStyleBackColor = true;
            // 
            // ucSynopsisText
            // 
            ucSynopsisText.Dock = DockStyle.Fill;
            ucSynopsisText.Location = new Point(3, 3);
            ucSynopsisText.Name = "ucSynopsisText";
            ucSynopsisText.Size = new Size(849, 693);
            ucSynopsisText.TabIndex = 0;
            // 
            // tabConcept
            // 
            tabConcept.Controls.Add(ucConceptText);
            tabConcept.Location = new Point(4, 24);
            tabConcept.Name = "tabConcept";
            tabConcept.Padding = new Padding(3);
            tabConcept.Size = new Size(855, 699);
            tabConcept.TabIndex = 3;
            tabConcept.Text = "Concept";
            tabConcept.UseVisualStyleBackColor = true;
            // 
            // ucConceptText
            // 
            ucConceptText.Dock = DockStyle.Fill;
            ucConceptText.Location = new Point(3, 3);
            ucConceptText.Name = "ucConceptText";
            ucConceptText.Size = new Size(849, 693);
            ucConceptText.TabIndex = 0;
            // 
            // tabOutcome
            // 
            tabOutcome.Controls.Add(ucOutcomeText);
            tabOutcome.Location = new Point(4, 24);
            tabOutcome.Name = "tabOutcome";
            tabOutcome.Padding = new Padding(3);
            tabOutcome.Size = new Size(855, 699);
            tabOutcome.TabIndex = 4;
            tabOutcome.Text = "Outcome";
            tabOutcome.UseVisualStyleBackColor = true;
            // 
            // ucOutcomeText
            // 
            ucOutcomeText.Dock = DockStyle.Fill;
            ucOutcomeText.Location = new Point(3, 3);
            ucOutcomeText.Name = "ucOutcomeText";
            ucOutcomeText.Size = new Size(849, 693);
            ucOutcomeText.TabIndex = 0;
            // 
            // UC_Chapter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pagerChapter);
            Name = "UC_Chapter";
            Size = new Size(863, 727);
            pagerChapter.ResumeLayout(false);
            tabBodyText.ResumeLayout(false);
            tabScenes.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ToolBarScenes.ResumeLayout(false);
            ToolBarScenes.PerformLayout();
            tabSynopsis.ResumeLayout(false);
            tabConcept.ResumeLayout(false);
            tabOutcome.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TabControl pagerChapter;
        private TabPage tabBodyText;
        private TabPage tabScenes;
        private TabPage tabSynopsis;
        private TabPage tabConcept;
        private TabPage tabOutcome;
        private SplitContainer splitContainer1;
        private ListBox lboScenes;
        private UC_RichText ucBodyText;
        private UC_RichText ucSynopsisText;
        private UC_RichText ucConceptText;
        private UC_RichText ucOutcomeText;
        private UC_RichText ucSceneText;
        private ToolStrip ToolBarScenes;
        private ToolStripButton btnAdd;
        private ToolStripButton btnEdit;
        private ToolStripButton btnDelete;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnUp;
        private ToolStripButton btnDown;
    }
}
