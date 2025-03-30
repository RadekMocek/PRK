# Yappembler

## Reference

### Hello world – commenting code and printing text

```sql
/* This is a comment! We are using the PRINT command on the line below: */
PRINT "Hello world!"

/*
Multi-line comments
are supported.
*/

```

Element that starts and ends with double quotation marks, with any number of any characters in between, is called _string_. Backslash `\` serves as a escape character, so `PRINT "Hello \"World\"!"` command will print `Hello "World"!`.

### Program structure and whitespace behaviour

Program in Yappembler consists of commands. Each command is on its own line ⇒ there must be a newline character after every command.

```sql
/* Correct */
PRINT "First command"
PRINT "Second command"

/* Wrong */
PRINT "First command" PRINT "Second command"

```

Every command is some combination of keywords, operators, expressions and identifiers. These elements in command are separated by space. It doesn't matter how many spaces are there between elements, but there has to be atleast one.

```sql
/* Correct */
PRINT "OK"
PRINT   "OK"
    PRINT "OK"
        PRINT       "OK"

/* Wrong */
PRINT"NOK"

```

### Variables

Variables allow us to store integers.

#### Creating variables and variable identifier

Variable names must start with a lowercase ASCII letter. This first character may be followed by arbitrary amount of: any lowercase or uppercase ASCII letter, any digit, a underscore.

To create a variable we use the CREATE command. Multiple variables can be created with one CREATE command, their identifiers are divided with spaces.

Every variable must be created before we can write to it / read from it. Variable scope is global.

```sql
/* Correct */
CREATE varA
CREATE varB varC
    CREATE  varD  varE      varF
CREATE a123___

/* Wrong */
CREATE VAR
CREATE Var
CREATE _var
CREATE 0var
CREATE řeřicha
CREATEvarA varB

```

#### Setting variables

After we create a variable, we can set its value via the SET command.

```sql
/* Correct */
SET varA TO 0
SET     varB    TO      2000

/* Wrong */
SETvarA TO 0
SET varATO 0
SET varA TO0
SET varATO0
SETvarA TO0
SET varA TO0
SET 0 TO 1
SET x TO "abc"

```

#### Setting variables from user input

If `TO <number>` part in the SET command is replaced with `USERIN`, program will try to parse user's terminal input into a number and set it to given variable.

```sql
/* Correct */
SET varA USERIN
SET     varB    USERIN

/* Wrong */
SET varA TO USERIN

```

#### Printing variables

```sql
PRINT varA

```

### Value expressions

Value expressions consist of constant integers (`'0' | '-'? [1-9] [0-9]*`) and/or variable identificators, which must be separated with mathematical operators (`+`, `-`, `*`, `/`, `%`, `^`). These operators are binary, unary minus operator `-` is also allowed. Brackets (`(` and `)`) can be used to specify operation priority.

* Value expressions can be used in:
  * The `SET` command
  * The `PRINT` command
  * Logical expressions
  * The `REPEAT` command
* Operation priority:
  1. `()`, `-` — brackets and unary minus
  2. `^` — exponentiation
  3. `*`, `/`, `%` — multiply, divide, modulo
  4. `+`, `-` — add, subtract

```sql
/* Correct */
SET x TO x+1
SET x TO    x  +   1
PRINT a+a*a
PRINT a+(a*a)
PRINT (a * a / (-a)) ^ (a % a)
PRINT -a
PRINT a - - - - - - - - a
PRINT a+a*-a
PRINT a+a*(-a)
PRINT a+a*-(a)

/* Wrong */
PRINT (a+a)*
PRINT (a*a+(a))+a*a)
PRINT a+
PRINT *a
PRINT +a
PRINT a+*
PRINT a+(a+a
PRINT )a*a)
PRINT a++a
PRINT (a+
PRINT a)*a
PRINT a + + + a

```

From now on, variable identifiers and integer constants will also be adressed as value expressions.

### String interpolation

The `PRINT` command can be followed by any amount of value expressions and strings. They can be separated by spaces, but it's not mandatory.

```sql
/* Correct */
PRINT "The value of x is " x " and the value of y is not " (y * 2) ^ 3 "."
PRINT "The value of x is "   x   " and the value of y is not "   (y * 2) ^ 3   "."
PRINT "The value of x is "x" and the value of y is not "(y * 2) ^ 3"."
PRINT "The value of negative x is " -x
PRINT """"""

/* Wrong */
PRINT "The value of x is ", x, "."
PRINT "The value of x is " + x + "."
PRINT "The value of x is " x " thanks for your attention
PRINT """""

```

String interpolation in Yappembler can get wild because you can write multiple value expressions without string between them.

```sql
/* Correct */
PRINT x+1 x+2 x+3       /*This is evaluated as three separate expressions: x+1, x+2, x+3*/
PRINT x+1x+2x+3         /*Same here*/
PRINT x + 1 x + 2x + 3  /*Same here*/
PRINT x+1 "" x+2 "" x+3 /*Expressions can be separated by empty strings to improve readability*/
PRINT x+1+  x+2         /*This is evaluated as a single expression: x+1+x+2*/

/* Wrong */
PRINT x+1+ "" x+2
PRINT ("")
```

### Logical expressions

Logical expressions consists of atleast one _comparison_. ...

### Condition branching

### Loops

## List of keywords and operators

```sql
/* Keywords */
CREATE
PRINT
SET
TO
USERIN

/* Strings */
""
\

/* Value expressions */
+
-
*
/
%
^
()

/* Logical expressions */
OR
AND
()
!()
==
<>
>=
>
<=
<

/* Code blocks */
;;
```

<!--
```sql
/* Correct */

/* Wrong */

```
-->
