using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronSwift.Compiler.Lexer.Tokens;

namespace IronSwift.Compiler.Lexer
{
    public class Lexer
    {
        private Stream stream;

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

            int readValue;
            while ((readValue = stream.ReadByte()) != -1)
            {
                yield return new SingleEqualsToken();
            }
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
