using System;
using Redox.Core;

namespace Redox.Bootstrapper
{
    public class Bootstrap
    {
        public static async void Initialize(string modDir)
        {
            Console.WriteLine("[Bootstrapper] Starting bootstrapper....");
            await RedoxMod.GetMod().InitializeAsync(modDir);
        }
    }
}