using QUT.Gppg;

namespace Mcs3Rob
{
    internal struct AstUnsigned : IAst
    {
        public uint Value { get; }
        public bool KnownHexValue { get; }
        public LexLocation LexLocation { get; }

        public AstUnsigned(LexLocation lexLocation, uint value)
            : this(lexLocation, value, false)
        {
        }

        public AstUnsigned(LexLocation lexLocation, uint value, bool knownHexValue)
        {
            LexLocation = lexLocation;
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