using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Repositories
{
    /// <summary>
    /// Repository interface for vehicle entities.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a vehicle.
        /// </summary>
        /// <param name="vehicle">Vehicle to add.</param>
        /// <returns>Task.</returns>
        Task Add(Vehicle vehicle);

        /// <summary>
        /// Gets a vehicle by id.
        /// </summary>
        /// <param name="id">Vehicle id.</param>
        /// <returns>The vehicle or null.</returns>
        Task<Vehicle> GetById(Guid id);

        /// <summary>
        /// Get available vehicles.
        /// </summary>
        /// <returns>The list of available vehicles.</returns>
        Task<IReadOnlyList<Vehicle>> GetAvailableVehicles();
    }
}
