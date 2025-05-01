using Interpret.Service;
using Interpret.Utility;
using static System.Console;

namespace Interpret
{
    internal class Program
    {
        /// <summary>
        /// Parses given command line arguments and runs program/test(s) accordingly.
        /// </summary>
        /// <param name="args">Command line arguments</param>
        static void Main(string[] args)
        {
            const string argTip = "Try one of these:\n  ./Interpret run <yappp_file_name>\n  ./Interpret test <yappt_file_name>\n  ./Interpret testdir <directory_with_yappt_files>";

            var nArgs = args.Length;

            if (nArgs == 0) {
                WriteLine("No arguments provided.\n" + argTip);
            }
            else if (nArgs == 2) {
                var what = args[0];
                var where = args[1];

                if (what == "run" || what == "test") {
                    var contents = FileReader.ReadFile(where);

                    if (contents is null) {
                        WriteLine($"File '{where}' does not exist.");
                        return;
                    }

                    if (what == "run") {
                        Runner.ParseAndVisit(contents, false, null);
                    }
                    else {
                        Runner.Test(contents);
                    }
                }
                else if (what == "testdir") {
                    var files = FileReader.GetDirectoryTestFileCollection(where);

                    if (files is null) {
                        WriteLine($"Directory '{where}' does not exist.");
                        return;
                    }

                    WriteLine($"Found {files.Length} .yappt files...");
                    int nTests = 0, nPassedTests = 0, nFailedTests = 0, nSkippedTests = 0;
                    foreach (var file in files) {
                        WriteLine($"==== {file} ====");
                        var contents = FileReader.ReadFile(file);
                        if (contents is not null) {
                            var testSuiteResult = Runner.Test(contents);
                            nTests += testSuiteResult.NTests;
                            nPassedTests += testSuiteResult.NPassedTests;
                            nFailedTests += testSuiteResult.NFailedTests;
                            nSkippedTests += testSuiteResult.NSkippedTests;
                        }
                    }

                    WriteLine($"\n==== FINAL RESULT ====\nOut of {nTests} tests in {files.Length} files:\n  {nPassedTests} passed\n  {nFailedTests} failed\n  {nSkippedTests} couldn't be parsed\n");
                }
                else {
                    WriteLine($"Invalid argument '{what}'.\n{argTip}");
                    return;
                }
            }
        }
    }
}
