namespace StoryWriter
{
    public class Project : BaseEntity
    {
        // ● constants
        static public readonly string SComponent = "Component";
        static public readonly string SGroup = "Group";
        static public readonly string SSubGroup = "SubGroup";
        static public readonly string SChapter = "Chapter";
        static public readonly string SScene = "Scene";

        void LoadGroups()
        {
            GroupList.Clear();

            string SqlText = $"SELECT * FROM {SGroup}";

            DataTable Table = App.SqlStore.Select(SqlText);

            foreach (DataRow Row in Table.Rows)
            {
                Group item = new();
                item.LoadFrom(Row);
                GroupList.Add(item);
            }
        }
        void LoadSubGroups()
        {
            SubGroupList.Clear();

            string SqlText = $"SELECT * FROM {SSubGroup}";

            DataTable Table = App.SqlStore.Select(SqlText);

            foreach (DataRow Row in Table.Rows)
            {
                SubGroup item = new();
                item.LoadFrom(Row);
                SubGroupList.Add(item);
            }
        }

        /// <summary>
        /// Loads all components from the database into FlatComponentList and TreeComponentList.
        /// </summary>
        void LoadComponents()
        {
            ComponentList.Clear();

            string SqlText = $"SELECT * FROM {SComponent}";

            DataTable Table = App.SqlStore.Select(SqlText);             

            foreach (DataRow Row in Table.Rows)
            {
                Component item = new ();
                item.LoadFrom(Row);
                ComponentList.Add(item);
            } 
        }
        /// <summary>
        /// Loads all chapters and their associated scenes from the database into ChapterList.
        /// </summary>
        void LoadChapters()
        {
            ChapterList.Clear();

            string sql = $@"
        SELECT *
        FROM {SChapter}
        ORDER BY OrderIndex, CreatedAt
    ";

            DataTable table = App.SqlStore.Select(sql);
            foreach (DataRow row in table.Rows)
            {
                var ch = new Chapter();
                ch.LoadFrom(row);
                ChapterList.Add(ch);
            }

            foreach (var ch in ChapterList)
            {
 
                LoadScenes(ch);
            }
        }
        /// <summary>
        /// Loads all scenes associated with a given chapter from the database into the chapter's SceneList.
        /// </summary>
        void LoadScenes(Chapter Chapter)
        {
            Chapter.SceneList.Clear();

            string SqlText = $@"
        SELECT *
        FROM {SScene}
        WHERE ChapterId = :ChapterId
        ORDER BY OrderIndex, CreatedAt
    ";

            var Params = new Dictionary<string, object>
            {
                ["ChapterId"] = Chapter.Id
            };

            DataTable table = App.SqlStore.Select(SqlText, Params);
            foreach (DataRow row in table.Rows)
            {
                var sc = new Scene();
                sc.LoadFrom(row);
                Chapter.SceneList.Add(sc);
            }
        }


