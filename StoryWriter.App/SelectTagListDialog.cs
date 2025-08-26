namespace StoryWriter
{
    public partial class SelectTagListDialog : Form
    {
        List<Tag> TagList;
        Component Component;
        List<string> ComponentTagList;

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
            edtComponentName.Text = Component.Name;

            lboTags.BeginUpdate();

            lboTags.Items.AddRange(TagList.ToArray());

            for (int i = 0; i < TagList.Count; i++)
            {
                Tag Tag = TagList[i];
                if (ComponentTagList.Contains(Tag.Id))
                {
                    lboTags.SetItemChecked(i, true);
                }
            }

            lboTags.EndUpdate();
        }
        void ControlsToItem()
        {
            TagList.Clear();

            for (int i = 0; i < lboTags.Items.Count; i++)
            {
                Tag Tag = lboTags.Items[i] as Tag;
                if (lboTags.GetItemChecked(i))
                    TagList.Add(Tag);
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
        public SelectTagListDialog()
        {
            InitializeComponent();
        }



        static public bool ShowModal(List<Tag> TagList, Component Component, List<string> ComponentTagList)
        {
            using (SelectTagListDialog dlg = new ())
            {
                dlg.TagList = TagList;
                dlg.Component = Component;
                dlg.ComponentTagList = ComponentTagList;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
