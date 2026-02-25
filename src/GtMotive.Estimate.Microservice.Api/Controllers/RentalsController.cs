using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public sealed class RentalsController(IMediator mediator, IAppLogger<RentalsController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IAppLogger<RentalsController> _logger = logger;

        [HttpPost]
        public async Task<IActionResult> Rent([FromBody][Required] RentVehicleRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            _logger.LogInformation("Rent vehicle {vehicleId}", request.VehicleId);
            var presenter = await _mediator.Send(request);
            return presenter.ActionResult;
        }

        [HttpPost("{id}/return")]
        public async Task<IActionResult> ReturnVehicle(Guid id)
        {
            _logger.LogDebug("Return Vehicle with rental id: {rentalId}", id);

            var request = new ReturnVehicleRequest { RentalId = id };
            var presenter = await _mediator.Send(request);
            return presenter.ActionResult;
        }
    }
}
