namespace Mcs3Rob
{
    public abstract class RobFileHeader : RobHeader
    {
        public int EmulationModuleNumber { get; set; }
        public int RomSize { get; set; }
        public RobControlUnitType ControlUnitType { get; set; }
    }

    public class RobFileHeaderRom : RobFileHeader
    {
        public int RomCycleTriggerAddress { get; set; }
        public int RomReserved4 { get; set; }
        public int RomBusMonitoringTimeGate { get; set; }
        public int RomReserved6 { get; set; }
        public uint RomRamOffset { get; set; }
        
    }

    public class RobFileHeaderCan : RobFileHeader
    {
        public int CanBusFrequency { get; set; }
        public int CanEcuNodeId { get; set; }
        public int CanDefaultSamplingInterval { get; set; }
        public int CanIdentifierDtoCcp { get; set; }
        public int CanIdentifierCroCcp { get; set; }
        
    }

    public class RobFileHeaderAnio : RobFileHeader
    {
        public int AnioSignalAveragingTime { get; set; }
        public int AnioReserved4 { get; set; }
        public int AnioScanRate { get; set; }
        public int AnioReserved6 { get; set; }
        public int AnioReserved7 { get; set; }
    }
}