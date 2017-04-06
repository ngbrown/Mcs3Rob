using System.Collections.Generic;

namespace Mcs3Rob
{
    /// <summary>
    /// The PROCONST2 description block is used to define program constants.
    /// The description contains information on the location and data type
    /// along with the variable name and conversion formula.
    /// </summary>
    public class RobConstants : RobDescriptionBlock
    {
        public RobConstantsHeader Header { get; set; }

        /// <summary>
        /// List of <see cref="RobParameter"/>. These identify "static" data.
        /// </summary>
        public List<RobParameter> Parameters { get; set; }
    }
}