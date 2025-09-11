namespace StoryWriter
{
    public partial class SelectItemsForItemDialog : Form
    {
        BaseEntity ForItem;
        List<BaseEntity> ItemList;           // initally all Tags - on OK only the selected ones        
        List<string> ItemSelectedIdList;     // Tag Ids under this Component

        List<BaseEntity> AvailList = new();
        List<BaseEntity> SelectedList = new();

        // ● private
        void FormInitialize()
        {
            AcceptButton = btnOK;
            CancelButton = btnCancel;

            ItemToControls();
            btnOK.Click += (s, e) => ControlsToItem();

            btnSelectAll.Click += (s, e) => SelectAll();
            btnUnselectAll.Click += (s, e) => UnSelectAll();
            btnSelectOne.Click += (s, e) => SelectOne();
            btnUnselectOne.Click += (s, e) => UnSelectOne();

            lboAvail.MouseDoubleClick += (s, e) => SelectOne();
            lboSelected.MouseDoubleClick += (s, e) => UnSelectOne();

        }
        void ItemToControls()
        {
            edtItemName.Text = ForItem.Name;

            for (int i = 0; i < ItemList.Count; i++)
            {
                BaseEntity Item = ItemList[i];
                if (ItemSelectedIdList.Contains(Item.Id))
                    SelectedList.Add(Item);
                else
                    AvailList.Add(Item);
            }

            UpdateListBoxes();
        }
        void ControlsToItem()
        {
            ItemList.Clear();
            ItemList.AddRange(SelectedList.ToArray());

            DialogResult = DialogResult.OK;
        }
        
        void UpdateListBox(ListBox Box, List<BaseEntity> List)
        {
            Box.BeginUpdate();
            Box.Items.Clear();
            Box.Items.AddRange(List.ToArray());
            Box.EndUpdate();
        }
        void UpdateListBoxes()
        {
            UpdateListBox(lboAvail, AvailList);
            UpdateListBox(lboSelected, SelectedList);
        }
        void SelectAll()
        {
            SelectedList.AddRange(AvailList);
            AvailList.Clear();
            UpdateListBoxes();
        }
        void UnSelectAll()
        {
            AvailList.AddRange(SelectedList);
            SelectedList.Clear();
            UpdateListBoxes();
        }
        void SelectOne()
        {
            if (lboAvail.SelectedItem != null)
            {
                BaseEntity Item = lboAvail.SelectedItem as BaseEntity;
                int LastSelectedIndex = lboAvail.SelectedIndex;
                AvailList.Remove(Item);
                SelectedList.Add(Item);
                UpdateListBoxes();

                lboSelected.SelectedItem = Item;

                SetSelectedAfterSelectOne(lboAvail, LastSelectedIndex);
            }
                
        }
        void UnSelectOne()
        {
            if (lboSelected.SelectedItem != null)
            {
                BaseEntity Item = lboSelected.SelectedItem as BaseEntity;
                int LastSelectedIndex = lboSelected.SelectedIndex;
                SelectedList.Remove(Item);
                AvailList.Add(Item);
                UpdateListBoxes();

                lboAvail.SelectedItem = Item;

                SetSelectedAfterSelectOne(lboSelected, LastSelectedIndex);
            }
 
        }
        void SetSelectedAfterSelectOne(ListBox Box, int LastSelectedIndex)
        {
            if (LastSelectedIndex > 0 && Box.Items.Count > 0)
            {
                Box.SelectedIndex = LastSelectedIndex - 1;                
            }
        }


        // ● overrides
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            if (!DesignMode)
                FormInitialize();
        }

        // ● construction
        public SelectItemsForItemDialog()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Displays the dialog
        /// </summary>
        /// <param name="ItemList"> Initally all Items, e.g. Tags - on OK only the selected ones</param>
        /// <param name="ForItem"> The Item to select items for. </param>
        /// <param name="ItemSelectedIdList">Item Ids already selected for this Item</param>
        /// <returns></returns>
        static public bool ShowModal(string Title, List<BaseEntity> ItemList, BaseEntity ForItem, List<string> ItemSelectedIdList)
        {
            using (SelectItemsForItemDialog dlg = new ())
            {
                dlg.Text = Title;
                dlg.ItemList = ItemList;
                dlg.ForItem = ForItem;
                dlg.ItemSelectedIdList = ItemSelectedIdList;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
