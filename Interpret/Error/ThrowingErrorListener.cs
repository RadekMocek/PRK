using Antlr4.Runtime;
using Antlr4.Runtime.Misc;

// https://stackoverflow.com/a/26573239
// https://stackoverflow.com/a/18486405

namespace Interpret.Error
{
    internal class ThrowingErrorListener : IAntlrErrorListener<int>, IAntlrErrorListener<IToken>
    {
        public static readonly ThrowingErrorListener instance = new();

        public void SyntaxError(TextWriter output, IRecognizer recognizer, int offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new ParseCanceledException($"Parsing error at line {line} char {charPositionInLine} :: {msg}");
        }

        public void SyntaxError(TextWriter output, IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new ParseCanceledException($"Parsing error at line {line} char {charPositionInLine} :: {msg}");
        }
    }
}
