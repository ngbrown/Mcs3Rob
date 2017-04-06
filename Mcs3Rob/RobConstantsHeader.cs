namespace Mcs3Rob
{
    public class RobConstantsHeader : RobHeader
    {
        /// <summary>
        /// Default = 0
        /// </summary>
        public int Reserved0 { get; set; }

        /// <summary>
        /// RAM block address offset.
        /// </summary>
        public int RamBlockAddressOffset { get; set; }
    }
}