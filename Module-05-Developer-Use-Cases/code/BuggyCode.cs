using Microsoft.EntityFrameworkCore;
using System. Threading.Tasks;

namespace ShoppingCart.Buggy
{
    /// <summary>
    /// BUGGY CODE - Contains a null reference exception
    /// Bug: When a product is out of stock (Quantity = 0), 
    /// we still try to add it to cart without checking
    /// </summary>
    public class BuggyOrderService
    {
        private readonly ApplicationDbContext _context;

        public BuggyOrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        // BUG: NullReferenceException when product. Stock is null
        // BUG: Division by zero when trying to calculate discount percentage
        public async Task<OrderResult> CreateOrderAsync(string userId, int productId, int quantity)
        {
            var product = await _context.Products. FindAsync(productId);
            
            // BUG: Not checking if product is null
            var stock = product.Stock; // NullReferenceException if product doesn't exist
            
            if (stock < quantity)
            {
                return new OrderResult
                {
                    Success = false,
                    Message = "Insufficient stock"
                };
            }

            // BUG: Division by zero if originalPrice is 0
            var discount = (product. OriginalPrice - product.Price) / product.OriginalPrice * 100;

            var order = new Order
            {
                UserId = userId,
                ProductId = productId,
                Quantity = quantity,
                TotalPrice = product.Price * quantity,
                DiscountPercent = discount
            };

            _context.Orders.Add(order);
            
            // BUG: Not updating stock after order
            await _context.SaveChangesAsync();

            return new OrderResult
            {
                Success = true,
                OrderId = order.Id,
                Message = "Order created successfully"
            };
        }
    }

    public class OrderResult
    {
        public bool Success { get; set; }
        public int OrderId { get; set; }
        public string Message { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountPercent { get; set; }
    }

    public class ProductWithStock :  Product
    {
        public int Stock { get; set; }
        public decimal OriginalPrice { get; set; }
    }
}