namespace StoryWriter
{
    public class TagProxy
    {
        public TagProxy() { }

        public void From(Tag Source, ExportService Service)
        {
            this.Id = Source.Id;
            this.Title = Source.Name;
        }
        public void To(ImportService Service)
        {
            Tag Dest = new();
            Dest.Id = this.Id;
            Dest.Name = this.Title;
            Dest.Insert();
        }

        public string Id { get; set; }
        public string Title { get; set; }
    }
}