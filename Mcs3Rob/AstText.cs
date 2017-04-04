using System;
using QUT.Gppg;

namespace Mcs3Rob
{
    internal struct AstText : IAst
    {
        public string Value { get; }
        public LexLocation LexLocation { get; }

        public AstText(LexLocation lexLocation, string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            LexLocation = lexLocation;
            Value = value;
        }

        public override string ToString()
        {
            return this.Value ?? "0";
        }
    }
}