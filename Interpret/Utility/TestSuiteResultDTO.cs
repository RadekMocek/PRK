namespace Interpret.Utility
{
    internal record struct TestSuiteResultDTO(
        int NTests,
        int NPassedTests,
        int NFailedTests,
        int NSkippedTests
    );
}
