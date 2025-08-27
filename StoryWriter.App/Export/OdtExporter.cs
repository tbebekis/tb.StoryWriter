namespace StoryWriter
{
    public class OdtExporter
    {
        ExportService Service;

        // ● construction
        public OdtExporter(ExportService Service)
        {
            this.Service = Service;
        }

        // ● public
        public void Execute()
        {
            if (!Service.CheckLibreOfficeExists())
                return;

            string RtfFilePath = Service.ExportToRtf();

            App.WaitForFileAvailable(RtfFilePath); // Wait for RTF file to be created and ready for reading
            LibreOfficeExporter.Export(RtfFilePath, DocFormatType.Odt);
        }
    }
}
