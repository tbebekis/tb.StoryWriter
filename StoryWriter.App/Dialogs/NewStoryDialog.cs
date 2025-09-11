namespace StoryWriter
{

    public partial class NewStoryDialog : Form
    {
        void FormInitialize()
        {
            AcceptButton = btnOK;
            CancelButton = btnCancel;

            edtProjectName.Focus();

            btnOK.Click += (s, e) =>
            {
                string S = edtProjectName.Text.Trim();
                if (!Story.IsValidStoryName(S))
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

        public NewStoryDialog()
        {
            InitializeComponent();
        }

        static public bool ShowModal(out string ProjectName)
        {
            ProjectName = null;
            using (NewStoryDialog dlg = new NewStoryDialog())
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
