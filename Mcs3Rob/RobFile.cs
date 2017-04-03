using System.Collections.Generic;

namespace Mcs3Rob
{
    public class RobFile
    {
        public RobFileHeader FileHeader { get; set; }
        public RobDeviceParams DeviceParams { get; set; }
        public RobConstants Constants { get; set; }
        public RobVariables Variables { get; set; }

        public List<RobCharacteristicMap> CharacteristicMaps { get; set; }
        public List<RobRomText> RomTexts { get; set; }
    }
}