using System;
using System.Collections.Generic;
using System.Text;

namespace Cw2
{
    class NameComparer : IEqualityComparer<ActiveStudies>
    {
        public bool Equals(ActiveStudies x, ActiveStudies y)
        {
            return StringComparer
                 .InvariantCultureIgnoreCase
                 .Equals(x.name, y.name);
        }

        public int GetHashCode(ActiveStudies obj)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .GetHashCode(obj.name);
        }
    }
}
