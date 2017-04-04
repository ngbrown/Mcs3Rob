using System;
using QUT.Gppg;

namespace Mcs3Rob
{
    public class ErrorContext
    {
        private readonly int errorCode;
        public int StartLine { get; }
        public int StartColumn { get; }
        public int EndLine { get; }
        public int EndColumn { get; }

        public string Message { get; }

        public Exception Exception { get; }

        internal ErrorContext(int errorCode, QUT.Gppg.LexLocation lexLocation)
        {
            this.errorCode = errorCode;
            Message = GetMessage(errorCode);
            this.StartLine = lexLocation.StartLine;
            this.StartColumn = lexLocation.StartColumn;
            this.EndLine = lexLocation.EndLine;
            this.EndColumn = lexLocation.EndColumn;
        }

        internal ErrorContext(int errorCode, LexLocation lexLocation, string format)
            : this(errorCode, lexLocation)
        {
            Message = string.Format("{0}({1})", GetMessage(errorCode), format);
        }

        internal ErrorContext(int errorCode, LexLocation lexLocation, Exception exception)
            : this(errorCode, lexLocation)
        {
            Message = string.Format("{0}({1})", GetMessage(errorCode), exception);
        }

        private static string GetMessage(int errorCode)
        {
            string message;
            switch (errorCode)
            {
                case 1: message = "Fatal syntax error"; break;
                case 2: message = "Unrecoverable scanner error"; break;
                case 3: message = "Unrecoverable parser error"; break;
                case 4: message = "Unrecoverable exception"; break;
                case 79: message = "Illegal character in this context"; break;
                default: message = $"Unknown error ({errorCode})"; break;
            }

            return message;
        }

        public override string ToString()
        {
            string fileName = "SourceFile.rob";
            return $"{fileName}({StartLine}, {StartColumn}, {EndLine}, {EndColumn}) : error P{errorCode:D4} : {Message}";
        }
    }
}