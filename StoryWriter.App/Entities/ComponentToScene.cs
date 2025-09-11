namespace StoryWriter
{ 

    /// <summary>
    /// Represents a correlation between a scene and a component.
    /// </summary>
    public class ComponentToScene
    {

        public ComponentToScene()
        {
        }

        /// <summary>
        /// Builds a dictionary of properties of this instance for database operations.
        /// </summary>
        public Dictionary<string, object> ToDictionary()
        {
            if (string.IsNullOrWhiteSpace(SceneId) || string.IsNullOrWhiteSpace(ComponentId))
                throw new Exception("SceneId and ComponentId must be set.");

            var Dict = new Dictionary<string, object>();
            Dict["SceneId"] = SceneId;
            Dict["ComponentId"] = ComponentId;
            return Dict;
        }
        /// <summary>
        /// Loads the properties of this object from a DataRow.
        /// </summary>
        public void LoadFrom(DataRow Row)
        {
            SceneId = Row.AsString("SceneId");
            ComponentId = Row.AsString("ComponentId");

            //Scene = App.CurrentStory.SceneList.FirstOrDefault(x => x.Id == SceneId);
            //Component = App.CurrentStory.ComponentList.FirstOrDefault(x => x.Id == ComponentId);
        }

        /// <summary>
        /// Inserts this instance into the database.
        /// </summary>
        public bool Insert()
        {
            string SqlText = $"INSERT INTO {Story.SComponentToScene} (SceneId, ComponentId) VALUES (:SceneId, :ComponentId)";

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
            string SqlText = $"delete from {Story.SComponentToScene} where SceneId = :SceneId and ComponentId = :ComponentId";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentStory.ComponentToSceneList.Remove(this);
            return true;
        }

        static public void DeleteByComponent(string ComponentId)
        {
            string SqlText = $"delete from {Story.SComponentToScene} where ComponentId = :ComponentId";
            App.SqlStore.ExecSql(SqlText, new Dictionary<string, object> { ["ComponentId"] = ComponentId });
            App.CurrentStory.ReLoadComponentToScenes();
        }
        static public void DeleteByScene(string SceneId)
        {
            string SqlText = $"delete from {Story.SComponentToScene} where SceneId = :SceneId";
            App.SqlStore.ExecSql(SqlText, new Dictionary<string, object> { ["SceneId"] = SceneId });
            App.CurrentStory.ReLoadComponentToScenes();
        }

        /// <summary>
        /// Returns true if both Tag and Component are set.
        /// </summary>
        public bool IsOk() => GetScene() != null && GetComponent() != null;
 
        public Scene GetScene() => App.CurrentStory.SceneList.FirstOrDefault(x => x.Id == SceneId);
        public Component GetComponent() => App.CurrentStory.ComponentList.FirstOrDefault(x => x.Id == ComponentId);
 

        public string SceneId { get; set; }
        public string ComponentId { get; set; }
    }
}
