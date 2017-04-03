using System;
using System.Collections.Generic;
using System.Globalization;
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
                sb.AppendLine(this.GraduationItems.ToString());
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
            sb.AppendLine(".");
            sb.AppendLine();

            foreach (var descriptionBlock in this.DescriptionBlocks.Items)
            {
                sb.AppendLine(descriptionBlock.ToString());
            }

            return sb.ToString();
        }
    }

    internal class AstVariableLine : IAst
    {
        public string SourceLine { get; }

        public IReadOnlyList<IAst> Items { get; }

        public AstVariableLine(string line)
        {
            if (line == null) throw new ArgumentNullException(nameof(line));

            SourceLine = line;
            Items = ParseVariableLine(line);
        }

        private static IReadOnlyList<IAst> ParseVariableLine(string line)
        {
            var list = new List<IAst>();
            var split = line.Split(new[] {','}, StringSplitOptions.None);

            for (var i = 0; i < split.Length; i++)
            {
                var s = split[i].Trim();

                switch (i)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        list.Add(ParseNumber(s));
                        break;
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        list.Add(new AstText(s));
                        break;
                    case 10:
                        list.Add(ParseNumber(s));
                        break;
                    default:
                        throw new InvalidOperationException($"Too many values in string: \"{line}\"");
                }
            }

            return list;
        }

        private static IAst ParseNumber(string s)
        {
            long value;
            bool knownHexValue;
            if (s.StartsWith("$"))
            {
                knownHexValue = true;
                value = long.Parse(s.Substring(1), NumberStyles.HexNumber);
            }
            else if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                knownHexValue = true;
                value = long.Parse(s.Substring(2), NumberStyles.HexNumber);
            }
            else
            {
                knownHexValue = false;
                value = long.Parse(s, NumberStyles.Integer);
            }

            if (knownHexValue || value > 32 && (value & (value - 1)) == 0 || value > int.MaxValue)
            {
                // hack to switch to hex on values that are power of 2
                knownHexValue = true;
                return new AstUnsigned((uint)value, knownHexValue);
            }

            return new AstInteger((int)value, knownHexValue);
        }

        public override string ToString()
        {
            return string.Join(", ", Items);
        }
    }

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
        public bool KnownHexValue { get; }

        public AstInteger(int value)
            :this(value, false)
        {
        }

        public AstInteger(int value, bool knownHexValue)
        {
            Value = value;
            KnownHexValue = knownHexValue;
        }

        public override string ToString()
        {
            if (this.KnownHexValue)
            {
                return "$" + this.Value.ToString("X4");
            }
            else
            {
                return this.Value.ToString();
            }
        }
    }

    internal struct AstUnsigned : IAst
    {
        public uint Value { get; }
        public bool KnownHexValue { get; }

        public AstUnsigned(uint value)
            : this(value, false)
        {
        }

        public AstUnsigned(uint value, bool knownHexValue)
        {
            Value = value;
            KnownHexValue = knownHexValue;
        }

        public override string ToString()
        {
            if (this.KnownHexValue)
            {
                return "$" + this.Value.ToString("X4");
            }
            else
            {
                return this.Value.ToString();
            }
        }
    }

    internal struct AstFloat : IAst
    {
        public double Value { get; }

        public AstFloat(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}