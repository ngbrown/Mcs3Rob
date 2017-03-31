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

        private int countdownToPop;
        private Token charHeaderToken;

        internal void Initialize()
        {
            this.yy_push_state(Mcs3RobScanner.FILEHEADER);
        }

        private Token GetNumber()
        {
            NumberStyles numberStyle;
            Token match = Token.NUMBER;
            int postFactor = 1;
            var s = yytext.Trim(' ', '\t');
            yylval.s = s;
            if (s.StartsWith("$"))
            {
                s = s.Substring(1);
                numberStyle = NumberStyles.HexNumber;
                match = Token.HEXNUMBER;
            }
            else if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            {
                s = s.Substring(2);
                numberStyle = NumberStyles.HexNumber;
                match = Token.HEXNUMBER;
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
            return match;
        }

        private void StartCountdown(int count)
        {
#if TRACE_ACTIONS
            Console.Error.WriteLine("start countdown {0}: {1}", count, EscapeString(yytext));
#endif
            this.countdownToPop = count;
        }

        private void CountdownOrPopState()
        {
            if (this.countdownToPop > 0)
            {
                if ((--this.countdownToPop) == 0)
                {
#if TRACE_ACTIONS
                    Console.Error.WriteLine("stop countdown, pop-state: {0}", EscapeString(yytext));
#endif
                    yy_pop_state();
                }
            }
        }

        private int Make(Token token)
        {
            yylval.s = yytext;

#if TRACE_ACTIONS
            Console.Error.WriteLine("scan token {0}: {1}", TerminalToString((int)token), EscapeString(yytext));
#endif

            return (int) token;
        }

        private Token GetString()
        {
            yylval.s = yytext;
            return Token.TEXT;
        }

        private int YY_START_LAST = 0;
        private int charHeaderCount = 0;

        private Token GetCharHeader()
        {
            if (YY_START_LAST != YY_START)
            {
                YY_START_LAST = YY_START;
                charHeaderCount = 0;
            }

            if (YY_START == CHARLINE2HEADER || YY_START == CHARMAP2HEADER || YY_START == CHARSPACEHEADER)
            {
                switch (charHeaderCount++)
                {
                    case 0: return GetString(); // Description of characteristic map
                    case 1: return GetString(); // Label of characteristic map (Listing label)
                    case 2: return GetNumber(); // ROM address of characteristic map
                    case 3: return GetNumber(); // reserve = 0
                    case 4: return GetNumber(); // reserve = 0
                }
            }
            else if (YY_START == UAXIS || YY_START == VAXIS || YY_START == WAXIS || YY_START == QAXIS)
            {
                switch (charHeaderCount++)
                {
                    case 0: return GetNumber(); // MC value Element type
                    case 1: return GetNumber(); // MC value direction: 0=normal, 1=reverse
                    case 2: return GetNumber(); // Minimum MC model value
                    case 3: return GetNumber(); // Maximum MC model value
                    case 4: return GetNumber(); // ROM address graduation table (-1 = not in ROM)
                    case 5: return GetNumber(); // Axis direction in MC model: 0=normal, 1=reverse
                    case 6: return GetString(); // Full axis description
                    case 7: return GetString(); // Physical label abbreviation
                    case 8: return GetString(); // Unit
                    case 9: return GetString(); // Conversion Formula MC model -> physical
                    case 10: return GetNumber(); // Decimal points
                    case 11: return GetNumber(); // Dimension (number of values on axis)
                    case 12: return GetNumber(); // Graduation Table axis direction: 0=as map, 1=reverse
                    case 13: return GetNumber(); // reserve
                    case 14: return GetNumber(); // Graduation model: 0=MC model, 1=physical
                    default:
                        if (YY_START == QAXIS)
                        {
                            // No graduation model on dependant axis
                            return Token.SCANERROR;
                        }

                        GetString();
                        return Token.GRADUATIONMODELLINE;
                }
            }

            return Token.SCANERROR;
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
