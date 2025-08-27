namespace StoryWriter
{
    public class SceneProxy
    {
        public SceneProxy() { }

        public void From(Scene Source, ExportService Service)
        {
            this.Id = Source.Id;
            this.Title = Source.Name;
            this.ChapterId = Source.Chapter.Id;
            this.BodyText = Service.GetText(Source.BodyText);
            this.CreatedAt = Source.CreatedAt;
            this.UpdatedAt = Source.UpdatedAt;
            this.OrderIndex = Source.OrderIndex;
        }
        public void To(ImportService Service)
        {
            Scene Dest = new();
            Dest.Id = this.Id;
            Dest.Name = this.Title;
            Dest.ChapterId = this.ChapterId;
            Dest.BodyText = Service.GetText(this.BodyText);
            Dest.CreatedAt = this.CreatedAt;
            Dest.UpdatedAt = this.UpdatedAt;
            Dest.OrderIndex = this.OrderIndex;
            Dest.Insert();
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string ChapterId { get; set; }
        public string BodyText { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int OrderIndex { get; set; } = 0;
    }
}