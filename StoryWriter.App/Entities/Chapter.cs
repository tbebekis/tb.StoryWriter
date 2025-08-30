namespace StoryWriter
{
    /// <summary>
    /// Book chapter data model
    /// </summary>
    public class Chapter: BaseEntity
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
                ["Id"] = Id,
                ["Name"] = Name,
                ["Synopsis"] = Synopsis,
                ["Concept"] = Concept,
                ["Outcome"] = Outcome,
                ["BodyText"] = BodyText,
                ["CreatedAt"] = CreatedAt,   // stored as TEXT/ISO8601 or per your CDATE_TIME
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
            Name = Row.AsString("Name");
            Synopsis = Row.AsString("Synopsis");
            Concept = Row.AsString("Concept");
            Outcome = Row.AsString("Outcome");
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
            // Set timestamps if not already set
            if (CreatedAt == default) 
                CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            OrderIndex = App.CurrentProject.ChapterList.Count + 1;

            string SqlText = @$"
        INSERT INTO {Project.SChapter} (Id, Name, Synopsis, Concept, Outcome, BodyText, CreatedAt, UpdatedAt, OrderIndex)
        VALUES (:Id, :Name, :Synopsis, :Concept, :Outcome, :BodyText, :CreatedAt, :UpdatedAt, :OrderIndex)
    "; 

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
            // Touch update timestamp
            UpdatedAt = DateTime.UtcNow;

            string SqlText =
                $"UPDATE {Project.SChapter} " +
                "SET Name = :Name, Synopsis = :Synopsis, Concept = :Concept, Outcome = :Outcome, " +
                "BodyText = :BodyText, UpdatedAt = :UpdatedAt, OrderIndex = :OrderIndex " +
                "WHERE Id = :Id";

            var Params = ToDictionary(); 
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            string SqlText = $"DELETE FROM {Project.SChapter} WHERE Id = :Id";

            var Params = ToDictionary(); 
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }

        /// <summary>
        /// Updates only the order index of this chapter
        /// </summary>
        public bool UpdateOrderIndex()
        {
            string SqlText = $"UPDATE {Project.SChapter} SET OrderIndex = :OrderIndex WHERE Id = :Id";
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
            string SqlText = $"UPDATE {Project.SChapter} SET BodyText = :BodyText WHERE Id = :Id";
            var Params = new Dictionary<string, object>
            {
                ["BodyText"] = BodyText,
                ["Id"] = Id
            };
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }
        public bool UpdateSynopsis()
        {
            string SqlText = $"UPDATE {Project.SChapter} SET Synopsis = :Synopsis WHERE Id = :Id";
            var Params = new Dictionary<string, object>
            {
                ["Synopsis"] = Synopsis,
                ["Id"] = Id
            };
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }
        public bool UpdateConcept()
        {
            string SqlText = $"UPDATE {Project.SChapter} SET Concept = :Concept WHERE Id = :Id";
            var Params = new Dictionary<string, object>
            {
                ["Concept"] = Concept,
                ["Id"] = Id
            };
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }
        public bool UpdateOutcome()
        {
            string SqlText = $"UPDATE {Project.SChapter} SET Outcome = :Outcome WHERE Id = :Id";
            var Params = new Dictionary<string, object>
            {
                ["Outcome"] = Outcome,
                ["Id"] = Id
            };
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }

        /// <summary>
        /// Returns true if any of this instance rich texts contains a specified term.
        /// </summary>
        public override bool RichTextContainsTerm(string Term)
        {
            if (App.RichTextContainsTerm(BodyText, Term)
                || App.RichTextContainsTerm(Synopsis, Term)
                || App.RichTextContainsTerm(Concept, Term)
                || App.RichTextContainsTerm(Outcome, Term))
                return true;
                
            return false;
        }

        // ● scenes
        /// <summary>
        /// True if a chapter exists by name
        /// </summary>
        public bool SceneExists(string Name, string Id = "")
        {
            return Project.ItemExists(SceneList.Cast<BaseEntity>(), Name, Id);
        }
        /// <summary>
        /// Renumbers the scenes in this chapter
        /// </summary>
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
