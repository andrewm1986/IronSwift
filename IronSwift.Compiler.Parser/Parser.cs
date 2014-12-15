using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronSwift.Compiler.Lexer.Tokens;
using IronSwift.Compiler.Parser.AbstractSyntaxTree;

namespace IronSwift.Compiler.Parser
{
    public class Parser
    {
        public IEnumerable<Token> tokens;

        private Parser()
        {

        }

        public static Parser FromEnumerable(IEnumerable<Token> tokens)
        {
            var parser = new Parser {tokens = tokens};

            return parser;
        }

        public DocumentNode GetTree()
        {
            return new DocumentNode();
        }
    }
}
