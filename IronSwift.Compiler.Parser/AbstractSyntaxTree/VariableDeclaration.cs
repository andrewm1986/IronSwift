using System;
using System.Collections.Generic;
using IronSwift.Compiler.Lexer.Tokens;
using IronSwift.Compiler.Parser.Helpers;

namespace IronSwift.Compiler.Parser.AbstractSyntaxTree
{
    public class VariableDeclaration : Node
    {
        protected override IEnumerable<Type> StartingTokens => new[] {typeof (VarKeywordToken), typeof(LetKeywordToken)};

        public override Node Consume(ITwoWayEnumerator<Token> stream)
        {
            var node = new VariableDeclaration();

            // First token should be var or let
            if (stream.Current is VarKeywordToken)
            {
                node.Constant = false;
            }
            else if (stream.Current is LetKeywordToken)
            {
                node.Constant = true;
            }
            else
            {
                throw new UnexpectedTokenException(stream.Current);
            }

            // Next we should have an identifier
            node.Identifier = GetNextNonTrivialToken<IdentifierToken>(stream);

            var nextToken = GetNextNonTrivialToken(stream);
            if (nextToken is SingleEqualsToken)
            {
                // Declaration and assignment
                node.Expression = (Expression)Expression.Consume(stream);
            }
            else
            {
                // Just a declaration
                // but, whoops!, we read something we have no business
                // to read, so roll back

                if (!stream.MovePrevious())
                {
                    throw new InvalidOperationException("Couldn't move backwards");
                }
            }

            return node;
        }

        public bool Constant { get; private set; }

        public IdentifierToken Identifier { get; private set; }
        public IdentifierToken Type { get; private set; }

        public Expression Expression { get; private set; }
    }
}
