namespace StoryWriter
{
    public partial class AppSettingDialog : Form
    {
        // ● private
        AppSettings Settings = App.Settings;
 
        void FormInitialize()
        {             
            
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
            void SetNumericValue(NumericUpDown Control, int SettingsValue)
            {
                Control.Value = Math.Max((int)Control.Minimum, Math.Min((int)Control.Maximum, SettingsValue));
            }

            chkLoadLast.Checked = Settings.LoadLastProjectOnStartup;
            chkAutoSave.Checked = Settings.AutoSave;
            SetNumericValue(edtAutoSaveSecondsInterval, Settings.AutoSaveSecondsInterval);
            SetNumericValue(nudFontSize, Settings.FontSize); 

            mmoDefaultTags.Text = string.Join(Environment.NewLine, Settings.DefaultTags);
        }
        void ControlsToItem()
        {
            Settings.LoadLastProjectOnStartup = chkLoadLast.Checked;
            Settings.AutoSave = chkAutoSave.Checked;
            Settings.AutoSaveSecondsInterval = (int)edtAutoSaveSecondsInterval.Value;
            Settings.FontFamily = string.IsNullOrWhiteSpace(cboFontFamily.Text) ? "Arial" : cboFontFamily.Text.Trim();
            Settings.FontSize = (int)nudFontSize.Value;

            Settings.DefaultTags.Clear();
            foreach (string Line in mmoDefaultTags.Lines)
            {
                if (!string.IsNullOrWhiteSpace(Line))
                    Settings.DefaultTags.Add(Line.Trim());
            }

            Settings.Save();

            string Message = $"Application Settings saved.";
            LogBox.AppendLine(Message);

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
