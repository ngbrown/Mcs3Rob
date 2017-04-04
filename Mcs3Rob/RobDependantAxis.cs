namespace Mcs3Rob
{
    public class RobDependantAxis
    {
        public RobElementType ElementType { get; set; }
        public RobDirection ValueDirection { get; set; }
        public long MinimumLimit { get; set; }
        public long MaximumLimit { get; set; }
        public int Reserve4 { get; set; }
        public int Reserve5 { get; set; }
        public string Description { get; set; }
        public string PhysicalLabelAbbreviation { get; set; }
        public string EngineeringUnit { get; set; }
        public string ConversionEquation { get; set; }
        public int? DecimalPlaces { get; set; }
        public int Reserve11 { get; set; }
        public int Reserve12 { get; set; }
        public int Reserve13 { get; set; }
    }
}