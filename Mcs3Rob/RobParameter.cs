namespace Mcs3Rob
{
    public class RobParameter
    {
        public RobElementType ElementType { get; set; }

        public uint? Bitmask { get; set; }
        public RobDirection? Direction { get; set; }

        public long MinimumLimit { get; set; }
        public long MaximumLimit { get; set; }
        public uint Address { get; set; }
        public RobDisplayMode DisplayMode { get; set; }
        public string Description { get; set; }
        public string VariableName { get; set; }
        public string EngineeringUnit { get; set; }
        public string ConversionEquation { get; set; }
        public int? DecimalPlaces { get; set; }
    }
}