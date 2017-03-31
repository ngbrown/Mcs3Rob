%namespace Mcs3Rob
%scannertype Mcs3RobScanner
%visibility internal
%tokentype Token

%option stack, minimize, parser, verbose, persistbuffer, noembedbuffers 

DotChr          [^\r\n]
Eol             (\r\n?|\n)
NotWh           [^ \t\r\n]
Space           [ \t]
HexNumber       ($|0x|0X)[0-9A-Fa-f]+
Number          [+\-]?[0-9]+
SizeNumber      [0-9]+"KB"
LineComment     ";"{DotChr}*
EndMarker       ^\.
GroupName       [A-Z][A-Z0-9]*


%x FILEHEADER
%x DEVPARAM
%x PROCONST2
%x PROVARI2
%x CHARLINE2
%x CAHRMAP2
%x CHARSPACE
%x ROMTEXT
%x XPEOL     // Expecting end of line (should be blank here)


%{

%}

%%

/* Scanner body */

<*>{LineComment}   /* skip */

<XPEOL>{Eol}           { yy_pop_state(); return Make(Token.EOL); }

<FILEHEADER, DEVPARAM>{
    {HexNumber}		{ GetNumber(); yy_push_state(XPEOL); return Make(Token.HEXNUMBER); }
    {Number}		{ GetNumber(); yy_push_state(XPEOL); return Make(Token.NUMBER); }
    {SizeNumber}   { GetNumber(); yy_push_state(XPEOL); return Make(Token.NUMBER); }

}

<FILEHEADER, DEVPARAM>{
    {EndMarker}     { yy_pop_state(); yy_push_state(XPEOL); return Make(Token.ENDMARKER); }
}

<INITIAL>^("DEVPARM"|"DEVPARAM")     { yy_push_state(DEVPARAM); return Make(Token.DEVPARAM); }

<DEVPARAM>{
    <XPEOL>","       { yy_pop_state(); return Make(Token.comma); }
}


<*>{Space}+		/* skip */

/* Catch all non-whitespace not part of any other token */
<*>{NotWh}     { ScanError(79, TokenSpan()); return Make(Token.SCANERROR); }


%{
    /* Epilog from LEX file */
	yylloc = TokenSpan();
%}

%%