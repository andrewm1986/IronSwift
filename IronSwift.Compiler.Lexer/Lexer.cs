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
            var lexer = new Lexer {stream = stream};

            return lexer;
        }

        public IEnumerable<Token> GetTokens()
        {
            EnsureStreamIsOpen();
            EnsureTokenTypesAreLoaded();

            var possibleTokens = tokenTypes.ToDictionary(t => t, t => t.Automaton.Initial);
            
            int readValue;
            while ((readValue = stream.ReadByte()) != -1)
            {
                foreach (var token in possibleTokens.ToList())
                {
                    var nextStep = token.Value.Step((char) readValue);
                    if (nextStep == null && !token.Value.Accept)
                    {
                        possibleTokens.Remove(token.Key);
                    }
                    else
                    {
                        possibleTokens[token.Key] = nextStep;
                    }
                }

                yield return new SingleEqualsToken();
            }
        }

        private void EnsureTokenTypesAreLoaded()
        {
            if (tokenTypes != null)
            {
                return;
            }

            tokenTypes = Assembly.GetAssembly(GetType())
                .DefinedTypes.Where(t => t.IsSubclassOf(typeof (Token)))
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
