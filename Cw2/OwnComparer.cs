using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Cw2
{
    class OwnComparer : IEqualityComparer<Student>
    {
        public new bool Equals(Student x, Student y)
        {
            return StringComparer
                .InvariantCultureIgnoreCase
                .Equals($"{ x.firstName} { x.lastName} {x.birthDate}",
                $"{ y.firstName} { y.lastName} {y.birthDate}");
        }

        public int GetHashCode(Student obj)
        {
                return StringComparer
                        .InvariantCultureIgnoreCase
                        .GetHashCode($"{obj.firstName} {obj.lastName} {obj.index} ");
        }
    }
}
