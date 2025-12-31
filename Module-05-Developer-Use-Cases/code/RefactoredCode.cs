using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Refactored
{
    /// <summary>
    /// REFACTORED CODE - Modern C# implementation
    /// Improvements:
    /// - Async/await pattern for better scalability
    /// - LINQ with Include to prevent N+1 queries
    /// - Proper error handling with specific exceptions
    /// - ILogger for structured logging
    /// - Null-coalescing and modern C# features
    /// - Early returns to reduce nesting
    /// </summary>
    public class RefactoredCartService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<RefactoredCartService> _logger;

        public RefactoredCartService(ApplicationDbContext context, ILogger<RefactoredCartService> logger)
        {
            _context = context ??  throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Gets the cart total with tax calculation
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>CartTotal with subtotal and tax, or null if cart doesn't exist</returns>
        public async Task<CartTotal? > GetCartTotalAsync(string userId)
        {
            try
            {
                var cart = await _context.Carts
                    .Include(c => c.Items)
                    .ThenInclude(i => i.Product)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null || ! cart.Items.Any())
                {
                    _logger.LogInformation("No cart found for user {UserId}", userId);
                    return null;
                }

                var subtotal = cart.Items.Sum(item => item.Product.Price * item.Quantity);
                var tax = subtotal * 0.08m;

                _logger.LogInformation(
                    "Calculated cart total for user {UserId}: Subtotal={Subtotal}, Tax={Tax}",
                    userId, subtotal, tax);

                return new CartTotal
                {
                    Subtotal = subtotal,
                    Tax = tax,
                    TotalWithTax = subtotal + tax
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating cart total for user {UserId}", userId);
                throw;
            }
        }

        /// <summary>
        /// Adds an item to the user's cart
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <param name="productId">The product to add</param>
        /// <param name="quantity">The quantity to add</param>
        /// <returns>True if successful</returns>
        /// <exception cref="InvalidOperationException">Thrown when product doesn't exist</exception>
        /// <exception cref="ArgumentException">Thrown when quantity is invalid</exception>
        public async Task<bool> AddItemToCartAsync(string userId, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be positive", nameof(quantity));
            }

            var product = await _context.Products. FindAsync(productId);
            if (product == null)
            {
                _logger.LogWarning("Product {ProductId} not found", productId);
                throw new InvalidOperationException($"Product {productId} not found");
            }

            var cart = await _context.Carts
                . Include(c => c.Items)
                .FirstOrDefaultAsync(c => c. UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime. UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.Carts.Add(cart);
                _logger.LogInformation("Created new cart for user {UserId}", userId);
            }

            var existingItem = cart.Items. FirstOrDefault(i => i.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.UpdatedAt = DateTime.UtcNow;
                _logger.LogInformation(
                    "Updated quantity for product {ProductId} in cart.  New quantity: {Quantity}",
                    productId, existingItem.Quantity);
            }
            else
            {
                var newItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    CreatedAt = DateTime. UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                cart.Items.Add(newItem);
                _logger.LogInformation("Added product {ProductId} to cart", productId);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Gets all items in the user's cart with product details
        /// </summary>
        /// <param name="userId">The user identifier</param>
        /// <returns>List of cart item details</returns>
        public async Task<List<CartItemDetail>> GetCartItemsAsync(string userId)
        {
            try
            {
                var cart = await _context.Carts
                    .Include(c => c.Items)
                    .ThenInclude(i => i. Product)
                    .FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    _logger.LogInformation("No cart found for user {UserId}", userId);
                    return new List<CartItemDetail>();
                }

                var details = cart.Items.Select(item => new CartItemDetail
                {
                    ProductName = item.Product. Name,
                    Price = item.Product.Price,
                    Quantity = item.Quantity,
                    Subtotal = item.Product.Price * item. Quantity
                }).ToList();

                _logger.LogInformation(
                    "Retrieved {Count} items from cart for user {UserId}",
                    details.Count, userId);

                return details;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving cart items for user {UserId}", userId);
                throw;
            }
        }
    }
}