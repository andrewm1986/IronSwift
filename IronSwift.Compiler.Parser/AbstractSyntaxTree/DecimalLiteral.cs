using System;
using System.Collections.Generic;
using IronSwift.Compiler.Lexer.Tokens;
using IronSwift.Compiler.Parser.Helpers;

namespace IronSwift.Compiler.Parser.AbstractSyntaxTree
{
    public class DecimalLiteral : Expression
    {
        protected override IEnumerable<Type> StartingTokens => new[] {typeof (DecimalLiteralToken)};

        public override Node Consume(ITwoWayEnumerator<Token> stream)
        {
            return new DecimalLiteral {Value = decimal.Parse(stream.Current.LiteralText)};
        }

        public decimal Value { get; protected set; }
    }
}