namespace StoryWriter
{

    /// <summary>
    /// Represents a group.
    /// </summary>
    public class Group: BaseEntity
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
            this.Id = Row.AsString("Id");
            this.Name = Row.AsString("Name");
        }

        /// <summary>
        /// Inserts this instance into the database.
        /// </summary>
        public bool Insert()
        {
            if (App.CurrentProject.GroupExists(this.Name))
            {
                App.ErrorBox($"A group with the name '{this.Name}' already exists.");
                return false;
            }

            string sqlText = $"INSERT INTO {Project.SGroup} (Id, Name) VALUES (:Id, :Name)";
            var parameters = this.ToDictionary(); // see helper below
            App.SqlStore.ExecSql(sqlText, parameters);

            App.CurrentProject.GroupList.Add(this);
            return true;
        }
        /// <summary>
        /// Updates this instance in the database.
        /// </summary>
        public bool Update()
        {
            if (App.CurrentProject.GroupExists(this.Name, this.Id))
            {
                App.ErrorBox($"A group with the name '{this.Name}' already exists.");
                return false;
            }

            string sqlText = $"UPDATE {Project.SGroup} SET Name = :Name WHERE Id = :Id";
            var parameters = this.ToDictionary(); // see helper below
            App.SqlStore.ExecSql(sqlText, parameters);

            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            if (!App.QuestionBox($"Are you sure you want to delete the group '{this.Name}'?"))
                return false;

            string SqlText = $"delete from {Project.SGroup} where Id = :Id";
            var Params = this.ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);

            App.CurrentProject.GroupList.Remove(this);

            return true;
        }



    }
}
