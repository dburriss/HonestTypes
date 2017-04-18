using LanguageExt;
using System;

namespace FunC
{
    public static partial class Fun
    {
        public static R Using<TDisp, R>(TDisp disposable
         , Func<TDisp, R> func) where TDisp : IDisposable
        {
            using (var disp = disposable) return func(disp);
        }

        public static Unit Using<TDisp>(TDisp disposable
           , Action<TDisp> act) where TDisp : IDisposable
           => Using(disposable, act.ToFunc());

        public static R Using<TDisp, R>(Func<TDisp> createDisposable
           , Func<TDisp, R> func) where TDisp : IDisposable
        {
            using (var disp = createDisposable()) return func(disp);
        }

        public static Unit Using<TDisp>(Func<TDisp> createDisposable
           , Action<TDisp> action) where TDisp : IDisposable
           => Using(createDisposable, action.ToFunc());
    }
}
