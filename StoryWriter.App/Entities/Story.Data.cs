namespace StoryWriter
{
    public partial class Story
    {
        /// <summary>
        /// Loads all tags from the database into TagList
        /// </summary>
        public void ReLoadTags()
        {
            TagList.Clear();

            string SqlText = $"SELECT * FROM {STag} ORDER BY Name";

            DataTable Table = App.SqlStore.Select(SqlText);

            foreach (DataRow Row in Table.Rows)
            {
                Tag item = new();
                item.LoadFrom(Row);
                AddToList(item);
            }

            App.PerformItemListChanged(this, ItemType.Tag);
        }
        /// <summary>
        /// Loads all component types from the database into ComponentTypeList
        /// </summary>
        public void ReLoadComponentTypes()
        {
            ComponentTypeList.Clear();

            string SqlText = $"SELECT * FROM {SComponentType} ORDER BY Name";

            DataTable Table = App.SqlStore.Select(SqlText);

            foreach (DataRow Row in Table.Rows)
            {
                ComponentType item = new();
                item.LoadFrom(Row);
                AddToList(item);
            }

            App.PerformItemListChanged(this, ItemType.ComponentType);
        }
        /// <summary>
        /// Loads all components from the database into FlatComponentList and TreeComponentList.
        /// </summary>
        public void ReLoadComponents()
        {
            ComponentList.Clear();

            string SqlText = $"SELECT * FROM {SComponent} ORDER BY Name";

            DataTable Table = App.SqlStore.Select(SqlText);

            foreach (DataRow Row in Table.Rows)
            {
                Component item = new();
                item.LoadFrom(Row);
                AddToList(item);
            }

            App.PerformItemListChanged(this, ItemType.Component);
        }

        /// <summary>
        /// Loads all chapters and their associated scenes from the database into ChapterList.
        /// </summary>
        public void ReLoadChapters()
        {
            ChapterList.Clear();

            string sql = $@"
        SELECT *
        FROM {SChapter}
        ORDER BY OrderIndex, CreatedAt
    ";

            DataTable table = App.SqlStore.Select(sql);
            foreach (DataRow row in table.Rows)
            {
                Chapter ch = new();
                ch.LoadFrom(row);
                AddToList(ch);
            }

            App.PerformItemListChanged(this, ItemType.Chapter);

        }
        public void ReLoadScenes()
        {
            SceneList.Clear();

            string sql = $@"
        SELECT *
        FROM {SScene}
    ";
        
            DataTable table = App.SqlStore.Select(sql);
            foreach (DataRow row in table.Rows)
            {
                Scene sc = new();
                sc.LoadFrom(row);
                AddToList(sc);
            }

            App.PerformItemListChanged(this, ItemType.Scene);
        }

        public void ReLoadNotes()
        {
            NoteList.Clear();

            string sql = $@"
        SELECT *
        FROM {SNote}
        ORDER BY OrderIndex
    ";

            DataTable table = App.SqlStore.Select(sql);

            foreach (DataRow row in table.Rows)
            {
                Note note = new();
                note.LoadFrom(row);
                AddToList(note);
            }

            App.PerformItemListChanged(this, ItemType.Note);
        }

        /// <summary>
        /// Loads all TagToComponent items from the database into TagToComponentList
        /// </summary>
        public void ReLoadTagToComponents()
        {
            TagToComponentList.Clear();

            string SqlText = $"SELECT * FROM {STagToComponent}";

            DataTable Table = App.SqlStore.Select(SqlText);

            foreach (DataRow Row in Table.Rows)
            {
                TagToComponent item = new();
                item.LoadFrom(Row);
                //if (item.IsOk())
                    AddToList(item);
            }

            App.PerformItemListChanged(this, ItemType.TagToComponent);
        }

        public void ReLoadComponentToScenes()
        {
            ComponentToSceneList.Clear();

            string SqlText = $"SELECT * FROM {SComponentToScene}";

            DataTable Table = App.SqlStore.Select(SqlText);

            foreach (DataRow Row in Table.Rows)
            {
                ComponentToScene item = new();
                item.LoadFrom(Row);
                //if (item.IsOk())
                    AddToList(item);
            }

            App.PerformItemListChanged(this, ItemType.ComponentToScene);
        }

 
    }
}
