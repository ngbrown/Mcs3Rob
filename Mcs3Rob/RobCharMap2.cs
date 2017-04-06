namespace Mcs3Rob
{
    /// <summary>
    /// Characteristic maps are value tables in the ROM of a control unit.
    /// Mathematically, they are functions with an output value dependent
    /// on two input parameters.
    /// </summary>
    public class RobCharMap2 : RobCharacteristicMap
    {
        /// <summary>
        /// The "U" axis is an input
        /// </summary>
        public RobIndependantAxis UAxis { get; set; }

        /// <summary>
        /// The "V" axis is an input
        /// </summary>
        public RobIndependantAxis VAxis { get; set; }

        /// <summary>
        /// The "Q" axis is the output of the table.
        /// </summary>
        public RobDependantAxis QAxis { get; set; }
    }
}