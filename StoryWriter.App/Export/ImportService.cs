namespace StoryWriter
{
    public class ImportService
    {
        // ● construction
        public ImportService() 
        { 
        }

        public void Import(string FilePath)
        {
            this.FilePath = FilePath;
            string Ext = Path.GetExtension(FilePath).TrimStart('.').ToLowerInvariant();

            switch (Ext)
            {
                case "json": 
                    ImportMode = ImportMode.JSON;
                    new JsonImporter(this).Execute();
                    break;
                case "xml":  ImportMode = ImportMode.XML; break;
                default:
                    string Message = $"Unsupported file extension: {Ext}.";
                    App.ErrorBox(Message);
                    LogBox.AppendLine(Message);
                    return;
            }
        }

        public string GetText(string PlainText) => App.ToRtfText(PlainText);

        public ImportMode ImportMode { get; set; }
        public string FilePath { get; set; }

    }
}
