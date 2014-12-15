using Fare;

namespace IronSwift.Compiler.Lexer.Tokens
{
    public abstract class Token
    {
        public string LiteralText { get; protected set; }

        protected Token(string literalText)
        {
            LiteralText = literalText;
        }
    }

    public class VarKeywordToken : Token
    {
        public VarKeywordToken(string literalText) : base(literalText)
        {
        }
    }

    public class OpenParenthesisToken : Token
    {
        public OpenParenthesisToken(string literalText) : base(literalText)
        {
        }
    }

    public class CloseParenthesisToken : Token
    {
        public CloseParenthesisToken(string literalText) : base(literalText)
        {
        }
    }

    public class CommaToken : Token
    {
        public CommaToken(string literalText) : base(literalText)
        {
        }
    }

    public class LetKeywordToken : Token
    {
        public LetKeywordToken(string literalText) : base(literalText)
        {
        }
    }

    public class IdentifierToken : Token
    {
        public IdentifierToken(string literalText) : base(literalText)
        {
        }
    }

    public class WhitespaceToken : Token
    {
        public WhitespaceToken(string literalText) : base(literalText)
        {
        }
    }

    public class SingleEqualsToken : Token
    {
        public SingleEqualsToken(string literalText) : base(literalText)
        {
        }
    }

    public class ColonKeywordToken : Token
    {
        public ColonKeywordToken(string literalText) : base(literalText)
        {
        }
    }

    public class DecimalLiteralToken : Token
    {
        public DecimalLiteralToken(string literalText) : base(literalText)
        {
        }
    }
}