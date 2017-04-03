using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mcs3Rob
{
    internal interface IAst
    {
    }

    internal class AstSeq : IAst
    {
        public IReadOnlyList<IAst> Items { get; }

        public AstSeq(IAst firstItem)
        {
            if (firstItem == null) throw new ArgumentNullException(nameof(firstItem));

            var itemList = firstItem as AstSeq;
            if (itemList?.Items?.Count == 1)
            {
                firstItem = itemList.Items[0];
            }

            Items = new List<IAst>() {firstItem};
        }

        public AstSeq(IEnumerable<IAst> itemList)
        {
            if (itemList == null) throw new ArgumentNullException(nameof(itemList));

            Items = new List<IAst>(itemList);
        }

        public AstSeq(params IAst[] items)
            : this((IEnumerable<IAst>)items)
        {
        }

        public AstSeq Append(IAst newItem)
        {
            if (newItem == null) throw new ArgumentNullException(nameof(newItem));

            var itemList = newItem as AstSeq;
            if (itemList?.Items?.Count == 1)
            {
                newItem = itemList.Items[0];
            }

            return new AstSeq(Items.Concat(new[] {newItem}));
        }

        public override string ToString()
        {
            return string.Join(", ", Items);
        }
    }

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

    internal class AstCharacteristicMapBlock : AstDescriptionBlock
    {
        public IReadOnlyList<AstIndependantAxis> IndependantAxis { get; }
        public AstSeq DependantAxis { get; }

        public AstCharacteristicMapBlock(string type, AstSeq headers, AstSeq dependantAxis, params IAst[] independantAxis)
            : base(type, headers, null)
        {
            if (independantAxis == null) throw new ArgumentNullException(nameof(independantAxis));
            if (dependantAxis == null) throw new ArgumentNullException(nameof(dependantAxis));

            IndependantAxis = independantAxis.Cast<AstIndependantAxis>().ToList();
            DependantAxis = dependantAxis;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine(this.Type);
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
                foreach (var item in this.GraduationItems.Items)
                {
                    sb.AppendLine(item.ToString());
                }
            }

            sb.AppendLine(".");

            return sb.ToString();
        }
    }

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

            foreach (var descriptionBlock in this.DescriptionBlocks.Items)
            {
                sb.AppendLine(descriptionBlock.ToString());
            }

            return sb.ToString();
        }
    }

    internal struct AstText : IAst
    {
        public string Value { get; }

        public AstText(string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        public override string ToString()
        {
            return this.Value ?? "0";
        }
    }

    internal struct AstInteger : IAst
    {
        public int Value { get; }

        public AstInteger(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}