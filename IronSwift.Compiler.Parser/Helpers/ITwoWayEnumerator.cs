using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronSwift.Compiler.Parser.Helpers
{
    public interface ITwoWayEnumerator<out T> : IEnumerator<T>
    {
        bool MovePrevious();
    }
}
