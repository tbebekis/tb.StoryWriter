namespace StoryWriter
{
    public partial class EditItemDialog : Form
    {
        void FormInitialize()
        {
            AcceptButton = btnOK;
            CancelButton = btnCancel; 

            edtName.Focus();

            btnOK.Click += (s, e) =>
            {
                if (string.IsNullOrEmpty(edtName.Text.Trim()))
                {
                    App.WarningBox("Please enter a name.");
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

        public EditItemDialog()
        {
            InitializeComponent();
        }

        static public bool ShowModal(string Title, string ParentName, ref string ResultName)
        {
            //string NameToEdit = ResultName;
            
            using (EditItemDialog dlg = new ())
            {
                dlg.edtName.Text = ResultName;
                dlg.Text = Title; 

                dlg.edtParentName.Text = ParentName;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ResultName = dlg.edtName.Text.Trim();
                    return true;
                }
            }
            return false;
        }
    }
}
