using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Interpret.Error;
using Interpret.Service.Visitor;
using Interpret.Utility;
using System.Text.RegularExpressions;
using static System.Console;

namespace Interpret.Service
{
    internal static class Runner
    {
        /// <summary>
        /// Runs Yappembler program or test.
        /// </summary>
        /// <param name="program">String with the program</param>
        /// <param name="isTest">Is this program part of a Test Suite?</param>
        /// <param name="printOutputs">If isTest is true, this will be filled with program's PRINT outputs instead of printing them to the stdout.</param>
        /// <returns>True if program runtime ended successfully without any exception. False if any error occured.</returns>
        public static bool ParseAndVisit(string program, bool isTest, List<string> printOutputs)
        {
            ICharStream stream = CharStreams.fromString(program);

            YappemblerLexer lexer = new(stream);
            lexer.RemoveErrorListeners();
            lexer.AddErrorListener(ThrowingErrorListener.instance);

            ITokenStream tokens = new CommonTokenStream(lexer);

            YappemblerParser parser = new(tokens);
            parser.RemoveErrorListeners();
            parser.AddErrorListener(ThrowingErrorListener.instance);

            IParseTree tree;
            try {
                tree = parser.program();
            }
            catch (ParseCanceledException e) {
                if (isTest) {
                    printOutputs.Add(e.Message);
                }
                else {
                    WriteLine(e.Message);
                }
                return false;
            }
            
            MainVisitor visitor = new(isTest, printOutputs);

            try {
                visitor.Visit(tree);
            }
            catch (NikolivException e) {
                if (isTest) {
                    printOutputs.Add(e.Message);
                }
                else {
                    WriteLine(e.Message);
                }
                return false;
            }

            return true;
        }

        /// <summary>
        /// Runs all tests from given Yappebler Test Suite string.
        /// </summary>
        /// <param name="testSuite">Contenst of .yappt file</param>
        /// <returns>Test Suite results</returns>
        public static TestSuiteResultDTO Test(string testSuite)
        {
            const string pat = @"(-?\d+)|Exception|(""[^""]*"")";
            Regex r = new(pat);
            var testNumber = 1;
            bool isExceptionExpected;

            int nPassedTests = 0, nFailedTests = 0, nSkippedTests = 0;
            var tests = testSuite.Split("@@\n")[1..];
            foreach (var test in tests) {
                WriteLine($"=== Test {testNumber++} ===");
                var testLines = test.Split("\n");

                if (testLines.Length >= 3 && testLines[0].StartsWith("@Name:") && testLines[1].StartsWith("@Expect:") && testLines[1].Length > 8) {
                    var testName = testLines[0][6..].Trim();
                    var expectedValuesRaw = testLines[1][8..];
                    var programToTest = string.Join("\n", testLines[2..]);
                    WriteLine($"Running test with name '{testName}'...");

                    isExceptionExpected = false;
                    List<string> expectedValues = [];
                    var matches = r.Matches(expectedValuesRaw);
                    foreach (var match in matches) {
                        if (match.ToString() == "Exception") {
                            isExceptionExpected = true;
                            break;
                        }
                        expectedValues.Add(match.ToString());
                    }
                    var expectedValuesToPrint = string.Join(", ", expectedValues);
                    if (isExceptionExpected) {
                        if (expectedValuesToPrint.Length != 0) expectedValuesToPrint += ", ";
                        expectedValuesToPrint += "ERROR";
                    }
                    WriteLine("Expecting: " + expectedValuesToPrint);

                    List<string> printOutputs = [];
                    var didWeGetExpection = !ParseAndVisit(programToTest, true, printOutputs);
                    WriteLine("      Got: " + string.Join(", ", printOutputs));

                    if (didWeGetExpection) printOutputs = printOutputs[..^1];

                    if (expectedValues.SequenceEqual(printOutputs) && isExceptionExpected == didWeGetExpection) {
                        nPassedTests++;
                        WriteLine("=> Test PASSED");
                    }
                    else {
                        nFailedTests++;
                        WriteLine("=> Test FAILED");
                    }
                }
                else {
                    nSkippedTests++;
                    WriteLine("Invalid test definition. Skipping...");
                }
            }

            WriteLine($"Out of {tests.Length} tests:\n  {nPassedTests} passed\n  {nFailedTests} failed\n  {nSkippedTests} couldn't be parsed");
            return new TestSuiteResultDTO(tests.Length, nPassedTests, nFailedTests, nSkippedTests);
        }
    }
}
