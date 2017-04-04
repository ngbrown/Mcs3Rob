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
EndMarker       ^\.
GroupName       [A-Z][A-Z0-9]*
NotComma        [^\r\n,;]
Comma           {Space}*","{Space}*
CharLine        [^\.; \t\r\n][^;\r\n]*

LineComment     ";"{DotChr}*
ListText        {NotComma}*

%x FILEHEADER
%x DEVPARAM
%x PROCONST2
%x PROVARI2HEADER
%x PROCONST2HEADER
%x CHARLINE2HEADER
%x CHARMAP2HEADER
%x CHARSPACEHEADER
%x NUMBERLINE
%x TEXTLINE
%x UAXIS
%x VAXIS
%x WAXIS
%x QAXIS
%x ROMTEXT
%x VARIABLELIST
%x XPEOL     // Expecting end of line (should be blank here)


%{

%}

%%

/* Scanner body */

<*>{LineComment}   /* skip */

<XPEOL>{
    {Eol}       { yy_pop_state(); CountdownOrPopState(); return Make(Token.EOL); }
    ","         { yy_pop_state(); CountdownOrPopState(); return Make(Token.comma); }
}

<FILEHEADER, DEVPARAM, PROVARI2HEADER, PROCONST2HEADER>{
    {HexNumber}		{ GetNumber(); yy_push_state(XPEOL); return Make(Token.HEXNUMBER); }
    {Number}		{ GetNumber(); yy_push_state(XPEOL); return Make(Token.NUMBER); }
    {SizeNumber}   { GetNumber(); yy_push_state(XPEOL); return Make(Token.NUMBER); }
}

<VARIABLELIST>{ListText}({Comma}{ListText}?){7,10}  { yy_push_state(XPEOL); return Make(Token.VARIABLELINE); }

<CHARLINE2HEADER, CHARMAP2HEADER, CHARSPACEHEADER, UAXIS, VAXIS, WAXIS, QAXIS, ROMTEXT>{
    {CharLine}      { this.charHeaderToken = GetCharHeader(); yy_push_state(XPEOL); return Make(this.charHeaderToken); }
}

<FILEHEADER, DEVPARAM, UAXIS, VAXIS, WAXIS, QAXIS, ROMTEXT>{
    {EndMarker}     { yy_pop_state(); yy_push_state(XPEOL); return Make(Token.ENDMARKER); }
}

<INITIAL, VARIABLELIST>{
    ^("DEVPARM"|"DEVPARAM") {
        yy_clear_stack();
        yy_push_state(DEVPARAM);
        return Make(Token.DEVPARAM);
    }

    ^"PROVARI2" {
        yy_clear_stack();
        yy_push_state(VARIABLELIST);
        yy_push_state(PROVARI2HEADER);
        StartCountdown(2);
        return Make(Token.PROVARI2);
    }

    ^"PROCONST2" {
        yy_clear_stack();
        yy_push_state(VARIABLELIST);
        yy_push_state(PROCONST2HEADER);
        StartCountdown(2);
        return Make(Token.PROCONST2);
    }

    ^"CHARLINE2" {
        yy_clear_stack();
        yy_push_state(QAXIS);
        yy_push_state(UAXIS);
        yy_push_state(CHARLINE2HEADER);
        StartCountdown(5);
        return Make(Token.CHARLINE2);
    }

    ^"CHARMAP2" {
        yy_clear_stack();
        yy_push_state(QAXIS);
        yy_push_state(VAXIS);
        yy_push_state(UAXIS);
        yy_push_state(CHARMAP2HEADER);
        StartCountdown(5);
        return Make(Token.CHARMAP2);
    }

    ^"CHARSPACE" {
        yy_clear_stack();
        yy_push_state(QAXIS);
        yy_push_state(WAXIS);
        yy_push_state(VAXIS);
        yy_push_state(UAXIS);
        yy_push_state(CHARSPACEHEADER);
        StartCountdown(5);
        return Make(Token.CHARSPACE);
    }

    ^"ROMTEXT" {
        yy_clear_stack();
        yy_push_state(ROMTEXT);
        return Make(Token.ROMTEXT);
    }
}

<*>{Space}+		/* skip */

/* Catch all non-whitespace not part of any other token */
<*>{NotWh}     { ScanError(79, TokenSpan()); return Make(Token.SCANERROR); }


%{
    /* Epilog from LEX file */
	yylloc = TokenSpan();
%}

%%