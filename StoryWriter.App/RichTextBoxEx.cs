
namespace StoryWriter
{
    public class RichTextBoxEx : RichTextBox
    {
        const int PFM_SPACEBEFORE = 64;
        const int PFM_SPACEAFTER = 128;
        const int EM_SETPARAFORMAT = 1095;
        const int SCF_SELECTION = 1;





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


        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        static public int SelectionParagraphSpacingBefore { get; set; } = 50;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        static public int SelectionParagraphSpacingAfter { get; set; } = 150;


    }


}
