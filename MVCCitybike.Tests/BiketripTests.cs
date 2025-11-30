using Xunit;
using Microsoft.EntityFrameworkCore;
using MvcBiketripsMay2021.Data;
using MVCCitybike.Models;
using System;
using System.Linq;

namespace MVCCitybike.Tests
{
    public class BiketripTests
    {
        private MvcBiketripsMay2021Context GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<MvcBiketripsMay2021Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new MvcBiketripsMay2021Context(options);
        }

        [Fact]
        public void CanAddBiketrip()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var trip = new BiketripsMay2021
            {
                Departure = DateTime.Parse("2021-05-01T10:00:00"),
                Return = DateTime.Parse("2021-05-01T10:30:00"),
                Departure_station_id = 501,
                Departure_station_name = "Start Station",
                Return_station_id = 502,
                Return_station_name = "End Station",
                Covered_distance_m = 2500,
                Duration_sec = 1800
            };

            // Act
            context.BiketripsMay2021.Add(trip);
            context.SaveChanges();

            // Assert
            var savedTrip = context.BiketripsMay2021.FirstOrDefault();
            Assert.NotNull(savedTrip);
            Assert.Equal(2500, savedTrip.Covered_distance_m);
            Assert.Equal(1800, savedTrip.Duration_sec);
        }

        [Fact]
        public void TripDurationIsCalculatedCorrectly()
        {
            // Arrange
            var departure = DateTime.Parse("2021-05-01T10:00:00");
            var returnTime = DateTime.Parse("2021-05-01T10:30:00");
            var expectedDuration = (returnTime - departure).TotalSeconds;

            // Act
            var trip = new BiketripsMay2021
            {
                Departure = departure,
                Return = returnTime,
                Duration_sec = (int)expectedDuration
            };

            // Assert
            Assert.Equal(1800, trip.Duration_sec); // 30 minutes = 1800 seconds
        }

        [Theory]
        [InlineData(2500, 1800)] // 2.5km in 30 min
        [InlineData(5000, 3600)] // 5km in 1 hour
        [InlineData(1000, 600)]  // 1km in 10 min
        public void CanCalculateAverageSpeed(int distanceMeters, int durationSeconds)
        {
            // Arrange
            var trip = new BiketripsMay2021
            {
                Covered_distance_m = distanceMeters,
                Duration_sec = durationSeconds
            };

            // Act
            var speedKmh = ((double)trip.Covered_distance_m / 1000.0) / (trip.Duration_sec / 3600.0);

            // Assert
            Assert.True(speedKmh > 0);
            Assert.True(speedKmh < 50); // Reasonable bike speed
        }

        [Fact]
        public void CanFilterTripsByStation()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            context.BiketripsMay2021.AddRange(
                new BiketripsMay2021 
                { 
                    Departure = DateTime.Now, 
                    Return = DateTime.Now.AddMinutes(30),
                    Departure_station_id = 501, 
                    Departure_station_name = "Station 501",
                    Return_station_id = 502,
                    Return_station_name = "Station 502",
                    Covered_distance_m = 2000,
                    Duration_sec = 1800
                },
                new BiketripsMay2021 
                { 
                    Departure = DateTime.Now, 
                    Return = DateTime.Now.AddMinutes(20),
                    Departure_station_id = 501, 
                    Departure_station_name = "Station 501",
                    Return_station_id = 503,
                    Return_station_name = "Station 503",
                    Covered_distance_m = 1500,
                    Duration_sec = 1200
                },
                new BiketripsMay2021 
                { 
                    Departure = DateTime.Now, 
                    Return = DateTime.Now.AddMinutes(25),
                    Departure_station_id = 502, 
                    Departure_station_name = "Station 502",
                    Return_station_id = 503,
                    Return_station_name = "Station 503",
                    Covered_distance_m = 1800,
                    Duration_sec = 1500
                }
            );
            context.SaveChanges();

            // Act
            var tripsFromStation501 = context.BiketripsMay2021
                .Where(t => t.Departure_station_id == 501)
                .ToList();

            // Assert
            Assert.Equal(2, tripsFromStation501.Count);
        }

        [Fact]
        public void CanSortTripsByDuration()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            context.BiketripsMay2021.AddRange(
                new BiketripsMay2021 { Departure = DateTime.Now, Return = DateTime.Now, Departure_station_name = "Station A", Return_station_name = "Station B", Duration_sec = 3600 },
                new BiketripsMay2021 { Departure = DateTime.Now, Return = DateTime.Now, Departure_station_name = "Station B", Return_station_name = "Station C", Duration_sec = 1200 },
                new BiketripsMay2021 { Departure = DateTime.Now, Return = DateTime.Now, Departure_station_name = "Station C", Return_station_name = "Station D", Duration_sec = 2400 }
            );
            context.SaveChanges();

            // Act
            var sortedTrips = context.BiketripsMay2021
                .OrderBy(t => t.Duration_sec)
                .ToList();

            // Assert
            Assert.Equal(1200, sortedTrips.First().Duration_sec);
            Assert.Equal(3600, sortedTrips.Last().Duration_sec);
        }
    }
}
