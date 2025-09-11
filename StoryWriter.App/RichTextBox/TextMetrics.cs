namespace StoryWriter
{
    /// <summary>
    /// Provides word counting and page estimation utilities.
    /// </summary>
    static public class TextMetrics
    {
        /// <summary>
        /// The result of a word count operation.
        /// </summary>
        public struct TextStats
        {
            /// <summary>
            /// The number of words.
            /// </summary>
            public int WordCount;
            /// <summary>
            /// The estimated number of pages.
            /// </summary>
            public double EstimatedPages;
        }

        // ● private
        /// <summary>
        /// Unicode-friendly regex (δέχεται ' ’ - μέσα στη λέξη)
        /// </summary>
        static readonly Regex WordRegex = new Regex(
            @"\p{L}[\p{L}\p{N}\p{Mn}’'\-]*",
            RegexOptions.Compiled);

        // ● private
        /// <summary>
        /// Counts words using a Unicode-aware regex.
        /// </summary>
        static int CountWords(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0;
            return WordRegex.Matches(text).Count;
        }
        /// <summary>
        /// Counts words using a fast non-regex scanner (best for large texts).
        /// </summary>
        static int CountWordsFast(string text)
        {
            if (string.IsNullOrEmpty(text)) return 0;
            int count = 0; bool inWord = false;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                var cat = char.GetUnicodeCategory(c);

                bool isCore = char.IsLetterOrDigit(c)
                           || cat == System.Globalization.UnicodeCategory.NonSpacingMark
                           || cat == System.Globalization.UnicodeCategory.SpacingCombiningMark;

                bool isInnerPunct = c == '\'' || c == '’' || c == '-';

                if (isCore || (inWord && isInnerPunct))
                {
                    if (!inWord) { inWord = true; count++; }
                }
                else inWord = false;
            }
            return count;
        }
        /// <summary>
        /// Estimates pages from word count given a words-per-page setting.
        /// </summary>
        static double EstimatePagesFromWords(int wordCount, int wordsPerPage = 250)
            => wordCount <= 0 ? 0 : (double)wordCount / Math.Max(1, wordsPerPage);

        // ● public
        /// <summary>
        /// Computes word count and pages on a background thread (cancellable).
        /// </summary>
        static public Task<TextStats> ComputeAsync(string text, int wordsPerPage, CancellationToken ct)
            => Task.Run(() =>
            {
                ct.ThrowIfCancellationRequested();
                int words = CountWordsFast(text); // ή CountWords(text)
                ct.ThrowIfCancellationRequested();
                double pages = EstimatePagesFromWords(words, wordsPerPage);
                return new TextStats { WordCount = words, EstimatedPages = pages };
            }, ct);
    }

}
 