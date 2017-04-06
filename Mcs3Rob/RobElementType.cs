namespace Mcs3Rob
{
    /// <summary>
    /// Used to define a data element type
    /// </summary>
    public enum RobElementType
    {
        /// <summary>
        /// Unsigned Byte, 8 bits
        /// </summary>
        UInt8 = 0,

        /// <summary>
        /// Signed Byte, 8 bits
        /// </summary>
        Int8 = 1,

        /// <summary>
        /// Unsigned Word, Lo-Hi, Intel Order, 16 bits.
        /// </summary>
        UInt16Intel = 2,

        /// <summary>
        /// Signed Word, Lo-Hi, Intel Order, 16 bits.
        /// </summary>
        Int16Intel = 3,

        /// <summary>
        /// Unsigned Word, Hi-Lo, Motorola Order, 16 bits.
        /// </summary>
        UInt16Motorola = 4,

        /// <summary>
        /// Signed Word, Hi-Lo, Motorola Order, 16 bits.
        /// </summary>
        Int16Motorola = 5,

        /// <summary>
        /// Unsigned Long, Lo-Hi, Intel Order, 32 bits.
        /// </summary>
        UInt32Intel = 6,

        /// <summary>
        /// Signed Long, Lo-Hi, Intel Order, 32 bits.
        /// </summary>
        Int32Intel = 7,

        /// <summary>
        /// Unsigned Long, Hi-Lo, Motorola Order, 32 bits.
        /// </summary>
        UInt32Motorola = 8,

        /// <summary>
        /// Signed Long, Hi-Lo, Motorola Order, 32 bits.
        /// </summary>
        Int32Motorola = 9,

        /// <summary>
        /// IEEE Float, Lo-Hi, Intel Order, 32 bits.
        /// </summary>
        FloatIntel = 10,

        /// <summary>
        /// IEEE Float, Hi-Lo, Motorola Order, 32 bits.
        /// </summary>
        FloatMotorola = 11,
    }
}