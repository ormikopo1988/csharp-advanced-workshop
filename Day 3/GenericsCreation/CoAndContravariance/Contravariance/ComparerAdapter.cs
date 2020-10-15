using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace CoAndContravariance.Contravariance
{
    // This adapter just takes the IMyComparer interface that we've written for ourselves
    // and just turns it effectively in an IComparer interface.
    public class ComparerAdapter<T> : IComparer<T>
    {
        private IMyComparer<T> _innerComparer;

        public ComparerAdapter(IMyComparer<T> innerComparer)
        {
            _innerComparer = innerComparer;
        }

        public int Compare([AllowNull] T x, [AllowNull] T y)
        {
            return _innerComparer.Compare(x, y);
        }
    }
}