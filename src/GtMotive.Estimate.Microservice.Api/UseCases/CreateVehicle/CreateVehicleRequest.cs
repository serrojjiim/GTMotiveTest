using System;
using System.ComponentModel.DataAnnotations;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle
{
    /// <summary>
    /// Request DTO create a new vehicle.
    /// </summary>
    public class CreateVehicleRequest : IRequest<IWebApiPresenter>
    {
        /// <summary>
        /// Gets or sets brand.
        /// </summary>
        [Required]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets model.
        /// </summary>
        [Required]
        public string Model { get; set; }

        /// <summary>
        /// Gets or sets license plate.
        /// </summary>
        [Required]
        public string LicensePlate { get; set; }

        /// <summary>
        /// Gets or sets manufacturing date.
        /// </summary>
        [Required]
        public DateTime? ManufacturingDate { get; set; }
    }
}
