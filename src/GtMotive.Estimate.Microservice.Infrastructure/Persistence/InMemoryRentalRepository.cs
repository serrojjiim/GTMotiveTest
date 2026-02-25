using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence
{
    /// <summary>
    /// Rental repository.
    /// </summary>
    public class InMemoryRentalRepository : IRentalRepository
    {
        private static readonly ConcurrentDictionary<Guid, Rental> _rentals = new();

        /// <inheritdoc/>
        public Task Add(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);
            _rentals.TryAdd(rental.Id, rental);
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task<Rental> GetActiveRentalByCustomer(string customerIdentifier)
        {
            var rental = _rentals.Values
                .FirstOrDefault(r => r.CustomerIdentifier == customerIdentifier && r.IsActive);

            return Task.FromResult(rental);
        }

        /// <inheritdoc/>
        public Task<Rental> GetActiveRentalByVehicle(Guid vehicleId)
        {
            var rental = _rentals.Values
                .FirstOrDefault(r => r.VehicleId == vehicleId && r.IsActive);

            return Task.FromResult(rental);
        }

        /// <inheritdoc/>
        public Task<Rental> GetById(Guid rentalId)
        {
            _rentals.TryGetValue(rentalId, out var rental);
            return Task.FromResult(rental);
        }

        /// <inheritdoc/>
        public Task Update(Rental rental)
        {
            ArgumentNullException.ThrowIfNull(rental);
            _rentals[rental.Id] = rental;
            return Task.CompletedTask;
        }
    }
}
