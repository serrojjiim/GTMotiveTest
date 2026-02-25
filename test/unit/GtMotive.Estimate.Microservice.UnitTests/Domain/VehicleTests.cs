using System;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Domain
{
    public class VehicleTests
    {
        [Fact]
        public void Constructor_WithValidData_ShouldCreateAvailableVehicle()
        {
            var vehicle = CreateValidVehicle();

            Assert.True(vehicle.IsAvailable);
            Assert.Equal("Toyota", vehicle.Brand);
        }

        [Fact]
        public void Constructor_WithOldManufacturingDate_ShouldThrowException()
        {
            var oldDate = DateTime.UtcNow.AddYears(-6);

            Assert.Throws<DomainException>(() =>
                new Vehicle(Guid.NewGuid(), "Toyota", "Corolla", "license1", oldDate));
        }

        [Fact]
        public void Constructor_WithFutureDate_ShouldThrowException()
        {
            var futureDate = DateTime.UtcNow.AddDays(30);

            Assert.Throws<DomainException>(() =>
                new Vehicle(Guid.NewGuid(), "Toyota", "Corolla", "license1", futureDate));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Constructor_WithEmptyBrand_ShouldThrowException(string brand)
        {
            Assert.Throws<DomainException>(() =>
                new Vehicle(Guid.NewGuid(), brand, "Corolla", "license1", DateTime.UtcNow.AddYears(-1)));
        }

        [Fact]
        public void Constructor_WithEmptyGuid_ShouldThrowException()
        {
            Assert.Throws<DomainException>(() =>
                new Vehicle(Guid.Empty, "Toyota", "Corolla", "license1", DateTime.UtcNow.AddYears(-1)));
        }

        [Fact]
        public void MarkAsRented_WhenAvailable_ShouldSetUnavailable()
        {
            var vehicle = CreateValidVehicle();

            vehicle.MarkAsRented();

            Assert.False(vehicle.IsAvailable);
        }

        [Fact]
        public void MarkAsRented_WhenAlreadyRented_ShouldThrowException()
        {
            var vehicle = CreateValidVehicle();
            vehicle.MarkAsRented();

            Assert.Throws<DomainException>(vehicle.MarkAsRented);
        }

        private static Vehicle CreateValidVehicle()
        {
            return new Vehicle(
                Guid.NewGuid(), "Toyota", "Corolla", "license1", DateTime.UtcNow.AddYears(-1));
        }
    }
}
