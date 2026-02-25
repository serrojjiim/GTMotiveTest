using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// handler for get available vehicles.
    /// </summary>
    /// <param name="useCase">Use case.</param>
    /// <param name="presenter">Presenter.</param>
    public class GetAvailableVehiclesRequestHandler(IGetAvailableVehiclesUseCase useCase, GetAvailableVehiclesPresenter presenter) : IRequestHandler<GetAvailableVehiclesRequest, IWebApiPresenter>
    {
        private readonly IGetAvailableVehiclesUseCase _useCase = useCase;
        private readonly GetAvailableVehiclesPresenter _presenter = presenter;

        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(GetAvailableVehiclesRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(new GetAvailableVehiclesInput());
            return _presenter;
        }
    }
}
