using System;

namespace FunC
{
    public static class Curry
    {
        public static Func<T1, Func<T2, T3, R>> CurryFirst<T1, T2, T3, R>
         (this Func<T1, T2, T3, R> @this) => t1 => (t2, t3) => @this(t1, t2, t3);
    }
}
