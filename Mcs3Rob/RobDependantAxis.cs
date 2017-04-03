namespace Mcs3Rob
{
    public class RobDependantAxis
    {
        public RobElementType ElementType { get; set; }
        public RobDirection ValueDirection { get; set; }
        public int MinimumLimit { get; set; }
        public int MaximumLimit { get; set; }
        public int Reserve1 { get; set; }
        public int Reserve2 { get; set; }
        public string Description { get; set; }
        public string PhysicalLabelAbbreviation { get; set; }
        public string EngineeringUnit { get; set; }
        public string ConversionEquation { get; set; }
        public int? DecimalPlaces { get; set; }
        public int Reserve3 { get; set; }
        public int Reserve4 { get; set; }
        public int Reserve5 { get; set; }
    }
}