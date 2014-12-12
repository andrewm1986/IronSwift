namespace IronSwift.Compiler.Lexer.Tokens
{
    public abstract class Token
    {
        public string LiteralText { get; protected set; }
        public abstract string RegularExpression { get; }
    }

    public class VarKeywordToken : Token
    {
        public override string RegularExpression => "var";
    }

    public class LetKeywordToken : Token
    {
        public override string RegularExpression => "let";
    }

    public class IdentifierToken : Token
    {
        public override string RegularExpression => "[a-zA-Z][a-zA-Z0-9]*";
    }

    public class WhitespaceToken : Token
    {
        public override string RegularExpression => "[ \t\r\n]+";
    }

    public class SingleEqualsToken : Token
    {
        public override string RegularExpression => "=";
    }

    public class ColonKeywordToken : Token
    {
        public override string RegularExpression => ":";
    }

    public class DecimalLiteralToken : Token
    {
        public override string RegularExpression => "[0-9]+(\\.[0-9]+)?";
    }
}