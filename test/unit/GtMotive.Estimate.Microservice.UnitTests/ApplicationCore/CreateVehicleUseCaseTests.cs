using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Repositories;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore
{
    public class CreateVehicleUseCaseTests
    {
        private readonly Mock<IVehicleRepository> _repoMock;
        private readonly Mock<ICreateVehicleOutputPort> _outputMock;
        private readonly Mock<IAppLogger<CreateVehicleUseCase>> _loggerMock;
        private readonly CreateVehicleUseCase _useCase;

        public CreateVehicleUseCaseTests()
        {
            _repoMock = new Mock<IVehicleRepository>();
            _outputMock = new Mock<ICreateVehicleOutputPort>();
            _loggerMock = new Mock<IAppLogger<CreateVehicleUseCase>>();
            _useCase = new CreateVehicleUseCase(_repoMock.Object, _outputMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Execute_WithValidInput_ShouldPersistAndNotify()
        {
            var input = new CreateVehicleInput("Seat", "Ibiza", "1234FNK", DateTime.UtcNow.AddYears(-1));

            await _useCase.Execute(input);

            _repoMock.Verify(r => r.Add(It.Is<Vehicle>(v => v.Brand == "Seat" && v.IsAvailable)), Times.Once);
            _outputMock.Verify(p => p.StandardHandle(It.IsAny<CreateVehicleOutput>()), Times.Once);
        }

        [Fact]
        public async Task Execute_WithOldDate_ShouldThrowException()
        {
            var input = new CreateVehicleInput("Seat", "Ibiza", "1234FNK", DateTime.UtcNow.AddYears(-6));

            await Assert.ThrowsAsync<DomainException>(() => _useCase.Execute(input));
            _repoMock.Verify(r => r.Add(It.IsAny<Vehicle>()), Times.Never);
        }
    }
}
