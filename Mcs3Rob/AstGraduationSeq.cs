using System;
using System.Collections.Generic;
using System.Linq;

namespace Mcs3Rob
{
    internal class AstGraduationSeq : AstSeq
    {
        public AstGraduationSeq(IEnumerable<IAst> itemList)
            : base(itemList)
        {
        }

        public AstGraduationSeq(string graduationLine)
            : base(ParseGraduationLine(graduationLine))
        {
        }

        public AstGraduationSeq Append(string graduationLine)
        {
            return new AstGraduationSeq(this.Items.Concat(ParseGraduationLine(graduationLine)));
        }

        private static IEnumerable<IAst> ParseGraduationLine(string graduationLine)
        {
            var split = graduationLine.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            return split.Select<string, IAst>(s => new AstFloat(double.Parse(s.Trim())));
        }
    }
}