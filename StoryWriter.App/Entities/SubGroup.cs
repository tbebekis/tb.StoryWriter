using Tripous;

namespace StoryWriter
{
    /// <summary>
    /// A SubGroup
    /// </summary>
    public class SubGroup : BaseEntity
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
            if (App.CurrentProject.SubGroupExists(Name))
            {
                App.ErrorBox($"A sub-group with the name '{this.Name}' already exists.");
                return false;
            }

            string sqlText = $"INSERT INTO {Project.SSubGroup} (Id, Name) VALUES (:Id, :Name)";
            var parameters = this.ToDictionary(); // see helper below
            App.SqlStore.ExecSql(sqlText, parameters);

            App.CurrentProject.SubGroupList.Add(this);
            return true;
        }
        /// <summary>
        /// Updates this instance in the database.
        /// </summary>
        public bool Update()
        {
            if (App.CurrentProject.SubGroupExists(Name, Id))
            {
                App.ErrorBox($"A sub-group with the name '{this.Name}' already exists.");
                return false;
            }

            string sqlText = $"UPDATE {Project.SSubGroup} SET Name = :Name WHERE Id = :Id";
            var parameters = this.ToDictionary(); // see helper below
            App.SqlStore.ExecSql(sqlText, parameters);

            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            if (!App.QuestionBox($"Are you sure you want to delete the sub-group '{this.Name}'?"))
                return false;

            string SqlText = $"delete from {Project.SSubGroup} where Id = :Id";
            var Params = this.ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);

            App.CurrentProject.SubGroupList.Remove(this);

            return true;
        }


    }
}
