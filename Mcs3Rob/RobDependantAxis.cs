namespace Mcs3Rob
{
    /// Used as the Q-Axis of <see cref="RobCharLine2"/>,
    /// <see cref="RobCharMap2"/>, and <see cref="RobCharSpace"/>.
    public class RobDependantAxis
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

        public int Reserve4 { get; set; }
        public int Reserve5 { get; set; }

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
        public int? DecimalPlaces { get; set; }

        public int Reserve11 { get; set; }
        public int Reserve12 { get; set; }
        public int Reserve13 { get; set; }
    }
}