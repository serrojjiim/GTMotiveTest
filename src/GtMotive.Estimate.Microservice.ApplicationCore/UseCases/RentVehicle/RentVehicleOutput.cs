using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Output DTO for rent vehicle use case.
    /// </summary>
    /// <param name="rentalId">Rental id.</param>
    /// <param name="vehicleId">Vehicle id.</param>
    /// <param name="customerIdentifier">Customer id.</param>
    /// <param name="startDate">Start date.</param>
    public class RentVehicleOutput(Guid rentalId, Guid vehicleId, string customerIdentifier, DateTime startDate) : IUseCaseOutput
    {
        /// <summary>Gets rental id.</summary>
        public Guid RentalId { get; } = rentalId;

        /// <summary>Gets vehicle id.</summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>Gets customer id.</summary>
        public string CustomerIdentifier { get; } = customerIdentifier;

        /// <summary>Gets start date.</summary>
        public DateTime StartDate { get; } = startDate;
    }
}
