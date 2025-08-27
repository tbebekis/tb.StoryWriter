namespace StoryWriter
{
    public class JsonImporter
    {
        ImportService Service;

        // ● construction
        public JsonImporter(ImportService Service)
        {
            this.Service = Service;
        }

        // ● public
        public void Execute()
        { 
            string JsonText = File.ReadAllText(Service.FilePath);
            ProjectProxy Proxy = Json.Deserialize<ProjectProxy>(JsonText);
            Proxy.To(Service);
        }
    }
}
