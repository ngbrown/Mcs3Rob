namespace Mcs3Rob
{
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
}