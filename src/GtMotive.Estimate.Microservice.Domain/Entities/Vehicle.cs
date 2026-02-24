using System;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Represents a vehicle in the fleet.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Max age of the car.
        /// </summary>
        public const int MaxFleetAgeInYears = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="id">Vehicle id.</param>
        /// <param name="brand">Vehicle brand.</param>
        /// <param name="model">Vehicle model.</param>
        /// <param name="licensePlate">vehicle licence plate.</param>
        /// <param name="manufacturingDate">Vehicle manufacturing date.</param>
        public Vehicle(Guid id, string brand, string model, string licensePlate, DateTime manufacturingDate)
        {
            if (id == Guid.Empty)
            {
                throw new DomainException("Vehicle identifier cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(brand))
            {
                throw new DomainException("Brand required.");
            }

            if (string.IsNullOrWhiteSpace(model))
            {
                throw new DomainException("Model required.");
            }

            if (string.IsNullOrWhiteSpace(licensePlate))
            {
                throw new DomainException("license plate required.");
            }

            if (manufacturingDate > DateTime.UtcNow)
            {
                throw new DomainException("Manufacturing date cannot be in the future.");
            }

            var oldest = DateTime.UtcNow.AddYears(-MaxFleetAgeInYears);
            if (manufacturingDate < oldest)
            {
                throw new DomainException(
                    $"Vehicle have more than {MaxFleetAgeInYears}. Manufacturing date must be after {oldest:yyyy-MM-dd}.");
            }

            Id = id;
            Brand = brand;
            Model = model;
            LicensePlate = licensePlate;
            ManufacturingDate = manufacturingDate;
            IsAvailable = true;
        }

        /// <summary>
        /// Gets vehicle id.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets vehicle brand.
        /// </summary>
        public string Brand { get; }

        /// <summary>
        /// Gets vehicle model.
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Gets vehicle licence plate.
        /// </summary>
        public string LicensePlate { get; }

        /// <summary>
        /// Gets vehicle manufacturing date.
        /// </summary>
        public DateTime ManufacturingDate { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the vehicle is available.
        /// </summary>
        public bool IsAvailable { get; set; }
    }
}
