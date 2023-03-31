using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using VerifyTests.EntityFramework;

namespace SnapshotTesting;

[UsesVerify]
public class ExampleWithEntityFramework
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
    
    [Fact]
    public async Task SnapshotExampleWithEntityFramework()
    {
        // Arrange
        var optionsBuilder = new DbContextOptionsBuilder<SampleDbContext>();
        optionsBuilder.UseSqlite("DataSource=:memory:");
        optionsBuilder.EnableRecording();
        
        await using var data = new SampleDbContext(optionsBuilder.Options);
        await data.Database.OpenConnectionAsync();
        await data.Database.EnsureCreatedAsync();

        EfRecording.StartRecording();

        // Act
        var result = data.Companies.Where(c => c.Content == "Hotel Plus").ToList();

        // Assert
        var entries = EfRecording.FinishRecording();
        await Verify(entries);
    }
}

public class Company
{
    public int Id { get; set; }
    public string Content { get; set; }
}

public class SampleDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; }

    public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
    {
    }
}