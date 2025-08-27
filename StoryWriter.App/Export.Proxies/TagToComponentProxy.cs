namespace StoryWriter
{
    public class TagToComponentProxy
    {

        public TagToComponentProxy()
        {
        }

        public void From(TagToComponent Source, ExportService Service)
        {
            this.TagId = Source.Tag.Id;
            this.TagTitle = Source.Tag.Name;
            this.ComponentId = Source.Component.Id;
            this.ComponentTitle = Source.Component.Name;
        }
        public void To(ImportService Service)
        {
            TagToComponent Dest = new();
            Dest.Tag = App.CurrentProject.TagList.FirstOrDefault(x => x.Id == this.TagId);
            Dest.Component = App.CurrentProject.ComponentList.FirstOrDefault(x => x.Id == this.ComponentId);
            Dest.Insert();
        }

        public string TagId { get; set; }
        public string TagTitle { get; set; }
        public string ComponentId { get; set; }
        public string ComponentTitle { get; set; }
    }
}