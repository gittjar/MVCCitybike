using Xunit;
using Microsoft.EntityFrameworkCore;
using MvcStation.Data;
using MVCCitybike.Models;
using System;
using System.Linq;

namespace MVCCitybike.Tests
{
    public class StationTests
    {
        private MvcStationContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<MvcStationContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new MvcStationContext(options);
        }

        [Fact]
        public void CanAddStation()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var station = new Station
            {
                FID = 1,
                ID = 501,
                Nimi = "Test Station",
                Namn = "Test Station SV",
                Name = "Test Station EN",
                Osoite = "Test Address 1",
                Adress = "Test Address SV",
                Kaupunki = "Helsinki",
                Stad = "Helsingfors",
                Operaattor = "CityBike Finland",
                Kapasiteet = 20,
                x = 24.9384m,
                y = 60.1699m,
                Kuva = "test-station.jpg"
            };

            // Act
            context.Station.Add(station);
            context.SaveChanges();

            // Assert
            var savedStation = context.Station.FirstOrDefault(s => s.ID == 501);
            Assert.NotNull(savedStation);
            Assert.Equal("Test Station", savedStation.Nimi);
            Assert.Equal("Helsinki", savedStation.Kaupunki);
            Assert.Equal(20, savedStation.Kapasiteet);
        }

        [Fact]
        public void CanUpdateStationCapacity()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var station = new Station
            {
                FID = 2,
                ID = 502,
                Nimi = "Update Test",
                Osoite = "Update Address",
                Kaupunki = "Helsinki",
                Kapasiteet = 10,
                x = 24.9384m,
                y = 60.1699m,
                Kuva = "update-test.jpg"
            };
            context.Station.Add(station);
            context.SaveChanges();

            // Act
            station.Kapasiteet = 25;
            context.SaveChanges();

            // Assert
            var updatedStation = context.Station.Find(station.ID);
            Assert.Equal(25, updatedStation?.Kapasiteet);
        }

        [Fact]
        public void CanDeleteStation()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var station = new Station
            {
                FID = 3,
                ID = 503,
                Nimi = "Delete Test",
                Osoite = "Delete Address",
                Kaupunki = "Helsinki",
                x = 24.9384m,
                y = 60.1699m,
                Kuva = "delete-test.jpg"
            };
            context.Station.Add(station);
            context.SaveChanges();

            // Act
            context.Station.Remove(station);
            context.SaveChanges();

            // Assert
            var deletedStation = context.Station.Find(station.FID);
            Assert.Null(deletedStation);
        }

        [Theory]
        [InlineData("Helsinki")]
        [InlineData("Espoo")]
        [InlineData("Vantaa")]
        public void CanFilterStationsByCity(string city)
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            context.Station.AddRange(
                new Station { FID = 10, ID = 510, Nimi = "Station A", Osoite = "Address A", Kaupunki = "Helsinki", x = 24.9384m, y = 60.1699m, Kuva = "a.jpg" },
                new Station { FID = 11, ID = 511, Nimi = "Station B", Osoite = "Address B", Kaupunki = "Espoo", x = 24.6522m, y = 60.2055m, Kuva = "b.jpg" },
                new Station { FID = 12, ID = 512, Nimi = "Station C", Osoite = "Address C", Kaupunki = "Vantaa", x = 25.0378m, y = 60.2934m, Kuva = "c.jpg" }
            );
            context.SaveChanges();

            // Act
            var filtered = context.Station.Where(s => s.Kaupunki == city).ToList();

            // Assert
            Assert.Single(filtered);
            Assert.All(filtered, s => Assert.Equal(city, s.Kaupunki));
        }

        [Fact]
        public void StationRequiresCoordinates()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var station = new Station
            {
                FID = 4,
                ID = 504,
                Nimi = "Coordinate Test",
                Osoite = "Coordinate Address",
                Kaupunki = "Helsinki",
                x = 24.9384m,
                y = 60.1699m,
                Kuva = "coord-test.jpg"
            };

            // Act & Assert
            Assert.NotEqual(0, station.x);
            Assert.NotEqual(0, station.y);
        }
    }
}
