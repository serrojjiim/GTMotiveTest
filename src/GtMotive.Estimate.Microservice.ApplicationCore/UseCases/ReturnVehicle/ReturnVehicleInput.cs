using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Input DTO for return vehicle use case.
    /// </summary>
    /// <param name="rentalId">Rental id.</param>
    public class ReturnVehicleInput(Guid rentalId) : IUseCaseInput
    {
        /// <summary>Gets rental id.</summary>
        public Guid RentalId { get; } = rentalId;
    }
}
