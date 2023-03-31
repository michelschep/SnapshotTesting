using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.Testing;


namespace SnapshotTesting;

[UsesVerify]
public class ExampleWithWebApi
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
        
        VerifyHttp.Enable();
    }

    [Fact]
    public async Task When_AskingAnApiForSomething()
    {
        // Arrange
        using var application = new WebApplicationFactory<Program>();
        using var client = application.CreateClient();

        // Act
        var actual = await client.GetAsync("/WeatherForecast");

        // Assert
        await Verify(actual)
            .IgnoreMembers("Version");;
    }
}