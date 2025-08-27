namespace StoryWriter
{

    /// <summary>
    /// Represents a group.
    /// </summary>
    public class Tag: BaseEntity
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
            string SqlText = $"INSERT INTO {Project.STag} (Id, Name) VALUES (:Id, :Name)";

            var Params = ToDictionary(); // see helper below
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentProject.AddToList(this);
            return true;
        }
        /// <summary>
        /// Updates this instance in the database.
        /// </summary>
        public bool Update()
        {
            string SqlText = $"UPDATE {Project.STag} SET Name = :Name WHERE Id = :Id";

            var Params = ToDictionary(); // see helper below
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            string SqlText = $"delete from {Project.STag} where Id = :Id";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentProject.TagList.Remove(this);
            return true;
        }
    
         
    }
}
