namespace StoryWriter
{
    /// <summary>
    /// Book chapter data model
    /// </summary>
    public class Chapter : BaseEntity
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
            if (CreatedAt == default)
                CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;

            OrderIndex = App.CurrentStory.ChapterList.Count + 1;

            string SqlText = @$"
        INSERT INTO {Story.SChapter} (Id, Name, Synopsis, BodyText, CreatedAt, UpdatedAt, OrderIndex)
        VALUES (:Id, :Name, :Synopsis, :BodyText, :CreatedAt, :UpdatedAt, :OrderIndex)
    ";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentStory.AddToList(this);
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
                $"UPDATE {Story.SChapter} " +
                "SET Name = :Name, Synopsis = :Synopsis, " +
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
            string SqlText = $"DELETE FROM {Story.SScene} WHERE ChapterId = :Id";
            App.SqlStore.ExecSql(SqlText, new Dictionary<string, object> { ["Id"] = Id });

            SqlText = $"DELETE FROM {Story.SChapter} WHERE Id = :Id";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);

            //App.CurrentStory.ChapterList.Remove(this);
            App.CurrentStory.ChapterDeleted(this); // re-loads all needed item lists

            return true;
        }

        /// <summary>
        /// Updates only the order index of this chapter
        /// </summary>
        public bool UpdateOrderIndex()
        {
            string SqlText = $"UPDATE {Story.SChapter} SET OrderIndex = :OrderIndex WHERE Id = :Id";
            var Params = new Dictionary<string, object>
            {
                ["OrderIndex"] = OrderIndex,
                ["Id"] = Id
            };
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }
        /// <summary>
        /// Updates only the body text of this instance
        /// </summary>
        public bool UpdateBodyText()
        {
            string SqlText = $"UPDATE {Story.SChapter} SET BodyText = :BodyText WHERE Id = :Id";
            var Params = new Dictionary<string, object>
            {
                ["BodyText"] = BodyText,
                ["Id"] = Id
            };
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }
        /// <summary>
        /// Updates only the synopsis of this instance
        /// </summary>
        public bool UpdateSynopsisText()
        {
            string SqlText = $"UPDATE {Story.SChapter} SET Synopsis = :Synopsis WHERE Id = :Id";
            var Params = new Dictionary<string, object>
            {
                ["Synopsis"] = Synopsis,
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
            if (App.RichTextContainsTerm(BodyText, Term) || App.RichTextContainsTerm(Synopsis, Term))
                return true;

            return false;
        }

        // ● scenes
        /// <summary>
        /// The scenes in this chapter
        /// </summary>
        public List<Scene> GetSceneList()
        {
            List<Scene> SceneList = App.CurrentStory.SceneList;

            SceneList = SceneList.Where(x => x.ChapterId == Id).ToList();
            SceneList.Sort((x, y) => x.OrderIndex.CompareTo(y.OrderIndex));
            return SceneList;
        }
        /// <summary>
        /// Renumbers the scenes in this chapter
        /// </summary>
        public void RenumberScenes(List<Scene> SceneList = null)
        {
            if (SceneList == null)
                SceneList = GetSceneList();

            int OrderIndex = 0;
            foreach (var Scene in SceneList)
            {
                OrderIndex++;
                Scene.OrderIndex = OrderIndex;
            }

            foreach (var Scene in SceneList)
                Scene.UpdateOrderIndex();
        }

        // ● properties
        /// <summary>
        /// Short summary / plot abstraction
        /// </summary>
        public string Synopsis { get; set; } = string.Empty;
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

 
    }
}
