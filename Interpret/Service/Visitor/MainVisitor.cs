using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Interpret.Error;
using System.Text;
using static System.Console;

namespace Interpret.Service.Visitor
{
    internal class MainVisitor : YappemblerBaseVisitor<object>
    {
        private readonly Dictionary<string, long> variables;
        private readonly MathVisitor mathVisitor;
        private readonly BoolVisitor boolVisitor;

        private readonly bool isTest;
        private readonly List<string> printOutputs;

        public MainVisitor(bool isTest, List<string> printOutputs)
        {
            variables = [];
            mathVisitor = new(variables);
            boolVisitor = new(variables, mathVisitor);

            this.isTest = isTest;
            this.printOutputs = printOutputs;
        }

        /// <summary>
        /// Starting rule <b>program : lines EOF ;</b>
        /// </summary>
        public override object VisitProgram([NotNull] YappemblerParser.ProgramContext context)
        {
            return Visit(context.lines());
        }

        /// <summary>
        /// Lines consist of commands
        /// </summary>
        public override object VisitLines([NotNull] YappemblerParser.LinesContext context)
        {
            foreach (var child in context.children) {
                if (child is YappemblerParser.CommandContext) {
                    Visit(child);
                }
            }
            return null;
        }

        /// <summary>
        /// Command <b>'CREATE ' ( VARID )* VARID</b>
        /// </summary>
        public override object VisitCreate_command([NotNull] YappemblerParser.Create_commandContext context)
        {
            foreach (var child in context.children) {
                if (child is ITerminalNode terminalNode && terminalNode.Symbol.Type == YappemblerParser.VARID) {
                    var varname = child.GetText();
                    if (!variables.ContainsKey(varname)) {
                        variables[varname] = 0;
                    }
                    else {
                        throw new NikolivException(context, $"Variable '{varname}' was already created.");
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Command <b>'SET ' VARID 'TO ' expr</b>
        /// </summary>
        public override object VisitSet_command([NotNull] YappemblerParser.Set_commandContext context)
        {
            var varname = context.VARID().GetText();
            if (variables.ContainsKey(varname)) {
                variables[varname] = mathVisitor.Visit(context.expr());
            } else {
                throw new NikolivException(context, $"Variable '{varname}' was not created");
            }
            return null;
        }

        /// <summary>
        /// Command <b>'SET ' VARID 'USERIN'</b>
        /// </summary>
        public override object VisitUserin_command([NotNull] YappemblerParser.Userin_commandContext context)
        {
            var varname = context.VARID().GetText();
            if (variables.ContainsKey(varname)) {
                variables[varname] = UserinTerminal.GetUserInput();
            }
            else {
                throw new NikolivException(context, $"Variable '{varname}' was not created");
            }
            return null;
        }

        /// <summary>
        /// Command <b>'PRINT ' interpol</b>
        /// </summary>
        public override object VisitPrint_command([NotNull] YappemblerParser.Print_commandContext context)
        {
            var output = EvaluateStringInterpolation(context.interpol());
            if (isTest) {
                printOutputs.Add(output);
            }
            else {
                WriteLine(output);
            }
            return null;
        }

        /// <summary>
        /// The PRINT command can be followed by any amount of alternating value expressions and strings
        /// </summary>
        private string EvaluateStringInterpolation(YappemblerParser.InterpolContext context)
        {
            StringBuilder sb = new();
            foreach (var child in context.children) {
                if (child is ITerminalNode terminalNode && terminalNode.Symbol.Type == YappemblerParser.STRING) {
                    sb.Append(child.GetText()[1..^1]);
                }
                else if (child is YappemblerParser.ExprContext) {
                    sb.Append(mathVisitor.Visit(child));
                }
            }
            var result = sb.ToString();

            if (isTest) {
                if (!(context.children.Count == 1 && context.children[0] is YappemblerParser.ExprContext)) {
                    result = $"\"{result}\"";
                }
            }

            return result;
        }

        /// <summary>
        /// Command <b>'IF ' logc NEWLINE lines ( 'ELIF ' logc NEWLINE lines )* ( 'ELSE' NEWLINE lines )? ';;'</b>
        /// </summary>
        public override object VisitIf_command([NotNull] YappemblerParser.If_commandContext context)
        {
            // IF
            if (boolVisitor.Visit(context.logc(0))) {
                Visit(context.lines(0));
                return null;
            }
            // ELIFs
            else if (context.ELIF() is not null) {
                for (int i = 1; i < context.logc().Length; i++) {
                    if (boolVisitor.Visit(context.logc(i))) {
                        Visit(context.lines(i));
                        return null;
                    }
                }
            }
            // ELSE
            if (context.ELSE() is not null) {
                Visit(context.lines().Last());
            }
            return null;
        }

        /// <summary>
        /// Command <b>'REPEAT ' expr NEWLINE lines ';;'</b>
        /// </summary>
        public override object VisitRepeat_command([NotNull] YappemblerParser.Repeat_commandContext context)
        {
            var nCycles = mathVisitor.Visit(context.expr());
            for (long i = 0; i < nCycles; i++) {
                Visit(context.lines());
            }
            return null;
        }

        /// <summary>
        /// Command <b>'UNTIL ' logc NEWLINE lines ';;'</b>
        /// </summary>
        public override object VisitUntil_command([NotNull] YappemblerParser.Until_commandContext context)
        {
            while (true) {
                if (boolVisitor.Visit(context.logc())) return null;
                Visit(context.lines());
            }
        }
    }
}
