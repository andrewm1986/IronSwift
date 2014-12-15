using Fare;

namespace IronSwift.Compiler.Lexer.Tokens
{
    public abstract class Token
    {
        public string LiteralText { get; protected set; }
        public abstract string RegularExpression { get; }
        public abstract int Priority { get; }

        private Automaton automaton;
        public Automaton Automaton => automaton ?? (automaton = new RegExp(RegularExpression).ToAutomaton());

        public abstract Token FromText(string literalText);
    }

    public class VarKeywordToken : Token
    {
        public override string RegularExpression => "var";

        public override int Priority { get; } = 0;
        public override Token FromText(string literalText)
        {
            return new VarKeywordToken { LiteralText = literalText };
        }
    }

    public class OpenParenthesisToken : Token
    {
        public override string RegularExpression => "\\(";

        public override int Priority { get; } = 0;

        public override Token FromText(string literalText)
        {
            return new OpenParenthesisToken { LiteralText = literalText };
        }
    }

    public class CloseParenthesisToken : Token
    {
        public override string RegularExpression => "\\)";

        public override int Priority { get; } = 0;

        public override Token FromText(string literalText)
        {
            return new CloseParenthesisToken { LiteralText = literalText };
        }
    }

    public class CommaToken : Token
    {
        public override string RegularExpression => ",";

        public override int Priority { get; } = 0;

        public override Token FromText(string literalText)
        {
            return new CommaToken { LiteralText = literalText };
        }
    }

    public class LetKeywordToken : Token
    {
        public override string RegularExpression => "let";

        public override int Priority { get; } = 0;
        public override Token FromText(string literalText)
        {
            return new LetKeywordToken {LiteralText = literalText};
        }
    }

    public class IdentifierToken : Token
    {
        public override string RegularExpression => "[a-zA-Z][a-zA-Z0-9]*";

        public override int Priority { get; } = 1;
        public override Token FromText(string literalText)
        {
            return new IdentifierToken { LiteralText = literalText };
        }
    }

    public class WhitespaceToken : Token
    {
        public override string RegularExpression => "[ \t\r\n]+";

        public override int Priority { get; } = 0;
        public override Token FromText(string literalText)
        {
            return new WhitespaceToken { LiteralText = literalText };
        }
    }

    public class SingleEqualsToken : Token
    {
        public override string RegularExpression => "=";

        public override int Priority { get; } = 0;
        public override Token FromText(string literalText)
        {
            return new SingleEqualsToken { LiteralText = literalText };
        }
    }

    public class ColonKeywordToken : Token
    {
        public override string RegularExpression => ":";

        public override int Priority { get; } = 0;
        public override Token FromText(string literalText)
        { 
            return new ColonKeywordToken { LiteralText = literalText };
        }
    }

    public class DecimalLiteralToken : Token
    {
        public override string RegularExpression => "[0-9]+(\\.[0-9]+)?";

        public override int Priority { get; } = 0;
        public override Token FromText(string literalText)
        {
            return new DecimalLiteralToken { LiteralText = literalText };
        }
    }
}