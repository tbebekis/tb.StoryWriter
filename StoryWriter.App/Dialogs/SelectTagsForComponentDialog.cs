namespace StoryWriter
{
    public partial class SelectTagsForComponentDialog : Form
    {
        Component Component;
        List<Tag> TagList;                  // initally all Tags - on OK only the selected ones        
        List<string> ComponentTagsList;     // Tag Ids under this Component

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
                if (ComponentTagsList.Contains(Tag.Id))
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
        public SelectTagsForComponentDialog()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Displays the dialog
        /// </summary>
        /// <param name="TagList"> Initally all Tags - on OK only the selected ones</param>
        /// <param name="Component"> The Component </param>
        /// <param name="ComponentTagsList">Tag Ids under this Component</param>
        /// <returns></returns>
        static public bool ShowModal(List<Tag> TagList, Component Component, List<string> ComponentTagsList)
        {
            using (SelectTagsForComponentDialog dlg = new ())
            {
                dlg.TagList = TagList;
                dlg.Component = Component;
                dlg.ComponentTagsList = ComponentTagsList;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
