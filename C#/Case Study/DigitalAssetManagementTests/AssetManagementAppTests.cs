using NUnit.Framework;
using DigitalAssetManagement.Dao;
using DigitalAssetManagement.Entity;
using System;
using static DigitalAssetManagement.MyExceptions.Exception;

namespace DigitalAssetManagement.Tests
{
    [TestFixture]
    public class AssetManagementAppTests
    {
        private IAssetManagementService _service;

        [SetUp]
        public void Setup()
        {
            _service = new AssetManagementServiceImpl("AppSettings.json");
        }

        // Test case for asset creation (Add Asset)
        [Test]
        public void AddAsset_ShouldAddAssetSuccessfully()
        {
            // Arrange
            Asset newAsset = new Asset
            {
                Name = "Laptop",
                Type = "Electronics",
                SerialNumber = "20231001204",
                PurchaseDate = DateTime.Now,  // Use DateTime directly
                Location = "Office",
                Status = "Repair",
                OwnerId = 101
            };

            // Act
            bool result = _service.AddAsset(newAsset);

            // Assert
            Assert.IsTrue(result, "Asset should be added successfully.");
        }

        // Test case for adding asset to maintenance
        [Test]
        public void PerformMaintenance_ShouldAddMaintenanceSuccessfully()
        {
            // Arrange
            MaintenanceRecord maintenance = new MaintenanceRecord
            {
                AssetId = 201,
                MaintenanceDate = DateTime.Now,  // Use DateTime directly
                Description = "Hardware check",
                Cost = 100
            };

            // Act
            bool result = _service.PerformMaintenance(maintenance.AssetId, maintenance.MaintenanceDate.ToString("yyyy-MM-dd"), maintenance.Description, maintenance.Cost);

            // Assert
            Assert.IsTrue(result, "Maintenance should be added successfully.");
        }

        // Test case for asset reservation
        [Test]
        public void ReserveAsset_ShouldReserveAssetSuccessfully()
        {
            // Arrange
            Reservation reservation = new Reservation
            {
                AssetId = 202,
                EmployeeId = 102,
                ReservationDate = DateTime.Now,  // Use DateTime directly
                StartDate = DateTime.Now.AddDays(1),  // Use DateTime directly
                EndDate = DateTime.Now.AddDays(7)  // Use DateTime directly
            };

            // Act
            bool result = _service.ReserveAsset(reservation.AssetId, reservation.EmployeeId, reservation.ReservationDate.ToString("yyyy-MM-dd"), reservation.StartDate.ToString("yyyy-MM-dd"), reservation.EndDate.ToString("yyyy-MM-dd"));

            // Assert
            Assert.IsTrue(result, "Asset should be reserved successfully.");
        }

        // Test case for handling exception when asset ID or employee ID not found
        [Test]
        public void DeleteAsset_ShouldThrowExceptionWhenAssetNotFound()
        {
            // Arrange
            int invalidAssetId = 9999; // Non-existent AssetId

            // Act & Assert
            var ex = Assert.Throws<AssetNotFoundException>(() => _service.DeleteAsset(invalidAssetId));
            Assert.AreEqual("Asset not found.", ex.Message);
        }


        [Test]
        public void ReserveAsset_ShouldThrowExceptionWhenEmployeeIdNotFound()
        {
            // Arrange
            Reservation reservation = new Reservation
            {
                AssetId = 201,
                EmployeeId = 1022, // Invalid EmployeeId (non-existent)
                ReservationDate = DateTime.Now,  // Use DateTime directly
                StartDate = DateTime.Now.AddDays(1),  // Use DateTime directly
                EndDate = DateTime.Now.AddDays(7)  // Use DateTime directly
            };

            // Act & Assert
            var ex = Assert.Throws<EmployeeNotFoundException>(() =>
                _service.ReserveAsset(
                    reservation.AssetId,
                    reservation.EmployeeId,
                    reservation.ReservationDate.ToString("yyyy-MM-dd"),
                    reservation.StartDate.ToString("yyyy-MM-dd"),
                    reservation.EndDate.ToString("yyyy-MM-dd")
                )
            );

            Assert.AreEqual("Employee not found.", ex.Message);
        }

    }
}
