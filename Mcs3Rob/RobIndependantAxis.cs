using System.Collections.Generic;

namespace Mcs3Rob
{
    /// <summary>
    /// Used as the U-Axis, V-Axis, or W-Axis of <see cref="RobCharLine2"/>,
    /// <see cref="RobCharMap2"/>, and <see cref="RobCharSpace"/>.
    /// </summary>
    public class RobIndependantAxis
    {
        /// <summary>
        /// This defines the data element type.  See <see cref="RobElementType"/> for definitions.
        /// </summary>
        public RobElementType ElementType { get; set; }

        /// <summary>
        /// Controls how the value is changed by defining a direction.
        /// </summary>
        public RobDirection ValueDirection { get; set; }

        /// <summary>
        /// Sets the minimum limits that the microcontroller data can be changed.
        /// The applied formula and the element type will affect the actual limits.
        /// </summary>
        public long MinimumLimit { get; set; }

        /// <summary>
        /// Sets the maximum limits that the microcontroller data can be changed.
        /// The applied <see cref="ConversionEquation"/> and the <see cref="ElementType"/> will affect the actual limits.
        /// </summary>
        public long MaximumLimit { get; set; }

        /// <summary>
        /// ROM address graduation table (-1 = not in ROM, but in <see cref="DefinitionValues"/>).
        /// </summary>
        public int AddressGraduationTable { get; set; }

        /// <summary>
        /// Axis Direction in MC model
        /// </summary>
        public RobDirection AxisDirection { get; set; }

        /// <summary>
        /// Full axis description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Physical label abbreviation
        /// </summary>
        public string PhysicalLabelAbbreviation { get; set; }

        /// <summary>
        /// The engineering unit display text. Limited to 7 characters.
        /// </summary>
        public string EngineeringUnit { get; set; }

        /// <summary>
        /// Defines the relationship between the data in the microcontroller model
        /// and the engineering model.
        /// Limited to 255 characters.
        /// The ECU data value is represented by the leter 'x' in the formula.
        /// </summary>
        public string ConversionEquation { get; set; }

        /// <summary>
        /// Defines the number of digits after the decimal point to be
        /// displayed.  The valid range is from 0 to 4.
        /// </summary>
        public int DecimalPlaces { get; set; }

        /// <summary>
        /// Dimension (number of values on axis)
        /// </summary>
        public int Dimension { get; set; }

        /// <summary>
        /// Graduation Table axis direction: 0=as map, 1=reverse
        /// </summary>
        public RobDirection GraduationTableAxisDirection { get; set; }
        
        public int Reserve13 { get; set; }

        /// <summary>
        /// Graduation model: 0=MC model, 1=physical
        /// </summary>
        public RobGraduationModel GraduationModel { get; set; }

        /// <summary>
        /// When the graduation table is not in ROM(-1) the values must be supplied in the definition
        /// </summary>
        public List<double> DefinitionValues { get; set; }
    }
}