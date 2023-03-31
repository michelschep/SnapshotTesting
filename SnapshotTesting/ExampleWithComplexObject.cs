using System.Runtime.CompilerServices;

namespace SnapshotTesting;

[UsesVerify]
public class ExampleWithComplexObject
{
    [ModuleInitializer]
    public static void Initialize()
    {
        DerivePathInfo(
            (sourceFile, projectDirectory, type, method) =>
            {
                return new(
                    directory: Path.Combine(projectDirectory, "VerifiedSnapshots"),
                    typeName: type.Name,
                    methodName: method.Name);
            });
    }
}