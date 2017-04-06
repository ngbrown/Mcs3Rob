namespace Mcs3Rob
{
    /// <summary>
    /// Characteristic lines are value tables in the ROM of a control unit.
    /// Mathematically, they are functions with an output value dependent
    /// on one input parameter.
    /// </summary>
    public class RobCharLine2 : RobCharacteristicMap
    {
        /// <summary>
        /// The "U" axis is the input
        /// </summary>
        public RobIndependantAxis UAxis { get; set; }

        /// <summary>
        /// The "Q" axis is the output of the table.
        /// </summary>
        public RobDependantAxis QAxis { get; set; }
    }
}