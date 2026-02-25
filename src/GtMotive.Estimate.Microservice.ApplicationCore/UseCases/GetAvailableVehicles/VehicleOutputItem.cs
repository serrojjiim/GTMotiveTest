using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Represents a vehicle item in the output.
    /// </summary>
    /// <param name="vehicleId">Vehicle vehicle id.</param>
    /// <param name="brand">Vehicle brand.</param>
    /// <param name="model">Vehicle model.</param>
    /// <param name="licensePlate">Vehicle license plate.</param>
    /// <param name="manufacturingDate">Vehicle manufacturing date.</param>
    public class VehicleOutputItem(Guid vehicleId, string brand, string model, string licensePlate, DateTime manufacturingDate)
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
