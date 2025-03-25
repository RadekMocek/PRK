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

### String interpolation

### Condition branching

### Loops

## List of keywords and operators

```sql
CREATE
PRINT
SET
TO
USERIN
```

<!--
```sql
/* Correct */

/* Wrong */

```
-->
