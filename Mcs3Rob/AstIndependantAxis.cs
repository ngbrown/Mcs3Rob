using System;
using System.Text;

namespace Mcs3Rob
{
    internal class AstIndependantAxis : IAst
    {
        public AstSeq AxisHeader { get; }
        public AstSeq GraduationItems { get; }

        public AstIndependantAxis(AstSeq axisHeader, AstSeq graduationItems = null)
        {
            if (axisHeader == null) throw new ArgumentNullException(nameof(axisHeader));

            AxisHeader = axisHeader;
            GraduationItems = graduationItems;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            foreach (var header in this.AxisHeader.Items)
            {
                sb.AppendLine(header.ToString());
            }

            if (this.GraduationItems != null)
            {
                sb.AppendLine(this.GraduationItems.ToString());
            }

            sb.AppendLine(".");

            return sb.ToString();
        }
    }
}