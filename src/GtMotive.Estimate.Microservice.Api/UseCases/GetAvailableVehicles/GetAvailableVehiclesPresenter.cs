using System;
using System.Linq;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Presenter for get available vehicles useCase.
    /// </summary>
    public sealed class GetAvailableVehiclesPresenter : IWebApiPresenter, IGetAvailableVehiclesOutputPort
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; }

        /// <inheritdoc/>
        public void StandardHandle(GetAvailableVehiclesOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

#pragma warning disable SA1010 // Opening square brackets should be spaced correctly
            var viewModel = new GetAvailableVehiclesResponse
            {
                Vehicles = [.. response.Vehicles.Select(v => new VehicleResponseItem
                {
                    VehicleId = v.VehicleId,
                    Brand = v.Brand,
                    Model = v.Model,
                    LicensePlate = v.LicensePlate,
                    ManufacturingDate = v.ManufacturingDate,
                })],
            };
#pragma warning restore SA1010 // Opening square brackets should be spaced correctly

            ActionResult = new OkObjectResult(viewModel);
        }
    }
}
