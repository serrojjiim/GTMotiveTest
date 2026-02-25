using System;
using System.Linq;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// UseCase for get all available vehicles.
    /// </summary>
    /// <param name="vehicleRepository">Vehicle repository.</param>
    /// <param name="outputPort">Output port.</param>
    /// <param name="logger">Logger.</param>
    public class GetAvailableVehiclesUseCase(
        IVehicleRepository vehicleRepository,
        IGetAvailableVehiclesOutputPort outputPort,
        IAppLogger<GetAvailableVehiclesUseCase> logger) : IGetAvailableVehiclesUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        private readonly IGetAvailableVehiclesOutputPort _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        private readonly IAppLogger<GetAvailableVehiclesUseCase> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        /// <inheritdoc/>
        public async Task Execute(GetAvailableVehiclesInput input)
        {
            _logger.LogInformation("Retrieving available vehicles.");

            var vehicles = await _vehicleRepository.GetAvailableVehicles();

            _logger.LogInformation("Found {count} available vehicles.", vehicles.Count);

            var items = vehicles.Select(v => new VehicleOutputItem(
                v.Id,
                v.Brand,
                v.Model,
                v.LicensePlate,
                v.ManufacturingDate)).ToList();

            _outputPort.StandardHandle(new GetAvailableVehiclesOutput(items));
        }
    }
}
