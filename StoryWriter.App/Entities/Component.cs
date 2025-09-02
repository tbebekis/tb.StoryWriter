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
        }

        /// <summary>
        /// Inserts this instance into the database.
        /// </summary>
        public bool Insert()
        {
            string SqlText = $"INSERT INTO {Project.SComponent} (Id, Name, BodyText) VALUES (:Id, :Name, :BodyText)";    
            
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
            string SqlText = $"UPDATE {Project.SComponent} SET Name = :Name, BodyText = :BodyText WHERE Id = :Id";    
            
            var Params = ToDictionary(); 
            App.SqlStore.ExecSql(SqlText, Params);
            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            string SqlText = $"delete from {Project.SComponent} where Id = :Id";

            var Params = ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);
            App.CurrentProject.ComponentList.Remove(this);
            return true;
        }


        static public DataTable GetComponentTagsTable()
        {
            string SqlText = $@"
select 
  co.Id        as ComponentId,
  co.Name      as Component,
  t.Id         as TagId, 
  t.Name       as Tag  
from 
  {Project.STag} t
     inner join {Project.STagToComponent} tc on tc.TagId = t.Id
     inner join {Project.SComponent} co on co.Id = tc.ComponentId
order by
  co.Name 
";
            DataTable Table = App.SqlStore.Select(SqlText);

            return Table;
        }

        public List<Tag> GetTagList()
        {
            DataTable Table = GetComponentTagsTable();

            DataRow[] Rows = Table.Select($"ComponentId = '{Id}'");  

            List<Tag> TagList = new List<Tag>();
            foreach (DataRow Row in Rows)
            {
                Tag item = new Tag();

                item.Id = Row.AsString("TagId");
                item.Name = Row.AsString("Tag");
                TagList.Add(item);
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


        /// <summary>
        /// Returns true if any of this instance rich texts contains a specified term.
        /// </summary>
        public override bool RichTextContainsTerm(string Term)
        {
            if (string.IsNullOrWhiteSpace(BodyText))
                return false;

            return App.RichTextContainsTerm(BodyText, Term);
        }

        // ● properties
        /// <summary>
        /// The text of this instance.  
        /// </summary>
        public string BodyText { get; set; }

    }
}
