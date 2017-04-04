using System;
using QUT.Gppg;

namespace Mcs3Rob
{
    public class ParserContextErrorException : Exception
    {
        public ParserContextErrorException(string message)
            : base(message)
        {
        }

        public ParserContextErrorException(string message, ParserLocation location)
            : this(message)
        {
            this.Location = location;
        }

        internal ParserContextErrorException(string message, IAst ast)
            : this(message, new ParserLocation(ast.LexLocation))
        {
        }

        public ParserLocation Location { get; }
    }
}