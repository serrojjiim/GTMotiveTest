using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Specs
{
    [Collection(TestCollections.TestServer)]
    public class VehiclesControllerTests(GenericInfrastructureTestServerFixture fixture) : InfrastructureTestBase(fixture)
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        [Fact]
        public async Task PostVehicle_WithValidModel_ShouldReturnCreated()
        {
            var client = Fixture.Server.CreateClient();
            var request = new
            {
                Brand = "Renault",
                Model = "Clio",
                LicensePlate = "9999ZZZ",
                ManufacturingDate = DateTime.UtcNow.AddYears(-1),
            };

            using var content = new StringContent(
                JsonSerializer.Serialize(request, JsonOptions),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(new Uri("/api/vehicles", UriKind.Relative), content);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task PostVehicle_WithOldDate_ShouldReturnBadRequest()
        {
            var client = Fixture.Server.CreateClient();
            var request = new
            {
                Brand = "Fiat",
                Model = "Punto",
                LicensePlate = "OLD0000",
                ManufacturingDate = DateTime.UtcNow.AddYears(-10),
            };

            using var content = new StringContent(
                JsonSerializer.Serialize(request, JsonOptions),
                Encoding.UTF8,
                "application/json");

            var response = await client.PostAsync(new Uri("/api/vehicles", UriKind.Relative), content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAvailableVehicles_ShouldReturnOk()
        {
            var client = Fixture.Server.CreateClient();

            var response = await client.GetAsync(new Uri("/api/vehicles/available", UriKind.Relative));

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
