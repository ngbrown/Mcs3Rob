using System;
using System.Text;
using QUT.Gppg;

namespace Mcs3Rob
{
    internal class AstDescriptionBlock : IAst
    {
        public string GroupName { get; }
        public AstSeq Variables { get; }
        public AstSeq Headers { get; }
        public LexLocation LexLocation { get; }

        public AstDescriptionBlock(LexLocation lexLocation, string groupName, AstSeq headers, AstSeq variables = null)
        {
            if (groupName == null) throw new ArgumentNullException(nameof(groupName));
            if (headers == null) throw new ArgumentNullException(nameof(headers));

            LexLocation = lexLocation;
            GroupName = groupName;
            Variables = variables;
            Headers = headers;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(this.GroupName);
            foreach (var header in this.Headers.Items)
            {
                sb.AppendLine(header.ToString());
            }

            if (this.Variables == null)
            {
                sb.AppendLine(".");
            }
            else
            {
                sb.AppendLine();
                foreach (var variablesItem in this.Variables.Items)
                {
                    sb.AppendLine(variablesItem.ToString());
                }
            }

            sb.AppendLine();
            return sb.ToString();
        }
    }
}