grammar yappembler;


/*----*/
/* GR */
/*----*/

program : ( command NEWLINE | NEWLINE )+ ;

command        
        : 'CREATE ' VARID
        | 'SET ' VARID 'TO ' expr
;

expr
        : expr expr_oper expr
        | '(' expr ')'
        | INTEGER
        | VARID
;

expr_oper
        : '+' | '-' | '*' | '/' | '^' | '%'
;

/*----*/
/* LX */
/*----*/

NEWLINE : '\r'? '\n' ;

VARID : [a-z] [a-zA-Z0-9_\-]* ;

INTEGER : '0' | '-'? [1-9] [0-9]* ;

//SPACE : ' ' ;

WS : [ \t]+ -> skip ;
