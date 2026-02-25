using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// A vehicle item in the response.
    /// </summary>
    public sealed class VehicleResponseItem
    {
        /// <summary>Gets or sets vehicle id.</summary>
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
