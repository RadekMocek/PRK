grammar yappembler;

program
        : lines EOF
;

lines
        : ( command NEWLINE | NEWLINE )*
;


command
        : 'CREATE ' ( VARID )* VARID
        | 'SET ' VARID 'TO ' expr
        | 'SET ' VARID 'USERIN'
        | 'PRINT ' interpol
        | 'IF ' logc NEWLINE lines ( 'ELIF ' logc NEWLINE lines )* ( 'ELSE' NEWLINE lines )? ';;'
        | 'REPEAT ' expr NEWLINE lines ';;'
        | 'UNTIL ' logc NEWLINE lines ';;'
;

interpol
        : STRING ( expr STRING )*
        | STRING ( expr STRING )* expr
        | expr ( STRING expr )*
        | expr ( STRING expr )* STRING
;

expr
        : expr_mul
        | expr ( '+' | '-' ) expr_mul
;

expr_mul
        : expr_pow
        | expr_mul ( '*' | '/' | '%' ) expr_pow
;

expr_pow
        : expr_item
        | expr_item '^' expr_pow
;

expr_item
        : '(' expr ')'
        | INTEGER
        | VARID
        | '-' expr_item
;

logc
        : logc_and
        | logc OR logc_and
;

logc_and
        : logc_item
        | logc_and AND logc_item
;

logc_item
        : comp
        | '(' logc ')'
        | '!(' logc ')'
;

comp
        : expr comp_oper expr
;

comp_oper
        : '==' | '>=' | '<=' | '<>' | '>' | '<'
;

NEWLINE : '\r'? '\n' ;

VARID : [a-z] [a-zA-Z0-9_]* ;

INTEGER : '0' | [1-9] [0-9]* ;

STRING : '"' ( ~["\r\n] | '\\' . )* '"' ;

WS_SKIP : [ \t]+ -> skip ;

COMMENT_SKIP : '/*' .*? '*/' -> skip ;

fragment WS : [ \t]+ ;
OR : WS 'OR' WS ;
AND : WS 'AND' WS ;
