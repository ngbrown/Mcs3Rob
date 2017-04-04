using System;
using System.Collections.Generic;
using System.Globalization;
using QUT.Gppg;

namespace Mcs3Rob
{
    internal class AstVariableLine : AstSeq
    {
        public string SourceLine { get; }

        public AstVariableLine(LexLocation lexLocation, string line)
            : base(lexLocation, ParseVariableLine(lexLocation, line))
        {
            if (line == null) throw new ArgumentNullException(nameof(line));

            SourceLine = line;
        }

        private static IReadOnlyList<IAst> ParseVariableLine(LexLocation lexLocation, string line)
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
                        list.Add(ParseNumber(lexLocation, s));
                        break;
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        list.Add(new AstText(lexLocation, s));
                        break;
                    case 10:
                        list.Add(ParseNumber(lexLocation, s));
                        break;
                    default:
                        throw new InvalidOperationException($"Too many values in string: \"{line}\"");
                }
            }

            return list;
        }

        private static IAst ParseNumber(LexLocation lexLocation, string s)
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
                return new AstUnsigned(lexLocation, (uint)value, knownHexValue);
            }

            return new AstInteger(lexLocation, (int)value, knownHexValue);
        }

        public override string ToString()
        {
            return string.Join(", ", Items);
        }
    }
}