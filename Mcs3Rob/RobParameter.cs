namespace Mcs3Rob
{
    public class RobParameter
    {
        public RobElementType ElementType { get; set; }

        public int? Bitmask { get; set; }
        public RobDirection? Direction { get; set; }

        public int MinimumLimit { get; set; }
        public int MaximumLimit { get; set; }
        public int Address { get; set; }
        public RobDisplayMode DisplayMode { get; set; }
        public string Description { get; set; }
        public string VariableName { get; set; }
        public string EngineeringUnit { get; set; }
        public string ConversionEquation { get; set; }
        public int? DecimalPlaces { get; set; }
    }
}