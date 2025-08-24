namespace StoryWriter
{
    public partial class MainForm : Form
    {
        PagerHandler SideBarPagerHandler;
        PagerHandler ContentPagerHandler; 

        // ● private
        void FormInitialize()
        {
            Ui.MainForm = this;

            LogBox.Initialize(edtLog);

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

            App.Initialize(this);

            btnNewProject.Click += (s, e) => App.CreateNewProject();
            btnOpenProject.Click += (s, e) => App.OpenProject();
            btnSettings.Click += (s, e) => App.ShowSettingsDialog();
            btnToggleSideBar.Click += (s, e) => ToggleSideBar();
            btnToggleLog.Click += (s, e) => ToggleLog();
            btnExportToRtf.Click += (s, e) => ExportToRtf();
            btnExportToDocx.Click += (s, e) => ExportToDocx();
            btnExportToOdt.Click += (s, e) => ExportToOdt();
            btnExit.Click += (s, e) => Close();
 
        }

        void ToggleSideBar()
        {
            splitMain.Panel1Collapsed = !splitMain.Panel1Collapsed;
        }
        void ToggleLog()
        {
            splitContent.Panel2Collapsed = !splitContent.Panel2Collapsed;            
        }

        void ExportToRtf()
        {
            if (App.CurrentProject == null) 
                return;

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "RTF Files (*.rtf)|*.rtf|All Files (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string FilePath = dlg.FileName;
                    App.ExportCurrentProjectToRtf(FilePath);

                    LogBox.AppendLine($"{App.CurrentProject.Name} exported to {FilePath}");
                }
            } 
        }
        void ExportToDocx()
        {
            if (App.CurrentProject == null)
                return;

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "Word Files (*.docx)|*.docx|All Files (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string FilePath = dlg.FileName;
                    App.ExportCurrentProjectToDocx(FilePath);

                    LogBox.AppendLine($"{App.CurrentProject.Name} exported to {FilePath}");
                }
            }
        }
        void ExportToOdt()
        {
            if (App.CurrentProject == null)
                return;

            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "ODF Text Document (*.odt)|*.odt|All Files (*.*)|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    string FilePath = dlg.FileName;
                    App.ExportCurrentProjectToOdt(FilePath);

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
