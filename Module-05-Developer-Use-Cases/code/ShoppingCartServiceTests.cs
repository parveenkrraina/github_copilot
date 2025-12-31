using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;
using Xunit;
using ShoppingCart.Services;

namespace ShoppingCart.Tests
{
    public class ShoppingCartServiceTests :  IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<ILogger<ShoppingCartService>> _loggerMock;
        private readonly ShoppingCartService _service;

        public ShoppingCartServiceTests()
        {
            // Setup in-memory database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName:  Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _loggerMock = new Mock<ILogger<ShoppingCartService>>();
            _service = new ShoppingCartService(_context, _loggerMock. Object);

            // Seed test data
            SeedTestData();
        }

        private void SeedTestData()
        {
            var products = new[]
            {
                new Product { Id = 1, Name = "Laptop", Price = 999. 99m, Description = "High-performance laptop" },
                new Product { Id = 2, Name = "Mouse", Price = 29.99m, Description = "Wireless mouse" },
                new Product { Id = 3, Name = "Keyboard", Price = 79.99m, Description = "Mechanical keyboard" }
            };

            _context.Products.AddRange(products);
            _context.SaveChanges();
        }

        [Fact]
        public async Task AddItemAsync_WithValidData_ShouldAddItemSuccessfully()
        {
            // Arrange
            var userId = "user123";
            var productId = 1;
            var quantity = 2;

            // Act
            var result = await _service.AddItemAsync(userId, productId, quantity);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.CartTotal.Should().BeGreaterThan(0);
            result.Message.Should().Be("Item added successfully");

            var cart = await _service. GetCartAsync(userId);
            cart.Should().NotBeNull();
            cart!.Items.Should().HaveCount(1);
            cart.Items. First().ProductId.Should().Be(productId);
            cart.Items.First().Quantity.Should().Be(quantity);
        }

        [Fact]
        public async Task AddItemAsync_WithExistingItem_ShouldIncreaseQuantity()
        {
            // Arrange
            var userId = "user456";
            var productId = 2;
            await _service.AddItemAsync(userId, productId, 1);

            // Act
            await _service.AddItemAsync(userId, productId, 2);

            // Assert
            var cart = await _service.GetCartAsync(userId);
            cart.Should().NotBeNull();
            cart!.Items.Should().HaveCount(1);
            cart.Items.First().Quantity.Should().Be(3);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task AddItemAsync_WithInvalidQuantity_ShouldThrowArgumentException(int quantity)
        {
            // Arrange
            var userId = "user789";
            var productId = 1;

            // Act
            Func<Task> act = async () => await _service.AddItemAsync(userId, productId, quantity);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Quantity must be positive*");
        }

        [Fact]
        public async Task AddItemAsync_WithNonExistentProduct_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var userId = "user999";
            var productId = 9999;
            var quantity = 1;

            // Act
            Func<Task> act = async () => await _service.AddItemAsync(userId, productId, quantity);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"Product with ID {productId} not found");
        }

        [Fact]
        public async Task RemoveItemAsync_WithExistingItem_ShouldRemoveSuccessfully()
        {
            // Arrange
            var userId = "user111";
            var productId = 1;
            await _service.AddItemAsync(userId, productId, 2);

            // Act
            var result = await _service.RemoveItemAsync(userId, productId);

            // Assert
            result. Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Message.Should().Be("Item removed successfully");

            var cart = await _service.GetCartAsync(userId);
            cart! .Items.Should().BeEmpty();
        }

        [Fact]
        public async Task RemoveItemAsync_WithNonExistentItem_ShouldReturnFailure()
        {
            // Arrange
            var userId = "user222";
            var productId = 999;
            await _service.AddItemAsync(userId, 1, 1); // Add different product

            // Act
            var result = await _service.RemoveItemAsync(userId, productId);

            // Assert
            result. Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Item not found in cart");
        }

        [Fact]
        public async Task RemoveItemAsync_WithNoCart_ShouldReturnFailure()
        {
            // Arrange
            var userId = "userWithNoCart";
            var productId = 1;

            // Act
            var result = await _service.RemoveItemAsync(userId, productId);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message.Should().Be("Cart not found");
        }

        [Fact]
        public async Task CalculateTotalAsync_WithMultipleItems_ShouldCalculateCorrectly()
        {
            // Arrange
            var userId = "user333";
            await _service.AddItemAsync(userId, 1, 1); // $999.99
            await _service. AddItemAsync(userId, 2, 2); // $29.99 * 2 = $59.98

            // Act
            var total = await _service.CalculateTotalAsync(userId);

            // Assert
            var expectedSubtotal = 999.99m + 59.98m; // 1059.97
            var expectedTax = expectedSubtotal * 0.08m; // 84.7976
            var expectedTotal = expectedSubtotal + expectedTax; // 1144.7676

            total.Should().NotBeNull();
            total. Subtotal.Should().Be(expectedSubtotal);
            total. Tax.Should().Be(expectedTax);
            total.TotalWithTax.Should().Be(expectedTotal);
        }

        [Fact]
        public async Task CalculateTotalAsync_WithEmptyCart_ShouldReturnZero()
        {
            // Arrange
            var userId = "userEmptyCart";

            // Act
            var total = await _service. CalculateTotalAsync(userId);

            // Assert
            total.Should().NotBeNull();
            total.Subtotal.Should().Be(0);
            total.Tax.Should().Be(0);
            total.TotalWithTax.Should().Be(0);
        }

        [Fact]
        public async Task GetCartAsync_WithItems_ShouldReturnCartWithProducts()
        {
            // Arrange
            var userId = "user444";
            await _service. AddItemAsync(userId, 1, 1);
            await _service.AddItemAsync(userId, 2, 3);

            // Act
            var cart = await _service.GetCartAsync(userId);

            // Assert
            cart.Should().NotBeNull();
            cart!.UserId.Should().Be(userId);
            cart.Items.Should().HaveCount(2);
            cart.Items.Should().OnlyContain(item => item.Product != null);
        }

        [Fact]
        public async Task GetCartAsync_WithNoCart_ShouldReturnNull()
        {
            // Arrange
            var userId = "userNoCart";

            // Act
            var cart = await _service.GetCartAsync(userId);

            // Assert
            cart.Should().BeNull();
        }

        [Fact]
        public async Task ClearCartAsync_WithItems_ShouldRemoveAllItems()
        {
            // Arrange
            var userId = "user555";
            await _service.AddItemAsync(userId, 1, 1);
            await _service.AddItemAsync(userId, 2, 2);
            await _service.AddItemAsync(userId, 3, 3);

            // Act
            var result = await _service.ClearCartAsync(userId);

            // Assert
            result.Should().BeTrue();

            var cart = await _service. GetCartAsync(userId);
            cart! .Items.Should().BeEmpty();
        }

        [Fact]
        public async Task ClearCartAsync_WithNoCart_ShouldReturnFalse()
        {
            // Arrange
            var userId = "userNothingToClear";

            // Act
            var result = await _service. ClearCartAsync(userId);

            // Assert
            result.Should().BeFalse();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}