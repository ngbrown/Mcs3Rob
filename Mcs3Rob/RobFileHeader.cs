namespace Mcs3Rob
{
    public class RobFileHeader : RobHeader
    {
        public int EmulationModuleNumber { get; set; }
        public int RomSize { get; set; }
        public int ControlUnitType { get; set; }

        public int? RomCycleTriggerAddress { get; set; }
        public int? CanBusFrequency { get; set; }
        public int? AnioSignalAveragingTime { get; set; }

        public int CanEcuNodeId { get; set; }

        public int? RomBusMonitoringTimeGate { get; set; }
        public int? CanDefaultSamplingInterval { get; set; }
        public int? AnioScanRate { get; set; }

        public int CanIdentifierDtoCcp { get; set; }

        public int? RomRamOffset { get; set; }
        public int? CanIdentifierCroCcp { get; set; }

    }
}