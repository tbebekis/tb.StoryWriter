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
            Dict["Group"] = Group;
            Dict["SubGroup"] = SubGroup;
            Dict["BodyText"] = BodyText;
            return Dict;
        }
        /// <summary>
        /// Loads the properties of this object from a DataRow.
        /// </summary>
        public void LoadFrom(DataRow Row)
        {
            this.Id = Row.AsString("Id");            
            this.Name = Row.AsString("Name");
            this.Group = Row.AsString("Group");
            this.SubGroup = Row.AsString("SubGroup");
            this.BodyText = Row.AsString("BodyText");
        }

        /// <summary>
        /// Inserts this instance into the database.
        /// </summary>
        public bool Insert()
        {
            if (App.CurrentProject.ComponentList.Any(item => item.Name.IsSameText(this.Name)))
            {
                App.ErrorBox($"A component with the name '{this.Name}' already exists.");
                return false;
            }

            string sqlText = $"INSERT INTO {Project.SComponent} (Id, Name, Group, SubGroup, BodyText) VALUES (:Id, :Name, :Group, :SubGroup, :BodyText)";            
            var parameters = this.ToDictionary(); // see helper below
            App.SqlStore.ExecSql(sqlText, parameters);

            App.CurrentProject.ComponentList.Add(this);
            return true;
        }
        /// <summary>
        /// Updates this instance in the database.
        /// </summary>
        public bool Update()
        {
            if (App.CurrentProject.ComponentList.Any(item => item.Name.IsSameText(this.Name) && item.Id != this.Id))
            {
                App.ErrorBox($"A component with the name '{this.Name}' already exists.");
                return false;
            }

            string sqlText = $"UPDATE {Project.SComponent} SET Name = :Name, Group = :Group, SubGroup = :SubGroup, BodyText = :BodyText WHERE Id = :Id";            
            var parameters = this.ToDictionary(); // see helper below
            App.SqlStore.ExecSql(sqlText, parameters);

            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            if (!App.QuestionBox($"Are you sure you want to delete the component '{this.Title}'?"))
                return false;

            string SqlText = $"delete from {Project.SComponent} where Id = :Id";
            var Params = this.ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);

            App.CurrentProject.ComponentList.Remove(this);

            return true;
        }

        // ● properties
        /// <summary>
        /// The name of the group this instance belongs to, e.g. Planet
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// The name of the subgroup this instance belongs to, e.g. Planet
        /// </summary>
        public string SubGroup { get; set; }
        /// <summary>
        /// The text of this instance.  
        /// </summary>
        public string BodyText { get; set; }

        /// <summary>
        /// Returns the title of this instance.
        /// </summary>
        public string Title
        {
            get
            {
                string Result = Name;

                if (!string.IsNullOrWhiteSpace(Group) && !string.IsNullOrWhiteSpace(SubGroup))
                    Result = $"{Group}.{SubGroup} > {Name}";
                else if (!string.IsNullOrWhiteSpace(Group) && string.IsNullOrWhiteSpace(SubGroup))
                    Result = $"{Group} > {Name}";
                else if (string.IsNullOrWhiteSpace(Group) && !string.IsNullOrWhiteSpace(SubGroup))
                    Result = $"{SubGroup} > {Name}";

                return Result;
            }
        }
    }
}
