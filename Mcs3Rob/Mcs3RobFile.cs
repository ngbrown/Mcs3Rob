using System.Collections.Generic;

namespace Mcs3Rob
{
    public class Mcs3RobFile
    {
        public RobFileHeader FileHeader { get; set; }
        public RobDeviceParams DeviceParams { get; set; }
        public RobConstants Constants { get; set; }
        public RobVariables Variables { get; set; }

        public List<RobCharacteristicMap> CharacteristicMaps { get; set; }
        public List<RobRomText> RomTexts { get; set; }
    }

    public class RobHeader
    {
        public List<string> RawLines { get; set; }
    }

    public class RobDescriptionBlock
    {
        
    }

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

    public class RobDeviceParams : RobDescriptionBlock
    {
        
    }

    public class RobCanDeviceParams : RobDeviceParams
    {
        public int BaseAddressBinaryImage { get; set; }
        public int BaseAddressMeasurementData { get; set; }
        public int CanCcpIdentifierDto { get; set; }
        public int CanCcpIdentifierCro { get; set; }
        public int AnalogOutput1Control { get; set; }
        public int AnalogOutput2Control { get; set; }
        public int AnalogOutput3Control { get; set; }
        public int AnalogOutput4Control { get; set; }
    }

    public class RobRomDeviceParams : RobDeviceParams
    {
        public int OffsetRomImage { get; set; }
        public int OffsetProvariAddress { get; set; }
    }

    public class RobConstantsHeader : RobHeader
    {
        public int Reserved { get; set; }
        public int RamBlockAddressOffset { get; set; }
    }

    public enum RobElementType
    {
        UInt8 = 0,
        Int8 = 1,
        UInt16Intel = 2,
        Int16Intel = 3,
        UInt16Motorola = 4,
        Int16Motorola = 5,
        UInt32Intel = 6,
        Int32Intel = 7,
        UInt32Motorola = 8,
        Int32Motorola = 9,
        FloatIntel = 10,
        FloatMotorola = 11,
    }

    public enum RobDirection
    {
        /// <summary>
        /// The value increments with the '+' key and decrements with the '-' key
        /// </summary>
        Normal = 0,

        /// <summary>
        /// The value decrements with the '+' key and increments with the '-' key
        /// </summary>
        Reverse = 1,
    }

    public enum RobDisplayMode
    {
        AnalogDisplay = 1,
        DigitalDisplay0To7 = 2,
        DigitalDisplay1To8 = 3,
    }

    public class RobParameter
    {
        public RobElementType ElementType { get; set; }

        public int? Bitmask { get; set; }
        public RobDirection? Direction { get; set; }

        public int MinimumLimit { get; set; }
        public int MaximumLimit { get; set; }
        public int Address { get; set; }
        public RobDisplayMode DisplayMode { get; set; }
        public string Description { get; set; }
        public string VariableName { get; set; }
        public string EngineeringUnit { get; set; }
        public string ConversionEquation { get; set; }
        public int? DecimalPlaces { get; set; }
    }

    public class RobConstants : RobDescriptionBlock
    {
        public RobConstantsHeader Header { get; set; }

        public List<RobParameter> Parameters { get; set; }
    }

    public class RobVariablesHeader : RobHeader
    {
        public int Reserved { get; set; }
        public int RamBlockAddressOffset { get; set; }
    }

    public class RobVariables : RobDescriptionBlock
    {
        public RobVariablesHeader Header { get; set; }

        public List<RobParameter> Parameters { get; set; }
    }

    public class RobRomText : RobDescriptionBlock
    {
        public string Description { get; set; }
        public string Label { get; set; }
        public int Address { get; set; }
        public int TextType { get; set; }
        public int MaxLength { get; set; }
    }

    public class RobIndependantAxis
    {
        public RobElementType ElementType { get; set; }
        public RobDirection ValueDirection { get; set; }
        public int MinimumLimit { get; set; }
        public int MaximumLimit { get; set; }
        public int AddressGraduationTable { get; set; }
        public RobDirection AxisDirection { get; set; }
        public string Description { get; set; }
        public string PhysicalLabelAbbreviation { get; set; }
        public string EngineeringUnit { get; set; }
        public string ConversionEquation { get; set; }
        public int DecimalPlaces { get; set; }
        public int Dimension { get; set; }
        public RobDirection GraduationTableAxisDirection { get; set; }
        public int Reserve { get; set; }
        public int GraduationModel { get; set; }

        /// <summary>
        /// When the graduation table is not in ROM(-1) the values must be supplied in the definition
        /// </summary>
        public List<int> DefinitionValues { get; set; }
    }

    public class RobDependantAxis
    {
        public RobElementType ElementType { get; set; }
        public RobDirection ValueDirection { get; set; }
        public int MinimumLimit { get; set; }
        public int MaximumLimit { get; set; }
        public int Reserve1 { get; set; }
        public int Reserve2 { get; set; }
        public string Description { get; set; }
        public string PhysicalLabelAbbreviation { get; set; }
        public string EngineeringUnit { get; set; }
        public string ConversionEquation { get; set; }
        public int? DecimalPlaces { get; set; }
        public int Reserve3 { get; set; }
        public int Reserve4 { get; set; }
        public int Reserve5 { get; set; }
    }

    public class RobCharacteristicMapHeader : RobHeader
    {
        public string Description { get; set; }
        public string Label { get; set; }
        public int AddressOfMap { get; set; }
        public int Reserve1 { get; set; }
        public int Reserve2 { get; set; }
    }

    public class RobCharacteristicMap
    {
        public RobCharacteristicMapHeader Header { get; set; }
    }

    public class RobCharLine2 : RobCharacteristicMap
    {
        public RobIndependantAxis UAxis { get; set; }
        public RobDependantAxis QAxis { get; set; }
    }

    public class RobCharMap3 : RobCharacteristicMap
    {
        public RobIndependantAxis UAxis { get; set; }
        public RobIndependantAxis VAxis { get; set; }
        public RobDependantAxis QAxis { get; set; }
    }

    public class RobCharSpace : RobCharacteristicMap
    {
        public RobIndependantAxis UAxis { get; set; }
        public RobIndependantAxis VAxis { get; set; }
        public RobIndependantAxis WAxis { get; set; }
        public RobDependantAxis QAxis { get; set; }
    }
}