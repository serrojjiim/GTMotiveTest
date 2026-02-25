using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    /// <summary>
    /// Handler for return a vehicle.
    /// </summary>
    /// <param name="useCase">Use case.</param>
    /// <param name="presenter">Presenter.</param>
    public class ReturnVehicleRequestHandler(IReturnVehicleUseCase useCase, ReturnVehiclePresenter presenter) : IRequestHandler<ReturnVehicleRequest, IWebApiPresenter>
    {
        private readonly IReturnVehicleUseCase _useCase = useCase;
        private readonly ReturnVehiclePresenter _presenter = presenter;

        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(ReturnVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new ReturnVehicleInput(request.RentalId.Value);
            await _useCase.Execute(input);
            return _presenter;
        }
    }
}
