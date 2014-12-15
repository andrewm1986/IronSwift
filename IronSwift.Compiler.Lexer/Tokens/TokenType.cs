using Fare;

namespace IronSwift.Compiler.Lexer.Tokens
{
    public abstract class TokenType
    {
        public abstract string RegularExpression { get; }
        public abstract int Priority { get; }

        private Automaton automaton;
        public Automaton Automaton => automaton ?? (automaton = new RegExp(RegularExpression).ToAutomaton());

        public abstract Token FromText(string literalText);
    }

    public class VarKeywordTokenType : TokenType
    {
        public override string RegularExpression => "var";

        public override int Priority { get; } = 0;
        public override Token FromText(string literalText)
        {
            return new VarKeywordToken(literalText);
        }
    }

    public class OpenParenthesisTokenType : TokenType
    {
        public override string RegularExpression => "\\(";

        public override int Priority { get; } = 0;

        public override Token FromText(string literalText)
        {
            return new OpenParenthesisToken(literalText);
        }
    }

    public class CloseParenthesisTokenType : TokenType
    {
        public override string RegularExpression => "\\)";

        public override int Priority { get; } = 0;

        public override Token FromText(string literalText)
        {
            return new CloseParenthesisToken(literalText);
        }
    }

    public class CommaTokenType : TokenType
    {
        public override string RegularExpression => ",";

        public override int Priority { get; } = 0;

        public override Token FromText(string literalText)
        {
            return new CommaToken(literalText);
        }
    }

    public class LetKeywordTokenType : TokenType
    {
        public override string RegularExpression => "let";

        public override int Priority { get; } = 0;
        public override Token FromText(string literalText)
        {
            return new LetKeywordToken(literalText);
        }
    }

    public class IdentifierTokenType : TokenType
    {
        public override string RegularExpression => "[a-zA-Z][a-zA-Z0-9]*";

        public override int Priority { get; } = 1;
        public override Token FromText(string literalText)
        {
            return new IdentifierToken(literalText);
        }
    }

    public class WhitespaceTokenType : TokenType
    {
        public override string RegularExpression => "[ \t\r\n]+";

        public override int Priority { get; } = 0;
        public override Token FromText(string literalText)
        {
            return new WhitespaceToken(literalText);
        }
    }

    public class SingleEqualsTokenType : TokenType
    {
        public override string RegularExpression => "=";

        public override int Priority { get; } = 0;
        public override Token FromText(string literalText)
        {
            return new SingleEqualsToken(literalText);
        }
    }

    public class ColonKeywordTokenType : TokenType
    {
        public override string RegularExpression => ":";

        public override int Priority { get; } = 0;
        public override Token FromText(string literalText)
        { 
            return new ColonKeywordToken(literalText);
        }
    }

    public class DecimalLiteralTokenType : TokenType
    {
        public override string RegularExpression => "[0-9]+(\\.[0-9]+)?";

        public override int Priority { get; } = 0;
        public override Token FromText(string literalText)
        {
            return new DecimalLiteralToken(literalText);
        }
    }
}