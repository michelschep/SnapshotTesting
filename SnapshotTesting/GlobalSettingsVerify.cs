using System.Runtime.CompilerServices;

namespace SnapshotTesting;

public static class GlobalSettingsVerify
{
    [ModuleInitializer]
    public static void Initialize()
    {
        DerivePathInfo(
            (sourceFile, projectDirectory, type, method) => new(
                directory: Path.Combine(projectDirectory, "VerifiedSnapshots"),
                typeName: type.Name,
                methodName: method.Name));
    }
}