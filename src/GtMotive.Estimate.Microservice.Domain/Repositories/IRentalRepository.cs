using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Repositories
{
    /// <summary>
    /// Repository interface for rentals.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Add rental.
        /// </summary>
        /// <param name="rental">Rental to add.</param>
        /// <returns>Task.</returns>
        Task Add(Rental rental);

        /// <summary>
        /// Get active rental by customer id.
        /// </summary>
        /// <param name="customerIdentifier">Customer id.</param>
        /// <returns>active rental or null.</returns>
        Task<Rental> GetActiveRentalByCustomer(string customerIdentifier);

        /// <summary>
        /// Get active rental by vehicle id.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <returns>Active rental or null.</returns>
        Task<Rental> GetActiveRentalByVehicle(Guid vehicleId);

        /// <summary>
        /// Gets a rental by id.
        /// </summary>
        /// <param name="rentalId">Rental id.</param>
        /// <returns>Rental or null.</returns>
        Task<Rental> GetById(Guid rentalId);

        /// <summary>
        /// Update rental.
        /// </summary>
        /// <param name="rental">Rental to update.</param>
        /// <returns>Task.</returns>
        Task Update(Rental rental);
    }
}
