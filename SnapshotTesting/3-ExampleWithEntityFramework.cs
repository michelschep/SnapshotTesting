using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using VerifyTests.EntityFramework;

namespace SnapshotTesting;

[UsesVerify]
public class ExampleWithEntityFramework
{
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
        data.Companies.Add(new Company { Name = "Hilton Hotel" });
        //data.SaveChanges();
        data.Companies.Add(new Company { Name = "Hotel Plus" });
        //data.SaveChanges();
        data.Companies.Where(c => c.Name == "Hotel Plus");//.ToList();

        // Assert
        var entries = EfRecording.FinishRecording();
        await Verify(entries);
    }
}

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class SampleDbContext : DbContext
{
    public DbSet<Company> Companies { get; set; }

    public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options)
    {
    }
}