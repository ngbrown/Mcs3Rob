namespace Mcs3Rob
{
    /// <summary>
    /// The header configures the MCS for ROM/Data link that the controller is using.
    /// Based on the type of controller or data access, this may be either
    /// <see cref="RobFileHeaderRom"/>, <see cref="RobFileHeaderCan"/>, or <see cref="RobFileHeaderAnio"/>.
    /// </summary>
    public abstract class RobFileHeader : RobHeader
    {
        /// <summary>
        /// Sets the ROM emulation mode or MCS device number [1..4]
        /// </summary>
        public int EmulationModuleNumber { get; set; }

        /// <summary>
        /// ROM size or ROM type or Binary image size  Usually specified in KB.
        /// </summary>
        public int RomSize { get; set; }

        /// <summary>
        /// Control unit type.  This value is reserved for special hardware.
        /// See <see cref="RobControlUnitType"/> for definitions.
        /// </summary>
        public RobControlUnitType ControlUnitType { get; set; }
    }

    public class RobFileHeaderRom : RobFileHeader
    {
        /// <summary>
        /// Sets the trigger address for the MCS.  This allows the MCS to synchronize
        /// data acquisition with the ECU main control loop.
        /// Set to -1 if the address is unknown. Default = -1.
        /// </summary>
        public int RomCycleTriggerAddress { get; set; }

        public int RomReserved4 { get; set; }

        /// <summary>
        /// Sets the "Fixed" data sampling rate for the MCS. This value is used
        /// if the trigger address is not accessed. Default = 20ms.
        /// </summary>
        public int RomBusMonitoringTimeGate { get; set; }

        public int RomReserved6 { get; set; }

        /// <summary>
        /// Sets the offset into memory for ROM and RAM. The value is 4 digit HEX($) value.
        /// The first 2 digits specify the ROM offset and the last 2 define the RAM offest.
        /// $0804 = ROM offset of $8000 and a RAM offset of $4000. Default = 0.
        /// </summary>
        public uint RomRamOffset { get; set; }
        
    }

    public class RobFileHeaderCan : RobFileHeader
    {
        /// <summary>
        /// Bus frequency (kbps)
        /// </summary>
        public int CanBusFrequency { get; set; }

        /// <summary>
        /// CAN ECU Node ID
        /// </summary>
        public int CanEcuNodeId { get; set; }

        /// <summary>
        /// Default sampling interval (ms)
        /// </summary>
        public int CanDefaultSamplingInterval { get; set; }

        /// <summary>
        /// Identifier for DTO CCP V1.00
        /// </summary>
        public int CanIdentifierDtoCcp { get; set; }

        /// <summary>
        /// Identifier for CRO CCP V1.00
        /// </summary>
        public int CanIdentifierCroCcp { get; set; }
        
    }

    public class RobFileHeaderAnio : RobFileHeader
    {
        /// <summary>
        /// Signal averaging time (ms)
        /// </summary>
        public int AnioSignalAveragingTime { get; set; }

        public int AnioReserved4 { get; set; }

        /// <summary>
        /// Scan rate (ms)
        /// </summary>
        public int AnioScanRate { get; set; }

        public int AnioReserved6 { get; set; }
        public int AnioReserved7 { get; set; }
    }
}