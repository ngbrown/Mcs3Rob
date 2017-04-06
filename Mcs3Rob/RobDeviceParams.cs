namespace Mcs3Rob
{
    /// <summary>
    /// Definitions about the device.
    /// Could be either "DEVPARAM" or "DEVPARM" (the second one is based on a document, not an actual file).
    /// See <see cref="RobRomDeviceParams"/> or <see cref="RobCanDeviceParams"/>.
    /// </summary>
    public abstract class RobDeviceParams : RobDescriptionBlock
    {
    }
}