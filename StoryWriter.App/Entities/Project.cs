namespace StoryWriter
{
    public class Project
    {
        // ● constants
        static public readonly string SComponent = "Component";
        static public readonly string SChapter = "Chapter";
        static public readonly string SScene = "Scene";

        /// <summary>
        /// Loads all components from the database into FlatComponentList and TreeComponentList.
        /// </summary>
        void LoadComponents()
        {
            FlatComponentList.Clear();
            TreeComponentList.Clear();

            string SqlText = "SELECT * FROM Component";

            DataTable Table = App.SqlStore.Select(SqlText);             

            foreach (DataRow Row in Table.Rows)
            {
                Component item = new ();
                item.LoadFrom(Row);
                FlatComponentList.Add(item);
            }

            var childsHash = FlatComponentList.ToLookup(item => item.ParentId);

            foreach (var item in FlatComponentList)
            {
                item.Children = childsHash[item.Id].ToList();
                foreach (var Child in item.Children)
                    Child.Parent = item;
            }

            TreeComponentList = FlatComponentList.Where(item => item.HasNoParent())
                    .ToList();
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
        /// <summary>
        /// Adds the Component table to the schema version and populates it with initial data.
        /// </summary>
        static public void AddTableComponent(SchemaVersion sv)
        {
            string TableName = SComponent;
            string SqlText = $@"
create table {TableName} (
    Id						{SysConfig.PrimaryKeyStr()} 
    ,ParentId               {SysConfig.ForeignKeyStr()}
    ,Name                   @NVARCHAR(128)       @NOT_NULL  
    ,IsGroup                @BOOL default 0      @NOT_NULL  
    ,Notes                  @BLOB_TEXT           @NULL    

    ,constraint UK_{TableName}_00 unique (Name)
)
";
            sv.AddTable(SqlText);

            // main component groups
            string PersonId = "4739BDAF-07F2-46AE-A694-8AE2B552FF5D";
            string LocationId = "5E16404A-21AC-49F1-A26F-EF727BAD68B7";
            string PeoplesId = "516D25EC-A64C-4367-9877-9951F07177A7";
            string CultureId = "C8B1F946-587E-40A3-99ED-BD924614C4F9";
            string HistoricEventId = "5685EA42-158A-42F0-B9D5-CC5FDF2BE1A5";
            string ArtifactId = "3EBFC3DD-B9B3-4DCB-9449-D016213D5BE8";

            // sub groups of Person
            string CharacterId = "F5805305-E984-4466-BD7A-7B34AF9CF5C3";
            string HistoricId = "BB54E43B-5984-4924-AFE2-0BEC72490F61";

            // sub groups of Location
            string PlanetId = "16F21892-9873-4341-81A1-FDC2E17EA9D1";
            string SatelliteId = "54886725-B858-4510-ACCA-86AEF20B59EF";
            string ContinentId = "77DF037A-4D5A-4F16-BDFD-9B8D5CFE9046";
            string CountryId = "0C4F812B-8A04-43AB-B1DF-EFB1EE6B8A11";
            string CityId = "F9E83741-223C-4F49-B4C3-83E4B897306B";
            string VillageId = "EEFAF85A-B176-4248-B755-F042F5D7AEEF";
            string MountainId = "E4613FBB-2F8D-4DA2-B9CC-6B961672D1CC";
            string SeeId = "BA787B2B-8FFD-4695-89DE-A73FC6C691FB";
            string RiverId = "B6F78ADA-FF9A-43F0-9DBE-A62ECA925A3A";
            string LakeId = "05567AB2-C30A-4362-91EC-D7BCED5E6BAE";
            string VolcanoId = "B68756F6-5CF1-48C7-9370-33DEB3E075F4";

            string CreaateInsertSql(string Id, string ParentId, string Name, string Notes = "")
            {
                return $"insert into {TableName} (Id, ParentId, Name, IsGroup, Notes) values ('{Id}', '{ParentId}', '{Name}', 1, '{Notes}')";
            }
 
            sv.AddStatementAfter(CreaateInsertSql(PersonId, "", "Person"));
            sv.AddStatementAfter(CreaateInsertSql(CharacterId, PersonId, "Character"));
            sv.AddStatementAfter(CreaateInsertSql(HistoricId, PersonId, "Historic"));

            sv.AddStatementAfter(CreaateInsertSql(LocationId, "", "Location")); 
            sv.AddStatementAfter(CreaateInsertSql(PlanetId, LocationId, "Planet"));
            sv.AddStatementAfter(CreaateInsertSql(SatelliteId, LocationId, "Satellite"));
            sv.AddStatementAfter(CreaateInsertSql(ContinentId, LocationId, "Continent"));
            sv.AddStatementAfter(CreaateInsertSql(CountryId, LocationId, "Country"));
            sv.AddStatementAfter(CreaateInsertSql(CityId, LocationId, "City"));
            sv.AddStatementAfter(CreaateInsertSql(VillageId, LocationId, "Village"));
            sv.AddStatementAfter(CreaateInsertSql(MountainId, LocationId, "Mountain"));
            sv.AddStatementAfter(CreaateInsertSql(SeeId, LocationId, "See"));
            sv.AddStatementAfter(CreaateInsertSql(RiverId, LocationId, "River"));
            sv.AddStatementAfter(CreaateInsertSql(LakeId, LocationId, "Lake"));
            sv.AddStatementAfter(CreaateInsertSql(VolcanoId, LocationId, "Volcano"));

            sv.AddStatementAfter(CreaateInsertSql(PeoplesId, "", "Peoples"));
            sv.AddStatementAfter(CreaateInsertSql(CultureId, "", "Culture"));
            sv.AddStatementAfter(CreaateInsertSql(HistoricEventId, "", "Historic Event"));
            sv.AddStatementAfter(CreaateInsertSql(ArtifactId, "", "Artifact"));
        }
        /// <summary>
        /// Adds the Chapter table to the schema version.
        /// </summary>
        static public void AddTableChapter(SchemaVersion sv)
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
        static public void AddTableScene(SchemaVersion sv)
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
 
            AddTableComponent(Version);
            AddTableChapter(Version);
            AddTableScene(Version);

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

        // ● components
        /// <summary>
        /// True if a group/component exists by name
        /// </summary>
        public bool ComponentExists(string Name, string Id = "")
        {
            if (string.IsNullOrWhiteSpace(Id))
                return FlatComponentList.FirstOrDefault(item => item.Name.IsSameText(Name)) != null;

            return FlatComponentList.FirstOrDefault(item => item.Name.IsSameText(Name) && item.Id == Id) != null;
        }
        /// <summary>
        /// Adds a group/component either to its parent children, if it has a parent, or to the main tree component list.
        /// </summary>
        public void AddComponentToLists(Component Comp)
        {
            FlatComponentList.Add(Comp);
            if (Comp.Parent != null)
                Comp.Parent.Children.Add(Comp);
            else
                TreeComponentList.Add(Comp);
        }
        /// <summary>
        /// Removes a group/component either from its parent children, if it has a parent, or from the main tree component list.
        /// </summary>
        public void RemoveComponentFromLists(Component Comp)
        {
            FlatComponentList.Remove(Comp);
            if (Comp.Parent != null)
                Comp.Parent.Children.Remove(Comp);
            else
                TreeComponentList.Remove(Comp);
        }

        // ● chapters
        /// <summary>
        /// True if a chapter exists by name
        /// </summary>
        public bool ChapterExists(string Name, string Id = "")
        {
            if (string.IsNullOrWhiteSpace(Id))
                return ChapterList.FirstOrDefault(item => item.Name.IsSameText(Name)) != null;

            return ChapterList.FirstOrDefault(item => item.Name.IsSameText(Name) && item.Id == Id) != null;
        }
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

            List<LinkItem> TempLinkList = new List<LinkItem>();

            var Components = FlatComponentList.Where(item => !item.IsGroup && item.Name.ContainsText(Term));
            if (Components.Count() > 0)
            {
                foreach (var Component in Components)
                { 
                    var Link = new LinkItem(ItemType.Component, Component.Name, Component);                     
                    TempLinkList.Add(Link);
                }
            }

            var Chapters = ChapterList.Where(item => item.IsLinkOf(Term));
            if (Chapters.Count() > 0)
            {
                foreach (var Chapter in Chapters)
                {
                    var Link = new LinkItem(ItemType.Chapter, Chapter.Name, Chapter);
                    TempLinkList.Add(Link);
                }
            }


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
        /// The name of the project/book
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A list of components in a flat structure.
        /// </summary>
        public List<Component> FlatComponentList { get; private set; } = new List<Component>();
        /// <summary>
        /// A hierarchical tree of components.
        /// </summary>
        public List<Component> TreeComponentList { get; private set; } = new List<Component>();
        /// <summary>
        /// A list of chapters in the project.
        /// </summary>
        public List<Chapter> ChapterList { get; private set; } = new List<Chapter>();

      
    }
}
