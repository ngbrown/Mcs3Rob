using System;
using System.Text;

namespace Mcs3Rob
{
    internal class AstDescriptionBlock : IAst
    {
        public string Type { get; }
        public AstSeq Variables { get; }
        public AstSeq Headers { get; }

        public AstDescriptionBlock(string type, AstSeq headers, AstSeq variables = null)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (headers == null) throw new ArgumentNullException(nameof(headers));

            Type = type;
            Variables = variables;
            Headers = headers;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(this.Type);
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