namespace StoryWriter
{
    public class Note : BaseEntity
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
            Dict["OrderIndex"] = OrderIndex;
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
            OrderIndex = Row.AsInteger("OrderIndex");
        }

        /// <summary>
        /// Inserts this instance into the database.
        /// </summary>
        public bool Insert()
        {
            string SqlText = $"INSERT INTO {Story.SNote} (Id, Name, BodyText, OrderIndex) VALUES (:Id, :Name, :BodyText, :OrderIndex)";

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
            string SqlText = $"UPDATE {Story.SNote} SET Name = :Name, BodyText = :BodyText, OrderIndex = :OrderIndex WHERE Id = :Id";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            string SqlText = $"delete from {Story.SNote} where Id = :Id";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentStory.NoteList.Remove(this);
            return true;
        }

        /// <summary>
        /// Updates the order index of this instance in the database.
        /// </summary>
        public bool UpdateOrderIndex()
        {
            string SqlText = $"UPDATE {Story.SNote} SET OrderIndex = :OrderIndex WHERE Id = :Id";
            var Params = new Dictionary<string, object>
            {
                ["OrderIndex"] = OrderIndex,
                ["Id"] = Id
            };
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }
        /// <summary>
        /// Updates the body text of this instance in the database.
        /// </summary>
        public bool UpdateBodyText()
        {
            string SqlText = $"UPDATE {Story.SNote} SET BodyText = :BodyText WHERE Id = :Id";
            var Params = new Dictionary<string, object>
            {
                ["BodyText"] = BodyText,
                ["Id"] = Id
            };
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }

        /// <summary>
        /// Returns true if any of this instance rich texts contains a specified term.
        /// </summary>
        public override bool BodyTextContainsTerm(string Term, bool WholeWordOnly)
        {
            if (string.IsNullOrWhiteSpace(BodyText))
                return false;

            return App.RichTextContainsTerm(BodyText, Term, WholeWordOnly);
        }

        // ● properties
        /// <summary>
        /// The text of this instance.  
        /// </summary>
        public string BodyText { get; set; }
        /// <summary>
        /// Sort order of this item.
        /// </summary>
        public int OrderIndex { get; set; } = 0;
    }
}

/*
Id
Name
BodyText
OrderIndex 
 */
