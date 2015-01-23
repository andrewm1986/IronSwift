using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IronSwift.Compiler.Lexer.Tokens;
using IronSwift.Compiler.Parser.AbstractSyntaxTree;
using IronSwift.Compiler.Parser.Helpers;

namespace IronSwift.Compiler.Parser
{
    public class Parser
    {
        protected static IList<Node> NodeTypes;

        protected IEnumerable<Token> Tokens;

        private Parser()
        {

        }

        public static Parser FromEnumerable(IEnumerable<Token> tokens)
        {
            var parser = new Parser {Tokens = tokens};

            return parser;
        }

        public Document GetDocument()
        {
            EnsureNodeTypesAreLoaded();

            var document = new Document();

            var stream = new TwoWayEnumerator<Token>(Tokens.GetEnumerator());

            do
            {
                var node = NodeTypes.FirstOrDefault(n => n.CanStartConsumingFrom(stream.Current));
                if (node == null)
                {
                    throw new UnexpectedTokenException(stream.Current);
                }

                document.Children.Add(node.Consume(stream));

            } while (stream.MoveNext());

            return document;
        }

        private void EnsureNodeTypesAreLoaded()
        {
            if (NodeTypes != null)
            {
                return;
            }

            NodeTypes = Assembly.GetAssembly(GetType())
                .DefinedTypes.Where(t => t.IsSubclassOf(typeof (Node)))
                .Select(t => Activator.CreateInstance(t.AsType()))
                .Cast<Node>()
                .ToList();

        }
    }
}
