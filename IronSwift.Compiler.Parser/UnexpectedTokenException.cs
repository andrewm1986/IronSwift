using System;
using IronSwift.Compiler.Lexer.Tokens;

namespace IronSwift.Compiler.Parser.AbstractSyntaxTree
{
    public class UnexpectedTokenException : Exception
    {
        public Token Token { get; set; }
        public Type Type { get; set; }

        public UnexpectedTokenException(Token token)
        {
            Token = token;
        }
        public UnexpectedTokenException(Token token, Type type)
        {
            Token = token;
            Type = type;
        }
    }
}