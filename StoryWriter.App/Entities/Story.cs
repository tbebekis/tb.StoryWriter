namespace StoryWriter
{
    public partial class Story
    {

        // ● construction
        /// <summary>
        /// Constructor
        /// </summary>
        public Story()
            : this("no name")
        {
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public Story(string Name)
        {
            this.Name = Name;
        }

        // ● static
        /// <summary>
        /// Returns true if the provided project name is valid (not empty, no invalid characters, does not start with a digit).
        /// </summary>
        static public bool IsValidStoryName(string ProjectName)
        {
            if (string.IsNullOrWhiteSpace(ProjectName))
                return false;
            char[] InvalidChars = Path.GetInvalidFileNameChars();

            foreach (char c in InvalidChars)
            {
                if (ProjectName.Contains(c))
                    return false;
            }

            if (char.IsDigit(ProjectName[0]))
                return false;

            return true;
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
            ReLoadTags();
            ReLoadComponentTypes();
            ReLoadComponents();
            
            ReLoadChapters();
            ReLoadScenes();

            ReLoadTagToComponents();
            ReLoadComponentToScenes();

            ReLoadNotes();
        }

        // ● exists
        /// <summary>
        /// True if a specified instance exists in the corresponding list
        /// </summary>
        static public bool ItemExists(IEnumerable<BaseEntity> List, string Name, string Id = "")
        {
            if (string.IsNullOrWhiteSpace(Id))
                return List.FirstOrDefault(item => item.Name.IsSameText(Name)) != null;

            return List.FirstOrDefault(item => item.Name.IsSameText(Name) && item.Id == Id) != null;
        }
        /// <summary>
        /// True if a specified instance exists in the corresponding list
        /// </summary>
        public bool TagExists(string Name, string Id = "") => ItemExists(TagList.Cast<BaseEntity>(), Name, Id);
        /// <summary>
        /// True if a specified instance exists in the corresponding list
        /// </summary>
        public bool ComponentTypeExists(string Name, string Id = "") => ItemExists(ComponentTypeList.Cast<BaseEntity>(), Name, Id);
        /// <summary>
        /// True if a specified instance exists in the corresponding list
        /// </summary>
        //public bool ComponentExists(string Name, string Id = "") => ItemExists(ComponentList.Cast<BaseEntity>(), Name, Id);
        public bool ComponentExists(Component Instance)
        {
            return ComponentList.FirstOrDefault(item => item.Id != Instance.Id && item.Name.IsSameText(Instance.Name)) != null;
        }
        /// <summary>
        /// True if a specified instance exists in the corresponding list
        /// </summary>
        public bool ChapterExists(string Name, string Id = "") => ItemExists(ChapterList.Cast<BaseEntity>(), Name, Id);
        /// <summary>
        /// True if a specified instance exists in the corresponding list
        /// </summary>
        public bool SceneExists(string Name, string Id = "") => ItemExists(SceneList.Cast<BaseEntity>(), Name, Id);
        /// <summary>
        /// True if a specified instance exists in the corresponding list
        /// </summary>
        public bool NoteExists(string Name, string Id = "") => ItemExists(NoteList.Cast<BaseEntity>(), Name, Id);
        /// <summary>
        /// True if a specified instance exists in the corresponding list
        /// </summary>
        public bool TagToComponentExists(TagToComponent Instance)
        {
            return TagToComponentList.FirstOrDefault(item => item.TagId == Instance.TagId && item.ComponentId == Instance.ComponentId) != null;
        }
        /// <summary>
        /// True if a specified instance exists in the corresponding list
        /// </summary>
        public bool ComponentToSceneExists(ComponentToScene Instance)
        {
            return ComponentToSceneList.FirstOrDefault(item => item.ComponentId == Instance.ComponentId && item.SceneId == Instance.SceneId) != null;
        }
        
        
        // ● add to list
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
        public void AddToList(ComponentType Instance) => AddToList(ComponentTypeList, Instance);
        /// <summary>
        /// Adds a specified instance to the corresponding list if it does not already exist
        /// </summary>
        public void AddToList(Component Instance) => AddToList(ComponentList, Instance);
        /// <summary>
        /// Adds a specified instance to the corresponding list if it does not already exist
        /// </summary>
        public void AddToList(Chapter Instance) => AddToList(ChapterList, Instance);
        /// <summary>
        /// Adds a specified instance to the corresponding list if it does not already exist
        /// </summary>
        public void AddToList(Scene Instance) => AddToList(SceneList, Instance);
        /// <summary>
        /// Adds a specified instance to the corresponding list if it does not already exist
        /// </summary>
        public void AddToList(Note Instance) => AddToList(NoteList, Instance);
        /// <summary>
        /// Adds a specified instance to the corresponding list if it does not already exist
        /// </summary>
        public void AddToList(TagToComponent Instance) => AddToList(TagToComponentList, Instance);
        /// <summary>
        /// Adds a specified instance to the corresponding list if it does not already exist
        /// </summary>
        public void AddToList(ComponentToScene Instance) => AddToList(ComponentToSceneList, Instance);

        // ● components and tags
        /// <summary>
        /// Adjusts the tags for a specified component
        /// </summary>
        public void AdjustComponentTags(Component Component, List<Tag> TagList)
        {
            string SqlText = $"delete from {Story.STagToComponent} where ComponentId = '{Component.Id}'";
            App.SqlStore.ExecSql(SqlText);

            ReLoadTagToComponents(); // empty the list and reload

            foreach (var Tag in TagList)
            {
                var TagToComponent = new TagToComponent();
                TagToComponent.TagId = Tag.Id;
                TagToComponent.ComponentId = Component.Id;

                TagToComponent.Insert();
            }
        }
        /// <summary>
        /// Adjusts the components for a specified tag
        /// </summary>
        public void AdjustTagComponents(Tag Tag, List<Component> ComponentList)
        {
            string SqlText = $"delete from {Story.STagToComponent} where TagId = '{Tag.Id}'";
            App.SqlStore.ExecSql(SqlText);

            ReLoadTagToComponents(); // empty the list and reload

            foreach (var Component in ComponentList)
            {
                var TagToComponent = new TagToComponent();
                TagToComponent.TagId = Tag.Id;
                TagToComponent.ComponentId = Component.Id;
                TagToComponent.Insert();
            }
        }

        // ● components and scenes
        public void AdjustComponentScenes(Component Component, List<Scene> SceneList)
        {
            string SqlText = $"delete from {Story.SComponentToScene} where ComponentId = '{Component.Id}'";
            App.SqlStore.ExecSql(SqlText);

            ReLoadComponentToScenes(); // empty the list and reload

            foreach (var Scene in SceneList)
            {
                var ComponentToScene = new ComponentToScene();
                ComponentToScene.ComponentId = Component.Id;
                ComponentToScene.SceneId = Scene.Id;
                ComponentToScene.Insert();
            }
        }
        public void AdjustSceneComponents(Scene Scene, List<Component> ComponentList)
        {
            string SqlText = $"delete from {Story.SComponentToScene} where SceneId = '{Scene.Id}'";
            App.SqlStore.ExecSql(SqlText);

            ReLoadComponentToScenes(); // empty the list and reload

            foreach (var Component in ComponentList)
            {
                var ComponentToScene = new ComponentToScene();
                ComponentToScene.ComponentId = Component.Id;
                ComponentToScene.SceneId = Scene.Id;
                ComponentToScene.Insert();
        
            }
        }
        public List<Component> GetSceneComponents(Scene Scene) => 
            ComponentToSceneList.Where(item => item.SceneId == Scene.Id).Select(item => item.GetComponent()).ToList();


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
        public void ChapterDeleted(Chapter Chapter)
        {
            ReLoadChapters();
            ReLoadScenes();
            ReLoadComponentToScenes();
        }


        // ● global search
        /// <summary>
        /// Returns a list of items that match a specified search term
        /// </summary>
        public void SearchItems(string Term)
        {
            

            List<LinkItem> LinkItems = new();
            List<LinkItem> TextLinkItems = new();

            if (!string.IsNullOrWhiteSpace(Term))
            {
                // ---------------------------------------------------------------------
                void AddLinks(IEnumerable<BaseEntity> EntityList, ItemType ItemType, bool WholeWordOnly)
                {
                    foreach (var Item in EntityList)
                    {

                        if (Item.NameContainsTerm(Term, WholeWordOnly)) //(WholeWordOnly? App.ContainsWord(Item.Name, Term) : Item.Name.ContainsText(Term))
                        {
                            var Link = new LinkItem(ItemType, LinkPlace.Title, Item.ToString(), Item);
                            LinkItems.Add(Link);
                        }
                        else if (Item.BodyTextContainsTerm(Term, WholeWordOnly))
                        {
                            var Link = new LinkItem(ItemType, LinkPlace.Text, Item.ToString(), Item);
                            TextLinkItems.Add(Link);
                        }
                    }
                }
                // ---------------------------------------------------------------------

                bool WholeWordOnly = Term.StartsWith("\"") && Term.EndsWith("\"");
                if (WholeWordOnly)
                {
                    Term = Term.TrimStart('"').TrimEnd('"');
                    LogBox.AppendLine($"Searching for whole word: '{Term}'");
                }
                else
                {
                    LogBox.AppendLine($"Searching for term: '{Term}'");
                }



                AddLinks(ComponentList.Cast<BaseEntity>(), ItemType.Component, WholeWordOnly);
                AddLinks(ChapterList.Cast<BaseEntity>(), ItemType.Chapter, WholeWordOnly);
                AddLinks(SceneList.Cast<BaseEntity>(), ItemType.Scene, WholeWordOnly);
                AddLinks(NoteList.Cast<BaseEntity>(), ItemType.Note, WholeWordOnly);
            }

            LinkItems.AddRange(TextLinkItems);

            if (LinkItems.Count == 0)
            {
                string Message = $"No search results for '{Term}'";
                LogBox.AppendLine(Message);
                App.InfoBox(Message);
            }
            else
            {
                App.PerformSearchResultsChanged(LinkItems);
            }


        }

        // ● properties 
        /// <summary>
        /// The name of this instance. 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The list of groups of this project.
        /// <para><strong>CAUTION: </strong> Do <strong>NOT</strong> add an item using directly the list. Use the corresponding <c>AddToList()</c> method.</para>
        /// </summary>
        public List<Tag> TagList { get; private set; } = new List<Tag>();
        /// <summary>
        /// The list of component types of this story.
        /// </summary>
        public List<ComponentType> ComponentTypeList { get; private set; } = new List<ComponentType>();
        /// <summary>
        /// The list of components of this project
        /// <para><strong>CAUTION: </strong> Do <strong>NOT</strong> add an item using directly the list. Use the corresponding <c>AddToList()</c> method.</para>
        /// </summary>
        public List<Component> ComponentList { get; private set; } = new List<Component>();
        /// <summary>
        /// A list of chapters in the project.
        /// <para><strong>CAUTION: </strong> Do <strong>NOT</strong> add an item using directly the list. Use the corresponding <c>AddToList()</c> method.</para>
        /// </summary>
        public List<Chapter> ChapterList { get; private set; } = new List<Chapter>();
        /// <summary>
        /// A list of scenes in the project.
        /// <para><strong>CAUTION: </strong> Do <strong>NOT</strong> add an item using directly the list. Use the corresponding <c>AddToList()</c> method.</para>
        /// </summary>
        public List<Scene> SceneList { get; private set; } = new List<Scene>();
        /// <summary>
        /// A list of scenes in the project.
        /// <para><strong>CAUTION: </strong> Do <strong>NOT</strong> add an item using directly the list. Use the corresponding <c>AddToList()</c> method.</para>
        /// </summary>
        public List<Note> NoteList { get; private set; } = new List<Note>();

        /// <summary>
        /// A list of TagToComponent items
        /// <para><strong>CAUTION: </strong> Do <strong>NOT</strong> add an item using directly the list. Use the corresponding <c>AddToList()</c> method.</para>
        /// </summary>
        public List<TagToComponent> TagToComponentList { get; private set; } = new List<TagToComponent>();
        /// <summary>
        /// A list of ComponentToScene items
        /// <para><strong>CAUTION: </strong> Do <strong>NOT</strong> add an item using directly the list. Use the corresponding <c>AddToList()</c> method.</para>
        /// </summary>
        public List<ComponentToScene> ComponentToSceneList { get; private set; } = new List<ComponentToScene>();

        
    }
}
