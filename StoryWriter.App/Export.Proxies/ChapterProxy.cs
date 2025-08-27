using DocumentFormat.OpenXml.Vml.Office;

namespace StoryWriter
{
    public class ChapterProxy
    {
 

        public ChapterProxy()
        {

        }
        public void From(Chapter Source, ExportService Service)
        {      
            this.Id = Source.Id;
            this.Title = Source.Name;
            this.BodyText = Service.GetText(Source.BodyText);
            this.Synopsis = Service.GetText(Source.Synopsis);
            this.Concept = Service.GetText(Source.Concept);
            this.Outcome = Service.GetText(Source.Outcome);
            this.CreatedAt = Source.CreatedAt;
            this.UpdatedAt = Source.UpdatedAt;
            this.OrderIndex = Source.OrderIndex;

            foreach (Scene Scene in Source.SceneList)
            {
                var Proxy = new SceneProxy();
                Proxy.From(Scene, Service);

                this.SceneList.Add(Proxy);
            }
        }
        public void To(ImportService Service)
        {
            Chapter Dest = new();
            Dest.Id = this.Id;
            Dest.Name = this.Title;
            Dest.BodyText = Service.GetText(this.BodyText);
            Dest.Synopsis = Service.GetText(this.Synopsis);
            Dest.Concept = Service.GetText(this.Concept);
            Dest.Outcome = Service.GetText(this.Outcome);
            Dest.CreatedAt = this.CreatedAt;
            Dest.UpdatedAt = this.UpdatedAt;
            Dest.OrderIndex = this.OrderIndex;
            Dest.Insert();

            foreach (SceneProxy Proxy in this.SceneList)
            {
                Proxy.To(Service);
            } 
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string BodyText { get; set; } = string.Empty;
        public string Synopsis { get; set; } = string.Empty;
        public string Concept { get; set; } = string.Empty;
        public string Outcome { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int OrderIndex { get; set; } = 0;
        public List<SceneProxy> SceneList { get; set; } = new List<SceneProxy>();
    }
}
