namespace StoryWriter
{
    public class ComponentProxy
    {
         

        public ComponentProxy() { }

        public void From(Component Source, ExportService Service)  
        {
            this.Id = Source.Id;
            this.Title = Source.Name;
            this.BodyText = Service.GetText(Source.BodyText);
        }
        public void To(ImportService Service)
        {
            Component Dest = new();
            Dest.Id = this.Id;
            Dest.Name = this.Title;
            Dest.BodyText = Service.GetText(this.BodyText);
            Dest.Insert();
        }

        public string GetTagsAsLine()
        {
            Component Source = App.CurrentProject.ComponentList.FirstOrDefault(x => x.Id == this.Id);
            return Source.GetTagsAsLine();
        }


        public string Id { get; set; }
        public string Title { get; set; }
        public string BodyText { get; set; }
    }
}