using System;

namespace Mcs3Rob
{
    internal struct AstText : IAst
    {
        public string Value { get; }

        public AstText(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public override string ToString()
        {
            return this.Value ?? "0";
        }
    }
}