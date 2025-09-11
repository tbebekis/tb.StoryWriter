namespace StoryWriter
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MainMenu = new MenuStrip();
            ToolBar = new ToolStrip();
            btnNewProject = new ToolStripButton();
            btnOpenProject = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            btnSettings = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            btnToggleSideBar = new ToolStripButton();
            btnToggleLog = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            btnExport = new ToolStripButton();
            btnImport = new ToolStripButton();
            toolStripSeparator6 = new ToolStripSeparator();
            btnExit = new ToolStripButton();
            StatusBar = new StatusStrip();
            splitMain = new SplitContainer();
            pagerSideBar = new TabControl();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            splitContent = new SplitContainer();
            pagerContent = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            edtLog = new RichTextBox();
            ToolBar.SuspendLayout();
            ((ISupportInitialize)splitMain).BeginInit();
            splitMain.Panel1.SuspendLayout();
            splitMain.Panel2.SuspendLayout();
            splitMain.SuspendLayout();
            pagerSideBar.SuspendLayout();
            ((ISupportInitialize)splitContent).BeginInit();
            splitContent.Panel1.SuspendLayout();
            splitContent.Panel2.SuspendLayout();
            splitContent.SuspendLayout();
            pagerContent.SuspendLayout();
            SuspendLayout();
            // 
            // MainMenu
            // 
            MainMenu.Location = new Point(0, 0);
            MainMenu.Name = "MainMenu";
            MainMenu.Size = new Size(871, 24);
            MainMenu.TabIndex = 0;
            MainMenu.Text = "menuStrip1";
            // 
            // ToolBar
            // 
            ToolBar.ImageScalingSize = new Size(24, 24);
            ToolBar.Items.AddRange(new ToolStripItem[] { btnNewProject, btnOpenProject, toolStripSeparator1, btnSettings, toolStripSeparator3, btnToggleSideBar, btnToggleLog, toolStripSeparator2, btnExport, btnImport, toolStripSeparator6, btnExit });
            ToolBar.Location = new Point(0, 24);
            ToolBar.Name = "ToolBar";
            ToolBar.Size = new Size(871, 31);
            ToolBar.TabIndex = 3;
            ToolBar.Text = "toolStrip1";
            // 
            // btnNewProject
            // 
            btnNewProject.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnNewProject.Image = Properties.Resources.book_add;
            btnNewProject.ImageTransparentColor = Color.Magenta;
            btnNewProject.Name = "btnNewProject";
            btnNewProject.Size = new Size(28, 28);
            btnNewProject.Text = "New Project";
            // 
            // btnOpenProject
            // 
            btnOpenProject.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnOpenProject.Image = Properties.Resources.book_edit;
            btnOpenProject.ImageTransparentColor = Color.Magenta;
            btnOpenProject.Name = "btnOpenProject";
            btnOpenProject.Size = new Size(28, 28);
            btnOpenProject.Text = "Open Project";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 31);
            // 
            // btnSettings
            // 
            btnSettings.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSettings.Image = Properties.Resources.setting_tools;
            btnSettings.ImageTransparentColor = Color.Magenta;
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(28, 28);
            btnSettings.Text = "Application Settings";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 31);
            // 
            // btnToggleSideBar
            // 
            btnToggleSideBar.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnToggleSideBar.Image = Properties.Resources.layout_sidebar;
            btnToggleSideBar.ImageTransparentColor = Color.Magenta;
            btnToggleSideBar.Name = "btnToggleSideBar";
            btnToggleSideBar.Size = new Size(28, 28);
            btnToggleSideBar.Text = "Toggle SideBar";
            // 
            // btnToggleLog
            // 
            btnToggleLog.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnToggleLog.Image = Properties.Resources.error_log;
            btnToggleLog.ImageTransparentColor = Color.Magenta;
            btnToggleLog.Name = "btnToggleLog";
            btnToggleLog.Size = new Size(28, 28);
            btnToggleLog.Text = "Toggle Log";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 31);
            // 
            // btnExport
            // 
            btnExport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnExport.Image = Properties.Resources.table_export;
            btnExport.ImageTransparentColor = Color.Magenta;
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(28, 28);
            btnExport.Text = "Export current Project";
            // 
            // btnImport
            // 
            btnImport.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnImport.Image = Properties.Resources.table_import;
            btnImport.ImageTransparentColor = Color.Magenta;
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(28, 28);
            btnImport.Text = "Import from file to a new Project";
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(6, 31);
            // 
            // btnExit
            // 
            btnExit.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnExit.Image = Properties.Resources.door_out;
            btnExit.ImageTransparentColor = Color.Magenta;
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(28, 28);
            btnExit.Text = "Exit";
            // 
            // StatusBar
            // 
            StatusBar.Location = new Point(0, 638);
            StatusBar.Name = "StatusBar";
            StatusBar.Size = new Size(871, 22);
            StatusBar.TabIndex = 4;
            StatusBar.Text = "statusStrip1";
            // 
            // splitMain
            // 
            splitMain.BorderStyle = BorderStyle.FixedSingle;
            splitMain.Dock = DockStyle.Fill;
            splitMain.Location = new Point(0, 55);
            splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            splitMain.Panel1.Controls.Add(pagerSideBar);
            // 
            // splitMain.Panel2
            // 
            splitMain.Panel2.Controls.Add(splitContent);
            splitMain.Size = new Size(871, 583);
            splitMain.SplitterDistance = 250;
            splitMain.SplitterWidth = 6;
            splitMain.TabIndex = 5;
            // 
            // pagerSideBar
            // 
            pagerSideBar.Controls.Add(tabPage3);
            pagerSideBar.Controls.Add(tabPage4);
            pagerSideBar.Dock = DockStyle.Fill;
            pagerSideBar.Location = new Point(0, 0);
            pagerSideBar.Name = "pagerSideBar";
            pagerSideBar.SelectedIndex = 0;
            pagerSideBar.Size = new Size(248, 581);
            pagerSideBar.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(240, 553);
            tabPage3.TabIndex = 0;
            tabPage3.Text = "tabPage3";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(255, 553);
            tabPage4.TabIndex = 1;
            tabPage4.Text = "tabPage4";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // splitContent
            // 
            splitContent.BorderStyle = BorderStyle.FixedSingle;
            splitContent.Dock = DockStyle.Fill;
            splitContent.Location = new Point(0, 0);
            splitContent.Name = "splitContent";
            splitContent.Orientation = Orientation.Horizontal;
            // 
            // splitContent.Panel1
            // 
            splitContent.Panel1.Controls.Add(pagerContent);
            // 
            // splitContent.Panel2
            // 
            splitContent.Panel2.Controls.Add(edtLog);
            splitContent.Size = new Size(615, 583);
            splitContent.SplitterDistance = 526;
            splitContent.SplitterWidth = 6;
            splitContent.TabIndex = 0;
            // 
            // pagerContent
            // 
            pagerContent.Controls.Add(tabPage1);
            pagerContent.Controls.Add(tabPage2);
            pagerContent.Dock = DockStyle.Fill;
            pagerContent.Location = new Point(0, 0);
            pagerContent.Name = "pagerContent";
            pagerContent.SelectedIndex = 0;
            pagerContent.Size = new Size(613, 524);
            pagerContent.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(605, 496);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(590, 496);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // edtLog
            // 
            edtLog.BackColor = Color.Gainsboro;
            edtLog.Dock = DockStyle.Fill;
            edtLog.Font = new Font("Courier New", 9F);
            edtLog.Location = new Point(0, 0);
            edtLog.Name = "edtLog";
            edtLog.Size = new Size(613, 49);
            edtLog.TabIndex = 0;
            edtLog.Text = "";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(871, 660);
            Controls.Add(splitMain);
            Controls.Add(StatusBar);
            Controls.Add(ToolBar);
            Controls.Add(MainMenu);
            MainMenuStrip = MainMenu;
            Name = "MainForm";
            Text = "Story Writer";
            WindowState = FormWindowState.Maximized;
            ToolBar.ResumeLayout(false);
            ToolBar.PerformLayout();
            splitMain.Panel1.ResumeLayout(false);
            splitMain.Panel2.ResumeLayout(false);
            ((ISupportInitialize)splitMain).EndInit();
            splitMain.ResumeLayout(false);
            pagerSideBar.ResumeLayout(false);
            splitContent.Panel1.ResumeLayout(false);
            splitContent.Panel2.ResumeLayout(false);
            ((ISupportInitialize)splitContent).EndInit();
            splitContent.ResumeLayout(false);
            pagerContent.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip MainMenu;
        private ToolStrip ToolBar;
        private ToolStripButton btnNewProject;
        private ToolStripButton btnOpenProject;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnSettings;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnToggleSideBar;
        private ToolStripButton btnToggleLog;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnExport;
        private ToolStripButton btnImport;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripButton btnExit;
        private StatusStrip StatusBar;
        private SplitContainer splitMain;
        private TabControl pagerSideBar;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private SplitContainer splitContent;
        private TabControl pagerContent;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private RichTextBox edtLog;
    }
}
