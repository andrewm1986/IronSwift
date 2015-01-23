using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronSwift.Compiler.Lexer.Tokens;
using IronSwift.Compiler.Parser.Helpers;

namespace IronSwift.Compiler.Parser.AbstractSyntaxTree
{
    public abstract class Node
    {
        protected abstract IEnumerable<Type> StartingTokens { get; }

        public bool CanStartConsumingFrom(Token token)
        {
            return StartingTokens.Contains(token.GetType());
        }

        public abstract Node Consume(ITwoWayEnumerator<Token> stream);

        protected Token GetNextNonTrivialToken(ITwoWayEnumerator<Token> stream)
        {
            throw new NotImplementedException();
        }
        protected T GetNextNonTrivialToken<T>(ITwoWayEnumerator<Token> stream) where T : Token
        {
            var token = GetNextNonTrivialToken(stream);
            var castedToken = token as T;
            if (castedToken != null)
            {
                return castedToken;
            }

            throw new UnexpectedTokenException(token, typeof(T));
        }
    }
}
