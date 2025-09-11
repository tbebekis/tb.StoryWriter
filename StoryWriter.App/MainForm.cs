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

            SideBarPagerHandler.SelectedTabColor = Color.Orange; // Color.Coral; // Color.LemonChiffon;
            ContentPagerHandler.SelectedTabColor = Color.Orange; // Color.Coral; // Color.LemonChiffon;

            SideBarPagerHandler.SelectedFontColor = Color.Black;
            ContentPagerHandler.SelectedFontColor = Color.Black;

            App.SideBarPagerHandler = SideBarPagerHandler;
            App.ContentPagerHandler = ContentPagerHandler;

            App.StoryClosed += (s, e) => this.Text = $"{STitle} - [none]";
            App.StoryOpened += (s, e) => this.Text = $"{STitle} - [{App.CurrentStory.Name}]";

            AddToolBarControls();
            /// 
            btnNewProject.Click += (s, e) => App.CreateNewStory(LoadToo: true);
            btnOpenProject.Click += (s, e) => App.SelectStoryToOpen();
            btnSettings.Click += (s, e) => App.ShowSettingsDialog();
            btnToggleSideBar.Click += (s, e) => ToggleSideBar();
            btnToggleLog.Click += (s, e) => ToggleLog();
            btnExport.Click += (s, e) => App.Export();
            btnImport.Click += (s, e) => App.Import();

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

            ToolStripControlHost hostZoom = new(nudZoom)
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
