namespace IronSwift.Compiler.Lexer.Tokens
{
    public abstract class Token
    {
        public string LiteralText { get; protected set; }
    }
    public class VarKeywordToken : Token
    {
    }

    public class LetKeywordToken : Token
    {
    }

    public class IdentifierToken : Token
    {
    }

    public class WhitespaceToken : Token
    {
    }

    public class SingleEqualsToken : Token
    {
    }

    public class ColonKeywordToken : Token
    {
    }
    public class DecimalLiteralToken : Token
    {
    }
}
