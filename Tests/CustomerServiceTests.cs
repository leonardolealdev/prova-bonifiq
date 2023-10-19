using Microsoft.EntityFrameworkCore;
using Moq;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        [Fact]
        public async Task CanPurchase_InvalidCustomerId_ThrowsArgumentException()
        {
            // Arrange
            var customerService = new CustomerService(null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => customerService.CanPurchase(0, 100.00m));
        }

        [Fact]
        public async Task CanPurchase_InvalidPurchaseValue_ThrowsArgumentException()
        {
            // Arrange
            var customerService = new CustomerService(null);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => customerService.CanPurchase(1, 0.00m));
        }

        [Fact]
        public async Task CanPurchase_NonRegisteredCustomer_ReturnsFalse()
        {
            // Arrange
            var customerId = 1;
            var purchaseValue = 100.00m;
            var dbContext = new Mock<TestDbContext>();
            dbContext.Setup(db => db.Customers.FindAsync(customerId)).ReturnsAsync((Customer)null);
            var customerService = new CustomerService(dbContext.Object);

            // Act
            var result = await customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CanPurchase_CustomerPurchasedThisMonth_ReturnsFalse()
        {
            // Arrange
            var customerId = 1;
            var purchaseValue = 100.00m;
            var dbContext = new Mock<TestDbContext>();
            dbContext.Setup(db => db.Customers.FindAsync(customerId)).ReturnsAsync(new Customer());
            var lastMonth = DateTime.UtcNow.AddMonths(-1);
            var customerService = new CustomerService(dbContext.Object);

            // Act
            var result = await customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CanPurchase_FirstTimeCustomerExceedsLimit_ReturnsFalse()
        {
            // Arrange
            var customerId = 1;
            var purchaseValue = 101.00m;
            var dbContext = new Mock<TestDbContext>();
            var customerService = new CustomerService(dbContext.Object);

            // Act
            var result = await customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task CanPurchase_AllConditionsMet_ReturnsTrue()
        {
            // Arrange
            var customerId = 1;
            var purchaseValue = 50.00m;
            var dbContext = new Mock<TestDbContext>();
            dbContext.Setup(db => db.Customers.FindAsync(customerId)).ReturnsAsync(new Customer());
            var lastMonth = DateTime.UtcNow.AddMonths(-1);
            var customerService = new CustomerService(dbContext.Object);

            // Act
            var result = await customerService.CanPurchase(customerId, purchaseValue);

            // Assert
            Assert.True(result);
        }
    }
}
