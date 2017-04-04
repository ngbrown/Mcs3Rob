%namespace Mcs3Rob
%partial
%parsertype Mcs3RobParser
%visibility internal
%tokentype Token

%union { 
			public long n;
			public string s;

            public IAst ast;
            public AstSeq astseq;
	   }

%start main

%token ENDMARKER
%token DEVPARAM, PROCONST2, PROVARI2, CHARLINE2, CHARMAP2, CHARSPACE, ROMTEXT
%token EOL
%token SCANERROR
%token <n> NUMBER
%token <n> HEXNUMBER
%token <s> TEXT
%token <s> VARIABLELINE
%token <s> GRADUATIONMODELLINE

%token comma ","

%type <ast> number text HeaderItem IndependantAxisItem DependantAxisItem DescriptionBlock
%type <ast> VariableDefinition IndependantAxis

%type <astseq> HeaderItemSeq HeaderSeq FileHeader DescriptionBlockSeq VariableSeq
%type <astseq> IndependantAxisItemSeq DependantAxisItemSeq GraduationItemSeq

%%

main
    : FileHeader DescriptionBlockSeq EOF { this.AstFile = new AstFile(@1, $1, $2); }
    ;

DescriptionBlockSeq
    : DescriptionBlock                      { $$ = new AstSeq(@1, $1); }
    | DescriptionBlockSeq DescriptionBlock  { $$ = $1.Append($2); }
    ;

FileHeader
    : HeaderSeq ENDMARKER EOL               { $$ = $1; }
    ;

DescriptionBlock
    : DEVPARAM HeaderSeq ENDMARKER EOL 
        { $$ = new AstDescriptionBlock(@1, "DEVPARAM", $2); }

    | PROVARI2 HeaderItem EOL HeaderItem EOL VariableSeq
        { $$ = new AstDescriptionBlock(@1, "PROVARI2", new AstSeq(@2, $2, $4), $6); }

    | PROCONST2 HeaderItem EOL HeaderItem EOL VariableSeq
        { $$ = new AstDescriptionBlock(@1, "PROCONST2", new AstSeq(@2, $2, $4), $6); }

    | CHARLINE2 HeaderItem EOL HeaderItem EOL HeaderItem EOL HeaderItem EOL HeaderItem EOL 
        IndependantAxis ENDMARKER EOL DependantAxisItemSeq ENDMARKER EOL 
        { $$ = new AstCharacteristicMapBlock(@1, "CHARLINE2", new AstSeq(@2, $2, $4, $6, $8, $10), $15, $12); }

    | CHARMAP2 HeaderItem EOL HeaderItem EOL HeaderItem EOL HeaderItem EOL HeaderItem EOL 
        IndependantAxis ENDMARKER EOL IndependantAxis ENDMARKER EOL DependantAxisItemSeq ENDMARKER EOL 
        { $$ = new AstCharacteristicMapBlock(@1, "CHARMAP2", new AstSeq(@2, $2, $4, $6, $8, $10), $18, $12, $15); }
    ;

HeaderSeq
    : HeaderItemSeq EOL                     { $$ = new AstSeq(@1, $1); }
    | HeaderSeq HeaderItemSeq EOL           { $$ = $1.Append($2); }
    ;

HeaderItemSeq
    : HeaderItem                            { $$ = new AstSeq(@1, $1); }
    | HeaderItemSeq "," HeaderItem          { $$ = $1.Append($3); }
    ;

HeaderItem
    : number                                { $$ = $1; }
    | text                                  { $$ = $1; }
    ;

VariableSeq
    : VariableDefinition EOL                { $$ = new AstSeq(@1, $1); }
    | VariableSeq VariableDefinition EOL    { $$ = $1.Append($2); }
    ;

VariableDefinition
    : VARIABLELINE                          { $$ = new AstVariableLine(@1, $1); }
    ;

IndependantAxis
    : IndependantAxisItemSeq                        { $$ = new AstIndependantAxis(@1, $1); }
    | IndependantAxisItemSeq GraduationItemSeq      { $$ = new AstIndependantAxis(@1, $1, $2); }
    ;

IndependantAxisItemSeq
    : IndependantAxisItem EOL                           { $$ = new AstSeq(@1, $1); }
    | IndependantAxisItemSeq IndependantAxisItem EOL    { $$ = $1.Append($2); }
    ;

IndependantAxisItem
    : number                                { $$ = $1; }
    | text                                  { $$ = $1; }
    ;

GraduationItemSeq
    : GRADUATIONMODELLINE EOL                       { $$ = new AstGraduationSeq(@1, $1); }
    | GraduationItemSeq GRADUATIONMODELLINE EOL     { $$ = ((AstGraduationSeq)$1).Append($2); }
    ;

DependantAxisItemSeq
    : DependantAxisItem EOL                         { $$ = new AstSeq(@1, $1); }
    | DependantAxisItemSeq DependantAxisItem EOL    { $$ = $1.Append($2); }
    ;

DependantAxisItem
    : number                                { $$ = $1; }
    | text                                  { $$ = $1; }
    ;

text
    : TEXT                                  { $$ = new AstText(@1, $1); }
    ;

number
    : HEXNUMBER                             { $$ = new AstInteger(@1, $1, true); }
    | NUMBER                                { $$ = new AstInteger(@1, $1); }
    ;

%%