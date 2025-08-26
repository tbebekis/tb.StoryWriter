namespace StoryWriter
{
    public class RichTextBoxEx : RichTextBox
    {
        const int PFM_SPACEBEFORE = 64;
        const int PFM_SPACEAFTER = 128;
        const int EM_SETPARAFORMAT = 1095;
        const int SCF_SELECTION = 1;
 
        const int WM_PASTE = 0x0302;

        [StructLayout(LayoutKind.Sequential)]
        private struct PARAFORMAT
        {
            public int cbSize;
            public uint dwMask;
            public short wNumbering;
            public short wReserved;
            public int dxStartIndent;
            public int dxRightIndent;
            public int dxOffset;
            public short wAlignment;
            public short cTabCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public int[] rgxTabs;

            // PARAFORMAT2 from here onwards.
            public int dySpaceBefore;
            public int dySpaceAfter;
            public int dyLineSpacing;
            public short sStyle;
            public byte bLineSpacingRule;
            public byte bOutlineLevel;
            public short wShadingWeight;
            public short wShadingStyle;
            public short wNumberingStart;
            public short wNumberingStyle;
            public short wNumberingTab;
            public short wBorderSpace;
            public short wBorderWidth;
            public short wBorders;
        }

        [DllImport("user32", CharSet = CharSet.Auto)]
        private static extern int SendMessage(HandleRef hWnd,
                                               int msg,
                                               int wParam,
                                               ref PARAFORMAT lp);


        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (!DesignMode && e.KeyCode == Keys.Enter)
            {
                SetSelectionParagraphSpacingBefore();
                SetSelectionParagraphSpacingAfter();
            }

            base.OnKeyDown(e);
        }


        /// <summary>
        /// Applies paragraph spacing to all paragraphs intersecting the given range.
        /// </summary>
        private void ApplyParagraphSpacingToRange(int start, int length)
        {
            if (start < 0 || length < 0 || start > TextLength) return;

            int startLine = GetLineFromCharIndex(Math.Min(start, TextLength));
            int endIdx = Math.Min(start + Math.Max(0, length) - 1, Math.Max(0, TextLength - 1));
            int endLine = GetLineFromCharIndex(Math.Max(start, endIdx));

            int blockStart = GetFirstCharIndexFromLine(startLine);
            int blockEnd = (endLine + 1 < Lines.Length)
                ? GetFirstCharIndexFromLine(endLine + 1)
                : TextLength;

            int origSelStart = SelectionStart;
            int origSelLen = SelectionLength;

            Select(blockStart, Math.Max(0, blockEnd - blockStart));
            SetSelectionParagraphSpacingBefore();
            SetSelectionParagraphSpacingAfter();

            SelectionStart = Math.Min(start + length, TextLength);
            SelectionLength = 0;
            ScrollToCaret();

            // Optional: restore original selection instead of caret-at-end
            // SelectionStart = origSelStart; SelectionLength = origSelLen;
        }

        /// <summary>
        /// Applies paragraph spacing to the paragraph containing the given character index.
        /// </summary>
        private void ApplySpacingToParagraphAt(int charIndex)
        {
            int line = GetLineFromCharIndex(Math.Min(charIndex, Math.Max(0, TextLength)));
            int blockStart = GetFirstCharIndexFromLine(line);
            int blockEnd = (line + 1 < Lines.Length)
                ? GetFirstCharIndexFromLine(line + 1)
                : TextLength;

            int caret = SelectionStart;

            Select(blockStart, Math.Max(0, blockEnd - blockStart));
            SetSelectionParagraphSpacingBefore();
            SetSelectionParagraphSpacingAfter();

            SelectionStart = Math.Min(caret, TextLength);
            SelectionLength = 0;
        }



        /// <summary>
        /// Intercepts WM_PASTE to reapply spacing to pasted paragraphs.
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            if (!DesignMode && m.Msg == WM_PASTE)
            {
                int selStart = SelectionStart;
                int selLen = SelectionLength;
                int lenBefore = TextLength;

                base.WndProc(ref m);

                int lenAfter = TextLength;
                int delta = lenAfter - (lenBefore - selLen);
                int insertedStart = selStart;
                int insertedLen = Math.Max(0, delta);

                if (insertedLen > 0)
                    ApplyParagraphSpacingToRange(insertedStart, insertedLen);
                else
                    ApplySpacingToParagraphAt(insertedStart);

                return;
            }

            base.WndProc(ref m);
        }

        public RichTextBoxEx()
        {
        }


        public void SetSelectionParagraphSpacingBefore()
        {
            PARAFORMAT fmt = new PARAFORMAT();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.dwMask = PFM_SPACEBEFORE;
            fmt.dySpaceBefore = SelectionParagraphSpacingBefore;
            SendMessage(new HandleRef(this, this.Handle),
                         EM_SETPARAFORMAT,
                         SCF_SELECTION,
                         ref fmt
                       );
        }
        public void SetSelectionParagraphSpacingAfter()
        {
            PARAFORMAT fmt = new PARAFORMAT();
            fmt.cbSize = Marshal.SizeOf(fmt);
            fmt.dwMask = PFM_SPACEAFTER;
            fmt.dySpaceAfter = SelectionParagraphSpacingAfter;
            SendMessage(new HandleRef(this, this.Handle),
                         EM_SETPARAFORMAT,
                         SCF_SELECTION,
                         ref fmt
                       );
        }

        /// <summary>
        /// In twips (1/1440 of an inch).
        /// <para>20 twips = 1pt</para>
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        static public int SelectionParagraphSpacingBefore { get; set; } = 50;       // 50 twips = ~2.5pt
        /// <summary>
        /// In twips (1/1440 of an inch).
        /// <para>20 twips = 1pt</para>
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        static public int SelectionParagraphSpacingAfter { get; set; } = 150;       // 150 twips = ~7.5pt
    }


}
