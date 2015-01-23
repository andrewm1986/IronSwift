using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronSwift.Compiler.Parser.AbstractSyntaxTree
{
    public class Document
    {
        public IList<Node> Children { get; protected set; } = new List<Node>();
    }
}
