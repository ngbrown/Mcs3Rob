using QUT.Gppg;

namespace Mcs3Rob
{
    internal struct AstInteger : IAst
    {
        public long Value { get; }
        public bool KnownHexValue { get; }
        public LexLocation LexLocation { get; }

        public AstInteger(LexLocation lexLocation, long value)
            :this(lexLocation, value, false)
        {
        }

        public AstInteger(LexLocation lexLocation, long value, bool knownHexValue)
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