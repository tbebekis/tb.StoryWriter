namespace StoryWriter
{
    public partial class SelectComponentsForTagDialog : Form
    {

        Tag TagItem;
        List<Component> ComponentList;      // initally all Components - on OK only the selected ones
        List<string> TagComponentsList;     // Component Ids under this Tag

        // ● private
        void FormInitialize()
        {
            AcceptButton = btnOK;
            CancelButton = btnCancel;

            ItemToControls();
            btnOK.Click += (s, e) => ControlsToItem();
        }
        void ItemToControls()
        {
            edtTagName.Text = TagItem.Name;

            lboComponents.BeginUpdate();

            lboComponents.Items.AddRange(ComponentList.ToArray());

            for (int i = 0; i < ComponentList.Count; i++)
            {
                Component Component = ComponentList[i];
                if (TagComponentsList.Contains(Component.Id))
                {
                    lboComponents.SetItemChecked(i, true);
                }
            }

            lboComponents.EndUpdate();
        }
        void ControlsToItem()
        {
            ComponentList.Clear();

            for (int i = 0; i < lboComponents.Items.Count; i++)
            {
                Component Component = lboComponents.Items[i] as Component;
                if (lboComponents.GetItemChecked(i))
                    ComponentList.Add(Component);
            }

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
        public SelectComponentsForTagDialog()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Displays the dialog
        /// </summary>
        /// <param name="ComponentList">Initally all Components - on OK only the selected ones</param>
        /// <param name="TagItem">The Tag</param>
        /// <param name="TagComponentsList">Component Ids under this Tag</param>
        /// <returns></returns>
        static public bool ShowModal(List<Component> ComponentList, Tag TagItem, List<string> TagComponentsList)
        {
            using (SelectComponentsForTagDialog dlg = new())
            {
                dlg.ComponentList = ComponentList;
                dlg.TagItem = TagItem;
                dlg.TagComponentsList = TagComponentsList;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
