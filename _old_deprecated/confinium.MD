# ⚠️ deprecated, [return home](../)

## _Confinium_ language

### Initial language idea

<details>

<summary>Expand / Collapse</summary>

Grammar rules in this section are all over the place. The main purpose of this section is to introduce the basic idea of the language and to get it out of my head ☺.

#### Program structure

```text
<program> → (" "*<command><newline>)+

<newline> → \n
```

Program consists of commands and each command is on its own line.

#### Printing and settings variables

```text
<command> → MAKE <var_id> <expr>
<command> → PRINT <expr>
<command> → PRINT_TEXT .+

<var_id> → [a-zA-Z][a-zA-Z0-9]*
```

`MAKE x 5` will assign number 5 into variable named _x_, `PRINT x` will print the variable.<br>`PRINT_TEXT x` will literally print the letter _x_.

#### Branching/looping

```text
<command> → UNTIL <comparison><newline><program>END

<comparison> → <expr> <comp_oper> <expr>

<comp_oper> → == | >= | <= | <>
```

`UNTIL x ... END` can be thought of as `while(!x) {...}` in C.

#### &lt;expr&gt;

```text
<expr> → <var_or_num> | <var_or_num><expr_oper><expr> | (<expr>) | (<expr>)<expr_oper><expr>

<var_or_num> → <var_id> | <number>

<number> → 0 | -?[1-9][0-9]*

<expr_oper> → + | - | * | / | ^ | %
```

#### Comment

```text
<command> → #.*
```

Lines beginning with a `#` are ignored.

</details>

### BNF

```ebnf
Program ::= (Space* Command Newline)+

Command ::=
      "MAKE" Space VarID Space Expr
    | "PRINT" Space Expr
    | "PRINT_TEXT" Space .+
    | "UNTIL" Space Comparison Newline Program "END"
    | "#" .*

VarID ::= [a-zA-Z][a-zA-Z0-9]*

Comparison ::= Expr Space CompOper Space Expr
CompOper ::= "==" | ">=" | "<=" | "<>"

Expr ::= VarOrNum | VarOrNum ExprOper Expr | "(" Expr ")" | "(" Expr ")" ExprOper Expr

VarOrNum ::= VarID | Number
Number ::= "0" | "-"?[1-9][0-9]*
ExprOper ::= "+" | "-" | "*" | "/" | "^" | "%"

Newline ::= "\n"
Space ::= " "
```

### Examples

```text
# Hello world
PRINT_TEXT Hello world!

# GCD (division based Euclidean algorithm)
MAKE a 127
MAKE b 721
UNTIL b == 0
  MAKE t b
  MAKE b a%b
  MAKE a t
END
PRINT_TEXT Greatest common divisor of a and b is:
PRINT a

# Condition branching
MAKE x 10
MAKE backup x
UNTIL x <> 10
  PRINT_TEXT x is equal to 10
  MAKE x 0
END
UNTIL backup == 10
  PRINT_TEXT x is not equal to 10
  MAKE backup 10
END

# Nesting loops
MAKE y 0
UNTIL y == 10
  MAKE x 0
  UNTIL x == 10
    PRINT y
    PRINT x
    PRINT_TEXT ---
    MAKE x x+1
  END
  MAKE y y+1
END
```

### Current observations

* Variables can only hold integers
* Variable names consist only of letters & numbers; they cannot start with a number
* There can be no spaces in expressions
* There must be two spaces in comparisons (one on each side of the operator)
* It is up to the programmer to decide the number of spaces before each command
