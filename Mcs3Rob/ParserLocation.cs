using QUT.Gppg;

namespace Mcs3Rob
{
    public struct ParserLocation
    {
        /// <summary>
        /// The line at which the text span starts.
        /// </summary>
        public int StartLine { get; }

        /// <summary>
        /// The column at which the text span starts.
        /// </summary>
        public int StartColumn { get; }

        /// <summary>
        /// The line on which the text span ends.
        /// </summary>
        public int EndLine { get; }

        /// <summary>
        /// The column of the first character
        /// beyond the end of the text span.
        /// </summary>
        public int EndColumn { get; }

        /// <summary>
        /// Constructor for text-span with given start and end.
        /// </summary>
        /// <param name="sl">start line</param>
        /// <param name="sc">start column</param>
        /// <param name="el">end line </param>
        /// <param name="ec">end column</param>
        public ParserLocation(int sl, int sc, int el, int ec)
        {
            StartLine = sl;
            StartColumn = sc;
            EndLine = el;
            EndColumn = ec;
        }

        internal ParserLocation(LexLocation lexLocation)
        {
            StartLine = lexLocation.StartLine;
            StartColumn = lexLocation.StartColumn;
            EndLine = lexLocation.EndLine;
            EndColumn = lexLocation.EndColumn;
        }

        public override string ToString()
        {
            if (EndLine != StartLine || StartColumn != EndColumn)
                return $"({StartLine}, {StartColumn}, {EndLine}, {EndColumn})";
            else
                return $"({StartLine}, {StartColumn})";
        }
    }
}