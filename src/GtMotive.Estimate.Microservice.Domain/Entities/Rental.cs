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
            ValidateEmptyId(id, "Rental id");
            ValidateEmptyId(vehicleId, "Vehicle id");

            if (string.IsNullOrWhiteSpace(customerIdentifier))
            {
                throw new DomainException("Customer id  required.");
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
        /// Gets rental end date (null while active).
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether rental is active.
        /// </summary>
        public bool IsActive => EndDate == null;

        /// <summary>
        /// Finish the rental setting end date.
        /// </summary>
        public void FinishRental()
        {
            if (!IsActive)
            {
                throw new DomainException(
                    $"Rental '{Id}' is already finished (ended on {EndDate:yyyy-MM-dd HH:mm}).");
            }

            EndDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Checks if this rental belongs to the specified customer.
        /// </summary>
        /// <param name="customerIdentifier">customer id.</param>
        /// <returns>True if the rental belongs to this customer.</returns>
        public bool BelongsToCustomer(string customerIdentifier)
        {
            return string.Equals(CustomerIdentifier, customerIdentifier, StringComparison.OrdinalIgnoreCase);
        }

        private static void ValidateEmptyId(Guid id, string name)
        {
            if (id == Guid.Empty)
            {
                throw new DomainException($"{name} can't be empty.");
            }
        }
    }
}
