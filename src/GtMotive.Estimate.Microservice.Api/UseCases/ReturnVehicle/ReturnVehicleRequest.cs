using System;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    /// <summary>
    /// Request DTO to return a vehicle.
    /// </summary>
    public class ReturnVehicleRequest : IRequest<IWebApiPresenter>
    {
        /// <summary>
        /// Gets or sets rental id.
        /// </summary>
        public Guid? RentalId { get; set; }
    }
}
