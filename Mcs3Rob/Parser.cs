using System;
using System.Collections.Generic;
using System.IO;
using QUT.Gppg;

namespace Mcs3Rob
{
    public class Parser
    {
        public IList<string> Scan(string path)
        {
            var scannedTokens = new List<string>();
            using (var file = File.OpenRead(path))
            {
                var scanner = new Mcs3RobScanner(file);
                scanner.Initialize();
                scanner.Error += (sender, args) => Error?.Invoke(sender, args);
                int tok;
                do
                {
                    tok = scanner.yylex();
                    scannedTokens.Add(((Token)tok).ToString());
                } while (tok > (int)Token.EOF);
            }

            return scannedTokens;
        }

        public string Parse(string path)
        {
            using (var file = File.OpenRead(path))
            {
                var parser = new Mcs3RobParser();
                parser.Error += (sender, args) => Error?.Invoke(sender, args);
                parser.Parse(file);

                return parser.AstFile.ToString();
            }
        }

        public virtual event EventHandler<Mcs3Rob.ErrorEventArgs> Error;

        protected virtual void OnError(ErrorEventArgs e)
        {
            Error?.Invoke(this, e);
        }
    }

    public class ErrorEventArgs : EventArgs
    {
        public ErrorEventArgs(ErrorContext errorContext)
        {
            this.Context = errorContext;
        }

        public ErrorContext Context { get; }

        public override string ToString()
        {
            return this.Context.ToString();
        }
    }

    public class ErrorContext
    {
        private readonly int errorCode;
        public int StartLine { get; }
        public int StartColumn { get; }
        public int EndLine { get; }
        public int EndColumn { get; }

        public string Message { get; }

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

        private static string GetMessage(int errorCode)
        {
            string message;
            switch (errorCode)
            {
                case 1: message = "Fatal syntax error"; break;
                case 2: message = "Unrecoverable scanner error"; break;
                case 3: message = "Unrecoverable parser error"; break;
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