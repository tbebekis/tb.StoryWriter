namespace StoryWriter
{
    /// <summary>
    /// Scene of the book.
    /// </summary>
    public class Scene : BaseEntity
    {

        // ● construction
        /// <summary>
        /// Constructor
        /// </summary>
        public Scene()
        {
        }

        /// <summary>
        /// Returns the name of this instance.
        /// </summary>
        public override string ToString()
        {
            string Result = $"{OrderIndex}.{Name}";
            return Result;
        }

        /// <summary>
        /// Builds a dictionary of properties of this instance for database operations.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, object> ToDictionary()
        {
            return new Dictionary<string, object>
            {
                ["Id"] = Id,
                ["ChapterId"] = ChapterId,
                ["Name"] = Name,
                ["BodyText"] = BodyText,
                ["CreatedAt"] = CreatedAt,
                ["UpdatedAt"] = UpdatedAt,
                ["OrderIndex"] = OrderIndex
            };
        }
        /// <summary>
        /// Loads the properties of this object from a DataRow.
        /// </summary>
        public void LoadFrom(DataRow Row)
        {
            Id = Row.AsString("Id");
            ChapterId = Row.AsString("ChapterId");
            Name = Row.AsString("Name");
            BodyText = Row.AsString("BodyText");
            CreatedAt = Row.AsDateTime("CreatedAt");
            UpdatedAt = Row.AsDateTime("UpdatedAt");
            OrderIndex = Row.AsInteger("OrderIndex");
        }

        /// <summary>
        /// Inserts this instance into the database.
        /// </summary>
        public bool Insert()
        { 
            if (Chapter.SceneExists(this))
            {
                App.ErrorBox($"A scene with the name '{Name}' already exists.");
                return false;
            }

            // Set timestamps
            if (CreatedAt == default) 
                CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            string SqlText = @$"
        INSERT INTO {Project.SScene} (Id, ChapterId, Name, BodyText, CreatedAt, UpdatedAt, OrderIndex)
        VALUES (:Id, :ChapterId, :Name, :BodyText, :CreatedAt, :UpdatedAt, :OrderIndex)
    ";
            var Params = ToDictionary();  
            App.SqlStore.ExecSql(SqlText, Params);
            
            return true;
        }
        /// <summary>
        /// Updates this instance in the database.
        /// </summary>
        public bool Update()
        {
            if (Chapter.SceneExists(this))
            {
                App.ErrorBox($"A scene with the name '{Name}' already exists.");
                return false;
            }

            UpdatedAt = DateTime.UtcNow;

            string SqlText =
                $"UPDATE {Project.SScene} " +
                "SET ChapterId = :ChapterId, Name = :Name, BodyText = :BodyText,  " +
                "UpdatedAt = :UpdatedAt, OrderIndex = :OrderIndex " +
                "WHERE Id = :Id";

            var Params = ToDictionary(); // same mapping as in Insert()
            App.SqlStore.ExecSql(SqlText, Params);

            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            if (!App.QuestionBox($"Are you sure you want to delete the scene '{this}'?"))
                return false;

            string SqlText = $"DELETE FROM {Project.SScene} WHERE Id = :Id";
            var Params = ToDictionary(); // ToDictionary must at least include :Id
            App.SqlStore.ExecSql(SqlText, Params);

            return true;
        }

        public bool UpdateOrderIndex()
        {
            string SqlText = $"UPDATE {Project.SScene} SET OrderIndex = :OrderIndex WHERE Id = :Id";
            var Params = new Dictionary<string, object>
            {
                ["OrderIndex"] = OrderIndex,
                ["Id"] = Id
            };
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }
        public bool UpdateBodyText()
        {
            string SqlText = $"UPDATE {Project.SScene} SET BodyText = :BodyText WHERE Id = :Id";
            var Params = new Dictionary<string, object>
            {
                ["BodyText"] = BodyText,
                ["Id"] = Id
            };
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }

        /// <summary>
        /// The parent Chapter of this scene.
        /// </summary>
        public Chapter Chapter => App.CurrentProject?.ChapterList.FirstOrDefault(ch => ch.Id == ChapterId);


        /// <summary>
        /// Foreign key reference to the parent Chapter
        /// </summary>
        public string ChapterId { get; set; }
        /// <summary>
        /// Full text body of the scene
        /// </summary>
        public string BodyText { get; set; } = string.Empty;
        /// <summary>
        /// Creation timestamp (UTC)
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Last update timestamp (UTC)
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Sort order of the scene within the chapter
        /// </summary>
        public int OrderIndex { get; set; } = 0;
    }

}
