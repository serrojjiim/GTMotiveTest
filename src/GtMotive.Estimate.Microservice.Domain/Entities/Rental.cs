using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Represents a vehicle rental.
    /// </summary>
    public class Rental
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class.
        /// </summary>
        /// <param name="id">Rental id.</param>
        /// <param name="vehicleId">Vehice id.</param>
        /// <param name="customerIdentifier">Customer id.</param>
        public Rental(Guid id, Guid vehicleId, string customerIdentifier)
        {
            if (id == Guid.Empty)
            {
                throw new DomainException("Rental id can't be empty.");
            }

            if (vehicleId == Guid.Empty)
            {
                throw new DomainException("Vehicle id can't be empty.");
            }

            if (string.IsNullOrWhiteSpace(customerIdentifier))
            {
                throw new DomainException("Customer id required.");
            }

            Id = id;
            VehicleId = vehicleId;
            CustomerIdentifier = customerIdentifier;
            StartDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Gets rental id.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets vehicle id.
        /// </summary>
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets customer id.
        /// </summary>
        public string CustomerIdentifier { get; }

        /// <summary>
        /// Gets rental start date.
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Gets or sets rental end date (null while active).
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets a value indicating whether rental is active.
        /// </summary>
        public bool IsActive => EndDate == null;
    }
}
