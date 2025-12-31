using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Moq;

namespace ShoppingCart.Fixed
{
    /// <summary>
    /// FIXED CODE - All bugs resolved with proper error handling
    /// </summary>
    public class FixedOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<FixedOrderService> _logger;

        public FixedOrderService(ApplicationDbContext context, ILogger<FixedOrderService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Creates an order with proper validation and stock management
        /// </summary>
        public async Task<OrderResult> CreateOrderAsync(string userId, int productId, int quantity)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User ID cannot be empty", nameof(userId));
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be positive", nameof(quantity));
            }

            // FIX: Check if product exists before accessing properties
            var product = await _context.Products
                . OfType<ProductWithStock>()
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                _logger.LogWarning("Product {ProductId} not found", productId);
                return new OrderResult
                {
                    Success = false,
                    Message = "Product not found"
                };
            }

            // FIX: Check stock availability
            if (product.Stock < quantity)
            {
                _logger.LogWarning(
                    "Insufficient stock for product {ProductId}.  Requested:  {Requested}, Available: {Available}",
                    productId, quantity, product.Stock);
                
                return new OrderResult
                {
                    Success = false,
                    Message = $"Insufficient stock. Only {product.Stock} available"
                };
            }

            // FIX:  Prevent division by zero when calculating discount
            decimal discount = 0;
            if (product. OriginalPrice > 0)
            {
                discount = (product.OriginalPrice - product.Price) / product.OriginalPrice * 100;
            }
            else
            {
                _logger.LogWarning("Product {ProductId} has invalid original price", productId);
            }

            var order = new Order
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity,
                TotalPrice = product.Price * quantity,
                DiscountPercent = discount,
                CreatedAt = DateTime. UtcNow
            };

            _context.Orders.Add(order);

            // FIX: Update stock after order
            product.Stock -= quantity;
            product.UpdatedAt = DateTime. UtcNow;

            await _context.SaveChangesAsync();

            _logger.LogInformation(
                "Order {OrderId} created successfully for user {UserId}.  Product: {ProductId}, Quantity:  {Quantity}",
                order.Id, userId, productId, quantity);

            return new OrderResult
            {
                Success = true,
                OrderId = order. Id,
                Message = "Order created successfully"
            };
        }
    }

    // TESTS FOR BUG FIXES
    public class FixedOrderServiceTests : IDisposable
    {
        private readonly ApplicationDbContext _context;
        private readonly Mock<ILogger<FixedOrderService>> _loggerMock;
        private readonly FixedOrderService _service;

        public FixedOrderServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName:  Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _loggerMock = new Mock<ILogger<FixedOrderService>>();
            _service = new FixedOrderService(_context, _loggerMock.Object);

            SeedTestData();
        }

        private void SeedTestData()
        {
            var products = new[]
            {
                new ProductWithStock
                {
                    Id = 1,
                    Name = "Laptop",
                    Price = 899.99m,
                    OriginalPrice = 999.99m,
                    Stock = 10
                },
                new ProductWithStock
                {
                    Id = 2,
                    Name = "Mouse",
                    Price = 25.00m,
                    OriginalPrice = 0, // Edge case: zero original price
                    Stock = 5
                },
                new ProductWithStock
                {
                    Id = 3,
                    Name = "Keyboard",
                    Price = 79.99m,
                    OriginalPrice = 79.99m,
                    Stock = 0 // Edge case: out of stock
                }
            };

            _context.Products.AddRange(products);
            _context.SaveChanges();
        }

        [Fact]
        public async Task CreateOrderAsync_WithValidData_ShouldCreateOrderAndUpdateStock()
        {
            // Arrange
            var userId = "user123";
            var productId = 1;
            var quantity = 2;

            // Act
            var result = await _service.CreateOrderAsync(userId, productId, quantity);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.OrderId.Should().BeGreaterThan(0);
            result.Message.Should().Be("Order created successfully");

            // Verify stock was updated
            var product = await _context.Products. FindAsync(productId) as ProductWithStock;
            product! .Stock.Should().Be(8); // 10 - 2 = 8
        }

        [Fact]
        public async Task CreateOrderAsync_WithNonExistentProduct_ShouldReturnFailure()
        {
            // Arrange - This test reproduces the original NullReferenceException bug
            var userId = "user456";
            var productId = 9999;
            var quantity = 1;

            // Act
            var result = await _service.CreateOrderAsync(userId, productId, quantity);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result. Message.Should().Be("Product not found");
        }

        [Fact]
        public async Task CreateOrderAsync_WithInsufficientStock_ShouldReturnFailure()
        {
            // Arrange
            var userId = "user789";
            var productId = 2; // Has stock of 5
            var quantity = 10; // Requesting more than available

            // Act
            var result = await _service.CreateOrderAsync(userId, productId, quantity);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result.Message. Should().Contain("Insufficient stock");
        }

        [Fact]
        public async Task CreateOrderAsync_WithZeroOriginalPrice_ShouldNotThrowDivisionByZero()
        {
            // Arrange - This test reproduces the division by zero bug
            var userId = "user111";
            var productId = 2; // Has OriginalPrice = 0
            var quantity = 1;

            // Act
            var result = await _service.CreateOrderAsync(userId, productId, quantity);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            
            var order = await _context.Orders. FirstOrDefaultAsync(o => o.Id == result.OrderId);
            order!.DiscountPercent.Should().Be(0); // Should default to 0, not throw
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public async Task CreateOrderAsync_WithInvalidQuantity_ShouldThrowArgumentException(int quantity)
        {
            // Arrange
            var userId = "user222";
            var productId = 1;

            // Act
            Func<Task> act = async () => await _service.CreateOrderAsync(userId, productId, quantity);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Quantity must be positive*");
        }

        [Fact]
        public async Task CreateOrderAsync_WithOutOfStockProduct_ShouldReturnFailure()
        {
            // Arrange
            var userId = "user333";
            var productId = 3; // Stock = 0
            var quantity = 1;

            // Act
            var result = await _service.CreateOrderAsync(userId, productId, quantity);

            // Assert
            result.Should().NotBeNull();
            result.Success.Should().BeFalse();
            result. Message.Should().Contain("Insufficient stock");
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}