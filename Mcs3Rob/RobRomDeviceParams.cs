namespace Mcs3Rob
{
    /// <summary>
    /// DEVPARM for ROM definitions
    /// </summary>
    public class RobRomDeviceParams : RobDeviceParams
    {
        /// <summary>
        /// 32 bit off-set into ROM image.  Default = 0
        /// </summary>
        public int OffsetRomImage { get; set; }

        /// <summary>
        /// 32 bit off-set for PROVARI addresse.  Default = Size of ROM in Bytes.
        /// </summary>
        public int OffsetProvariAddress { get; set; }
    }
}