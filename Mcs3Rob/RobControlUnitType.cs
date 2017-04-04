using System;

namespace Mcs3Rob
{
    [Flags]
    public enum RobControlUnitType
    {
        /// <summary>
        /// Bit0 = byte order (0-Motorola, 1-Intel)
        /// </summary>
        IntelByteOrder = (1 << 0),

        /// <summary>
        /// Bit1 = enable Bosch ECU (special protocol)
        /// </summary>
        EnableBoschEcu = (1 << 1),

        /// <summary>
        /// Bit2 = use kbps for CCP, else define bit timing regs
        /// </summary>
        UseKbpsForCcp = (1 << 2),

        /// <summary>
        /// Bit3 = force extended ID's for CCP, else auto select ID
        /// </summary>
        ForceExtendedIdsForCcp = (1 << 3),

        /// <summary>
        /// Bit4 = Remote Tx request operation for DTO
        /// </summary>
        RemoteTxRequestOperationDto = (1 << 4),

        /// <summary>
        /// Bit4 = enable 16-bit rom emulation
        /// </summary>
        Enable16BitRomEmulation = (1 << 4),

        /// <summary>
        /// Bit5 = Protocol version, 1-CCP V1.01+
        /// </summary>
        ProtocolVersion1Ccp = (1 << 5),

        /// <summary>
        /// Bit8 = SER
        /// </summary>
        SER = (1 << 8),

        /// <summary>
        /// Bit9 = CAN
        /// </summary>
        CAN = (1 << 9),

        /// <summary>
        /// Bit10 = ABUS
        /// </summary>
        ABUS = (1 << 10),

        /// <summary>
        /// Bit11 = enable floating cursor (EMU)
        /// </summary>
        EnableFloatingCursor = (1 << 11),
    }
}