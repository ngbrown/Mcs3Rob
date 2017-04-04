using System;

namespace Mcs3Rob
{
    public class ParserContextError : Exception
    {
        public ParserContextError(string message)
            : base(message)
        {
        }
    }
}