using System.Runtime.CompilerServices;

namespace SnapshotTesting;

public static class StaticSettingsUsage
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifierSettings.IgnoreStackTrace();
        
        DerivePathInfo(
            (sourceFile, projectDirectory, type, method) => new(
                directory: Path.Combine(projectDirectory, "VerifiedSnapshots"),
                typeName: type.Name,
                methodName: method.Name));
    }
}