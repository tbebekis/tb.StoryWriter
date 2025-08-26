namespace StoryWriter
{
    public partial class MainForm : Form
    {
        PagerHandler SideBarPagerHandler;
        PagerHandler ContentPagerHandler;
        

        const string STitle = "Story Writer";

        // ● private
        void FormInitialize()
        {
            Ui.MainForm = this;

            LogBox.Initialize(edtLog);

            this.Text = $"{STitle} - [none]";

            pagerSideBar.TabPages.Clear();
            pagerContent.TabPages.Clear();

            SideBarPagerHandler = new PagerHandler(pagerSideBar, typeof(TabPage));
            ContentPagerHandler = new PagerHandler(pagerContent, typeof(TabPage));

            SideBarPagerHandler.SelectedTabColor = Color.LemonChiffon; 
            ContentPagerHandler.SelectedTabColor = Color.LemonChiffon;

            SideBarPagerHandler.SelectedFontColor = Color.Black;
            ContentPagerHandler.SelectedFontColor = Color.Black;

            App.SideBarPagerHandler = SideBarPagerHandler;
            App.ContentPagerHandler = ContentPagerHandler;      
            
            App.ProjectClosed += (s, e) => this.Text = $"{STitle} - [none]"; 
            App.ProjectOpened += (s, e) => this.Text = $"{STitle} - [{App.CurrentProject.Name}]";

            AddToolBarControls();            

            btnNewProject.Click += (s, e) => App.CreateNewProject();
            btnOpenProject.Click += (s, e) => App.OpenProject();
            btnSettings.Click += (s, e) => App.ShowSettingsDialog();
            btnToggleSideBar.Click += (s, e) => ToggleSideBar();
            btnToggleLog.Click += (s, e) => ToggleLog();
            btnExportToText.Click += (s, e) => ExportToFile(btnExportToText);
            btnExportToRtf.Click += (s, e) => ExportToFile(btnExportToRtf);
            btnExportToDocx.Click += (s, e) => ExportToFile(btnExportToDocx);
            btnExportToOdt.Click += (s, e) => ExportToFile(btnExportToOdt);
            btnExit.Click += (s, e) => Close();

            App.ZoomFactor = App.Settings.ZoomFactor;
            App.Initialize(this);

        }
        void AddToolBarControls()
        {
            // ● Zoom Factor
            NumericUpDown nudZoom = new() 
            {
                Minimum = 0.5M,
                Maximum = 5.0M,
                DecimalPlaces = 2,
                Increment = 0.05M,
                Value = (decimal)App.Settings.ZoomFactor,
                BorderStyle = BorderStyle.FixedSingle,

            };
            nudZoom.ValueChanged += (s, e) => 
            { 
                App.ZoomFactor = nudZoom.Value;
                App.Settings.ZoomFactor = nudZoom.Value;
                App.Settings.Save();
            };

            ToolStripControlHost hostZoom = new (nudZoom)
            {
                AutoSize = false,
                Width = 50
            };

            int Index = ToolBar.Items.IndexOf(btnExit);

            Index--;
            ToolStripSeparator sepZoom = new();
            ToolBar.Items.Insert(Index, sepZoom);

            Index++;
            ToolBar.Items.Insert(Index, new ToolStripLabel("Zoom"));

            Index++;
            ToolBar.Items.Insert(Index, hostZoom);
        }

        void ToggleSideBar()
        {
            splitMain.Panel1Collapsed = !splitMain.Panel1Collapsed;
        }
        void ToggleLog()
        {
            splitContent.Panel2Collapsed = !splitContent.Panel2Collapsed;            
        }

        public void ExportToFile(ToolStripButton Button)
        {
            string Filter = "Text files (*.txt)|*.txt|All Files (*.*)|*.*";
            Action<string> ExportProc = App.ExportCurrentProjectToTxt;

            if (Button == btnExportToRtf)
            {
                Filter = "RTF Files (*.rtf)|*.rtf|All Files (*.*)|*.*";
                ExportProc = App.ExportCurrentProjectToRtf;
            }
            else if (Button == btnExportToDocx)
            {
                Filter = "Word Files (*.docx)|*.docx|All Files (*.*)|*.*";
                ExportProc = App.ExportCurrentProjectToDocx;
            }
            else if (Button == btnExportToOdt)
            {
                Filter = "ODF Text Document (*.odt)|*.odt|All Files (*.*)|*.*";
                ExportProc = App.ExportCurrentProjectToOdt;
            }

            using (SaveFileDialog dlg = new ())
            {
                dlg.Filter = Filter;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string FilePath = dlg.FileName;
                    ExportProc(FilePath);

                    LogBox.AppendLine($"{App.CurrentProject.Name} exported to {FilePath}");
                }
            }

        }

        // ● overrides
        protected override void OnShown(EventArgs e)
        {
            if (!DesignMode)
                FormInitialize();
            base.OnShown(e);
        }

        // ● construction
        public MainForm()
        {
            InitializeComponent();
        }
    }
}
