using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronSwift.Compiler.Parser.Helpers
{
    public class TwoWayEnumerator<T> : ITwoWayEnumerator<T>
    {
        private readonly IEnumerator<T> enumerator;
        private readonly List<T> buffer;
        private int index;

        public TwoWayEnumerator(IEnumerator<T> enumerator)
        {
            if (enumerator == null)
                throw new ArgumentNullException("enumerator");

            this.enumerator = enumerator;
            buffer = new List<T>();
            index = -1;
        }

        public bool MovePrevious()
        {
            if (index <= 0)
            {
                return false;
            }

            --index;
            return true;
        }

        public bool MoveNext()
        {
            if (index < buffer.Count - 1)
            {
                ++index;
                return true;
            }

            if (!enumerator.MoveNext())
            {
                return false;
            }

            buffer.Add(enumerator.Current);
            ++index;
            return true;
        }

        public T Current
        {
            get
            {
                if (index < 0 || index >= buffer.Count)
                    throw new InvalidOperationException();

                return buffer[index];
            }
        }

        public void Reset()
        {
            enumerator.Reset();
            buffer.Clear();
            index = -1;
        }

        public void Dispose()
        {
            enumerator.Dispose();
        }

        object System.Collections.IEnumerator.Current => Current;
    }
}
