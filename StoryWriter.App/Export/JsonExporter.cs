namespace StoryWriter
{
    public class JsonExporter
    {
        ExportService Service;

        // ● construction
        public JsonExporter(ExportService Service)
        {
            this.Service = Service;
        }

        // ● public
        public void Execute()
        {
            Tripous.Json.SaveToFile(Service.Project, Service.FilePath);
        }
    }
}
