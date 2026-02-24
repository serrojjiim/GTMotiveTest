using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Input DTO for create vehicle useCase.
    /// </summary>
    /// <param name="brand">vehicle brand.</param>
    /// <param name="model">Vehicle model.</param>
    /// <param name="licensePlate">Vehicle license plate.</param>
    /// <param name="manufacturingDate">Vehicle manufacturing date.</param>
    public class CreateVehicleInput(string brand, string model, string licensePlate, DateTime manufacturingDate) : IUseCaseInput
    {
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
