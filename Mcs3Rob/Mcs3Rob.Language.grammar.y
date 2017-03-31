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
%token DEVPARAM, PROCONST2, PROVARI2, CHARLINE2, CHARMAP2, CHARSPACE, ROMTEXT
%token EOL
%token SCANERROR
%token TEXT
%token VARIABLELINE
%token GRADUATIONMODELLINE

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
    | PROVARI2 HeaderItem EOL HeaderItem EOL VariableSeq { Console.Error.WriteLine("Rule -> PROVARI2: {0}", string.Join(", ", $1.n)); }
    | PROCONST2 HeaderItem EOL HeaderItem EOL VariableSeq { Console.Error.WriteLine("Rule -> PROCONST2: {0}", string.Join(", ", $1.n)); }
    | CHARLINE2 HeaderItem EOL HeaderItem EOL HeaderItem EOL HeaderItem EOL HeaderItem EOL IndependantAxis ENDMARKER EOL DependantAxisItemSeq ENDMARKER EOL { Console.Error.WriteLine("Rule -> CHARLINE2: {0}", string.Join(", ", $1.n)); }
    | CHARMAP2 HeaderItem EOL HeaderItem EOL HeaderItem EOL HeaderItem EOL HeaderItem EOL IndependantAxis ENDMARKER EOL IndependantAxis ENDMARKER EOL DependantAxisItemSeq ENDMARKER EOL { Console.Error.WriteLine("Rule -> CHARMAP2: {0}", string.Join(", ", $1.n)); }
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
    | text
    ;

VariableSeq
    : VariableSeq VariableDefinition EOL
    | VariableDefinition EOL
    ;

VariableDefinition
    : VARIABLELINE
    ;

IndependantAxis
    : IndependantAxisItemSeq
    | IndependantAxisItemSeq GraduationItemSeq
    ;

IndependantAxisItemSeq
    : IndependantAxisItemSeq IndependantAxisItem EOL
    | IndependantAxisItem EOL
    ;

IndependantAxisItem
    : number
    | text
    ;

GraduationItemSeq
    : GraduationItemSeq GRADUATIONMODELLINE EOL
    | GRADUATIONMODELLINE EOL
    ;

DependantAxisItemSeq
    : DependantAxisItemSeq DependantAxisItem EOL
    | DependantAxisItem EOL
    ;

DependantAxisItem
    : number
    | text
    ;

number
    : HEXNUMBER         { Console.Error.WriteLine("Rule -> hexnumber: {0}", $1.n); }
    | NUMBER            { Console.Error.WriteLine("Rule -> number: {0}", $1.n); }
    ;

text
    : TEXT              { Console.Error.WriteLine("Rule -> text: {0}", $1.s); }
    ;

%%