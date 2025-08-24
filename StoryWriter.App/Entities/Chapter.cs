namespace StoryWriter
{
    /// <summary>
    /// Book chapter data model
    /// </summary>
    public class Chapter
    {
        // ● construction
        /// <summary>
        /// Constructor
        /// </summary>
        public Chapter()
        {
        }

        /// <summary>
        /// Returns the name of this instance.
        /// </summary>
        public override string ToString()
        {
            return $"{OrderIndex}.{Name}"; 
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
                ["Name"] = this.Name,
                ["Synopsis"] = this.Synopsis,
                ["Concept"] = this.Concept,
                ["Outcome"] = this.Outcome,
                ["BodyText"] = this.BodyText,
                ["CreatedAt"] = this.CreatedAt,   // stored as TEXT/ISO8601 or per your CDATE_TIME
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
            this.Name = row.AsString("Name");
            this.Synopsis = row.AsString("Synopsis");
            this.Concept = row.AsString("Concept");
            this.Outcome = row.AsString("Outcome");
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
            // Optional in-memory uniqueness check (αν έχεις λίστα στο Project)
            if (App.CurrentProject?.ChapterList != null &&
                App.CurrentProject.ChapterList.Any(ch => ch.Name.IsSameText(this.Name)))
            {
                App.ErrorBox($"A chapter with the name '{this.Name}' already exists.");
                return false;
            }

            // Set timestamps if not already set
            if (CreatedAt == default) CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            const string sql = @"
        INSERT INTO {TableName} (Id, Name, Synopsis, Concept, Outcome, BodyText, CreatedAt, UpdatedAt, OrderIndex)
        VALUES (:Id, :Name, :Synopsis, :Concept, :Outcome, :BodyText, :CreatedAt, :UpdatedAt, :OrderIndex)
    ";

            // replace table placeholder (same pattern you used for components)
            string sqlText = sql.Replace("{TableName}", Project.SChapter);

            var parameters = this.ToDictionary(); // see helper below
            App.SqlStore.ExecSql(sqlText, parameters);

            return true;
        }
        /// <summary>
        /// Updates this instance in the database.
        /// </summary>
        public bool Update()
        {
            // In-memory uniqueness check (exclude current Id)
            if (App.CurrentProject?.ChapterList != null &&
                App.CurrentProject.ChapterList.Any(ch => ch.Name.IsSameText(this.Name) && ch.Id != this.Id))
            {
                App.ErrorBox($"A chapter with the name '{this.Name}' already exists.");
                return false;
            }

            // Touch update timestamp
            this.UpdatedAt = DateTime.UtcNow;

            string sqlText =
                $"UPDATE {Project.SChapter} " +
                "SET Name = :Name, Synopsis = :Synopsis, Concept = :Concept, Outcome = :Outcome, " +
                "BodyText = :BodyText, UpdatedAt = :UpdatedAt, OrderIndex = :OrderIndex " +
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
            if (!App.QuestionBox($"Are you sure you want to delete the chapter '{this.ToString()}'?"))
                return false;

            string sqlText = $"DELETE FROM {Project.SChapter} WHERE Id = :Id";
            var parameters = this.ToDictionary(); // ToDictionary must at least include :Id
            App.SqlStore.ExecSql(sqlText, parameters);

            return true;
        }

        /// <summary>
        /// Updates only the order index of this chapter
        /// </summary>
        public bool UpdateOrderIndex()
        {
            string SqlText = $"UPDATE {Project.SChapter} SET OrderIndex = :OrderIndex WHERE Id = :Id";
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
            string sqlText = $"UPDATE {Project.SChapter} SET BodyText = :BodyText WHERE Id = :Id";
            var parameters = new Dictionary<string, object>
            {
                ["BodyText"] = this.BodyText,
                ["Id"] = this.Id
            };
            App.SqlStore.ExecSql(sqlText, parameters);
            return true;
        }
        public bool UpdateSynopsis()
        {
            string sqlText = $"UPDATE {Project.SChapter} SET Synopsis = :Synopsis WHERE Id = :Id";
            var parameters = new Dictionary<string, object>
            {
                ["Synopsis"] = this.Synopsis,
                ["Id"] = this.Id
            };
            App.SqlStore.ExecSql(sqlText, parameters);
            return true;
        }
        public bool UpdateConcept()
        {
            string sqlText = $"UPDATE {Project.SChapter} SET Concept = :Concept WHERE Id = :Id";
            var parameters = new Dictionary<string, object>
            {
                ["Concept"] = this.Concept,
                ["Id"] = this.Id
            };
            App.SqlStore.ExecSql(sqlText, parameters);
            return true;
        }
        public bool UpdateOutcome()
        {
            string sqlText = $"UPDATE {Project.SChapter} SET Outcome = :Outcome WHERE Id = :Id";
            var parameters = new Dictionary<string, object>
            {
                ["Outcome"] = this.Outcome,
                ["Id"] = this.Id
            };
            App.SqlStore.ExecSql(sqlText, parameters);
            return true;
        }

        /// <summary>
        /// Returns true if this chapter contains the specified term
        /// </summary>
        public bool IsLinkOf(string Term)
        {
            bool Result = this.Name.ContainsText(Term);

            if (!Result)
                Result = this.SceneList.Any(item => item.Name.ContainsText(Term));

            return Result;
        }

        // ● chapters
        /// <summary>
        /// True if a chapter exists by name
        /// </summary>
        public bool SceneExists(string Name, string Id = "")
        {
            if (string.IsNullOrWhiteSpace(Id))
                return SceneList.FirstOrDefault(item => item.Name.IsSameText(Name)) != null;

            return SceneList.FirstOrDefault(item => item.Name.IsSameText(Name) && item.Id == Id) != null;
        }
        public void RenumberScenes()
        {
            int OrderIndex = 0;
            foreach (var Scene in SceneList)
            {
                OrderIndex++;
                Scene.OrderIndex = OrderIndex;
            }

            foreach (var Scene in SceneList)
                Scene.UpdateOrderIndex();
        }


        /// <summary>
        /// The project/book this chapter belongs to.
        /// </summary>
        public Project Project => App.CurrentProject;

        public string Id { get; set; }
        /// <summary>
        /// Chapter title (unique)
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Short summary / plot abstraction
        /// </summary>
        public string Synopsis { get; set; } = string.Empty;
        /// <summary>
        /// Main concept / key idea of the chapter
        /// </summary>
        public string Concept { get; set; } = string.Empty;
        /// <summary>
        /// Final outcome / status at the end of the chapter
        /// </summary>
        public string Outcome { get; set; } = string.Empty;
        /// <summary>
        /// Full manuscript text of the chapter
        /// </summary>
        public string BodyText { get; set; } = string.Empty;
        /// <summary>
        /// Creation date (UTC)
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Last update date (UTC)
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Sort order of the chapter in the book
        /// </summary>
        public int OrderIndex { get; set; } = 0;

        /// <summary>
        /// The list of scenes that belong to this chapter.
        /// </summary>
        public List<Scene> SceneList { get; private set; } = new List<Scene>();
    }
}