        // ● construction
        /// <summary>
        /// Constructor
        /// </summary>
        public Project() 
            : this("no name") 
        { 
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public Project(string Name)
        {
            this.Name = Name;
        }
 
        // ● static
        /// <summary>
        /// Returns true if the provided project name is valid (not empty, no invalid characters, does not start with a digit).
        /// </summary>
        static public bool IsValidProjectName(string ProjectName)
        {
            if (string.IsNullOrWhiteSpace(ProjectName))
                return false;
            char[] InvalidChars = System.IO.Path.GetInvalidFileNameChars();

            foreach (char c in InvalidChars)
            {
                if (ProjectName.Contains(c))
                    return false;
            }

            if (char.IsDigit(ProjectName[0]))
                return false;

            return true;
        }
        /// <summary>
        /// Returns the project name derived from the given project file name by removing the extension and replacing underscores with spaces.
        /// </summary>
        static public string ProjectFileNameToProjectName(string ProjectFileName)
        {
            string Result = Path.GetFileNameWithoutExtension(ProjectFileName);
            Result = Result.Replace('_', ' ');
            return Result;
        }
        /// <summary>
        /// Returns a valid project file name derived from the given project name by replacing spaces with underscores and appending the .db3 extension.
        /// </summary>
        static public string ProjectNameToProjectFileName(string ProjectName)
        {
            string Result = ProjectName.Replace(' ', '_');
            Result += ".db3";
            return Result;
        }

        // ● schema
        static public void AddGroupTable(SchemaVersion sv)
        {
            string TableName = SGroup;
            string SqlText = $@"
create table {TableName} (
    Id						{SysConfig.PrimaryKeyStr()} 
    ,Name                   @NVARCHAR(128)       @NOT_NULL  
    ,constraint UK_{TableName}_00 unique (Name)
)
";
            sv.AddTable(SqlText);

            string[] Groups = {"Character", "Location", "People", "Trait", "Event", "Artifact" };
 
            foreach (var Item in Groups)
            {
                SqlText = $"insert into {TableName} (Id, Name) VALUES ('{Sys.GenId(UseBrackets:false)}', ('{Item}'))";
                sv.AddStatementAfter(SqlText);
            }
        }
        static public void AddSubGroupTable(SchemaVersion sv)
        {
            string TableName = SSubGroup;
            string SqlText = $@"
create table {TableName} (
    Id						{SysConfig.PrimaryKeyStr()} 
    ,Name                   @NVARCHAR(128)       @NOT_NULL  
    ,constraint UK_{TableName}_00 unique (Name)
)
";
            sv.AddTable(SqlText);

            string[] SubGroups = {"Historic", "Planet", "Country", "City", "Mountain", "See" };

            foreach (var Item in SubGroups)
            {
                SqlText = $"insert into {TableName} (Id, Name) VALUES  ('{Sys.GenId(UseBrackets: false)}', ('{Item}'))";
                sv.AddStatementAfter(SqlText);
            }
        }

        /// <summary>
        /// Adds the Component table to the schema version and populates it with initial data.
        /// </summary>
        static public void AddComponentTable(SchemaVersion sv)
        {
            string TableName = SComponent;
            string SqlText = $@"
create table {TableName} (
    Id						{SysConfig.PrimaryKeyStr()} 
    ,Name                   @NVARCHAR(128)       @NOT_NULL  
    ,Group                  @NVARCHAR(128)       @NOT_NULL  
    ,SubGroup               @NVARCHAR(128)       @NOT_NULL  
    ,BodyText               @BLOB_TEXT           @NULL    

    ,constraint UK_{TableName}_00 unique (Name)
)
";
            sv.AddTable(SqlText);

        }
        /// <summary>
        /// Adds the Chapter table to the schema version.
        /// </summary>
        static public void AddChapterTable(SchemaVersion sv)
        {
            string TableName = SChapter;

            string SqlText = $@"
create table {TableName} (
     Id                 {SysConfig.PrimaryKeyStr()} 
    ,Name               @NVARCHAR(128)              @NOT_NULL           -- τίτλος κεφαλαίου
    ,Synopsis           @BLOB_TEXT                  @NULL               -- αυτό που είπες Plot/Abstraction
    ,Concept            @BLOB_TEXT                  @NULL               -- βασική ιδέα / νόημα
    ,Outcome            @BLOB_TEXT                  @NULL               -- τελικό status/έκβαση του κεφαλαίου (αν το ξεχωρίσεις)
    ,BodyText           @BLOB_TEXT                  @NULL                   -- πλήρες κείμενο
    ,CreatedAt          @DATE_TIME                  @NOT_NULL
    ,UpdatedAt          @DATE_TIME                  @NOT_NULL
    ,OrderIndex         integer default 0           @NOT_NULL

    ,constraint UK_{TableName}_00 unique (Name)
)
";
            sv.AddTable(SqlText);
        }
        /// <summary>
        /// Adds the Scene table to the schema version.
        /// </summary>
        static public void AddSceneTable(SchemaVersion sv)
        {
            string TableName = SScene;

            string SqlText = $@"
create table {TableName} (
    Id                      {SysConfig.PrimaryKeyStr()} 
    ,ChapterId              {SysConfig.ForeignKeyStr()}   
    ,Name                   @NVARCHAR(128)              @NOT_NULL                       -- προαιρετικός τίτλος σκηνής
    ,BodyText               @BLOB_TEXT                  @NULL                           -- κείμενο σκηνής
    ,Summary                @BLOB_TEXT                  @NULL                           -- μίνι περίληψη/σκοπός σκηνής
    ,CreatedAt              @DATE_TIME                  @NOT_NULL
    ,UpdatedAt              @DATE_TIME                  @NOT_NULL
    ,OrderIndex             integer default 0           @NOT_NULL                       -- σειρά εμφάνισης της σκηνής μέσα στο κεφάλαιο
   
    ,constraint UK_{TableName}_00 unique (Name)
)
";
            sv.AddTable(SqlText);
        }

        // ● public
        /// <summary>
        /// Returns the name of this instance.
        /// </summary>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// Creates the database schema for the project, including tables for components, chapters, and scenes.
        /// </summary>
        public void CreateSchema()
        {
            Schema ProjectSchema = Schemas.FindOrAdd(Sys.APPLICATION, Sys.DEFAULT); //Name
            SchemaVersion Version  = ProjectSchema.FindOrAdd(Version: 1);
 
            AddComponentTable(Version);
            AddChapterTable(Version);
            AddSceneTable(Version);

            Schemas.Execute();

            System.Threading.Thread.Sleep(2000); // wait for the database to be ready
        }

        // ● project open/close
        /// <summary>
        /// Closes all open content pages in the application.
        /// </summary>
        public void Close()
        {
            App.ContentPagerHandler.CloseAll();
        }
        /// <summary>
        /// Opens the project by loading components and chapters from the database.
        /// </summary>
        public void Open()
        {
            LoadComponents();
            LoadChapters();
        }

        // ● exists
        /// <summary>
        /// True if a group exists by name
        /// </summary>
        public bool GroupExists(string Name, string Id = "")
        {
            if (string.IsNullOrWhiteSpace(Id))
                return GroupList.FirstOrDefault(item => item.Name.IsSameText(Name)) != null;

            return GroupList.FirstOrDefault(item => item.Name.IsSameText(Name) && item.Id == Id) != null;
        }
        /// <summary>
        /// True if a sub-group exists by name
        /// </summary>
        public bool SubGroupExists(string Name, string Id = "")
        {
            if (string.IsNullOrWhiteSpace(Id))
                return SubGroupList.FirstOrDefault(item => item.Name.IsSameText(Name)) != null;

            return SubGroupList.FirstOrDefault(item => item.Name.IsSameText(Name) && item.Id == Id) != null;
        }
        /// <summary>
        /// True if a component exists by name
        /// </summary>
        public bool ComponentExists(string Name, string Id = "")
        {
            if (string.IsNullOrWhiteSpace(Id))
                return ComponentList.FirstOrDefault(item => item.Name.IsSameText(Name)) != null;

            return ComponentList.FirstOrDefault(item => item.Name.IsSameText(Name) && item.Id == Id) != null;
        }
        /// <summary>
        /// True if a chapter exists by name
        /// </summary>
        public bool ChapterExists(string Name, string Id = "")
        {
            if (string.IsNullOrWhiteSpace(Id))
                return ChapterList.FirstOrDefault(item => item.Name.IsSameText(Name)) != null;

            return ChapterList.FirstOrDefault(item => item.Name.IsSameText(Name) && item.Id == Id) != null;
        }

        // ● miscs
        public void RenumberChapters()
        {
            int OrderIndex = 0;
            foreach (var Chapter in ChapterList)
            {
                OrderIndex++;
                Chapter.OrderIndex = OrderIndex;
            }

            foreach (var Chapter in ChapterList)
                Chapter.UpdateOrderIndex();
        }
 
        // ● links
        public List<LinkItem> SearchItems(string Term)
        {
            List<LinkItem> TempLinkList = new ();

            if (!string.IsNullOrWhiteSpace(Term))
            {
                void AddLinks(IEnumerable<BaseEntity> EntityList, ItemType ItemType)
                {
                    var Entities = EntityList.Where(item => item.Name.ContainsText(Term));
                    if (Entities.Any())
                    {
                        foreach (var Item in Entities)
                        {
                            var Link = new LinkItem(ItemType, Item.Name, Item);
                            TempLinkList.Add(Link);
                        }
                    }
                }

                AddLinks(GroupList.Cast<BaseEntity>(), ItemType.Group);
                AddLinks(SubGroupList.Cast<BaseEntity>(), ItemType.SubGroup);
                AddLinks(ComponentList.Cast<BaseEntity>(), ItemType.Component);
                AddLinks(ChapterList.Cast<BaseEntity>(), ItemType.Chapter);
                AddLinks(GroupList.Cast<BaseEntity>(), ItemType.Group);
                foreach (var Chapter in ChapterList)
                    AddLinks(Chapter.SceneList.Cast<BaseEntity>(), ItemType.Scene);
            }

            
            return TempLinkList;

        }
        public void OpenLinkItem(LinkItem LinkItem)
        {
            switch (LinkItem.ItemType)
            {
                case ItemType.Component:
                    Component Comp = LinkItem.Item as Component;
                    App.ContentPagerHandler.ShowPage(typeof(UC_Component), Comp.Id, Comp);
                    break;
                case ItemType.Chapter:
                    //OpenChapter(LinkItem.ItemId);
                    break;
            }
        }
        public void OpenPageByTerm(string Term)
        {
            if (string.IsNullOrWhiteSpace(Term))
                return;

            List<LinkItem> TempLinkList = SearchItems(Term);   

            if (TempLinkList.Count == 0)
            {
                string Message = $"No results found for '{Term}'";
                LogBox.AppendLine(Message);
            }
            else if (TempLinkList.Count == 1)
            {
                LinkItem LinkItem = TempLinkList[0];
                OpenLinkItem(LinkItem);
            }

        }

        // ● properties 
        /// <summary>
        /// The list of groups of this project
        /// </summary>
        public List<Group> GroupList { get; private set; } = new List<Group>();
        /// <summary>
        /// The list of sub-groups of this project
        /// </summary>
        public List<SubGroup> SubGroupList { get; private set; } = new List<SubGroup>();
        /// <summary>
        /// The list of components of this project
        /// </summary>
        public List<Component> ComponentList { get; private set; } = new List<Component>();
        /// <summary>
        /// A list of chapters in the project.
        /// </summary>
        public List<Chapter> ChapterList { get; private set; } = new List<Chapter>();

      
    }
}
