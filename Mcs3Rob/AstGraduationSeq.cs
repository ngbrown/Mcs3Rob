using System;
using System.Collections.Generic;
using System.Linq;
using QUT.Gppg;

namespace Mcs3Rob
{
    internal class AstGraduationSeq : AstSeq
    {
        public AstGraduationSeq(LexLocation lexLocation, IEnumerable<IAst> itemList)
            : base(lexLocation, itemList)
        {
        }

        public AstGraduationSeq(LexLocation lexLocation, string graduationLine)
            : base(lexLocation, ParseGraduationLine(lexLocation, graduationLine))
        {
        }

        public AstGraduationSeq Append(string graduationLine)
        {
            return new AstGraduationSeq(LexLocation, this.Items.Concat(ParseGraduationLine(LexLocation, graduationLine)));
        }

        private static IEnumerable<IAst> ParseGraduationLine(LexLocation lexLocation, string graduationLine)
        {
            var split = graduationLine.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
            return split.Select<string, IAst>(s => new AstFloat(lexLocation, double.Parse(s.Trim())));
        }
    }
}