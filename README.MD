# DIY Compiler

## Initial language idea: _Confinium_

Grammar rules in this section are all over the place. The main purpose of this section is to introduce the basic idea of the language and to get it out of my head ☺.

### Program structure

```text
<program> → (<command><newline>)+

<newline> → \n
```

Program consists of commands and each command is on its own line.

### Printing and settings variables

```text
<command> → MAKE <var_id> <expr>
<command> → PRINT <expr>
<command> → PRINT TEXT .*

<var_id> → [a-zA-Z]+
```

`MAKE x 5` will assign number 5 into variable named _x_, `PRINT x` will print the variable.<br>`PRINT TEXT x` will literally print the letter _x_.

### Branching/looping

```text
<command> → UNTIL <comparison><newline><nested_program>END

<nested_program> → (  <command><newline>)+
                    ^^
                    two spaces

<comparison> → <expr> <comp_oper> <expr>

<comp_oper> → == | >= | <= | <>
```

* There must be spaces around `<comp_oper>`, one on each side.
  * Correct: `UNTIL a == b`
  * Incorrect: `UNTIL a==b` or `UNTIL a   == b`
* `UNTIL x ... END` can be thought of as `while(!x) {...}` in C.

### &lt;expr&gt;

```text
<expr> → <var_or_num> | <var_or_num><expr_oper><expr>

<var_or_num> → <var_id> | <number>

<number> → [0-9]+

<expr_oper> → + | - | * | / | ^ | %
```

* There cannot be spaces around `<expr_oper>` ⇒ expressions cannot contain spaces.
  * Correct: `MAKE a b+c`
  * Incorrect: `MAKE a b + c`

### Comment

```text
<command> → #.*
```

Lines beginning with a `#` are ignored.

### Examples

```text
# Hello world
PRINT TEXT Hello world!

# GCD (division based Euclidean algorithm)
MAKE a 127
MAKE b 721
UNTIL b == 0
  MAKE t b
  MAKE b a%b
  MAKE a t
END
PRINT TEXT Greatest common divisor of a and b is:
PRINT a

# Condition branching
MAKE x 10
MAKE backup x
UNTIL x <> 10
  PRINT TEXT x is equal to 10
  MAKE x 0
END
UNTIL backup == 10
  PRINT TEXT x is not equal to 10
  MAKE backup 10
END

# Nesting loops
MAKE y 0
UNTIL y == 10
  MAKE x 0
  UNTIL x == 10
    PRINT y
    PRINT x
    PRINT TEXT ---
    MAKE x x+1
  END
  MAKE y y+1
END
```

### Current observations

* Variables can hold only integers
* Negative integer can be written as `0-x`, eg negative nine is `0-9`
* Variable names consist only of letters
