using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using QUT.Gppg;

namespace Mcs3Rob
{
    internal partial class Mcs3RobParser
    {
        public AstFile AstFile { get; private set; }

        public Mcs3RobParser() : base(null) { }

        public void Parse(string s)
        {
            byte[] inputBuffer = System.Text.Encoding.Default.GetBytes(s);
            MemoryStream stream = new MemoryStream(inputBuffer);

            this.Parse(stream);
        }

        public void Parse(Stream stream)
        {
            var robScanner = new Mcs3RobScanner(stream);
            robScanner.Initialize();
            robScanner.Error += (sender, args) => Error?.Invoke(sender, args);

            this.Scanner = robScanner;
            try
            {
                if (!this.Parse())
                {
                    OnError(new ErrorEventArgs(new ErrorContext(3, this.Scanner.yylloc)));
                }
            }
            catch (Exception e)
            {
                OnError(new ErrorEventArgs(new ErrorContext(4, this.Scanner.yylloc, e)));
            }
        }

        private void ParseError(int errorCode, LexLocation lexLocation)
        {
            OnError(new ErrorEventArgs(new ErrorContext(errorCode, lexLocation)));
        }

        public virtual event EventHandler<Mcs3Rob.ErrorEventArgs> Error;

        protected virtual void OnError(ErrorEventArgs e)
        {
            Error?.Invoke(this, e);
        }
    }
}
