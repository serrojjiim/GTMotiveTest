using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle
{
    /// <summary>
    /// Handler for create vehicle.
    /// </summary>
    /// <param name="useCase">The UseCase.</param>
    /// <param name="presenter">presenter.</param>
    public class CreateVehicleRequestHandler(ICreateVehicleUseCase useCase, CreateVehiclePresenter presenter) : IRequestHandler<CreateVehicleRequest, IWebApiPresenter>
    {
        private readonly ICreateVehicleUseCase _useCase = useCase;
        private readonly CreateVehiclePresenter _presenter = presenter;

        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(CreateVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new CreateVehicleInput(
                request.Brand,
                request.Model,
                request.LicensePlate,
                request.ManufacturingDate.Value);

            await _useCase.Execute(input);
            return _presenter;
        }
    }
}
