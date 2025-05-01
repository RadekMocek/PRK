using Antlr4.Runtime;

namespace Interpret.Error
{
    [Serializable]
    internal class NikolivException(ParserRuleContext ctx, string message)
        : Exception($"Runtime error at line {ctx.Start.Line} :: {message}")
    {
    }
}
