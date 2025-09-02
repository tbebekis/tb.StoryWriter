namespace StoryWriter
{
    public partial class ExportOptionsDialog : Form
    {

        // ● private
        void FormInitialize()
        {
            AcceptButton = btnOK;
            CancelButton = btnCancel;

            lboFormats.DataSource = Enum.GetValues<ExportMode>();
            lboFormats.SelectedItem = ExportMode.JSON;
            lboFormats.MouseDoubleClick += (s, e) => btnOK.PerformClick();

            btnOK.Click += (s, e) => ControlsToItem();
        }
        void ControlsToItem()
        {
            DialogResult = DialogResult.OK;
        }

        // ● overrides
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (!DesignMode)
                FormInitialize();
        }

        // ● construction
        public ExportOptionsDialog()
        {
            InitializeComponent();
        }


        static public bool ShowModal(ExportService Service)
        {
            using (ExportOptionsDialog dlg = new ())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Service.ExportMode = (ExportMode)dlg.lboFormats.SelectedIndex;
                    //Service.RtfToPlainText = dlg.chConvertRtfToPlainText.Checked;
                    return true;
                }
            }

            return false;
        }
    }
}
