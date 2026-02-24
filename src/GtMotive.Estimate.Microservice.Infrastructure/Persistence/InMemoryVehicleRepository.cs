using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence
{
    public class InMemoryVehicleRepository : IVehicleRepository
    {
        private readonly ConcurrentDictionary<Guid, Vehicle> _vehicles = new();

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
    }
}
