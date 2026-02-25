using System.Collections.Generic;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.GetAvailableVehicles
{
    /// <summary>
    /// Output DTO for get available vehicles useCase.
    /// </summary>
    /// <param name="vehicles">List of available vehicles.</param>
    public class GetAvailableVehiclesOutput(IReadOnlyList<VehicleOutputItem> vehicles) : IUseCaseOutput
    {
        /// <summary>Gets list of available vehicles.</summary>
        public IReadOnlyList<VehicleOutputItem> Vehicles { get; } = vehicles;
    }
}
