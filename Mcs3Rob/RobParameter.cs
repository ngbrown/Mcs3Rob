namespace Mcs3Rob
{
    public class RobParameter
    {
        /// <summary>
        /// This defines the data element type.  See <see cref="RobElementType"/> for definitions.
        /// </summary>
        public RobElementType ElementType { get; set; }

        /// <summary>
        /// Defines a bit mask. This parameter applies to the microcontroller data
        /// before any formula's are applied.
        /// This parameter position can also be a <see cref="Direction"/>, in which case, this would be <c>null</c>.
        /// </summary>
        public uint? Bitmask { get; set; }

        /// <summary>
        /// Controls how the value is changed by defining a direction.
        /// This parameter position can also be a <see cref="Bitmask"/>, in which case, this would be <c>null</c>.
        /// </summary>
        public RobDirection? Direction { get; set; }

        /// <summary>
        /// Sets the minimum limits that the microcontroller data can be changed.
        /// The applied <see cref="ConversionEquation"/> and the <see cref="ElementType"/> will affect the actual limits.
        /// </summary>
        public long MinimumLimit { get; set; }

        /// <summary>
        /// Sets the maximum limits that the microcontroller data can be changed.
        /// The applied formula and the element type will affect the actual limits.
        /// </summary>
        public long MaximumLimit { get; set; }

        /// <summary>
        /// Defines the location in RAM or ROM of the data. This could be an absolute
        /// or relative memory address, or a CAN address.  If the offset in the header
        /// is 0, then the <see cref="Address"/> value is the absolute location for the data.
        /// </summary>
        public uint Address { get; set; }

        /// <summary>
        /// Constants can only be displayed in mode 1 <see cref="RobDisplayMode.AnalogDisplay"/>;
        /// all other modes are invalid.
        /// </summary>
        public RobDisplayMode DisplayMode { get; set; }

        /// <summary>
        /// Used to enter a description of the parameter.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Used to identify the actual variable. Duplicate names are not allowed.
        /// Limited to 20 characters.
        /// </summary>
        public string VariableName { get; set; }

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
    }
}