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
                ["Id"] = this.Id,
                ["ChapterId"] = this.ChapterId,
                ["Name"] = this.Name,
                ["BodyText"] = this.BodyText,
                ["CreatedAt"] = this.CreatedAt,
                ["UpdatedAt"] = this.UpdatedAt,
                ["OrderIndex"] = this.OrderIndex
            };
        }
        /// <summary>
        /// Loads the properties of this object from a DataRow.
        /// </summary>
        public void LoadFrom(DataRow row)
        {
            this.Id = row.AsString("Id");
            this.ChapterId = row.AsString("ChapterId");
            this.Name = row.AsString("Name");
            this.BodyText = row.AsString("BodyText");
            this.CreatedAt = row.AsDateTime("CreatedAt");
            this.UpdatedAt = row.AsDateTime("UpdatedAt");
            this.OrderIndex = row.AsInteger("OrderIndex");
        }

        /// <summary>
        /// Inserts this instance into the database.
        /// </summary>
        public bool Insert()
        {
            // Optional in-memory uniqueness check
            if (Chapter.SceneList != null &&
                Chapter.SceneList.Any(sc => sc.Name.IsSameText(this.Name)))
            {
                App.ErrorBox($"A scene with the name '{this.Name}' already exists.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(ChapterId))
            {
                App.ErrorBox("Scene must have a valid ChapterId.");
                return false;
            }

            // Set timestamps
            if (CreatedAt == default) CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            const string sql = @"
        INSERT INTO {TableName} (Id, ChapterId, Name, BodyText, CreatedAt, UpdatedAt, OrderIndex)
        VALUES (:Id, :ChapterId, :Name, :BodyText, :CreatedAt, :UpdatedAt, :OrderIndex)
    ";

            string sqlText = sql.Replace("{TableName}", Project.SScene);

            var parameters = this.ToDictionary();  
            App.SqlStore.ExecSql(sqlText, parameters);

            return true;
        }
        /// <summary>
        /// Updates this instance in the database.
        /// </summary>
        public bool Update()
        {
            // In-memory uniqueness check (exclude current Id)
            if (Chapter.SceneList != null &&
                Chapter.SceneList.Any(sc => sc.Name.IsSameText(this.Name) && sc.Id != this.Id))
            {
                App.ErrorBox($"A scene with the name '{this.Name}' already exists.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(ChapterId))
            {
                App.ErrorBox("Scene must have a valid ChapterId.");
                return false;
            }

            // Touch update timestamp
            this.UpdatedAt = DateTime.UtcNow;

            string sqlText =
                $"UPDATE {Project.SScene} " +
                "SET ChapterId = :ChapterId, Name = :Name, BodyText = :BodyText,  " +
                "UpdatedAt = :UpdatedAt, OrderIndex = :OrderIndex " +
                "WHERE Id = :Id";

            var parameters = this.ToDictionary(); // same mapping as in Insert()
            App.SqlStore.ExecSql(sqlText, parameters);

            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            if (!App.QuestionBox($"Are you sure you want to delete the scene '{this}'?"))
                return false;

            string sqlText = $"DELETE FROM {Project.SScene} WHERE Id = :Id";
            var parameters = this.ToDictionary(); // ToDictionary must at least include :Id
            App.SqlStore.ExecSql(sqlText, parameters);

            return true;
        }

        public bool UpdateOrderIndex()
        {
            string SqlText = $"UPDATE {Project.SScene} SET OrderIndex = :OrderIndex WHERE Id = :Id";
            var parameters = new Dictionary<string, object>
            {
                ["OrderIndex"] = this.OrderIndex,
                ["Id"] = this.Id
            };
            App.SqlStore.ExecSql(SqlText, parameters);
            return true;
        }
        public bool UpdateBodyText()
        {
            string sqlText = $"UPDATE {Project.SScene} SET BodyText = :BodyText WHERE Id = :Id";
            var parameters = new Dictionary<string, object>
            {
                ["BodyText"] = this.BodyText,
                ["Id"] = this.Id
            };
            App.SqlStore.ExecSql(sqlText, parameters);
            return true;
        }

        /// <summary>
        /// The parent Chapter of this scene.
        /// </summary>
        public Chapter Chapter => App.CurrentProject?.ChapterList.FirstOrDefault(ch => ch.Id == this.ChapterId);


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
