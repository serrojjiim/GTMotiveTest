using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Input DTO for rent vehicle use case.
    /// </summary>
    /// <param name="vehicleId">vehicle id.</param>
    /// <param name="customerIdentifier">Customer id.</param>
    public class RentVehicleInput(Guid vehicleId, string customerIdentifier) : IUseCaseInput
    {
        /// <summary>Gets vehicle id.</summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>Gets customer id.</summary>
        public string CustomerIdentifier { get; } = customerIdentifier;
    }
}
