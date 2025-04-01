# Yappembler

## Examples of code

### GCD – division based Euclidean algorithm

```sql
CREATE a b t og_a og_b

SET a TO 127
SET b TO 721
SET og_a TO a
SET og_b TO b

UNTIL b == 0
    SET t TO b
    SET b TO a % b
    SET a TO t
;;

PRINT "Greatest common divisor of " og_a " and " og_b " is " a

```

### Nesting loops

```sql
CREATE x y mx_size
SET mx_size TO 5
SET y TO 0

UNTIL !(y <= mx_size - 1)
    SET x TO 0
    REPEAT mx_size
        PRINT "(" y ", " x ")"
        SET x TO x + 1
        IF x == 4
            PRINT "Last inner iteration"
        ;;
    ;;
    SET y TO y + 1
;;

```

<!--
```sql

```
-->
