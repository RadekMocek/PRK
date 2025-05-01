using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Interpret.Error;

namespace Interpret.Service.Visitor
{
    internal class MathVisitor(Dictionary<string, long> variables) : YappemblerBaseVisitor<long>
    {
        private readonly Dictionary<string, long> variables = variables;

        /// <summary>
        /// <b>expr<br/>: expr_mul<br/>| expr ( '+' | '-' ) expr_mul<br/>;</b>
        /// </summary>
        public override long VisitExpr([NotNull] YappemblerParser.ExprContext context)
        {
            if (context.children.Count == 1) {
                return Visit(context.expr_mul());
            }
            else {
                var left = Visit(context.expr());
                var right = Visit(context.expr_mul());
                var ope = context.GetChild(1);

                if (ope is ITerminalNode terminalNode) {
                    if (terminalNode.Symbol.Type == YappemblerParser.OPE_MAT_PLUS) {
                        return left + right;
                    }
                    else if (terminalNode.Symbol.Type == YappemblerParser.OPE_MAT_MINUS) {
                        return left - right;
                    }
                }
                // This should never happen because invalid operator will be detected by grammar parser
                throw new NikolivException(context, $"Invalid operator '{ope}'. Allowed mathematical operators are: '+', '-', '*', '/', '%', '^'.");
            }
        }

        /// <summary>
        /// <b>expr_mul<br/>: expr_pow<br/>| expr_mul ( '*' | '/' | '%' ) expr_pow<br/>;</b>
        /// </summary>
        public override long VisitExpr_mul([NotNull] YappemblerParser.Expr_mulContext context)
        {
            if (context.children.Count == 1) {
                return Visit(context.expr_pow());
            }
            else {
                var left = Visit(context.expr_mul());
                var right = Visit(context.expr_pow());
                var ope = context.GetChild(1);

                if (ope is ITerminalNode terminalNode) {
                    if (terminalNode.Symbol.Type == YappemblerParser.OPE_MAT_STAR) {
                        return left * right;
                    }
                    else if (terminalNode.Symbol.Type == YappemblerParser.OPE_MAT_SLASH) {
                        return left / right;
                    }
                    else if (terminalNode.Symbol.Type == YappemblerParser.OPE_MAT_PERCENT) {
                        return left % right;
                    }
                }
                // This should never happen because invalid operator will be detected by grammar parser
                throw new NikolivException(context, $"Invalid operator '{ope}'. Allowed mathematical operators are: '+', '-', '*', '/', '%', '^'.");
            }
        }

        /// <summary>
        /// <b>expr_pow<br/>: expr_item<br/>| expr_item '^' expr_pow<br/>;</b>
        /// </summary>
        public override long VisitExpr_pow([NotNull] YappemblerParser.Expr_powContext context)
        {
            if (context.children.Count == 1) {
                return Visit(context.expr_item());
            }
            else {
                var left = Visit(context.expr_item());
                var right = Visit(context.expr_pow());
                return (long)Math.Pow(left, right);
            }
        }

        /// <summary>
        /// <b>expr_item<br/>: '(' expr ')'<br/>| INTEGER<br/>| VARID<br/>| '-' expr_item<br/>;</b>
        /// </summary>
        public override long VisitExpr_item([NotNull] YappemblerParser.Expr_itemContext context)
        {
            if (context.OPE_MAT_MINUS() is not null) {
                return -Visit(context.expr_item());
            }
            else if (context.INTEGER() is not null) {
                return long.Parse(context.INTEGER().GetText());
            }
            else if (context.VARID() is not null) {
                var varname = context.VARID().GetText();
                if (variables.TryGetValue(varname, out long value)) {
                    return value;
                }
                throw new NikolivException(context, $"Variable '{varname}' was not created");
            }
            else if (context.expr() is not null) {
                return Visit(context.expr());
            }

            throw new NikolivException(context, "Unexpected mathematical expression item");
        }
    }
}
