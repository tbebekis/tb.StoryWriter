namespace StoryWriter
{
    public partial class Story
    {
        // ● constants
        static public readonly string STag = "Tag";
        static public readonly string SComponentType = "ComponentType";
        static public readonly string SComponent = "Component";
        static public readonly string STagToComponent = "TagToComponent";
        static public readonly string SChapter = "Chapter";
        static public readonly string SScene = "Scene";
        static public readonly string SComponentToScene = "ComponentToScene";
        static public readonly string SNote = "Note";


        // ● static - schema
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
        }
        /// <summary>
        /// Adds a table to a schema version
        /// </summary>
        static public void AddComponentTypeTable(SchemaVersion sv)
        {
            string TableName = SComponentType;
            string SqlText = $@"
create table {TableName} (
    Id						{SysConfig.PrimaryKeyStr()} 
    ,Name                   @NVARCHAR(128)       @NOT_NULL  
    ,constraint UK_{TableName}_00 unique (Name)
)
";
            sv.AddTable(SqlText);
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
    ,TypeId                 {SysConfig.ForeignKeyStr()}   
    ,Name                   @NVARCHAR(128)       @NOT_NULL  
    ,BodyText               @BLOB_TEXT           @NULL        
    ,OrderIndex             integer default 0    @NOT_NULL 

    ,constraint UK_{TableName}_00 unique (Name)
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
    ,Name               @NVARCHAR(128)              @NOT_NULL         
    ,BodyText           @BLOB_TEXT                  @NULL     
    ,Synopsis           @BLOB_TEXT                  @NULL
    ,OrderIndex         integer default 0           @NOT_NULL
    ,CreatedAt          @DATE_TIME                  @NOT_NULL                        
    ,UpdatedAt          @DATE_TIME                  @NOT_NULL  
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
    ,Name                   @NVARCHAR(128)              @NOT_NULL                        
    ,BodyText               @BLOB_TEXT                  @NULL                            
    ,Synopsis               @BLOB_TEXT                  @NULL  
    ,OrderIndex             integer default 0           @NOT_NULL    
    ,CreatedAt              @DATE_TIME                  @NOT_NULL                        
    ,UpdatedAt              @DATE_TIME                  @NOT_NULL                      

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
        static public void AddComponentToSceneTable(SchemaVersion sv)
        {
            string TableName = SComponentToScene;
            string SqlText = $@"
create table {TableName} (
     
     ComponentId        {SysConfig.ForeignKeyStr()}
    ,SceneId            {SysConfig.ForeignKeyStr()}

    ,constraint UK_{TableName}_00 unique (ComponentId, SceneId)
)
";
            sv.AddTable(SqlText);
        }
        /// <summary>
        /// Adds a table to a schema version
        /// </summary>
        static public void AddNotesTable(SchemaVersion sv)
        {
            string TableName = SNote;
            string SqlText = $@"
create table {TableName} (
    Id						{SysConfig.PrimaryKeyStr()} 
    ,Name                   @NVARCHAR(128)       @NOT_NULL  
    ,BodyText               @BLOB_TEXT           @NULL        
    ,OrderIndex             integer default 0    @NOT_NULL 

    ,constraint UK_{TableName}_00 unique (Name)
)
";
            sv.AddTable(SqlText);

        }

        /// <summary>
        /// Creates the database schema for the project, including tables for components, chapters, and scenes.
        /// </summary>
        public void CreateSchema()
        {
            Schema ProjectSchema = Schemas.FindOrAdd(Sys.APPLICATION, Sys.DEFAULT); //Name
            SchemaVersion Version = ProjectSchema.FindOrAdd(Version: 1);

            AddTagTable(Version);
            AddComponentTypeTable(Version);
            AddComponentTable(Version);
            AddChapterTable(Version);
            AddSceneTable(Version);
            AddTagToComponentTable(Version);
            AddComponentToSceneTable(Version);
            AddNotesTable(Version);

            Schemas.Execute();

            System.Threading.Thread.Sleep(2000); // wait for the database to be ready
        }
    }
}
