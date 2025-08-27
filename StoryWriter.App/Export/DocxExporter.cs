namespace StoryWriter
{
    public class DocxExporter
    {
        ExportService Service;

        // ● construction
        public DocxExporter(ExportService Service)
        {
            this.Service = Service;
        }

        // ● public
        public void Execute()
        {
            var opts = new DocxExportOptions
            {
                IncludeToc = true,
                PageBreakBetweenChapters = true,
                Mode = ChapterExportMode.AltChunkRtf // ή PlainText για συμβατότητα με Libre/μη-Word viewers
            };

            OpenXmlExporter.ExportProjectToDocx(Service.Project, Service.FilePath, opts);
        }
    }
}
