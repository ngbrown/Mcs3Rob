namespace Mcs3Rob
{
    /// <summary>
    /// DEVPARAM for CAN definitions
    /// </summary>
    public class RobCanDeviceParams : RobDeviceParams
    {
        /// <summary>
        /// Base address for binary image (.bin file)
        /// </summary>
        public uint BaseAddressBinaryImage { get; set; }

        /// <summary>
        /// Base address for measurement data (variables)
        /// </summary>
        public uint BaseAddressMeasurementData { get; set; }

        /// <summary>
        /// CAN CCP V1.01+: Identifier for DTO
        /// </summary>
        public int CanCcpIdentifierDto { get; set; }

        /// <summary>
        /// CAN CCP V1.01+: Identifier for CRO
        /// </summary>
        public int CanCcpIdentifierCro { get; set; }

        /// <summary>
        /// line added for analog output control 
        /// </summary>
        public int AnalogOutput1Control { get; set; }

        /// <summary>
        /// line added for analog output control 
        /// </summary>
        public int AnalogOutput2Control { get; set; }

        /// <summary>
        /// line added for analog output control 
        /// </summary>
        public int AnalogOutput3Control { get; set; }

        /// <summary>
        /// line added for analog output control 
        /// </summary>
        public int AnalogOutput4Control { get; set; }
    }
}