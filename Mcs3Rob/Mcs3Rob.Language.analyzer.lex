%namespace Mcs3Rob
%scannertype Mcs3RobScanner
%visibility internal
%tokentype Token

%option stack, minimize, parser, verbose, persistbuffer, noembedbuffers 

Eol             (\r\n?|\n)
NotWh           [^ \t\r\n]
Space           [ \t]
HexNumber       $[0-9A-Fa-f]+
Number          [0-9]+
LineComment     ";"[^\r\n]*
EndMarker       ^\.
GroupName       [A-Z0-9]+

%x DEVPARAM
%x PROCONST2
%x PROVARI2
%x CHARLINE2
%x CAHRMAP2
%x CHARSPACE
%x ROMTEXT


%{

%}

%%

/* Scanner body */

<*>{HexNumber}		{ GetHexNumber(); return Make(Token.HEXNUMBER); }

<*>{Number}		{ GetNumber(); return Make(Token.NUMBER); }


<INITIAL, DEVPARAM>{EndMarker}     { yy_pop_state(); return Make(Token.ENDMARKER); }

^DEVPARM     { yy_push_state(DEVPARAM); return Make(Token.DEVPARAM); }

<DEVPARAM> {
    ,       { return Make(Token.comma); }
}

<*>{LineComment}   /* skip */

<*>{Space}+		/* skip */

%%