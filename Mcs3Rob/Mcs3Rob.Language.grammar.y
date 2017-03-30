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
    : DEVPARAM number comma number 
    | error
    ;

file_header
    : number number number number number number number number number ENDMARKER { Console.Error.WriteLine("Rule -> file_header: {0}", string.Join(", ", $1.n, $2.n, $3.n, $4.n, $5.n, $6.n, $7.n, $8.n, $9.n)); }
    ;

number
    : HEXNUMBER         { Console.Error.WriteLine("Rule -> hexnumber: {0}", $1.n); }
    | NUMBER            { Console.Error.WriteLine("Rule -> number: {0}", $1.n); }
    ;

%%