%namespace Mcs3Rob
%partial
%parsertype Mcs3RobParser
%visibility internal
%tokentype Token

%union { 
			public int n; 
			public string s; 
	   }

%start main

%token NUMBER
%token HEXNUMBER
%token LINECOMMENT
%token SPACE
%token ENDMARKER
%token DEVPARAM, PROCONST2, PROVARI2, CHARLINE2, CAHRMAP2, CHARSPACE, ROMTEXT
%token EOL
%token SCANERROR

%token comma ","

%%

main
    : file_header DescriptionSeq
    ;

DescriptionSeq
    : DescriptionSeq DescriptionBlock
    | DescriptionBlock
    ;

DescriptionBlock
    : DevparamBlock
    | error                         { ParseError(0, @1); }
    ;

file_header
    : HeaderSeq ENDMARKER EOL { Console.Error.WriteLine("Rule -> file_header: {0}", string.Join(", ", $1.n)); }
    ;

DevparamBlock
    : DEVPARAM HeaderSeq ENDMARKER EOL { Console.Error.WriteLine("Rule -> devparam: {0}", string.Join(", ", $1.n)); }
    ;

HeaderSeq
    : HeaderSeq HeaderItemSeq EOL
    | HeaderItemSeq EOL
    ;

HeaderItemSeq
    : HeaderItemSeq comma HeaderItem
    | HeaderItem
    ;

HeaderItem
    : number
    ;

number
    : HEXNUMBER         { Console.Error.WriteLine("Rule -> hexnumber: {0}", $1.n); }
    | NUMBER            { Console.Error.WriteLine("Rule -> number: {0}", $1.n); }
    ;

%%