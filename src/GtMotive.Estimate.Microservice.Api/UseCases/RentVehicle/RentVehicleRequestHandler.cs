using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    /// <summary>
    /// Handler for rent vehicle.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="RentVehicleRequestHandler"/> class.
    /// </remarks>
    /// <param name="useCase">Use case.</param>
    /// <param name="presenter">Presenter.</param>
    public class RentVehicleRequestHandler(IRentVehicleUseCase useCase, RentVehiclePresenter presenter) : IRequestHandler<RentVehicleRequest, IWebApiPresenter>
    {
        private readonly IRentVehicleUseCase _useCase = useCase;
        private readonly RentVehiclePresenter _presenter = presenter;

        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(RentVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new RentVehicleInput(request.VehicleId.Value, request.CustomerIdentifier);
            await _useCase.Execute(input);
            return _presenter;
        }
    }
}
