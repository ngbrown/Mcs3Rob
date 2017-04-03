using System.Collections.Generic;

namespace Mcs3Rob
{
    public class RobConstants : RobDescriptionBlock
    {
        public RobConstantsHeader Header { get; set; }

        public List<RobParameter> Parameters { get; set; }
    }
}