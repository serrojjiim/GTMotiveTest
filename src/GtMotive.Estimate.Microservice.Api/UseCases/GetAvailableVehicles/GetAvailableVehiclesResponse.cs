using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Response DTO for get available vehicles useCase.
    /// </summary>
    public sealed class GetAvailableVehiclesResponse
    {
        /// <summary>Gets or sets list of vehicles.</summary>
        [Required]
        public IReadOnlyList<VehicleResponseItem> Vehicles { get; set; }
    }
}
