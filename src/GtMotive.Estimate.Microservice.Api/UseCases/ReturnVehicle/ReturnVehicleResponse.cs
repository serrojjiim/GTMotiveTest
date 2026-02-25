using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    /// <summary>
    /// Response DTO for return vehicle use case.
    /// </summary>
    public sealed class ReturnVehicleResponse
    {
        /// <summary>Gets or sets rental id.</summary>
        [Required]
        public Guid RentalId { get; set; }

        /// <summary>Gets or sets vehicle id.</summary>
        [Required]
        public Guid VehicleId { get; set; }

        /// <summary>Gets or sets end date.</summary>
        [Required]
        public DateTime EndDate { get; set; }
    }
}
