namespace StoryWriter
{
    using System;
    using System.Diagnostics;
    using System.IO;

    public static class LibreOfficeExporter
    {
        static string FindSoffice()
        {
            // environment variable
            var envPath = Environment.GetEnvironmentVariable("PATH") ?? "";

            foreach (var dir in envPath.Split(Path.PathSeparator))
            {
                var exe = Path.Combine(dir, "soffice.exe");
                if (File.Exists(exe)) return exe;
                exe = Path.Combine(dir, "soffice");
                if (File.Exists(exe)) return exe;
                exe = Path.Combine(dir, "libreoffice");
                if (File.Exists(exe)) return exe;
            }

            // application path
            var candidates = new[]
            {
                @"C:\Program Files\LibreOffice\program\soffice.exe",
                @"C:\Program Files (x86)\LibreOffice\program\soffice.exe"
            };

            foreach (var c in candidates)
                if (File.Exists(c)) return c;

            return null;
        }

        /// <summary>
        /// Exports a RFT file to a different format
        /// </summary>
        public static void Export(string RftSourcePath, DocFormatType TargetFormatType, int timeoutMs = 300000)
        {
            string TargetFormat = TargetFormatType.GetExtension();

            if (!File.Exists(RftSourcePath)) 
                throw new FileNotFoundException($"Cannot convert to {TargetFormat}. Source file {RftSourcePath} does not exist.");

            string SOfficePath = FindSoffice() ?? throw new FileNotFoundException($"Cannot convert source file {RftSourcePath} to {TargetFormat}. LibreOffice not found.");
            string OutputFolder = Path.GetDirectoryName(RftSourcePath)!;

            var PSI = new ProcessStartInfo
            {
                FileName = SOfficePath,
                Arguments = $"--headless --convert-to {TargetFormat} --outdir \"{OutputFolder}\" \"{RftSourcePath}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var OfficeProcess = Process.Start(PSI) ?? throw new Exception($"Cannot convert source file {RftSourcePath} to {TargetFormat}. Cannot start LibreOffice.");
            if (!OfficeProcess.WaitForExit(timeoutMs)) 
            { 
                try 
                { 
                    OfficeProcess.Kill(); 
                } 
                catch 
                { 
                }

                throw new Exception($"Cannot convert source file {RftSourcePath} to {TargetFormat}. Timeout of {timeoutMs} ms exceeded.");
            }

            // delete the source file
            System.IO.File.Delete(RftSourcePath);

            var FileName = Path.GetFileNameWithoutExtension(RftSourcePath) + "." + TargetFormat.Trim().ToLowerInvariant();
            var OutputFilePath = Path.Combine(OutputFolder, FileName);
            if (!File.Exists(OutputFilePath))
                throw new Exception($"Cannot convert source file {RftSourcePath} to {TargetFormat}. Conversion failed."); 
        }

        /// <summary>
        /// Returns true if LibreOffice exists
        /// </summary>
        static public bool LibreOfficeExists() => FindSoffice() != null;

    }
}
