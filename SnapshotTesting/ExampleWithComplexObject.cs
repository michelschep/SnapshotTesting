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
    
    [Fact]
    public Task ExampleWithComplexObjectType()
    {
        // Arrange
        var expected = true;

        // Act
        var actual = RetrieveComplexObject();

        // Assert
        return Verify(actual);
    }
    
    private Groningen RetrieveComplexObject()
    {
        Groningen groningen = new Groningen();
        DevCampNoord devCampNoord = new DevCampNoord();
        Developer developer1 = new Developer
        {
            FirstName = "Alice",
            LastName = "Smith",
            Expertise = "Frontend",
            Languages = new List<string> { "HTML", "CSS", "JavaScript" }
        };

        Developer developer2 = new Developer
        {
            FirstName = "Bob",
            LastName = "Johnson",
            Expertise = "Backend",
            Languages = new List<string> { "C#", "Java", "Python" }
        };

        devCampNoord.Developers.Add(developer1);
        devCampNoord.Developers.Add(developer2);
        groningen.DevCamps.Add(devCampNoord);

        return groningen;
    }
}

public class Groningen
{
    public Guid CityId { get; set; }
    public string CityName { get; set; }
    public List<DevCampNoord> DevCamps { get; set; }

    public Groningen()
    {
        CityId = Guid.NewGuid();
        CityName = "Groningen";
        DevCamps = new List<DevCampNoord>();
    }
    

}

public class DevCampNoord
{
    public Guid CampId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Developer> Developers { get; set; }

    public DevCampNoord()
    {
        CampId = Guid.NewGuid();
        Name = "DevCampNoord";
        StartDate = DateTime.Now;
        EndDate = StartDate.AddDays(3);
        Developers = new List<Developer>();
    }
}

public class Developer
{
    public Guid DeveloperId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Expertise { get; set; }
    public List<string> Languages { get; set; }

    public Developer()
    {
        DeveloperId = Guid.NewGuid();
        FirstName = "John";
        LastName = "Doe";
        Expertise = "Full Stack";
        Languages = new List<string> { "C#", "JavaScript", "Python" };
    }
}