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
            ValidateEmptyId(id);
            ValidateEmptyString(brand, nameof(brand));
            ValidateEmptyString(model, nameof(model));
            ValidateEmptyString(licensePlate, nameof(licensePlate));
            ValidateManufacturingDate(manufacturingDate);

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
        /// Gets a value indicating whether the vehicle is available.
        /// Private setter to ensures changes only through domain.
        /// </summary>
        public bool IsAvailable { get; private set; }

        /// <summary>
        /// Marks vehicle rented.
        /// </summary>
        public void MarkAsRented()
        {
            if (!IsAvailable)
            {
                throw new DomainException($"Vehicle '{LicensePlate}' isn't available.");
            }

            IsAvailable = false;
        }

        /// <summary>
        /// Marks the vehicle available.
        /// </summary>
        public void MarkAsAvailable()
        {
            if (IsAvailable)
            {
                throw new DomainException($"Vehicle '{LicensePlate}' is already available.");
            }

            IsAvailable = true;
        }

        private static void ValidateEmptyId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new DomainException("Vehicle identifier can't be empty.");
            }
        }

        private static void ValidateEmptyString(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException($"{paramName} is required and can't be empty.");
            }
        }

        private static void ValidateManufacturingDate(DateTime manufacturingDate)
        {
            if (manufacturingDate > DateTime.UtcNow)
            {
                throw new DomainException("Vehicle manufacturing date can't be in the future.");
            }

            var oldestAllowed = DateTime.UtcNow.AddYears(-MaxFleetAgeInYears);
            if (manufacturingDate < oldestAllowed)
            {
                throw new DomainException(
                    $"Vehicle can't have more than {MaxFleetAgeInYears} years. " +
                    $"Max years allowed: {MaxFleetAgeInYears}.");
            }
        }
    }
}
