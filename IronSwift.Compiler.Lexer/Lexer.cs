using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Fare;
using IronSwift.Compiler.Lexer.Tokens;

namespace IronSwift.Compiler.Lexer
{
    public class Lexer
    {
        private Stream stream;
        private static IList<Token> tokenTypes;

        private Lexer()
        {

        }

        public static Lexer FromStream(Stream stream)
        {
            var lexer = new Lexer { stream = stream };

            return lexer;
        }

        public IEnumerable<Token> GetTokens()
        {
            EnsureStreamIsOpen();
            EnsureTokenTypesAreLoaded();

            var possibleTokens = (from t in tokenTypes
                                  select new TokenState
                                  {
                                      Token = t,
                                      InitialState = t.Automaton.Initial,
                                      CurrentState = t.Automaton.Initial,
                                      PreviousState = null,
                                      NextState = null,
                                  }).ToList();

            int readValue;
            var buffer = new StringBuilder();
            while ((readValue = stream.ReadByte()) != -1)
            {
                var yieldedToken = ProcessCharacter(possibleTokens, (char)readValue, ref buffer);

                if (yieldedToken != null)
                {
                    yield return yieldedToken;
                }
            }

            // Now we check for any tokens that end with the stream
            // Did we end the stream with any valid tokens?
            var finishedTokens = possibleTokens.Where(t => t.CurrentState != null && t.CurrentState.Accept).ToList();

            // ... no
            if (!finishedTokens.Any()) yield break;

            // ... yes
            // Which token did we match? Based on priority
            // i.e var is a keyword, not an indentifier
            var matchedToken = finishedTokens.OrderBy(t => t.Token.Priority).First();

            // Generate a new copy of the token
            yield return matchedToken.Token.FromText(buffer.ToString());
        }

        private static Token ProcessCharacter(List<TokenState> possibleTokens, char character, ref StringBuilder buffer)
        {
            Token yieldedToken = null;

            // Generate next steps
            foreach (var token in possibleTokens.Where(t => t.CurrentState != null))
            {
                token.NextState = token.CurrentState.Step(character);
            }

            // Did this character make any previously accepted token turn invalid?
            var finishedTokens = possibleTokens.Where(t => t.CurrentState != null && t.CurrentState.Accept && t.NextState == null).ToList();

            if (finishedTokens.Any())
            {
                // ... yes

                // Which token did we match? Based on priority
                // i.e var is a keyword, not an indentifier
                var matchedToken = finishedTokens.OrderBy(t => t.Token.Priority).First();

                // Generate a new copy of the token
                yieldedToken = matchedToken.Token.FromText(buffer.ToString());

                // Reset all the tokens to their initial state
                foreach (var token in possibleTokens)
                {
                    token.CurrentState = token.InitialState;
                    token.PreviousState = token.NextState = null;
                }

                // Reset the buffer
                buffer = new StringBuilder();

                // We read a character in from the stream but haven't processed it properly
                // So we need to process it again
                ProcessCharacter(possibleTokens, character, ref buffer);
            }
            else
            {
                // ... no

                // Progress each token's state
                foreach (var token in possibleTokens)
                {
                    token.PreviousState = token.CurrentState;
                    token.CurrentState = token.NextState;
                }

                // Store the character
                buffer.Append(character);
            }

            return yieldedToken;
        }

        private class TokenState
        {
            public Token Token { get; set; }
            public State InitialState { get; set; }
            public State PreviousState { get; set; }
            public State CurrentState { get; set; }
            public State NextState { get; set; }
        }

        private void EnsureTokenTypesAreLoaded()
        {
            if (tokenTypes != null)
            {
                return;
            }

            tokenTypes = Assembly.GetAssembly(GetType())
                .DefinedTypes.Where(t => t.IsSubclassOf(typeof(Token)))
                .Select(t => Activator.CreateInstance(t.AsType()))
                .Cast<Token>()
                .ToList();
        }

        private void EnsureStreamIsOpen()
        {
            if (!stream.CanRead)
            {
                throw new Exception("Stream cannot be read");
            }
        }
    }
}
