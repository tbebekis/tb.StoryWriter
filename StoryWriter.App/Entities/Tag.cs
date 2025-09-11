namespace StoryWriter
{
    /// <summary>
    /// Represents a tag.
    /// </summary>
    public class Tag : BaseEntity
    {
        // ● public 
        /// <summary>
        /// Builds a dictionary of properties of this instance for database operations.
        /// </summary>
        public Dictionary<string, object> ToDictionary()
        {
            var Dict = new Dictionary<string, object>();
            Dict["Id"] = Id;
            Dict["Name"] = Name;
            return Dict;
        }
        /// <summary>
        /// Loads the properties of this object from a DataRow.
        /// </summary>
        public void LoadFrom(DataRow Row)
        {
            Id = Row.AsString("Id");
            Name = Row.AsString("Name");
        }

        /// <summary>
        /// Inserts this instance into the database.
        /// </summary>
        public bool Insert()
        {
            string SqlText = $"INSERT INTO {Story.STag} (Id, Name) VALUES (:Id, :Name)";

            var Params = ToDictionary();  
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentStory.AddToList(this);
            return true;
        }
        /// <summary>
        /// Updates this instance in the database.
        /// </summary>
        public bool Update()
        {
            string SqlText = $"UPDATE {Story.STag} SET Name = :Name WHERE Id = :Id";

            var Params = ToDictionary();  
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            string SqlText = $"delete from {Story.STag} where Id = :Id";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentStory.TagList.Remove(this);
            return true;
        }

        public List<Component> GetComponentList()
        {
            List<Component> ComponentList = new();
            var TagToComponentList = App.CurrentStory.TagToComponentList.Where(x => x.TagId == Id).ToList();

            foreach (TagToComponent TagToComponent in TagToComponentList)
            {
                Component Component = TagToComponent.GetComponent();
                if (Component != null)
                    ComponentList.Add(Component);
            }       


            return ComponentList;
        }

    }
}
