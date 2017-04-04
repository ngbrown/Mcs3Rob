using QUT.Gppg;

namespace Mcs3Rob
{
    internal struct AstFloat : IAst
    {
        public double Value { get; }
        public LexLocation LexLocation { get; }

        public AstFloat(LexLocation lexLocation, double value)
        {
            LexLocation = lexLocation;
            Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}