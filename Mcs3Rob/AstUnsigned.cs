namespace Mcs3Rob
{
    internal struct AstUnsigned : IAst
    {
        public uint Value { get; }
        public bool KnownHexValue { get; }

        public AstUnsigned(uint value)
            : this(value, false)
        {
        }

        public AstUnsigned(uint value, bool knownHexValue)
        {
            Value = value;
            KnownHexValue = knownHexValue;
        }

        public override string ToString()
        {
            if (this.KnownHexValue)
            {
                return "$" + this.Value.ToString("X4");
            }
            else
            {
                return this.Value.ToString();
            }
        }
    }
}