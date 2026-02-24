using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle
{
    /// <summary>
    /// Presenter for create  vehicle UseCase.
    /// </summary>
    public sealed class CreateVehiclePresenter : IWebApiPresenter, ICreateVehicleOutputPort
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; }

        /// <inheritdoc/>
        public void StandardHandle(CreateVehicleOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            ActionResult = new CreatedResult(
                $"/api/vehicles/{response.VehicleId}",
                new CreateVehicleResponse
                {
                    VehicleId = response.VehicleId,
                    Brand = response.Brand,
                    Model = response.Model,
                    LicensePlate = response.LicensePlate,
                    ManufacturingDate = response.ManufacturingDate,
                });
        }
    }
}
