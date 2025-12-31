using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ShoppingCart.Services
{
    /// <summary>
    /// Service for managing shopping cart operations
    /// </summary>
    public class ShoppingCartService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ShoppingCartService> _logger;
        private const decimal TAX_RATE = 0.08m;

        public ShoppingCartService(ApplicationDbContext context, ILogger<ShoppingCartService> logger)
        {
            _context = context ??  throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Adds an item to the user's shopping cart
        /// </summary>
        /// <param name="userId">The user's unique identifier</param>
        /// <param name="productId">The product identifier to add</param>
        /// <param name="quantity">The quantity to add (must be positive)</param>
        /// <returns>CartResult indicating success and updated cart total</returns>
        /// <exception cref="ArgumentException">Thrown when quantity is not positive</exception>
        /// <exception cref="InvalidOperationException">Thrown when product doesn't exist</exception>
        public async Task<CartResult> AddItemAsync(string userId, int productId, int quantity)
        {
            if (quantity <= 0)
            {
                _logger.LogWarning("Attempted to add invalid quantity {Quantity} for user {UserId}", quantity, userId);
                throw new ArgumentException("Quantity must be positive", nameof(quantity));
            }

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                _logger.LogWarning("Product {ProductId} not found", productId);
                throw new InvalidOperationException($"Product with ID {productId} not found");
            }

            if (product.Price < 0)
            {
                _logger.LogError("Product {ProductId} has negative price {Price}", productId, product.Price);
                throw new InvalidOperationException("Product price cannot be negative");
            }

            var cart = await GetOrCreateCartAsync(userId);
            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cart. Id && ci.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                existingItem.UpdatedAt = DateTime.UtcNow;
                _logger.LogInformation("Updated quantity for product {ProductId} in cart {CartId}", productId, cart.Id);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.CartItems.Add(cartItem);
                _logger.LogInformation("Added new product {ProductId} to cart {CartId}", productId, cart.Id);
            }

            await _context.SaveChangesAsync();

            var total = await CalculateTotalAsync(userId);
            return new CartResult
            {
                Success = true,
                CartId = cart.Id,
                CartTotal = total. TotalWithTax,
                Message = "Item added successfully"
            };
        }

        /// <summary>
        /// Removes an item from the user's shopping cart
        /// </summary>
        /// <param name="userId">The user's unique identifier</param>
        /// <param name="productId">The product identifier to remove</param>
        /// <returns>CartResult indicating success and updated cart total</returns>
        public async Task<CartResult> RemoveItemAsync(string userId, int productId)
        {
            var cart = await _context.Carts
                . FirstOrDefaultAsync(c => c. UserId == userId);

            if (cart == null)
            {
                _logger.LogWarning("No cart found for user {UserId}", userId);
                return new CartResult
                {
                    Success = false,
                    Message = "Cart not found"
                };
            }

            var cartItem = await _context.CartItems
                . FirstOrDefaultAsync(ci => ci.CartId == cart.Id && ci.ProductId == productId);

            if (cartItem == null)
            {
                _logger.LogWarning("Product {ProductId} not found in cart {CartId}", productId, cart.Id);
                return new CartResult
                {
                    Success = false,
                    Message = "Item not found in cart"
                };
            }

            _context. CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Removed product {ProductId} from cart {CartId}", productId, cart. Id);

            var total = await CalculateTotalAsync(userId);
            return new CartResult
            {
                Success = true,
                CartId = cart.Id,
                CartTotal = total.TotalWithTax,
                Message = "Item removed successfully"
            };
        }

        /// <summary>
        /// Gets the user's shopping cart with all items
        /// </summary>
        /// <param name="userId">The user's unique identifier</param>
        /// <returns>Cart with items and product details</returns>
        public async Task<Cart? > GetCartAsync(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c. Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                _logger.LogInformation("No cart found for user {UserId}", userId);
            }

            return cart;
        }

        /// <summary>
        /// Calculates the total price of items in the cart including tax
        /// </summary>
        /// <param name="userId">The user's unique identifier</param>
        /// <returns>CartTotal with subtotal, tax, and total amounts</returns>
        public async Task<CartTotal> CalculateTotalAsync(string userId)
        {
            var cart = await _context.Carts
                . Include(c => c.Items)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null || ! cart.Items.Any())
            {
                return new CartTotal
                {
                    Subtotal = 0,
                    Tax = 0,
                    TotalWithTax = 0
                };
            }

            var subtotal = cart.Items.Sum(item => item.Product.Price * item.Quantity);
            var tax = subtotal * TAX_RATE;
            var totalWithTax = subtotal + tax;

            _logger.LogInformation("Calculated total for cart {CartId}: Subtotal={Subtotal}, Tax={Tax}, Total={Total}",
                cart.Id, subtotal, tax, totalWithTax);

            return new CartTotal
            {
                Subtotal = subtotal,
                Tax = tax,
                TotalWithTax = totalWithTax
            };
        }

        /// <summary>
        /// Clears all items from the user's cart
        /// </summary>
        /// <param name="userId">The user's unique identifier</param>
        /// <returns>True if cart was cleared, false if no cart exists</returns>
        public async Task<bool> ClearCartAsync(string userId)
        {
            var cart = await _context. Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c. UserId == userId);

            if (cart == null)
            {
                return false;
            }

            _context.CartItems.RemoveRange(cart.Items);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Cleared all items from cart {CartId}", cart.Id);
            return true;
        }

        private async Task<Cart> GetOrCreateCartAsync(string userId)
        {
            var cart = await _context.Carts
                . FirstOrDefaultAsync(c => c. UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context. Carts.Add(cart);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Created new cart {CartId} for user {UserId}", cart.Id, userId);
            }

            return cart;
        }
    }

    // Models
    public class CartResult
    {
        public bool Success { get; set; }
        public int CartId { get; set; }
        public decimal CartTotal { get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class CartTotal
    {
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal TotalWithTax { get; set; }
    }

    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<CartItem> Items { get; set; } = new();
    }

    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public Cart Cart { get; set; } = null!;
        public Product Product { get; set; } = null! ;
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    // DbContext (simplified)
    public class ApplicationDbContext :  DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}