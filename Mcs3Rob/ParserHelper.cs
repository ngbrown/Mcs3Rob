using System;
using System.Linq;

namespace Mcs3Rob
{
    internal static class ParserHelper
    {
        public static int ReadAsInt(this IAst ast)
        {
            if (ast is AstInteger)
            {
                long signed = ((AstInteger) ast).Value;
                if (signed > Int32.MaxValue || signed < Int32.MinValue)
                {
                    throw new ParserContextErrorException("Number to big for target integer field.", ast);
                }

                return (int)signed;
            }
            else if (ast is AstUnsigned)
            {
                uint unsigned = ((AstUnsigned) ast).Value;
                if ((long) unsigned > Int32.MaxValue)
                {
                    throw new ParserContextErrorException("Number to big for target integer field.", ast);
                }

                return (int) unsigned;
            }
            else
            {
                throw new ParserContextErrorException("Expected an integer", ast);
            }
        }

        public static long ReadAsLong(this IAst ast)
        {
            if (ast is AstInteger)
            {
                return ((AstInteger) ast).Value;
            }
            else if (ast is AstUnsigned)
            {
                return ((AstUnsigned) ast).Value;
            }
            else
            {
                throw new ParserContextErrorException("Expected an integer", ast);
            }
        }

        public static uint ReadAsUInt(this IAst ast)
        {
            if (ast is AstInteger)
            {
                long signed = ((AstInteger) ast).Value;
                if (signed < 0)
                {
                    throw new ParserContextErrorException("Number is negative and won't convert to unsigned.", ast);
                }
                if (signed > UInt32.MaxValue || signed < UInt32.MinValue)
                {
                    throw new ParserContextErrorException("Number to big for target integer field.", ast);
                }
                return (uint)signed;
            }
            else if (ast is AstUnsigned)
            {
                return ((AstUnsigned) ast).Value;
            }
            else
            {
                throw new ParserContextErrorException("Expected an signed/unsigned integer", ast);
            }
        }

        public static string ReadAsText(this IAst ast)
        {
            if (ast is AstText)
            {
                return ((AstText) ast).Value;
            }
            else
            {
                throw new ParserContextErrorException($"Expected a string but was {ast.GetType()}", ast);
            }
        }

        public static int ReadAsInt(this AstSeq astSeq, int index)
        {
            if (astSeq.Items.Count <= index)
            {
                throw new ParserContextErrorException("Expected another entry. List too short.", astSeq);
            }

            return astSeq.Items[index].ReadAsInt();
        }

        public static uint ReadAsUInt(this AstSeq astSeq, int index)
        {
            if (astSeq.Items.Count <= index)
            {
                throw new ParserContextErrorException("Expected another entry. List too short.", astSeq);
            }

            return astSeq.Items[index].ReadAsUInt();
        }

        public static long ReadAsLong(this AstSeq astSeq, int index)
        {
            if (astSeq.Items.Count <= index)
            {
                throw new ParserContextErrorException("Expected another entry. List too short.", astSeq);
            }

            return astSeq.Items[index].ReadAsLong();
        }

        public static string ReadAsText(this AstSeq astSeq, int index)
        {
            if (astSeq.Items.Count <= index)
            {
                throw new ParserContextErrorException("Expected another entry. List too short.", astSeq);
            }

            return astSeq.Items[index].ReadAsText();
        }

        public static AstDescriptionBlock ReadAsAstDescriptionBlock(this AstFile astFile, string groupName)
        {
            var astDeviceParams =
                astFile.DescriptionBlocks.Items.OfType<AstDescriptionBlock>()
                    .Single(x => x.GroupName.Equals(groupName, StringComparison.OrdinalIgnoreCase));
            return astDeviceParams;
        }

        public static bool IsKnownHexValue(this IAst item)
        {
            bool knownHexValue;
            if (item is AstUnsigned)
            {
                knownHexValue = ((AstUnsigned) item).KnownHexValue;
            }
            else if (item is AstInteger)
            {
                knownHexValue = ((AstInteger) item).KnownHexValue;
            }
            else
            {
                knownHexValue = false;
            }
            return knownHexValue;
        }
    }
}