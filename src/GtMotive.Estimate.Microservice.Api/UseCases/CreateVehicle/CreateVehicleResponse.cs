using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle
{
    /// <summary>
    /// Response  DTO for createvehicle UseCase.
    /// </summary>
    public sealed class CreateVehicleResponse
    {
        /// <summary>Gets or sets identifier.</summary>
        [Required]
        public Guid VehicleId { get; set; }

        /// <summary>Gets or sets brand.</summary>
        [Required]
        public string Brand { get; set; }

        /// <summary>Gets or sets model.</summary>
        [Required]
        public string Model { get; set; }

        /// <summary>Gets or sets license plate.</summary>
        [Required]
        public string LicensePlate { get; set; }

        /// <summary>Gets or sets manufacturing date.</summary>
        [Required]
        public DateTime ManufacturingDate { get; set; }
    }
}
