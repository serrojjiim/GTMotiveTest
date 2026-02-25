using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Use case for return vehicle.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ReturnVehicleUseCase"/> class.
    /// </remarks>
    /// <param name="vehicleRepository">vehicle repository.</param>
    /// <param name="rentalRepository">Rental repository.</param>
    /// <param name="outputPort">Output port.</param>
    /// <param name="logger">Logger.</param>
    public class ReturnVehicleUseCase(
        IVehicleRepository vehicleRepository,
        IRentalRepository rentalRepository,
        IReturnVehicleOutputPort outputPort,
        IAppLogger<ReturnVehicleUseCase> logger) : IReturnVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        private readonly IRentalRepository _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
        private readonly IReturnVehicleOutputPort _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        private readonly IAppLogger<ReturnVehicleUseCase> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        /// <inheritdoc/>
        public async Task Execute(ReturnVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            _logger.LogInformation("Processing return for RentalId={rentalId}", input.RentalId);

            var rental = await _rentalRepository.GetById(input.RentalId);
            if (rental == null)
            {
                _logger.LogWarning("Rental not found: {rentalId}", input.RentalId);
                _outputPort.NotFoundHandle($"Rental with id {input.RentalId} not found.");
                return;
            }

            var vehicle = await _vehicleRepository.GetById(rental.VehicleId);
            if (vehicle == null)
            {
                _logger.LogWarning(
                    "Vehicle {vehicleId} used in rental {rentalId} not found.",
                    rental.VehicleId.ToString(),
                    rental.Id);
                _outputPort.NotFoundHandle($"Vehicle with id {rental.VehicleId} not found.");
                return;
            }

            rental.FinishRental();
            vehicle.MarkAsAvailable();

            await _rentalRepository.Update(rental);

            _logger.LogInformation(
                "Rental {rentalId} finished at {endDate}",
                rental.Id,
                rental.EndDate);

            var output = new ReturnVehicleOutput(
                rental.Id,
                rental.VehicleId,
                rental.EndDate.Value);

            _outputPort.StandardHandle(output);
        }
    }
}
