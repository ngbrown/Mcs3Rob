namespace Mcs3Rob
{
    /// <summary>
    /// Characteristic maps are value tables in the ROM of a control unit.
    /// Mathematically, they are functions with an output value dependent
    /// on one, two, or three input parameter.
    /// See <see cref="RobCharLine2"/>, <see cref="RobCharMap2"/>, or <see cref="RobCharSpace"/>.
    /// </summary>
    public abstract class RobCharacteristicMap
    {
        /// <summary>
        /// Describes the name of the characteristic map and its location in memory.
        /// </summary>
        public RobCharacteristicMapHeader Header { get; set; }
    }
}