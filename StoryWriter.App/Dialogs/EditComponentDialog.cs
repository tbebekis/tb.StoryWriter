namespace StoryWriter
{
    public partial class EditComponentDialog : Form
    {
        Component Component;

        void FormInitialize()
        {
            AcceptButton = btnOK;
            CancelButton = btnCancel;

            List<ComponentType> ComponentTypeList = new(App.CurrentStory.ComponentTypeList);
            ComponentTypeList = ComponentTypeList.OrderBy(x => x.Name).ToList();
            lboComponentTypes.Items.AddRange(ComponentTypeList.ToArray());

            ComponentType SelectedType = ComponentTypeList.FirstOrDefault(x => x.Id == Component.TypeId);
            lboComponentTypes.SelectedIndex = SelectedType == null ? 0 : ComponentTypeList.IndexOf(SelectedType);
            
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
            lboComponentTypes.MouseDoubleClick += (s, e) => btnOK.PerformClick();

        }

        // ● overrides
        protected override void OnShown(EventArgs e)
        {
            if (!DesignMode)
                FormInitialize();
            base.OnShown(e);
        }

        public EditComponentDialog()
        {
            InitializeComponent();
        }


        static public bool ShowModal(string Title, Component Component)
        {
            //string NameToEdit = ResultName;

            using (EditComponentDialog dlg = new())
            {
                dlg.edtName.Text = Component.Name;
                dlg.Text = Title;
                dlg.Component = Component;
 
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    Component.Name = dlg.edtName.Text.Trim();
                    Component.TypeId = (dlg.lboComponentTypes.SelectedItem as ComponentType).Id;
                    return true;
                }
            }
            return false;
        }
    }
}
