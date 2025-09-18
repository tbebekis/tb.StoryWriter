namespace StoryWriter
{
 
    /// <summary>
    /// Fixes markdown links inside a rich-text string that actually contains markdown-only text.
    /// For each internal link like [Name](Name) it becomes [Name](Name.md).
    /// Preserves #anchors, ?queries and optional "title" parts, skips images (![]),
    /// skips external/absolute links (http:, mailto:, etc.) and links with slashes,
    /// and leaves targets that already have an extension as-is.
    /// </summary>
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text.RegularExpressions;

    static class MarkdownLinkTools
    {
        static readonly Regex LinkRx = new(@"(?<bang>!?)\[(?<text>[^\]]+)\]\(\s*(?<url>[^)\s""]+)(?<rest>[^)]*)\)", RegexOptions.Compiled);

        /// <summary>
        /// Returns the body text with fixed links.
        /// </summary>
        public static string FixLinks(string RichBodyText)
        {
            if (string.IsNullOrEmpty(RichBodyText))
                return RichBodyText ?? string.Empty;

            return LinkRx.Replace(RichBodyText, m =>
            {
                // Skip images
                if (m.Groups["bang"].Value == "!")
                    return m.Value;

                string text = m.Groups["text"].Value;
                string url = m.Groups["url"].Value;
                string rest = m.Groups["rest"].Value; // e.g. ' "Title"' or empty

                // Split url into base + tail (#anchor or ?query or both)
                string basePart = url;
                string tail = "";
                int hash = url.IndexOf('#');
                int q = url.IndexOf('?');
                int cut = (hash >= 0 && q >= 0) ? Math.Min(hash, q) : Math.Max(hash, q);
                if (cut >= 0) { basePart = url.Substring(0, cut); tail = url.Substring(cut); }

                // Ignore external schemes and paths
                if (HasScheme(basePart)) return m.Value;
                if (basePart.Contains('/')) return m.Value;
                if (basePart.Contains('\\')) return m.Value;

                // Ignore empty or anchor-only
                if (string.IsNullOrWhiteSpace(basePart)) return m.Value;

                // Already has extension?
                if (HasExtension(basePart)) return m.Value;

                // Append .md
                string fixedUrl = basePart + ".md" + tail;

                return $"[{text}]({fixedUrl}{rest})";
            });
        }

        /// <summary>
        /// Minimal scheme detector before ':' (http, mailto, custom).
        /// </summary>
        static bool HasScheme(string s)
        {
            int i = s.IndexOf(':');
            if (i <= 0) return false;
            for (int k = 0; k < i; k++)
            {
                char c = s[k];
                if (!(char.IsLetterOrDigit(c) || c == '+' || c == '-' || c == '.'))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if a token already ends with an extension (e.g., "foo.bar").
        /// Treats a trailing dot or a leading dot without another dot as "no extension".
        /// </summary>
        static bool HasExtension(string s)
        {
            int lastDot = s.LastIndexOf('.');
            if (lastDot <= 0 || lastDot == s.Length - 1) return false;
            // Something like ".well-known" (dot at index 0) returns false above; if you want true for that, adjust.
            return true;
        }
    
    
        static public void Execute()
        {
            foreach (var Component in App.CurrentStory.ComponentList)
            {
                string BodyText = Component.BodyText.Trim();
                Component.BodyText = FixLinks(BodyText);
                Component.Update();
            }
        }
    }
 

}
