using System;
using System.Reflection;

namespace System.Reflection
{
    public static class ObjectExtensions
    {
        public static TDelegate GetDelegate<TDelegate>(this Object obj, string methodName) 
            where TDelegate : class
        {
            MethodInfo info = obj
                .GetType()
                .GetMethod(
                    methodName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                );

            if (info != null)
            {
                return Delegate.CreateDelegate(
                    typeof(TDelegate),
                    obj,
                    info,
                    throwOnBindFailure: false
                ) as TDelegate;
            }

            CheckForWrongMethodNameCase(obj, methodName);
            return null;
        }

        static void CheckForWrongMethodNameCase(Object obj, string methodName)
        {
            MethodInfo info = obj
                .GetType()
                .GetMethod(
                    methodName,
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase
                );

            if (info != null)
            {
                throw new Exception(
                    $"'{methodName}' method was not bound. Please check it's correct case (capital {methodName[0]}?)"
                );
            }
        }

    }
}
