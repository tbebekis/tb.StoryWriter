namespace StoryWriter
{
    public partial class AppSettingDialog : Form
    {
        // ● private
        AppSettings Settings = new();
 
        void FormInitialize()
        {
            AcceptButton = btnOK;
            CancelButton = btnCancel;

            cboFontFamily.Focus();

            PopulateFonts();
            ItemToControls();

            btnOK.Click += (s, e) => ControlsToItem(); 
        }
        void PopulateFonts()
        {
            try
            {
                var fonts = new InstalledFontCollection()
                    .Families
                    .Select(f => f.Name)
                    .OrderBy(n => n)
                    .ToArray();

                cboFontFamily.Items.AddRange(fonts);

                // if the selected font is in the list, select it
                if (!string.IsNullOrWhiteSpace(Settings.FontFamily))
                {
                    var idx = cboFontFamily.FindStringExact(Settings.FontFamily);
                    if (idx >= 0)
                        cboFontFamily.SelectedIndex = idx;
                    else
                        cboFontFamily.Text = Settings.FontFamily;
                }
            }
            catch
            {
                // In case of errors, ignore
                if (string.IsNullOrWhiteSpace(cboFontFamily.Text))
                    cboFontFamily.Text = Settings.FontFamily ?? "Times New Roman";
            }
        }

        void ItemToControls()
        {
            chkLoadLast.Checked = Settings.LoadLastProjectOnStartup;
            chkAutoSave.Checked = Settings.AutoSave; 
            var safeSize = Math.Max((int)nudFontSize.Minimum, Math.Min((int)nudFontSize.Maximum, Settings.FontSize));
            nudFontSize.Value = safeSize;
        }
        void ControlsToItem()
        {
            Settings.LoadLastProjectOnStartup = chkLoadLast.Checked;
            Settings.AutoSave = chkAutoSave.Checked;
            Settings.FontFamily = string.IsNullOrWhiteSpace(cboFontFamily.Text) ? "Arial" : cboFontFamily.Text.Trim();
            Settings.FontSize = (int)nudFontSize.Value;

            this.DialogResult = DialogResult.OK;
        }


        // ● overrides
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (!DesignMode)
                FormInitialize();
        }

        // ● construction
        public AppSettingDialog()
        {
            InitializeComponent();
        }

        // ● static
        static public bool ShowModal()
        {
            using (AppSettingDialog dlg = new AppSettingDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    return true;
            }
            return false;
        }
    }
}
