using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence
{
    public class InMemoryVehicleRepository : IVehicleRepository
    {
        private static readonly ConcurrentDictionary<Guid, Vehicle> _vehicles = new();

        /// <summary>
        /// Clears all vehicles (for testing purposes).
        /// </summary>
        public static void Clear()
        {
            _vehicles.Clear();
        }

        public Task Add(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);
            _vehicles.TryAdd(vehicle.Id, vehicle);
            return Task.CompletedTask;
        }

        public Task<Vehicle> GetById(Guid id)
        {
            _vehicles.TryGetValue(id, out var vehicle);
            return Task.FromResult(vehicle);
        }

        /// <inheritdoc/>
        public Task<IReadOnlyList<Vehicle>> GetAvailableVehicles()
        {
            var available = _vehicles.Values
                .Where(v => v.IsAvailable)
                .ToList();

            return Task.FromResult<IReadOnlyList<Vehicle>>(available);
        }
    }
}
