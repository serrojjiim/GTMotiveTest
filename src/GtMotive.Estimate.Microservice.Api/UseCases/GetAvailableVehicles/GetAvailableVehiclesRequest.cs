using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Request DTO to get available vehicles.
    /// </summary>
    public class GetAvailableVehiclesRequest : IRequest<IWebApiPresenter>
    {
    }
}
