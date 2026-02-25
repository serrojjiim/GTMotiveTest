using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public sealed class VehiclesController(IMediator mediator, IAppLogger<VehiclesController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly IAppLogger<VehiclesController> _logger = logger;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody][Required] CreateVehicleRequest request)
        {
            _logger.LogInformation("POST /api/vehicles received");
            var presenter = await _mediator.Send(request);
            return presenter.ActionResult;
        }

        /// <summary>
        /// Gets all available vehicles.
        /// </summary>
        /// <returns>List of available vehicles.</returns>
        [HttpGet("available")]
        public async Task<IActionResult> GetAvailable()
        {
            _logger.LogDebug("Getting available vehicles.");

            var presenter = await _mediator.Send(new GetAvailableVehiclesRequest());
            return presenter.ActionResult;
        }
    }
}
