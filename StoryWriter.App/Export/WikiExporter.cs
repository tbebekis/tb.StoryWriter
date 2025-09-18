namespace StoryWriter
{
    public class WikiExporter
    {

        ExportContext ExportContext;
        string WikiPath;
        readonly string InvalidChars;

        string ComponentNameToFileName(string ComponentName)
        {
            string Result = ComponentName.Replace(" ", "_");
            foreach (char c in InvalidChars)
                Result = Result.Replace(c.ToString(), "");
            return Result;
        }

        // ● construction
        public WikiExporter(ExportContext ExportContext)
        {
            this.ExportContext = ExportContext;

            List<char> InvalidCharList = new(Path.GetInvalidFileNameChars());
            InvalidCharList.AddRange(new char[] { ':', '*', '?', '"', '<', '>', '|', '\\', '/',
                '(', ')', '[', ']', '{', '}', '!', '@', '#', '%', '$', ';', ',', '.', '?', '=' });
            InvalidChars = new string(InvalidCharList.ToArray());
        }

        // ● public
        public void Execute()
        {
            ExportContext.InPlainText = true;

            WikiPath = Path.Combine(ExportContext.ExportFolderPath, "WikiFiles");

            if (!Directory.Exists(WikiPath))
                Directory.CreateDirectory(WikiPath);

            StringBuilder sbIndex = new();
            StringBuilder SB = new();

            string ComponentName;
            string ComponentFileName;
            string FilePath;
            string BodyText;

            sbIndex.AppendLine("# Story Components");
            sbIndex.AppendLine();
            

            var CategoryDic = ExportContext.Story.ComponentList
                .OrderBy(x => x.Category)
                .GroupBy(x => x.Category).ToDictionary(x => x.Key, x => x.ToList());

            foreach (var Entry in CategoryDic)
            {
                string Category = Entry.Key;
                var ComponentList = Entry.Value;

                sbIndex.AppendLine($"## {Category}");
                sbIndex.AppendLine();

                foreach (var Component in ComponentList)
                {
                    BodyText = Component.BodyText.Trim();

                    BodyText = $@"Go to [Component Index](_Index.md)

{BodyText}
";
                    ComponentName = Component.Title;
                    ComponentFileName = ComponentNameToFileName(ComponentName);
                    FilePath = Path.Combine(WikiPath, $"{ComponentFileName}.md");

                    File.WriteAllText(FilePath, BodyText);

                    sbIndex.AppendLine($"* [{ComponentName}](./{ComponentFileName}.md)");
                }



                FilePath = Path.Combine(WikiPath, $"_Index.md");
                File.WriteAllText(FilePath, sbIndex.ToString());
            }
        }
    }
}
