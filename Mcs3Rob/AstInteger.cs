namespace Mcs3Rob
{
    internal struct AstInteger : IAst
    {
        public long Value { get; }
        public bool KnownHexValue { get; }

        public AstInteger(long value)
            :this(value, false)
        {
        }

        public AstInteger(long value, bool knownHexValue)
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