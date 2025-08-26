namespace StoryWriter
{
    public class Project : BaseEntity
    {
        // ● constants
        static public readonly string STag = "Tag";
        static public readonly string SComponent = "Component";
        static public readonly string STagToComponent = "TagToComponent";
        static public readonly string SChapter = "Chapter";
        static public readonly string SScene = "Scene";

        /// <summary>
        /// Loads all tags from the database into TagList
        /// </summary>
        void LoadTags()
        {
            TagList.Clear();

            string SqlText = $"SELECT * FROM {STag}";

            DataTable Table = App.SqlStore.Select(SqlText);

            foreach (DataRow Row in Table.Rows)
            {
                Tag item = new();
                item.LoadFrom(Row);
               AddToList(item);
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
                AddToList(item);
            } 
        }
        /// <summary>
        /// Loads all TagToComponent items from the database into TagToComponentList
        /// </summary>
        void LoadTagToComponents()
        {
            TagToComponentList.Clear();

            string SqlText = $"SELECT * FROM {STagToComponent}";

            DataTable Table = App.SqlStore.Select(SqlText);

            foreach (DataRow Row in Table.Rows)
            {
                TagToComponent item = new();
                item.LoadFrom(Row);
                if (item.IsOk())
                    AddToList(item);
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
                AddToList(ch);
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
        /// <summary>
        /// Adds a table to a schema version
        /// </summary>
        static public void AddTagTable(SchemaVersion sv)
        {
            string TableName = STag;
            string SqlText = $@"
create table {TableName} (
    Id						{SysConfig.PrimaryKeyStr()} 
    ,Name                   @NVARCHAR(128)       @NOT_NULL  
    ,constraint UK_{TableName}_00 unique (Name)
)
";
            sv.AddTable(SqlText);

            string[] Items = {"Character", "Location", "People", "Trait", "Event", "Artifact", "Planet" };
 
            foreach (var Item in Items)
            {
                SqlText = $"insert into {TableName} (Id, Name) VALUES ('{Sys.GenId(UseBrackets:false)}', '{Item}')";
                sv.AddStatementAfter(SqlText);
            }
        }
        /// <summary>
        /// Adds a table to a schema version
        /// </summary>
        static public void AddComponentTable(SchemaVersion sv)
        {
            string TableName = SComponent;
            string SqlText = $@"
create table {TableName} (
    Id						{SysConfig.PrimaryKeyStr()} 
    ,Name                   @NVARCHAR(128)       @NOT_NULL  
    ,BodyText               @BLOB_TEXT           @NULL    

    ,constraint UK_{TableName}_00 unique (Name)
)
";
            sv.AddTable(SqlText);

        }
        /// <summary>
        /// Adds a table to a schema version
        /// </summary>
        static public void AddTagToComponentTable(SchemaVersion sv)
        {
            string TableName = STagToComponent;
            string SqlText = $@"
create table {TableName} (
     TagId              {SysConfig.ForeignKeyStr()}   
    ,ComponentId        {SysConfig.ForeignKeyStr()}

    ,constraint UK_{TableName}_00 unique (TagId, ComponentId)
)
"; 
            sv.AddTable(SqlText);
        }

        /// <summary>
        /// Adds a table to a schema version
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
        /// Adds a table to a schema version
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

            AddTagTable(Version);
            AddComponentTable(Version);
            AddTagToComponentTable(Version);
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
            LoadTags();
            LoadComponents();
            LoadTagToComponents();
            LoadChapters();            
        }

        // ● exists
        /// <summary>
        /// True if a specified instance exists in the corresponding list
        /// </summary>
        static public bool ItemExists(IEnumerable<BaseEntity> List, BaseEntity Instance)
        {
            if (string.IsNullOrWhiteSpace(Instance.Id))
                return List.FirstOrDefault(item => item.Name.IsSameText(Instance.Name)) != null;

            return List.FirstOrDefault(item => item.Name.IsSameText(Instance.Name) && item.Id == Instance.Id) != null;
        }
        /// <summary>
        /// True if a specified instance exists in the corresponding list
        /// </summary>
        public bool ItemExists(Tag Instance) => ItemExists(TagList.Cast<BaseEntity>(), Instance);
        /// <summary>
        /// True if a specified instance exists in the corresponding list
        /// </summary>
        public bool ItemExists(Component Instance) => ItemExists(ComponentList.Cast<BaseEntity>(), Instance);
        /// <summary>
        /// True if a specified instance exists in the corresponding list
        /// </summary>
        public bool ItemExists(TagToComponent Instance) => TagToComponentList.FirstOrDefault(item => item.Tag.Id == Instance.Tag.Id && item.Component.Id == Instance.Component.Id) != null;
        /// <summary>
        /// True if a specified instance exists in the corresponding list
        /// </summary>
        public bool ItemExists(Chapter Instance) => ItemExists(ChapterList.Cast<BaseEntity>(), Instance);
 
        /// <summary>
        /// Adds a specified instance to the corresponding list if it does not already exist
        /// </summary>
        static public void AddToList(IList List, object Instance)
        {
            if (!List.Contains(Instance)) 
                List.Add(Instance);
        }
        /// <summary>
        /// Adds a specified instance to the corresponding list if it does not already exist
        /// </summary>
        public void AddToList(Tag Instance) => AddToList(TagList, Instance);
        /// <summary>
        /// Adds a specified instance to the corresponding list if it does not already exist
        /// </summary>
        public void AddToList(Component Instance) => AddToList(ComponentList, Instance);
        /// <summary>
        /// Adds a specified instance to the corresponding list if it does not already exist
        /// </summary>
        public void AddToList(TagToComponent Instance) => AddToList(TagToComponentList, Instance);
        /// <summary>
        /// Adds a specified instance to the corresponding list if it does not already exist
        /// </summary>
        public void AddToList(Chapter Instance) => AddToList(ChapterList, Instance);

        /// <summary>
        /// Adjusts the tags for a specified component
        /// </summary>
        public void AdjustComponentTags(Component Component, List<Tag> TagList)
        {
            string SqlText = $"delete from {Project.STagToComponent} where ComponentId = '{Component.Id}'";
            App.SqlStore.ExecSql(SqlText);

            LoadTagToComponents(); // empty the list and reload

            foreach (var Tag in TagList)
            {
                var TagToComponent = new TagToComponent(Tag, Component);
                TagToComponent.Insert();
            }
        }
         

        // ● miscs
        /// <summary>
        /// Renumbers the chapters
        /// </summary>
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
        /// <summary>
        /// Returns a list of items that match a specified search term
        /// </summary>
        List<LinkItem> SearchItems(string Term)
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

                
                AddLinks(ComponentList.Cast<BaseEntity>(), ItemType.Component);
                AddLinks(ChapterList.Cast<BaseEntity>(), ItemType.Chapter);
                foreach (var Chapter in ChapterList)
                    AddLinks(Chapter.SceneList.Cast<BaseEntity>(), ItemType.Scene);
            }
            
            return TempLinkList;

        }
        /// <summary>
        /// Shows the page for a specified link item, i.e. shows a component page or a chapter page.
        /// </summary>
        public void ShowPageByLinkItem(LinkItem LinkItem)
        {
            switch (LinkItem.ItemType)
            {
                case ItemType.Component:
                    Component Component = LinkItem.Item as Component;
                    App.ContentPagerHandler.ShowPage(typeof(UC_Component), Component.Id, Component);
                    break;
                case ItemType.Chapter:
                    Chapter Chapter = LinkItem.Item as Chapter;
                    App.ContentPagerHandler.ShowPage(typeof(UC_Chapter), Chapter.Id, Chapter);
                    break;
                case ItemType.Scene:
                    Scene Scene = LinkItem.Item as Scene;
                    App.ContentPagerHandler.ShowPage(typeof(UC_Chapter), Scene.Chapter.Id, Scene.Chapter);
                    break;

            }
        }
        /// <summary>
        /// Shows the page for a specified search term, i.e. shows a component page or a chapter page
        /// </summary>
        public void ShowPageByTerm(string Term)
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
                ShowPageByLinkItem(LinkItem);
            }
            else if (TempLinkList.Count > 1)
            {
                SearchResultsDialog.ShowModal(TempLinkList);
            }

        }

        // ● properties 
        /// <summary>
        /// The list of groups of this project.
        /// <para><strong>CAUTION: </strong> Do <strong>NOT</strong> add an item using directly the list. Use the corresponding <c>AddToList()</c> method.</para>
        /// </summary>
        public List<Tag> TagList { get; private set; } = new List<Tag>();
        /// <summary>
        /// The list of components of this project
        /// <para><strong>CAUTION: </strong> Do <strong>NOT</strong> add an item using directly the list. Use the corresponding <c>AddToList()</c> method.</para>
        /// </summary>
        public List<Component> ComponentList { get; private set; } = new List<Component>();
        /// <summary>
        /// A list of TagToComponent items
        /// <para><strong>CAUTION: </strong> Do <strong>NOT</strong> add an item using directly the list. Use the corresponding <c>AddToList()</c> method.</para>
        /// </summary>
        public List<TagToComponent> TagToComponentList { get; private set; } = new List<TagToComponent>();
        /// <summary>
        /// A list of chapters in the project.
        /// <para><strong>CAUTION: </strong> Do <strong>NOT</strong> add an item using directly the list. Use the corresponding <c>AddToList()</c> method.</para>
        /// </summary>
        public List<Chapter> ChapterList { get; private set; } = new List<Chapter>();

         
    }
}
