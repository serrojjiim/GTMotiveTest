using System;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Domain
{
    public class RentalTests
    {
        [Fact]
        public void Constructor_WithValidData_ShouldCreateActiveRental()
        {
            var rental = new Rental(Guid.NewGuid(), Guid.NewGuid(), "17481301J");

            Assert.True(rental.IsActive);
            Assert.Null(rental.EndDate);
        }

        [Fact]
        public void Constructor_WithEmptyCustomer_ShouldThrowException()
        {
            Assert.Throws<DomainException>(() =>
                new Rental(Guid.NewGuid(), Guid.NewGuid(), string.Empty));
        }

        [Fact]
        public void Constructor_WithEmptyRentalId_ShouldThrowExceptuion()
        {
            Assert.Throws<DomainException>(() =>
                new Rental(Guid.Empty, Guid.NewGuid(), "17481301J"));
        }

        [Fact]
        public void Complete_WhenActive_ShouldSetEndDate()
        {
            var rental = new Rental(Guid.NewGuid(), Guid.NewGuid(), "17481301J");

            rental.FinishRental();

            Assert.False(rental.IsActive);
            Assert.NotNull(rental.EndDate);
        }

        [Fact]
        public void Complete_WhenAlreadyCompleted_ShouldThrowException()
        {
            var rental = new Rental(Guid.NewGuid(), Guid.NewGuid(), "17481301J");
            rental.FinishRental();

            Assert.Throws<DomainException>(rental.FinishRental);
        }
    }
}
