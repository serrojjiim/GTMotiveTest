using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Repositories;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Use case for rent a vehicle.
    /// </summary>
    /// <param name="vehicleRepository">Vehicle repository.</param>
    /// <param name="rentalRepository">Rental repository.</param>
    /// <param name="outputPort">Output port.</param>
    /// <param name="logger">Logger.</param>
    public class RentVehicleUseCase(
        IVehicleRepository vehicleRepository,
        IRentalRepository rentalRepository,
        IRentVehicleOutputPort outputPort,
        IAppLogger<RentVehicleUseCase> logger) : IRentVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        private readonly IRentalRepository _rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
        private readonly IRentVehicleOutputPort _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        private readonly IAppLogger<RentVehicleUseCase> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        /// <inheritdoc/>
        public async Task Execute(RentVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            _logger.LogInformation(
                "Processing rental request: VehicleId={vehicleId}, CustomerId={customer}",
                input.VehicleId,
                input.CustomerIdentifier);

            // Only valid for vehicles because I haven't implemented Customer entity.
            // we will treat it as an external identifier. It could be an improvement to implement Customer and everything that goes with it.
            var vehicle = await _vehicleRepository.GetById(input.VehicleId);
            if (vehicle == null)
            {
                _logger.LogWarning("Vehicle not found: {vehicleId}", input.VehicleId);
                _outputPort.NotFoundHandle($"Vehicle with id {input.VehicleId} not found.");
                return;
            }

            // Business rule.
            var existingRental = await _rentalRepository.GetActiveRentalByCustomer(input.CustomerIdentifier);
            if (existingRental != null)
            {
                _logger.LogWarning(
                    "Customer {customer} already has active rental.",
                    input.CustomerIdentifier);
                throw new DomainException(
                    $"Customer '{input.CustomerIdentifier}' already has an active rental.");
            }

            var rental = new Rental(Guid.NewGuid(), vehicle.Id, input.CustomerIdentifier);

            // already throws DomainException if unavailable
            vehicle.MarkAsRented();

            await _rentalRepository.Add(rental);

            _logger.LogInformation(
                "Rental created with id = {rentalId}",
                rental.Id);

            var output = new RentVehicleOutput(
                rental.Id,
                rental.VehicleId,
                rental.CustomerIdentifier,
                rental.StartDate);

            _outputPort.StandardHandle(output);
        }
    }
}
