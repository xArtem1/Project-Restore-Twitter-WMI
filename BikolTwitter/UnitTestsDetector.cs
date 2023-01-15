using System;
using System.Reflection;

/// <summary>
/// Detect if we are running as part of a xUnit unit test.
/// This is DIRTY and should only be used if absolutely necessary 
/// as its usually a sign of bad design.
/// </summary>    
static class UnitTestDetector
{

    private static bool _runningFromXUnit = false;

    static UnitTestDetector()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (Assembly assem in assemblies)
        {
            // Can't do something like this as it will load the nUnit assembly
            // if (assem == typeof(NUnit.Framework.Assert))

            if (assem.FullName.ToLowerInvariant().StartsWith("bikoltwitter.tests"))
            {
                _runningFromXUnit = true;
                break;
            }
        }
    }

    public static bool IsRunningFromXUnit
    {
        get { return _runningFromXUnit; }
    }
}