grammar Yappembler;

program
        : lines EOF
;

lines
        : ( command NEWLINE | NEWLINE )*
;

command
        : create_command
        | set_command
        | userin_command
        | print_command
        | if_command
        | repeat_command
        | until_command
;

create_command : 'CREATE ' ( VARID )* VARID ;
set_command : 'SET ' VARID 'TO ' expr ;
userin_command : 'SET ' VARID 'USERIN' ;
print_command : 'PRINT ' interpol ;
if_command : 'IF ' logc NEWLINE lines ( 'ELIF ' logc NEWLINE lines )* ( 'ELSE' NEWLINE lines )? ';;' ;
repeat_command : 'REPEAT ' expr NEWLINE lines ';;' ;
until_command : 'UNTIL ' logc NEWLINE lines ';;' ;

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

/* ------ */

CREATE : 'CREATE ' ;
SET : 'SET ' ;
TO : 'TO ' ;
USERIN : 'USERIN' ;
PRINT : 'PRINT ' ;
IF : 'IF ' ;
ELIF : 'ELIF ' ;
ELSE : 'ELSE' ;
END : ';;' ;
REPEAT : 'REPEAT ' ;
UNTIL : 'UNTIL ' ;
OPE_MAT_PLUS : '+' ;
OPE_MAT_MINUS : '-' ;
OPE_MAT_STAR : '*' ;
OPE_MAT_SLASH : '/' ;
OPE_MAT_PERCENT : '%' ;
OPE_MAT_HAT : '^' ;
OPE_BRK_OPEN : '(' ;
OPE_BRK_CLOSE : ')' ;
OPE_BRK_OPEN_NEG : '!(' ;
OPE_LOG_EQ : '==' ;
OPE_LOG_GE : '>=' ;
OPE_LOG_LE : '<=' ;
OPE_LOG_NEQ : '<>' ;
OPE_LOG_GT : '>' ;
OPE_LOG_LT : '<' ;
