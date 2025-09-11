namespace StoryWriter
{
    public class StoryProxy
    {
 

        public StoryProxy()
        {
        }


        public void Export(ExportContext ExportContext)
        {

            Story Source = App.CurrentStory;

            this.Title = Source.Name;

            foreach (Tag Tag in Source.TagList)
            {
                var Proxy = new TagProxy();
                Proxy.Export(Tag, ExportContext);

                this.TagList.Add(Proxy);
            }

            foreach (ComponentType ComponentType in Source.ComponentTypeList)
            {
                var Proxy = new ComponentTypeProxy();
                Proxy.Export(ComponentType, ExportContext);

                this.ComponentTypeList.Add(Proxy);
            }

            foreach (Component Component in Source.ComponentList)
            {
                var Proxy = new ComponentProxy();
                Proxy.Export(Component, ExportContext);

                this.ComponentList.Add(Proxy);
            }

            foreach (Chapter Chapter in Source.ChapterList)
            {
                var Proxy = new ChapterProxy();
                Proxy.Export(Chapter, ExportContext);

                this.ChapterList.Add(Proxy);
            }

            foreach (Note Note in Source.NoteList)
            {
                var Proxy = new NoteProxy();
                Proxy.Export(Note, ExportContext);

                this.NoteList.Add(Proxy);
            }

            foreach (TagToComponent TagToComponent in Source.TagToComponentList)
            {
                var Proxy = new TagToComponentProxy();
                Proxy.Export(TagToComponent, ExportContext);

                this.TagToComponentList.Add(Proxy);
            }

            foreach (ComponentToScene ComponentToScene in Source.ComponentToSceneList)
            {
                var Proxy = new ComponentToSceneProxy();
                Proxy.Export(ComponentToScene, ExportContext);

                this.ComponentToSceneList.Add(Proxy);
            }


        }
 
        // ● properties 
        public string Title { get; set; }
        public List<TagProxy> TagList { get; set; } = new List<TagProxy>();
        public List<ComponentTypeProxy> ComponentTypeList { get; set; } = new List<ComponentTypeProxy>();
        public List<ComponentProxy> ComponentList { get; set; } = new List<ComponentProxy>();        
        public List<ChapterProxy> ChapterList { get; set; } = new List<ChapterProxy>();
        public List<NoteProxy> NoteList { get; set; } = new List<NoteProxy>();
        public List<TagToComponentProxy> TagToComponentList { get; set; } = new List<TagToComponentProxy>();
        public List<ComponentToSceneProxy> ComponentToSceneList { get; set; } = new List<ComponentToSceneProxy>();
    }

    public class ItemProxy
    {
        protected ExportContext ExportContext;
        protected ImportService ImportService;

        protected string GetText(string RtfText)
        {
            string Result = RtfText;

            if (ExportContext != null && ExportContext.InPlainText)
                Result = App.ToPlainText(RtfText);

            return Result;
        }
    }

    public class TagProxy: ItemProxy
    {
        public TagProxy() { }

        public void Export(Tag Source, ExportContext Service)
        {
            this.Id = Source.Id;
            this.Title = Source.ToString();
        }
        public void Import(ImportService Service)
        {
            Tag Dest = new();
            Dest.Id = this.Id;
            Dest.Name = this.Title;
            Dest.Insert();
        }

        public string Id { get; set; }
        public string Title { get; set; }
    }


    public class ComponentTypeProxy : ItemProxy
    {

        public ComponentTypeProxy() { }

        public void Export(ComponentType Source, ExportContext Service)
        {
            ExportContext = Service;

            this.Id = Source.Id;
            this.Title = Source.ToString();
        }
        public void Import(ImportService Service)
        {
            ImportService = Service;

            Tag Dest = new();
            Dest.Id = this.Id;
            Dest.Name = this.Title;
            Dest.Insert();
        }

        public string Id { get; set; }
        public string Title { get; set; }
    }


    public class ComponentProxy : ItemProxy
    {
        string fBodyText;

        public ComponentProxy() { }

        public void Export(Component Source, ExportContext Service)
        {
            ExportContext = Service;

            this.Id = Source.Id;
            this.TypeId = Source.TypeId;
            this.Title = Source.ToString();
            this.TypeName = Source.Category;
            this.BodyText = Source.BodyText;
        }
        public void Import(ImportService Service)
        {
            ImportService = Service;

            Component Dest = new();
            Dest.Id = this.Id;
            Dest.TypeId = this.TypeId;
            Dest.Name = this.Title;
            Dest.BodyText = this.BodyText;
            Dest.Insert();
        }

        public string GetTagsAsLine()
        {
            Component Source = App.CurrentStory.ComponentList.FirstOrDefault(x => x.Id == this.Id);
            return Source.GetTagsAsLine();
        }

        public string Id { get; set; }
        public string TypeId { get; set; }
        public string Title { get; set; }
        public string TypeName { get; set; }
        public string BodyText
        {
            get => GetText(fBodyText);
            set => fBodyText = value;
        }
    }


    public class ChapterProxy : ItemProxy
    {
        string fBodyText;
        string fSynopsis;

        public ChapterProxy()
        {

        }
        
        public void Export(Chapter Source, ExportContext Service)
        {
            ExportContext = Service;

            this.Id = Source.Id;
            this.Title = Source.ToString();
            this.BodyText = Source.BodyText;
            this.Synopsis = Source.Synopsis;
            this.CreatedAt = Source.CreatedAt;
            this.UpdatedAt = Source.UpdatedAt;
            this.OrderIndex = Source.OrderIndex;

            List<Scene> SourceSceneList = Source.GetSceneList();

            foreach (Scene Scene in SourceSceneList)
            {
                var Proxy = new SceneProxy();
                Proxy.Export(Scene, Service);

                this.SceneList.Add(Proxy);
            }
        }
        public void Import(ImportService Service)
        {
            ImportService = Service;

            Chapter Dest = new();
            Dest.Id = this.Id;
            Dest.Name = this.Title;
            Dest.BodyText = this.BodyText;
            Dest.Synopsis = this.Synopsis;
            Dest.CreatedAt = this.CreatedAt;
            Dest.UpdatedAt = this.UpdatedAt;
            Dest.OrderIndex = this.OrderIndex;
            Dest.Insert();

            foreach (SceneProxy Proxy in this.SceneList)
            {
                Proxy.Import(Service);
            }
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string BodyText
        {
            get => GetText(fBodyText);
            set => fBodyText = value;
        }
        public string Synopsis
        {
            get => GetText(fSynopsis);
            set => fSynopsis = value;
        }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int OrderIndex { get; set; } = 0;
        public List<SceneProxy> SceneList { get; set; } = new List<SceneProxy>();
    }


    public class SceneProxy : ItemProxy
    {
        string fBodyText;
        string fSynopsis;

        public SceneProxy() { }

        public void Export(Scene Source, ExportContext Service)
        {
            ExportContext = Service;

            this.Id = Source.Id;
            this.Title = Source.ToString();
            this.ChapterId = Source.Chapter.Id;
            this.Synopsis = Source.Synopsis;
            this.BodyText = Source.BodyText;
            this.CreatedAt = Source.CreatedAt;
            this.UpdatedAt = Source.UpdatedAt;
            this.OrderIndex = Source.OrderIndex;
        }
        public void Import(ImportService Service)
        {
            ImportService = Service;

            Scene Dest = new();
            Dest.Id = this.Id;
            Dest.Name = this.Title;
            Dest.ChapterId = this.ChapterId;
            Dest.Synopsis = this.Synopsis;
            Dest.BodyText = this.BodyText;
            Dest.CreatedAt = this.CreatedAt;
            Dest.UpdatedAt = this.UpdatedAt;
            Dest.OrderIndex = this.OrderIndex;
            Dest.Insert();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string ChapterId { get; set; }
        public string BodyText
        {
            get => GetText(fBodyText);
            set => fBodyText = value;
        }
        public string Synopsis
        {
            get => GetText(fSynopsis);
            set => fSynopsis = value;
        }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int OrderIndex { get; set; } = 0;
    }

    public class NoteProxy : ItemProxy
    {
        string fBodyText;

        public NoteProxy() { }

        public void Export(Note Source, ExportContext Service)
        {
            ExportContext = Service;

            this.Id = Source.Id;
            this.Title = Source.ToString();
            this.BodyText = Source.BodyText;
        }
        public void Import(ImportService Service)
        {
            ImportService = Service;

            Note Dest = new();
            Dest.Id = this.Id;
            Dest.Name = this.Title;
            Dest.BodyText = this.BodyText;
            Dest.Insert();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string BodyText
        {
            get => GetText(fBodyText);
            set => fBodyText = value;
        }
    }


    public class TagToComponentProxy : ItemProxy
    {

        public TagToComponentProxy()
        {
        }

        public void Export(TagToComponent Source, ExportContext Service)
        {
            ExportContext = Service;

            this.TagId = Source.TagId;
            this.TagTitle = Source.GetTag().Name;
            this.ComponentId = Source.ComponentId;
            this.ComponentTitle = Source.GetComponent().Name;
        }
        public void Import(ImportService Service)
        {
            ImportService = Service;

            TagToComponent Dest = new();
            Dest.TagId = this.TagId;
            Dest.ComponentId = this.ComponentId;
            Dest.Insert();
        }

        public string TagId { get; set; }
        public string TagTitle { get; set; }
        public string ComponentId { get; set; }
        public string ComponentTitle { get; set; }
    }


    public class ComponentToSceneProxy : ItemProxy
    {

        public ComponentToSceneProxy()
        {
        }

        public void Export(ComponentToScene Source, ExportContext Service)
        {
            ExportContext = Service;


            this.ComponentId = Source.ComponentId;
            this.ComponentTitle = Source.GetComponent().Name;
            this.SceneId = Source.SceneId;
            this.SceneTitle = Source.GetScene().Name;
        }
        public void Import(ImportService Service)
        {
            ImportService = Service;

            ComponentToScene Dest = new();
            
            Dest.ComponentId = this.ComponentId;
            Dest.SceneId = this.SceneId;
            Dest.Insert();
        }

 
        public string ComponentId { get; set; }
        public string ComponentTitle { get; set; }
        public string SceneId { get; set; }
        public string SceneTitle { get; set; }
    }
}
