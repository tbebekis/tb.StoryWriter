namespace StoryWriter
{
    public enum DocFormatType
    {
        Rtf,
        Docx,
        Odt,
        Pdf
    }

    static public class DocFormatTypeExtensions
    {
        public static string GetExtension(this DocFormatType type) => type switch
        {
            DocFormatType.Rtf => "rtf",
            DocFormatType.Docx => "docx",
            DocFormatType.Odt => "odt",
            DocFormatType.Pdf => "pdf",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
