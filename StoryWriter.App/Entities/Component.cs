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
            Dict["BodyText"] = BodyText;
            Dict["TypeId"] = TypeId;
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
            TypeId = Row.AsString("TypeId");
            OrderIndex = Row.AsInteger("OrderIndex");
        }

        /// <summary>
        /// Inserts this instance into the database.
        /// </summary>
        public bool Insert()
        {
            string SqlText = $"INSERT INTO {Story.SComponent} (Id, Name, BodyText, TypeId, OrderIndex) VALUES (:Id, :Name, :BodyText, :TypeId, :OrderIndex)";

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
            string SqlText = $"UPDATE {Story.SComponent} SET Name = :Name, BodyText = :BodyText, TypeId = :TypeId, OrderIndex = :OrderIndex WHERE Id = :Id";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            string SqlText = $"delete from {Story.SComponent} where Id = :Id";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentStory.ComponentList.Remove(this);
            return true;
        }

        /// <summary>
        /// Updates the order index of this instance in the database.
        /// </summary>
        public bool UpdateOrderIndex()
        {
            string SqlText = $"UPDATE {Story.SComponent} SET OrderIndex = :OrderIndex WHERE Id = :Id";
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
            string SqlText = $"UPDATE {Story.SComponent} SET BodyText = :BodyText WHERE Id = :Id";
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
        public override bool RichTextContainsTerm(string Term, bool WholeWordOnly)
        {
            if (string.IsNullOrWhiteSpace(BodyText))
                return false;

            return App.RichTextContainsTerm(BodyText, Term, WholeWordOnly);
        }

        // ● tags related
        public List<Tag> GetTagList()
        {
            List<Tag> TagList = new();
            var TagToComponentList = App.CurrentStory.TagToComponentList.Where(x => x.ComponentId == Id).ToList();

            foreach (TagToComponent TagToComponent in TagToComponentList)
            {
                Tag Tag = TagToComponent.GetTag();
                if (Tag != null)
                    TagList.Add(Tag);
            }


            return TagList;
        }
        public string GetTagsAsLine()
        {
            List<Tag> TagList = GetTagList();
            if (TagList.Count == 0)
                return "";

            List<string> TagNames = TagList.Select(x => x.Name).ToList();
            string Result = string.Join(", ", TagNames.ToArray());

            return Result;
        }
 

        // ● properties
        /// <summary>
        /// The parent ComponentType of this scene.
        /// </summary>
        public ComponentType ComponentType => App.CurrentStory?.ComponentTypeList.FirstOrDefault(ch => ch.Id == TypeId);
        public string Category => ComponentType != null ? ComponentType.Name : "";

        /// <summary>
        /// Foreign key reference to the parent ComponentType
        /// </summary>
        public string TypeId { get; set; }
        /// <summary>
        /// The text of this instance.  
        /// </summary>
        public string BodyText { get; set; }
        /// <summary>
        /// Sort order of this item.
        /// <para><strong>NOTE: </strong> >0 means is displayed in Quick View</para>
        /// </summary>
        public int OrderIndex { get; set; } = 0;

    }
}
/*
TABLE: Component
Id
Name
BodyText
TypeId
OrderIndex  ( >0 means is displayed in Quick View) 
 */