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

        /// <summary>
        /// Builds a dictionary of properties of this instance for database operations.
        /// </summary>
        public Dictionary<string, object> ToDictionary()
        {
            if (string.IsNullOrWhiteSpace(TagId) || string.IsNullOrWhiteSpace(ComponentId))
                throw new Exception("TagId and ComponentId must be set.");

            var Dict = new Dictionary<string, object>();
            Dict["TagId"] = TagId;
            Dict["ComponentId"] = ComponentId;
            return Dict;
        }
        /// <summary>
        /// Loads the properties of this object from a DataRow.
        /// </summary>
        public void LoadFrom(DataRow Row)
        {
            TagId = Row.AsString("TagId");
            ComponentId = Row.AsString("ComponentId");

            //Tag = App.CurrentStory.TagList.FirstOrDefault(x => x.Id == TagId);
            //Component = App.CurrentStory.ComponentList.FirstOrDefault(x => x.Id == ComponentId);
        }

        /// <summary>
        /// Inserts this instance into the database.
        /// </summary>
        public bool Insert()
        {
            if (App.CurrentStory.TagToComponentExists(this))
            {
                App.ErrorBox($"A correlation between '{GetTag().Name}' and '{GetComponent().Name}' already exists.");
                return false;
            }

            string SqlText = $"INSERT INTO {Story.STagToComponent} (TagId, ComponentId) VALUES (:TagId, :ComponentId)";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentStory.AddToList(this);
            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            if (!App.QuestionBox($"Are you sure you want to delete correlation between '{GetTag().Name}' and '{GetComponent().Name}'?"))
                return false;

            string SqlText = $"delete from {Story.STagToComponent} where TagId = :TagId and ComponentId = :ComponentId";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentStory.TagToComponentList.Remove(this);
            return true;
        }

        static public void DeleteByComponent(string ComponentId)
        {
            string SqlText = $"delete from {Story.STagToComponent} where ComponentId = :ComponentId";
            App.SqlStore.ExecSql(SqlText, new Dictionary<string, object> { ["ComponentId"] = ComponentId });
            App.CurrentStory.ReLoadTagToComponents();
        }
        static public void DeleteByTag(string TagId)
        {
            string SqlText = $"delete from {Story.STagToComponent} where TagId = :TagId";
            App.SqlStore.ExecSql(SqlText, new Dictionary<string, object> { ["TagId"] = TagId });
            App.CurrentStory.ReLoadTagToComponents();
        }

        /// <summary>
        /// Returns true if both Tag and Component are set.
        /// </summary>
        //public bool IsOk() => GetTag() != null && GetComponent() != null;

        public Tag GetTag() => App.CurrentStory.TagList.FirstOrDefault(x => x.Id == TagId);
        public Component GetComponent() => App.CurrentStory.ComponentList.FirstOrDefault(x => x.Id == ComponentId);  


        public string TagId { get; set; }
        public string ComponentId { get; set; }
    }
}
