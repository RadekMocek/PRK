# Yappembler Test Suite

## Contents

* [What is Yappembler Test Suite](#what-is-yappembler-test-suite)
* [How the testing works](#how-the-testing-works)
* [Yappembler Test Suite syntax](#yappembler-test-suite-syntax)
* [Running the tests](#running-the-tests)

## Reference guide

### What is Yappembler Test Suite

Yappembler Test Suite allows you to write simple tests for Yappembler programs.

These are the differences between normal Yappembler program and a Yappembler Test Suite:

* Yappembler program...
  * Is stored in file with `.yappp` file extension
  * Is run via the `run` parameter
  * One file contains one program
  * Output of `PRINT` commands is sent to the terminal
* Yappembler Test Suite...
  * Is stored in file with `.yappt` file extension
  * Is run via the `test` or `testdir` parameter
  * One file can contain multiple tests
  * Output of `PRINT` commands is saved for comparing with expected output

### How the testing works

In the `.yappt` file, before writing the code you want to test, you must specify the name of the test and expected output values of the tested code.

The only way for Yappembler program to "output" a value is via the `PRINT` command. So if your program is just one line containing `PRINT "Testing is awesome"`, than your only expected value should be `"Testing is awesome"`. In this example we print and expect a _string_, but you can also expect _integer_ or a _error_.

```sql
PRINT 5         /* This prints a integer, so we expect 5 */
PRINT 5 + 5     /* This is also a integer, we expect 10 */

PRINT "Hi!"     /* This is a string, we expect "Hi!" */
PRINT "5"       /* This is a string, we expect "5" */
PRINT 5 "?"     /* String aswell, we expect "5?" */
PRINT 5 ""      /* Integer 5 with an empty string makes it a string, so we expect "5" again */

PRINT x         /* Runtime error because x is not created, so we expect an error */
```

As you can see from the example, there is a difference between expecting `5` and `"5"`. If the whole example is our tested program, all the expected values will be (separated by spaces):

`5 10 "Hi!" "5" "5?" "5" Exception`

Keyword `Exception` is used for expecting an error. Writing more expected values after the `Exception` does not make sense because execution of the code stops right after running into an error. If the `Exception` is the only expected value for the code that means that either there are no `PRINT` commands before the error occurs or the tested code is not correctly written and immediately results in a parsing error.

### Yappembler Test Suite syntax

One `.yappt` file can contain multiple tests. Each test definition must contain (in this order):

* `@@` (two At signs on a seperate line)
* `@Name:` followed by a name of the test. Name is shown in the terminal when tests are running and does not have to be unique. (on a seperate line)
* `@Expect:` followed by a list of expected values separated by spaces (on a seperate line)
* Code that will be tested

Example of a test file with four tests:

```sql
@@
@Name: Test one
@Expect: "some text" 4 Exception

PRINT "some text"
PRINT 2 + 2
PRINT x

@@
@Name: Test two
@Expect: 5 10 "Hi!" "5" "5?" "5" Exception

PRINT 5
PRINT 5 + 5

PRINT "Hi!"
PRINT "5"
PRINT 5 "?"
PRINT 5 ""

PRINT x

@@
@Name: Test three
@Expect: "A" "C"

CREATE x
SET x TO 10

IF x <> 5
    PRINT "A"
ELSE
    PRINT "B"
;;

PRINT "C"

@@
@Name: Test four
@Expect: Exception

PRYNT "Hi!"
```

### Running the tests

To run a single `.yappt` file:

```ps1
.\Interpret.exe test <path_to_yappt_file>
```

To run all `.yappt` files inside a directory (not counting nested directories):

```ps1
.\Interpret.exe testdir <path_to_directory>
```
