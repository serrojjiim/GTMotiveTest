using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Output DTO for return vehicle use case.
    /// </summary>
    /// <param name="rentalId">Rental id.</param>
    /// <param name="vehicleId">Vehicle id.</param>
    /// <param name="endDate">End date.</param>
    public class ReturnVehicleOutput(Guid rentalId, Guid vehicleId, DateTime endDate) : IUseCaseOutput
    {
        /// <summary>Gets rental id.</summary>
        public Guid RentalId { get; } = rentalId;

        /// <summary>Gets vehicle id.</summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>Gets end date.</summary>
        public DateTime EndDate { get; } = endDate;
    }
}
