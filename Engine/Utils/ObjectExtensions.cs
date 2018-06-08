using System;
using System.Reflection;

namespace System.Reflection
{
    public static class ObjectExtensions
    {
        public static Delegate GetDelegate<T>(this Object obj, string methodName)
        {
            MethodInfo info = obj
                .GetType()
                .GetMethod(
                    methodName,
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance
                );

            if (info != null)
            {
                return Delegate.CreateDelegate(
                    typeof(T),
                    obj,
                    info,
                    throwOnBindFailure: false
                );
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
