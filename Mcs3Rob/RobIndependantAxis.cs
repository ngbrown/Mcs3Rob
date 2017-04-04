using System.Collections.Generic;

namespace Mcs3Rob
{
    public class RobIndependantAxis
    {
        public RobElementType ElementType { get; set; }
        public RobDirection ValueDirection { get; set; }
        public long MinimumLimit { get; set; }
        public long MaximumLimit { get; set; }
        public int AddressGraduationTable { get; set; }
        public RobDirection AxisDirection { get; set; }
        public string Description { get; set; }
        public string PhysicalLabelAbbreviation { get; set; }
        public string EngineeringUnit { get; set; }
        public string ConversionEquation { get; set; }
        public int DecimalPlaces { get; set; }
        public int Dimension { get; set; }
        public RobDirection GraduationTableAxisDirection { get; set; }
        public int Reserve13 { get; set; }
        public int GraduationModel { get; set; }

        /// <summary>
        /// When the graduation table is not in ROM(-1) the values must be supplied in the definition
        /// </summary>
        public List<double> DefinitionValues { get; set; }
    }
}