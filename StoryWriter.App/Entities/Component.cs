namespace StoryWriter
{
    /// <summary>
    /// Represents a component.
    /// <para>An actual component may represent a character, a location, an item, an event, etc. and it is child to a group or sub-group.</para>
    /// </summary>
    public class Component : BaseEntity
    {
        // ● public
        /// <summary>
        /// Returns the name of this instance.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }
        /// <summary>
        /// Builds a dictionary of properties of this instance for database operations.
        /// </summary>
        public Dictionary<string, object> ToDictionary()
        {
            var Dict = new Dictionary<string, object>();
            Dict["Id"] = Id;
            Dict["Name"] = Name;
            Dict["BodyText"] = BodyText;
            return Dict;
        }
        /// <summary>
        /// Loads the properties of this object from a DataRow.
        /// </summary>
        public void LoadFrom(DataRow Row)
        {
            Id = Row.AsString("Id");            
            Name = Row.AsString("Name");
            BodyText = Row.AsString("BodyText");
        }

        /// <summary>
        /// Inserts this instance into the database.
        /// </summary>
        public bool Insert()
        {
            if (App.CurrentProject.ItemExists(this))
            {
                App.ErrorBox($"A component with the name '{Name}' already exists.");
                return false;
            }

            string SqlText = $"INSERT INTO {Project.SComponent} (Id, Name, BodyText) VALUES (:Id, :Name, :BodyText)";    
            
            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentProject.AddToList(this);
            return true;
        }
        /// <summary>
        /// Updates this instance in the database.
        /// </summary>
        public bool Update()
        {
            if (!App.CurrentProject.ItemExists(this))
            {
                App.ErrorBox($"A component with the name '{Name}' not found.");
                return false;
            }

            string SqlText = $"UPDATE {Project.SComponent} SET Name = :Name, BodyText = :BodyText WHERE Id = :Id";    
            
            var Params = ToDictionary(); 
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            if (!App.QuestionBox($"Are you sure you want to delete the component '{Name}'?"))
                return false;

            string SqlText = $"delete from {Project.SComponent} where Id = :Id";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentProject.ComponentList.Remove(this);
            return true;
        }
 
        // ● properties
        /// <summary>
        /// The text of this instance.  
        /// </summary>
        public string BodyText { get; set; }

    }
}
