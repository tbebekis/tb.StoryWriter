namespace StoryWriter
{
    public class JsonExporter
    {
        ExportContext ExportContext;

        // ● construction
        public JsonExporter(ExportContext ExportContext)
        {
            this.ExportContext = ExportContext;
        }

        // ● public
        public void Execute()
        {
            ExportContext.InPlainText = false;
            string FilePath = Path.Combine(ExportContext.ExportFolderPath, "Backup.json");
            Tripous.Json.SaveToFile(ExportContext.Story, FilePath);
            App.WaitForFileAvailable(FilePath);
        }
    }
}
