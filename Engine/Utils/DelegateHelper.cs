using System;

namespace Engine
{
    /// Some helper functions for delegates.
    public static class DelegateHelper
    {
        /// Substracts all individual members of a given multicast delegate from a given source.
        /// Performs each substraction separately, so the invocation list of the delegate
        /// to substract does not have to be a sublist of the invocation list of the source. 
        public static Delegate SubstractAll(Delegate source, MulticastDelegate toSubstract)
        {
            if (source == null || toSubstract == null) return source;

            var result = source;
            foreach (var del in toSubstract.GetInvocationList())
            {
                result = Delegate.Remove(result, del);
            }

            return result;
        }
    }
}