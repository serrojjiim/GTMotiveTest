using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.Api.UseCases.CreateVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.GetAvailableVehicles;
using GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Specs
{
    public sealed class RentalWorkflowTests(CompositionRootTestFixture fixture) : FunctionalTestBase(fixture)
    {
        [Fact]
        public async Task FullWorkflow_CreateRentReturnAndVerify()
        {
            var vehicleId = Guid.Empty;
            var rentalId = Guid.Empty;

            // Create vehicle
            await Fixture.UsingHandlerForRequestResponse<CreateVehicleRequest, IWebApiPresenter>(async handler =>
            {
                var request = new CreateVehicleRequest
                {
                    Brand = "Seat",
                    Model = "Ibiza",
                    LicensePlate = "0187LTY",
                    ManufacturingDate = DateTime.UtcNow.AddYears(-1),
                };

                var result = await handler.Handle(request, default);

                var created = Assert.IsType<CreatedResult>(result.ActionResult);
                var response = Assert.IsType<CreateVehicleResponse>(created.Value);
                Assert.Equal(request.Brand, response.Brand);
                vehicleId = response.VehicleId;
            });

            // Rent vehicle
            await Fixture.UsingHandlerForRequestResponse<RentVehicleRequest, IWebApiPresenter>(async handler =>
            {
                var request = new RentVehicleRequest
                {
                    VehicleId = vehicleId,
                    CustomerIdentifier = "customerSergio",
                };

                var result = await handler.Handle(request, default);

                var ok = Assert.IsType<OkObjectResult>(result.ActionResult);
                var response = Assert.IsType<RentVehicleResponse>(ok.Value);
                Assert.Equal(vehicleId, response.VehicleId);
                rentalId = response.RentalId;
            });

            // Vehicle shouldn't be available
            await Fixture.UsingHandlerForRequestResponse<GetAvailableVehiclesRequest, IWebApiPresenter>(async handler =>
            {
                var result = await handler.Handle(new GetAvailableVehiclesRequest(), default);
                var ok = Assert.IsType<OkObjectResult>(result.ActionResult);
                var response = Assert.IsType<GetAvailableVehiclesResponse>(ok.Value);

                Assert.DoesNotContain(response.Vehicles, v => v.VehicleId == vehicleId);
            });

            // Return  vehicle
            await Fixture.UsingHandlerForRequestResponse<ReturnVehicleRequest, IWebApiPresenter>(async handler =>
            {
                var request = new ReturnVehicleRequest { RentalId = rentalId };
                var result = await handler.Handle(request, default);

                var ok = Assert.IsType<OkObjectResult>(result.ActionResult);
                var response = Assert.IsType<ReturnVehicleResponse>(ok.Value);
                Assert.Equal(rentalId, response.RentalId);
            });

            // Vehicle should be available again
            await Fixture.UsingHandlerForRequestResponse<GetAvailableVehiclesRequest, IWebApiPresenter>(async handler =>
            {
                var result = await handler.Handle(new GetAvailableVehiclesRequest(), default);
                var ok = Assert.IsType<OkObjectResult>(result.ActionResult);
                var response = Assert.IsType<GetAvailableVehiclesResponse>(ok.Value);

                Assert.Contains(response.Vehicles, v => v.VehicleId == vehicleId);
            });
        }
    }
}
