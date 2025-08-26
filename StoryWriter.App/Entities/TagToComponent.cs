namespace StoryWriter
{

    /// <summary>
    /// Represents a correlation between a tag and a component.
    /// </summary>
    public class TagToComponent
    {

        public TagToComponent()
        {

        }
        public TagToComponent(Tag Tag, Component Component)
        {
            this.Tag = Tag;
            this.Component = Component;
        }

        /// <summary>
        /// Builds a dictionary of properties of this instance for database operations.
        /// </summary>
        public Dictionary<string, object> ToDictionary()
        {
            var Dict = new Dictionary<string, object>();
            Dict["TagId"] = Tag.Id;
            Dict["ComponentId"] = Component.Id;
            return Dict;
        }
        /// <summary>
        /// Loads the properties of this object from a DataRow.
        /// </summary>
        public void LoadFrom(DataRow Row)
        {
            string TagId = Row.AsString("TagId");
            string ComponentId = Row.AsString("ComponentId");

            Tag = App.CurrentProject.TagList.FirstOrDefault(x => x.Id == TagId);
            Component = App.CurrentProject.ComponentList.FirstOrDefault(x => x.Id == ComponentId);
        }

        /// <summary>
        /// Inserts this instance into the database.
        /// </summary>
        public bool Insert()
        {
            if (App.CurrentProject.ItemExists(this))
            {
                App.ErrorBox($"A correlation between '{Tag.Name}' and '{Component.Name}' already exists.");
                return false;
            }

            string SqlText = $"INSERT INTO {Project.STagToComponent} (TagId, ComponentId) VALUES (:TagId, :ComponentId)";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentProject.AddToList(this);
            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            if (!App.QuestionBox($"Are you sure you want to delete correlation between '{Tag.Name}' and '{Component.Name}'?"))
                return false;

            string SqlText = $"delete from {Project.STagToComponent} where TagId = :TagId and ComponentId = :ComponentId";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentProject.TagToComponentList.Remove(this);
            return true;
        }


        /// <summary>
        /// Returns true if both Tag and Component are set.
        /// </summary>
        public bool IsOk() => Tag != null && Component != null;

        /// <summary>
        /// The Tag this instance is associated with.
        /// </summary>
        public Tag Tag { get; set; }
        /// <summary>
        /// The Component this instance is associated with.
        /// </summary>
        public Component Component { get; set; }
    }
}
