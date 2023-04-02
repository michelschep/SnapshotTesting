using System.Runtime.CompilerServices;
using FluentAssertions;

namespace SnapshotTesting;

[UsesVerify]
public class ExampleWithComplexObject
{
    [Fact]
    public Task ExampleWithComplexObjectType()
    {
        // Arrange
        var expected = true;

        // Act
        var actual = RetrieveComplexObject();

        // Assert

        // Init
        //actual.Should().Be(new DevNetNoord()
        //{
        //});

        // Demo
        return Verify(actual);
    }

    private DevNetNoord RetrieveComplexObject()
    {
        // Create some speakers
        Speaker speaker1 = new Speaker { ID = 1, Name = "John Smith", Topic = "Introduction to C#", CompanyName = "Acme Inc." };
        Speaker speaker2 = new Speaker { ID = 2, Name = "Jane Doe", Topic = "ASP.NET Core", CompanyName = "Globex Corporation" };
        Speaker speaker3 = new Speaker { ID = 3, Name = "Bob Johnson", Topic = "Database Design", CompanyName = "Initech" };

        // Create some attendees
        Attendee attendee1 = new Attendee { ID = 1, Name = "Alice Brown", CompanyName = "Acme Inc." };
        Attendee attendee2 = new Attendee { ID = 2, Name = "David Lee", CompanyName = "Globex Corporation" };
        Attendee attendee3 = new Attendee { ID = 3, Name = "Emily Chen", CompanyName = "Initech" };

        // Create some schedule items
        Schedule schedule1 = new Schedule { SpeakerName = "John Smith", Time = new DateTime(2023, 4, 15, 9, 0, 0), ConferenceRoom = "Room 1", Topic = "Introduction to C#" };
        Schedule schedule2 = new Schedule { SpeakerName = "Jane Doe", Time = new DateTime(2023, 4, 15, 10, 0, 0), ConferenceRoom = "Room 2", Topic = "ASP.NET Core" };
        Schedule schedule3 = new Schedule { SpeakerName = "Bob Johnson", Time = new DateTime(2023, 4, 15, 11, 0, 0), ConferenceRoom = "Room 3", Topic = "Database Design" };

        // Create a DevNetNoord instance
        DevNetNoord devNetNoord = new DevNetNoord
        {
            Speakers = new List<Speaker> { speaker1, speaker2, speaker3 },
            Attendees = new List<Attendee> { attendee1, attendee2, attendee3 },
            Schedule = new List<Schedule> { schedule1, schedule2, schedule3 },
            EventDateTime = new DateTime(2023, 4, 15, 9, 0, 0),
            EventLocation = "Groningen"
        };

        return devNetNoord;
    }

    class Speaker
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Topic { get; set; }
        public string CompanyName { get; set; }
    }

    class Attendee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
    }

    class Schedule
    {
        public string SpeakerName { get; set; }
        public DateTime Time { get; set; }
        public string ConferenceRoom { get; set; }
        public string Topic { get; set; }
    }

    class DevNetNoord
    {
        public List<Speaker> Speakers { get; set; }
        public List<Attendee> Attendees { get; set; }
        public List<Schedule> Schedule { get; set; }
        public DateTime EventDateTime { get; set; }
        public string EventLocation { get; set; }
    }
}