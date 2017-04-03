namespace Mcs3Rob
{
    internal struct AstFloat : IAst
    {
        public double Value { get; }

        public AstFloat(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}