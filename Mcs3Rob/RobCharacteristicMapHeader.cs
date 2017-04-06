namespace Mcs3Rob
{
    /// <summary>
    /// Describes the name of the <see cref="RobCharacteristicMap"/> and its location in memory.
    /// </summary>
    public class RobCharacteristicMapHeader : RobHeader
    {
        /// <summary>
        /// Description of characteristic map
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Label of characteristic map (Listing label)
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// ROM/RAM address of characteristic map
        /// </summary>
        public uint AddressOfMap { get; set; }

        /// <summary>
        /// Default = 0
        /// </summary>
        public int Reserve3 { get; set; }

        /// <summary>
        /// Default = 0
        /// </summary>
        public int Reserve4 { get; set; }
    }
}