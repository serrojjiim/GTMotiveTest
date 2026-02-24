using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Output DTO for create vehicle UseCase.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="CreateVehicleOutput"/> class.
    /// </remarks>
    /// <param name="vehicleId">The created vehicle id.</param>
    /// <param name="brand">Created vehicle brand.</param>
    /// <param name="model">Created vehicle model.</param>
    /// <param name="licensePlate">Created vehicle license plate.</param>
    /// <param name="manufacturingDate">Created vehicle manufacturing date.</param>
    public class CreateVehicleOutput(Guid vehicleId, string brand, string model, string licensePlate, DateTime manufacturingDate) : IUseCaseOutput
    {
        /// <summary>Gets vehicle id.</summary>
        public Guid VehicleId { get; } = vehicleId;

        /// <summary>Gets brand.</summary>
        public string Brand { get; } = brand;

        /// <summary>Gets model.</summary>
        public string Model { get; } = model;

        /// <summary>Gets license plate.</summary>
        public string LicensePlate { get; } = licensePlate;

        /// <summary>Gets manufacturing date.</summary>
        public DateTime ManufacturingDate { get; } = manufacturingDate;
    }
}
