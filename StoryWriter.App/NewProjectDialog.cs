namespace StoryWriter
{

    public partial class NewProjectDialog : Form
    {
        void FormInitialize()
        {
            AcceptButton = btnOK;
            CancelButton = btnCancel;

            edtProjectName.Focus();

            btnOK.Click += (s, e) =>
            {
                string S = edtProjectName.Text.Trim();
                if (!Project.IsValidProjectName(S))
                {
                    App.ErrorBox("The project name is invalid.");
                    return;
                }

                DialogResult = DialogResult.OK;
            };
        }

        // ● overrides
        protected override void OnShown(EventArgs e)
        {
            if (!DesignMode)
                FormInitialize();
            base.OnShown(e);
        }

        public NewProjectDialog()
        {
            InitializeComponent();
        }

        static public bool ShowModal(out string ProjectName)
        {
            ProjectName = null;
            using (NewProjectDialog dlg = new NewProjectDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ProjectName = dlg.edtProjectName.Text.Trim();
                    return true;
                }
            }
            return false;
        }
    }
}
