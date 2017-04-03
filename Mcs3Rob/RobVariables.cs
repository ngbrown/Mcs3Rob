using System.Collections.Generic;

namespace Mcs3Rob
{
    public class RobVariables : RobDescriptionBlock
    {
        public RobVariablesHeader Header { get; set; }

        public List<RobParameter> Parameters { get; set; }
    }
}