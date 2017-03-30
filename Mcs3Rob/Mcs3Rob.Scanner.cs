using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Mcs3Rob
{
    internal partial class Mcs3RobScanner
    {
        private void GetNumber()
        {
            yylval.s = yytext;
            yylval.n = int.Parse(yytext);
        }

        private void GetHexNumber()
        {
            var s = yytext;
            yylval.s = s;
            if (s.StartsWith("$"))
            {
                s = s.Substring(1);
            }
            yylval.n = int.Parse(s, NumberStyles.AllowHexSpecifier);
        }

        private int Make(Token token)
        {
            yylval.s = yytext;

#if TRACE_ACTIONS
            Console.Error.WriteLine("scan token {0}: {1}", TerminalToString((int)token), yytext);
#endif

            return (int) token;
        }

        void GetString()
        {
            yylval.s = yytext;
        }

        private string TerminalToString(int terminal)
        {
            if (((Token)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
                return ((Token)terminal).ToString();
            else
                return CharToString((char)terminal);
        }

        /// <summary>
        /// Return text representation of argument character
        /// </summary>
        /// <param name="input">The character to convert</param>
        /// <returns>String representation of the character</returns>
        private static string CharToString(char input)
        {
            switch (input)
            {
                case '\a': return @"'\a'";
                case '\b': return @"'\b'";
                case '\f': return @"'\f'";
                case '\n': return @"'\n'";
                case '\r': return @"'\r'";
                case '\t': return @"'\t'";
                case '\v': return @"'\v'";
                case '\0': return @"'\0'";
                default: return string.Format(CultureInfo.InvariantCulture, "'{0}'", input);
            }
        }

        public override void yyerror(string format, params object[] args)
		{
			base.yyerror(format, args);
			Console.WriteLine("{0} : Line: {1} : Row: {2} : {3}", this.buffer.FileName, yyline, yycol, string.Format(format, args));
			Console.WriteLine();
		}
    }
}
