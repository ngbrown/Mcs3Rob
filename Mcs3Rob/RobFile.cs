using System.Collections.Generic;

namespace Mcs3Rob
{
    public class RobFile
    {
        /// <summary>
        /// The header configures the MCS for ROM/Data link that the controller is using.
        /// </summary>
        public RobFileHeader FileHeader { get; set; }

        /// <summary>
        /// Definitions about the device.
        /// Could be either "DEVPARAM" or "DEVPARM" (the second one is based on a document, not an actual file).
        /// </summary>
        public RobDeviceParams DeviceParams { get; set; }

        /// <summary>
        /// The PROCONST2 description block is used to define program constants.
        /// The description contains information on the location and data type
        /// along with the variable name and conversion formula.
        /// </summary>
        public RobConstants Constants { get; set; }

        /// <summary>
        /// The PROVARI2 description block is used to define singular numeric values
        /// such as RPM and throttle position. The description contains information
        /// on the location and data type along with the variable name and conversion
        /// formula.
        /// </summary>
        public RobVariables Variables { get; set; }

        /// <summary>
        /// Characteristic maps (CHARLINE2, CHARMAP2, CHARSPACE) are value tables in the ROM of a control unit.
        /// Mathematically, they are functions with an output value dependent
        /// on one, two, or three input parameter.
        /// See <see cref="RobCharLine2"/>, <see cref="RobCharMap2"/>, or <see cref="RobCharSpace"/>.
        /// </summary>
        public List<RobCharacteristicMap> CharacteristicMaps { get; set; }

        /// <summary>
        /// Description and locations of text strings in ROM/RAM
        /// </summary>
        public List<RobRomText> RomTexts { get; set; }
    }
}