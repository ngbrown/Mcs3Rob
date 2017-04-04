using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QUT.Gppg;

namespace Mcs3Rob
{
    internal class AstCharacteristicMapBlock : AstDescriptionBlock
    {
        public IReadOnlyList<AstIndependantAxis> IndependantAxis { get; }
        public AstSeq DependantAxis { get; }

        public AstCharacteristicMapBlock(LexLocation lexLocation, string groupName, AstSeq headers, AstSeq dependantAxis, params IAst[] independantAxis)
            : base(lexLocation, groupName, headers, null)
        {
            if (independantAxis == null) throw new ArgumentNullException(nameof(independantAxis));
            if (dependantAxis == null) throw new ArgumentNullException(nameof(dependantAxis));

            IndependantAxis = independantAxis.Cast<AstIndependantAxis>().ToList();
            DependantAxis = dependantAxis;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(this.GroupName);
            foreach (var header in this.Headers.Items)
            {
                sb.AppendLine(header.ToString());
            }
            sb.AppendLine();

            foreach (var axis in this.IndependantAxis)
            {
                sb.AppendLine(axis.ToString());
            }

            foreach (var axisItem in this.DependantAxis.Items)
            {
                sb.AppendLine(axisItem.ToString());
            }

            return sb.ToString();
        }
    }
}