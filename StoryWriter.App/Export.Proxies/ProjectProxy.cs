namespace StoryWriter
{
    public class ProjectProxy
    {
 
        public ProjectProxy()
        {
        }

        public void From(ExportService Service)
        {
            Project Source = App.CurrentProject;

            this.Title = Source.Name;

            foreach (Tag Tag in Source.TagList)
            {
                var Proxy = new TagProxy();
                Proxy.From(Tag, Service);

                this.TagList.Add(Proxy);
            }

            foreach (Component Component in Source.ComponentList)
            {
                var Proxy = new ComponentProxy();
                Proxy.From(Component, Service);

                this.ComponentList.Add(Proxy);
            }

            foreach (TagToComponent TagToComponent in Source.TagToComponentList)
            {
                var Proxy = new TagToComponentProxy();
                Proxy.From(TagToComponent, Service);

                this.TagToComponentList.Add(Proxy);
            }

            foreach (Chapter Chapter in Source.ChapterList)
            {
                var Proxy = new ChapterProxy();
                Proxy.From(Chapter, Service);

                this.ChapterList.Add(Proxy);
            }
        }
        public void To(ImportService Service)
        {
            Project Dest = App.CurrentProject;

            foreach (TagProxy Proxy in TagList)
            {               
                Proxy.To(Service);                
            }

            foreach (ComponentProxy Proxy in ComponentList)
            {
                Proxy.To(Service);
            }

            foreach (TagToComponentProxy Proxy in TagToComponentList)
            {
                Proxy.To(Service);
            }

            foreach (ChapterProxy Proxy in ChapterList)
            {
                Proxy.To(Service);
            }
        }

        // ● properties 
        public string Title { get; set; }
        public List<TagProxy> TagList { get; set; } = new List<TagProxy>();
        public List<ComponentProxy> ComponentList { get; set; } = new List<ComponentProxy>();
        public List<TagToComponentProxy> TagToComponentList { get; set; } = new List<TagToComponentProxy>();
        public List<ChapterProxy> ChapterList { get; set; } = new List<ChapterProxy>();

    }
}
