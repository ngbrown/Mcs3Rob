using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using QUT.Gppg;

namespace Mcs3Rob
{
    internal partial class Mcs3RobScanner
    {
        public event EventHandler<Mcs3Rob.ErrorEventArgs> Error;

        internal void Initialize()
        {
            this.yy_push_state(Mcs3RobScanner.FILEHEADER);
        }

        private void GetNumber()
        {
            NumberStyles numberStyle;
            int postFactor = 1;
            var s = yytext;
            yylval.s = s;
            if (s.StartsWith("$"))
            {
                s = s.Substring(1);
                numberStyle = NumberStyles.HexNumber;
            }
            else if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                s = s.Substring(2);
                numberStyle = NumberStyles.HexNumber;
            }
            else if (s.EndsWith("KB", StringComparison.OrdinalIgnoreCase))
            {
                s = s.Substring(0, s.Length - 2);
                postFactor = 1024;
                numberStyle = NumberStyles.Integer;
            }
            else
            {
                numberStyle = NumberStyles.Integer;
            }

            yylval.n = int.Parse(s, numberStyle) * postFactor;
        }

        private int Make(Token token)
        {
            yylval.s = yytext;

#if TRACE_ACTIONS
            Console.Error.WriteLine("scan token {0}: {1}", TerminalToString((int)token), EscapeString(yytext));
#endif

            return (int) token;
        }

        private void GetString()
        {
            yylval.s = yytext;
        }

        private static string TerminalToString(int terminal)
        {
            if (((Token)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
                return ((Token)terminal).ToString();
            else
                return CharToString((char)terminal);
        }

        private static string EscapeString(string input)
        {
            return string.Join("", input.Select(EscapeChars));
        }

        private static string EscapeChars(char x)
        {
            switch (x)
            {
                case '\a': return @"\a";
                case '\b': return @"\b";
                case '\f': return @"\f";
                case '\n': return @"\n";
                case '\r': return @"\r";
                case '\t': return @"\t";
                case '\v': return @"\v";
                case '\0': return @"\0";
                default: return x.ToString();
            }
        }

        /// <summary>
        /// Return text representation of argument character
        /// </summary>
        /// <param name="input">The character to convert</param>
        /// <returns>String representation of the character</returns>
        private static string CharToString(char input)
        {
            return string.Format(CultureInfo.InvariantCulture, "'{0}'", EscapeChars(input));
        }

        public override void yyerror(string format, params object[] args)
		{
			base.yyerror(format, args);
            OnError(new ErrorEventArgs(new ErrorContext(1, TokenSpan(), format)));
			Console.WriteLine("{0} : Line: {1} : Row: {2} : {3}", this.buffer.FileName, yyline, yycol, string.Format(format, args));
			Console.WriteLine();
		}


        private LexLocation TokenSpan()
        {
            return new LexLocation(tokLin, tokCol, tokELin, tokECol);
        }


        private void ScanError(int errorCode, LexLocation lexLocation)
        {
            OnError(new ErrorEventArgs(new ErrorContext(errorCode, lexLocation)));
        }

        private void OnError(ErrorEventArgs e)
        {
            Error?.Invoke(this, e);
        }
    }
}
