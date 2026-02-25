using System;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    /// <summary>
    /// Response DTO for rent vehicle.
    /// </summary>
    public sealed class RentVehicleResponse
    {
        /// <summary>Gets or sets rental id.</summary>
        [Required]
        public Guid RentalId { get; set; }

        /// <summary>Gets or sets vehicle id.</summary>
        [Required]
        public Guid VehicleId { get; set; }

        /// <summary>Gets or sets customer id.</summary>
        [Required]
        public string CustomerIdentifier { get; set; }

        /// <summary>Gets or sets start date.</summary>
        [Required]
        public DateTime StartDate { get; set; }
    }
}
