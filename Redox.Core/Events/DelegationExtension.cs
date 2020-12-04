using System;

namespace Redox.Core.Events
{
    public static class DelegationExtension
    {
        public static void TryInvoke(this Delegate @delegate, params object[] args)
        {
            foreach (var invocation in @delegate.GetInvocationList())
            {
                try
                {
                    invocation.DynamicInvoke(args);
                }
                catch (Exception e)
                {
                    RedoxMod.GetMod().Logger.Exception(e);
                }
            }
        }
    }
}