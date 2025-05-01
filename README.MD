# _Yappembler_ language

```sql
/* 3...↲ 2...↲ 1...↲ Hello world! */
CREATE i
SET i TO 3
UNTIL i == 0
    PRINT i "..."
    SET i TO i - 1
;;
PRINT "Hello world!"
```

&nbsp;

* [Yappembler language specification](./docs/reference.MD)
* [Yappembler Test Suite specification] ⚠WIP

&nbsp;

* [__How to build & run on Linux__] ⚠WIP

&nbsp;

* [Examples of code](./docs/examples.MD)
* [ANTLR grammar](./ANTLR/Yappembler.g4)
* [C# interpreter & test suite runner](./Interpret/)
* [VSCode extension for syntax highlighting](./Highlighter/yappembler/)
