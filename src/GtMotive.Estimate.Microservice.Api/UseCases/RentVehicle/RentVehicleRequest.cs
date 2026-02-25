using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    /// <summary>
    /// Request DTO to rent vehicle.
    /// </summary>
    public class RentVehicleRequest : IRequest<IWebApiPresenter>
    {
        /// <summary>
        /// Gets or sets vehicle id.
        /// </summary>
        [Required]
        public Guid? VehicleId { get; set; }

        /// <summary>
        /// Gets or sets customer id.
        /// </summary>
        [Required]
        public string CustomerIdentifier { get; set; }
    }
}
