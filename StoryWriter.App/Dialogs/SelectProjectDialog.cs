namespace StoryWriter
{
    public partial class SelectProjectDialog : Form
    {
        void FormInitialize()
        {
            AcceptButton = btnOK;
            CancelButton = btnCancel;

            if (lboProjectNames.Items.Count > 0)
                lboProjectNames.SelectedIndex = 0;

            btnOK.Click += (s, e) => 
            {
                if (lboProjectNames.SelectedItem == null)
                {
                   App.WarningBox("Please select a project.");
                    return;
                }

                DialogResult = DialogResult.OK;
            };
            lboProjectNames.MouseDoubleClick += (s, e) => btnOK.PerformClick();
        }

        // ● overrides
        protected override void OnShown(EventArgs e)
        {
            if (!DesignMode)
                FormInitialize();
            base.OnShown(e);
        }

        public SelectProjectDialog()
        {
            InitializeComponent();
        }

        static public bool ShowModal(List<string> ProjectNames, out string SelectedProjectName)
        {
            SelectedProjectName = null;
            using (SelectProjectDialog dlg = new SelectProjectDialog())
            {
                dlg.lboProjectNames.Items.AddRange(ProjectNames.ToArray());
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.lboProjectNames.SelectedItem != null)
                    {
                        SelectedProjectName = dlg.lboProjectNames.SelectedItem.ToString();
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
