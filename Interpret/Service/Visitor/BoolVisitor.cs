using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Interpret.Error;

namespace Interpret.Service.Visitor
{
    internal class BoolVisitor(Dictionary<string, long> variables, MathVisitor mathVisitor) : YappemblerBaseVisitor<bool>
    {
        private readonly Dictionary<string, long> variables = variables;
        private readonly MathVisitor mathVisitor = mathVisitor;

        /// <summary>
        /// <b>logc<br/>: logc_and<br/>| logc OR logc_and<br/>;</b>
        /// </summary>
        public override bool VisitLogc([NotNull] YappemblerParser.LogcContext context)
        {
            if (context.children.Count == 1) {
                return Visit(context.logc_and());
            }
            else {
                var left = Visit(context.logc());
                var right = Visit(context.logc_and());
                return left || right;
            }
        }

        /// <summary>
        /// <b>logc_and<br/>: logc_item<br/>| logc_and AND logc_item<br/>;</b>
        /// </summary>
        public override bool VisitLogc_and([NotNull] YappemblerParser.Logc_andContext context)
        {
            if (context.children.Count == 1) {
                return Visit(context.logc_item());
            }
            else {
                var left = Visit(context.logc_and());
                var right = Visit(context.logc_item());
                return left && right;
            }
        }

        /// <summary>
        /// <b>logc_item<br/>: comp<br/>| '(' logc ')'<br/>| '!(' logc ')'<br/>;</b>
        /// </summary>
        public override bool VisitLogc_item([NotNull] YappemblerParser.Logc_itemContext context)
        {
            if (context.comp() is not null) {
                return Visit(context.comp());
            }
            else if (context.logc() is not null) {
                if (context.OPE_BRK_OPEN() is not null) {
                    return Visit(context.logc());
                }
                else if (context.OPE_BRK_OPEN_NEG() is not null) {
                    return !Visit(context.logc());
                }
            }

            throw new NikolivException(context, "Unexpected logical expression item");
        }

        /// <summary>
        /// <b>comp : expr comp_oper expr ;</b>
        /// </summary>
        public override bool VisitComp([NotNull] YappemblerParser.CompContext context)
        {
            var left = mathVisitor.Visit(context.expr(0));
            var right = mathVisitor.Visit(context.expr(1));
            var ope = context.GetChild(1).GetChild(0); // comp_oper : '==' | '>=' | '<=' | '<>' | '>' | '<' ;

            if (ope is ITerminalNode terminalNode) {
                return terminalNode.Symbol.Type switch {
                    YappemblerParser.OPE_LOG_EQ => left == right,
                    YappemblerParser.OPE_LOG_GE => left >= right,
                    YappemblerParser.OPE_LOG_LE => left <= right,
                    YappemblerParser.OPE_LOG_NEQ => left != right,
                    YappemblerParser.OPE_LOG_GT => left > right,
                    YappemblerParser.OPE_LOG_LT => left < right,
                    _ => throw new NikolivException(context, "Invalid comparison operator. Allowed comparison operators are: '==', '=>', '<=', '<>', '>', '<'.")
                };
            }

            throw new NikolivException(context, "Invalid comparison operator. Allowed comparison operators are: '==', '=>', '<=', '<>', '>', '<'.");
        }
    }
}
