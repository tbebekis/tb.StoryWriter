namespace StoryWriter
{

    /// <summary>
    /// Represents a component or a group of components in a hierarchical structure.
    /// <para>A component may represent a group, a sub-group or an actual component. These two are the only group levels allowed.</para>
    /// <para>A group may have 'Person` as its name where its sub-groups could be `Character` or `Historic`.</para>
    /// <para>An actual component may represent a character, a location, an item, an event, etc. and it is child to a group or sub-group.</para>
    /// </summary>
    public class Component
    {
        // ● construction
        /// <summary>
        /// Constructor
        /// </summary>
        public Component() { }

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
            Dict["ParentId"] = ParentId;
            Dict["Name"] = Name;
            Dict["IsGroup"] = IsGroup ? 1 : 0;
            Dict["Notes"] = Notes;
            return Dict;
        }
        /// <summary>
        /// Loads the properties of this object from a DataRow.
        /// </summary>
        public void LoadFrom(DataRow Row)
        {
            this.Id = Row.AsString("Id");
            this.ParentId = Row.AsString("ParentId");
            this.Name = Row.AsString("Name");
            this.IsGroup = Row.AsBoolean("IsGroup");       
            this.Notes = Row.AsString("Notes");            
        }

        /// <summary>
        /// Inserts this instance into the database.
        /// </summary>
        public bool Insert()
        {
            if (App.CurrentProject.FlatComponentList.Any(item => item.Name.IsSameText(this.Name)))
            {
                App.ErrorBox($"A component with the name '{this.Name}' already exists.");
                return false;
            }

            string SqlText = $"insert into {Project.SComponent} (Id, ParentId, Name, IsGroup, Notes) values (:Id, :ParentId, :Name, :IsGroup, :Notes)";
            var Params = this.ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);

            App.CurrentProject.AddComponentToLists(this);

            return true;
        }
        /// <summary>
        /// Updates this instance in the database.
        /// </summary>
        public bool Update()
        {
            if (App.CurrentProject.FlatComponentList.Any(item => item.Name.IsSameText(this.Name) && item.Id != this.Id))
            {
                App.ErrorBox($"A component with the name '{this.Name}' already exists.");
                return false;
            }

            string SqlText = $"update {Project.SComponent} set ParentId = :ParentId,  Name = :Name, IsGroup = :IsGroup, Notes = :Notes where Id = :Id";
            var Params = this.ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);

            return true;
        }
        /// <summary>
        /// Deletes this instance from the database.
        /// </summary>
        public bool Delete()
        {
            if (!App.QuestionBox($"Are you sure you want to delete the component '{this.Title}'?"))
                return false;

            string SqlText = $"delete from {Project.SComponent} where Id = :Id";
            var Params = this.ToDictionary();
            App.SqlStore.ExecSql(SqlText, Params);

            App.CurrentProject.RemoveComponentFromLists(this);            

            return true;
        }

        /// <summary>
        /// Returns true if this Node has no parent (is a root node).
        /// </summary>
        public bool HasNoParent() => string.IsNullOrWhiteSpace(ParentId);
        /// <summary>
        /// Returns true if this Node has any child nodes that are groups, or if any of its child nodes have groups.
        /// </summary>
        public bool HasGroups()
        {
            foreach (var Child in Children)
            {
                if (Child.IsGroup || Child.HasGroups())
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Returns true if this Node can have a new group added as child.
        /// </summary>
        public bool CanAddGroup()
        {
            return IsGroup && Level == 0;
        }
        /// <summary>
        /// Returns true if this Node can have a new component added as child.
        /// </summary>
        public bool CanAddComponent()
        {
            return IsGroup && !HasGroups();
        }
 
        // ● properties
        /// <summary>
        /// The unique identifier of this instance.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// The unique identifier of the parent instance, or empty if this instance has no parent.
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// The name of this instance. Must be unique across all instances.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// True if this instance is a group (can have child groups or components).
        /// </summary>
        public bool IsGroup { get; set; }
        /// <summary>
        /// The notes of this instance. Used with actual components, not groups.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Returns true if this Node has child nodes.
        /// </summary>
        public bool HasChildNodes => Children.Count > 0;
        /// <summary>
        /// Returns the level of this Node. A root node has level 0, its children have level 1 and so on.
        /// </summary>
        public int Level => Parent == null ? 0 : Parent.Level + 1;
        /// <summary>
        /// Returns the index of this Node in the list of its parent, if any, else -1
        /// </summary>
        public int Index => Parent == null ? -1 : Parent.Children.IndexOf(this);
        /// <summary>
        /// Returns the total number of Nodes of this node and its child nodes.
        /// </summary>
        public int TotalCount
        {
            get
            {
                int Result = Children.Count;
                foreach (var Child in Children)
                    Result += Child.TotalCount;
                return Result;
            }
        }

        /// <summary>
        /// The parent of this instance, or null if this instance has no parent.
        /// </summary>
        public Component Parent { get; set; }
        /// <summary>
        /// The list of child instances of this instance.
        /// </summary>
        public List<Component> Children { get; set; } = new List<Component>();
        /// <summary>
        /// Returns the title of this instance.
        /// </summary>
        public string Title => Parent != null ? $"{Parent.Name} > {Name}" : Name;
    }
}
