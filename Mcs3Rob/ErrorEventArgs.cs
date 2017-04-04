using System;

namespace Mcs3Rob
{
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
}