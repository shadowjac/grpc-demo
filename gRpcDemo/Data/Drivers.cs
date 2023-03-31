using System;
using Google.Protobuf.WellKnownTypes;

namespace gRpcDemo.Data
{
	public class Drivers
	{
        public static List<DriverResponse> ListDrivers = new List<DriverResponse>
        {
            new DriverResponse
            {
                Name = "Lewis Hamilton",
                CarNumber = 44,
                Experience = Experience.Veteran,
                Team = "Mercedes",
                BirthDate = Timestamp.FromDateTime(new DateTime(1980, 1, 1, 0,0,0, DateTimeKind.Utc)),
                Timestamp = Timestamp.FromDateTime(DateTime.UtcNow)
            },
            new DriverResponse
            {
                Name = "Max Verstappen",
                CarNumber = 1,
                Experience = Experience.Veteran,
                Team = "Red Bull",
                BirthDate = Timestamp.FromDateTime(new DateTime(1991, 1, 1, 0,0,0, DateTimeKind.Utc)),
                Timestamp = Timestamp.FromDateTime(DateTime.UtcNow)
            },
            new DriverResponse
            {
                Name = "Logan Sargent",
                CarNumber = 2,
                Experience = Experience.Rookie,
                Team = "Williams",
                BirthDate = Timestamp.FromDateTime(new DateTime(2000, 1, 1, 0,0,0, DateTimeKind.Utc)),
                Timestamp = Timestamp.FromDateTime(DateTime.UtcNow)
            },
            new DriverResponse
            {
                Name = "Charles Leclerc",
                CarNumber = 16,
                Experience = Experience.Veteran,
                Team = "Ferrari",
                BirthDate = Timestamp.FromDateTime(new DateTime(1995, 1, 1, 0,0,0, DateTimeKind.Utc)),
                Timestamp = Timestamp.FromDateTime(DateTime.UtcNow)
            }
        };
	}
}

