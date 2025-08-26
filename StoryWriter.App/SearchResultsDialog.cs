namespace StoryWriter
{
    public partial class SearchResultsDialog : Form
    {
        List<LinkItem> LinkItems;

        // ● private
        void FormInitialize()
        {
            AcceptButton = btnOK;
            CancelButton = btnCancel;

            ItemToControls();
            btnOK.Click += (s, e) => ControlsToItem();

            Grid.MouseDoubleClick += (s, e) => ControlsToItem();
        }
        void ItemToControls()
        {
            DataTable tblLinkItems = new ();

            tblLinkItems.Columns.Add("Type", typeof(string));
            tblLinkItems.Columns.Add("Name", typeof(string));
            tblLinkItems.Columns.Add("OBJECT", typeof(object));

            foreach (var item in LinkItems)
            {
                tblLinkItems.Rows.Add(item.ItemType.ToString(), item.Name, item);
            }

            tblLinkItems.AcceptChanges();

            Grid.AutoGenerateColumns = false;
            Grid.DataSource = tblLinkItems;
            Grid.InitializeReadOnly();
            Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        void ControlsToItem()
        {
            DataRow Row = Grid.CurrentDataRow();

            if (Row != null)
            {
                LinkItem LinkItem  = (LinkItem)Row["OBJECT"];
                App.CurrentProject.ShowPageByLinkItem(LinkItem);
                DialogResult = DialogResult.OK;
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
        public SearchResultsDialog()
        {
            InitializeComponent();
        }


        static public void ShowModal(List<LinkItem> LinkItems)
        {
            using (SearchResultsDialog dlg = new ())
            {
                dlg.LinkItems = LinkItems;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    return;
                }
            }
        }
    }
}
