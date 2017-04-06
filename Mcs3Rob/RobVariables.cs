using System.Collections.Generic;

namespace Mcs3Rob
{
    /// <summary>
    /// The PROVARI2 description block is used to define singular numeric values
    /// such as RPM and throttle position. The description contains information
    /// on the location and data type along with the variable name and conversion
    /// formula.
    /// </summary>
    public class RobVariables : RobDescriptionBlock
    {
        public RobVariablesHeader Header { get; set; }

        /// <summary>
        /// List of <see cref="RobParameter"/>. These identify dynamic data
        /// and can be requested for recording and display.
        /// </summary>
        public List<RobParameter> Parameters { get; set; }
    }
}