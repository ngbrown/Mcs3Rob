using System;
using System.Text;

namespace Mcs3Rob
{
    internal class AstFile : IAst
    {
        public AstSeq FileHeader { get; }
        public AstSeq DescriptionBlocks { get; }

        public AstFile(AstSeq fileHeader, AstSeq descriptionBlocks)
        {
            if (fileHeader == null) throw new ArgumentNullException(nameof(fileHeader));
            if (descriptionBlocks == null) throw new ArgumentNullException(nameof(descriptionBlocks));

            FileHeader = fileHeader;
            DescriptionBlocks = descriptionBlocks;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var item in this.FileHeader.Items)
            {
                sb.AppendLine(item.ToString());
            }
            sb.AppendLine(".");
            sb.AppendLine();

            foreach (var descriptionBlock in this.DescriptionBlocks.Items)
            {
                sb.AppendLine(descriptionBlock.ToString());
            }

            return sb.ToString();
        }
    }
}