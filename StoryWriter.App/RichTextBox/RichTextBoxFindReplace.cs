namespace StoryWriter
{
    using System.Windows.Forms;

    public static class RichTextBoxFindReplace
    {
        public static int FindNext(
            RichTextBox rtb,
            string query,
            bool matchCase = false,
            bool wholeWord = false,
            bool searchUp = false)
        {
            if (string.IsNullOrEmpty(query) || rtb.TextLength == 0) return -1;

            var opts = RichTextBoxFinds.NoHighlight;
            if (matchCase) opts |= RichTextBoxFinds.MatchCase;
            if (wholeWord) opts |= RichTextBoxFinds.WholeWord;
            if (searchUp) opts |= RichTextBoxFinds.Reverse;

            // ξεκίνα μετά το current selection όταν κάνεις forward
            int start = rtb.SelectionStart + (searchUp ? 0 : rtb.SelectionLength);
            int len = searchUp ? start : (rtb.TextLength - start);

            // προσοχή σε οριακές τιμές
            if (len < 0) len = 0;

            int hit = rtb.Find(query, start, start + len, opts);
            if (hit >= 0)
            {
                rtb.Select(hit, query.Length);
                rtb.ScrollToCaret();
            }
            return hit;
        }

        public static bool ReplaceSelection(RichTextBox rtb, string replacement)
        {
            if (rtb.SelectionLength == 0) return false;

            // Αν θέλεις να κρατήσεις το formatting της θέσης εισαγωγής, 
            // απλώς ορίζεις SelectedText (θα πάρει τρέχουσα μορφοποίηση selection).
            rtb.SelectedText = replacement;
            return true;
        }

        public static int ReplaceAll(
            RichTextBox rtb,
            string query,
            string replacement,
            bool matchCase = false,
            bool wholeWord = false)
        {
            if (string.IsNullOrEmpty(query) || rtb.TextLength == 0) return 0;

            // Κράτα αρχικό caret για να επιστρέψεις
            int savedStart = rtb.SelectionStart;
            int savedLen = rtb.SelectionLength;

            // Για να αποφύγεις flicker
            //rtb.BeginUpdate();   // → δες extension παρακάτω
            try
            {
                int count = 0;
                rtb.SelectionStart = 0;
                rtb.SelectionLength = 0;

                while (true)
                {
                    int hit = FindNext(rtb, query, matchCase, wholeWord, searchUp: false);
                    if (hit < 0) break;

                    rtb.SelectedText = replacement;
                    count++;

                    // Μετακίνησε το caret μετά την αντικατάσταση για να συνεχίσει σωστά
                    rtb.SelectionStart = hit + replacement.Length;
                    rtb.SelectionLength = 0;
                }

                return count;
            }
            finally
            {
                // Επανέφερε επιλογή (ή άφησε στο τέλος—ό,τι θες UX-wise)
                rtb.SelectionStart = savedStart;
                rtb.SelectionLength = savedLen;
                //rtb.EndUpdate();
            }
        }
    }

}
