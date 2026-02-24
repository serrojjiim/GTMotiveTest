using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// UseCase create new vehicle.
    /// </summary>
    /// <param name="vehicleRepository">Vehicle repository.</param>
    /// <param name="outputPort">Output port.</param>
    /// <param name="logger">Logger.</param>
    public class CreateVehicleUseCase(
        IVehicleRepository vehicleRepository,
        ICreateVehicleOutputPort outputPort,
        IAppLogger<CreateVehicleUseCase> logger) : ICreateVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        private readonly ICreateVehicleOutputPort _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        private readonly IAppLogger<CreateVehicleUseCase> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        /// <inheritdoc/>
        public async Task Execute(CreateVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            _logger.LogInformation(
                "Creating vehicle Brand={brand}, Model={model}, LicensePlate={plate}, ManufacturingDate={date}",
                input.Brand,
                input.Model,
                input.LicensePlate,
                input.ManufacturingDate);

            var vehicle = new Vehicle(
                Guid.NewGuid(),
                input.Brand,
                input.Model,
                input.LicensePlate,
                input.ManufacturingDate);

            await _vehicleRepository.Add(vehicle);

            _logger.LogInformation("Vehicle created with id={vehicleId}", vehicle.Id);

            var output = new CreateVehicleOutput(
                vehicle.Id,
                vehicle.Brand,
                vehicle.Model,
                vehicle.LicensePlate,
                vehicle.ManufacturingDate);

            _outputPort.StandardHandle(output);
        }
    }
}
