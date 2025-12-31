using System;
using System. Linq;

namespace ShoppingCart.Legacy
{
    /// <summary>
    /// LEGACY CODE - BEFORE REFACTORING
    /// This code demonstrates common anti-patterns: 
    /// - Synchronous database operations
    /// - Deep nesting
    /// - Multiple database queries (N+1 problem)
    /// - Poor error handling
    /// - Lack of logging
    /// - No null-coalescing or modern C# features
    /// </summary>
    public class LegacyCartService
    {
        private readonly ApplicationDbContext db;

        public LegacyCartService(ApplicationDbContext context)
        {
            db = context;
        }

        // LEGACY METHOD - DO NOT USE
        public CartTotal GetCartTotal(int userId)
        {
            try
            {
                var user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (user != null)
                {
                    var cart = db.Carts. Where(c => c.UserId == userId).FirstOrDefault();
                    if (cart != null)
                    {
                        decimal total = 0;
                        var items = db.CartItems. Where(i => i.CartId == cart. Id).ToList();
                        foreach (var item in items)
                        {
                            var product = db.Products.Where(p => p.Id == item.ProductId).FirstOrDefault();
                            if (product != null)
                            {
                                total = total + (product.Price * item.Quantity);
                            }
                        }
                        return new CartTotal { Total = total, Tax = total * 0.08m };
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        // LEGACY METHOD - Adds item with poor error handling
        public bool AddItemToCart(int userId, int productId, int quantity)
        {
            try
            {
                var user = db.Users.Where(u => u.Id == userId).FirstOrDefault();
                if (user == null)
                {
                    return false;
                }

                var product = db.Products.Where(p => p.Id == productId).FirstOrDefault();
                if (product == null)
                {
                    return false;
                }

                var cart = db. Carts.Where(c => c.UserId == userId).FirstOrDefault();
                if (cart == null)
                {
                    cart = new Cart();
                    cart.UserId = userId;
                    cart.CreatedAt = DateTime.Now;
                    db.Carts.Add(cart);
                    db.SaveChanges();
                }

                var existingItem = db.CartItems.Where(ci => ci.CartId == cart.Id && ci.ProductId == productId).FirstOrDefault();
                if (existingItem != null)
                {
                    existingItem.Quantity = existingItem.Quantity + quantity;
                }
                else
                {
                    var newItem = new CartItem();
                    newItem.CartId = cart.Id;
                    newItem.ProductId = productId;
                    newItem. Quantity = quantity;
                    newItem.CreatedAt = DateTime. Now;
                    db.CartItems.Add(newItem);
                }

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console. WriteLine(ex.Message);
                return false;
            }
        }

        // LEGACY METHOD - Gets all items with N+1 problem
        public List<CartItemDetail> GetCartItems(int userId)
        {
            var result = new List<CartItemDetail>();

            try
            {
                var cart = db.Carts.Where(c => c.UserId == userId).FirstOrDefault();
                if (cart != null)
                {
                    var items = db.CartItems.Where(i => i.CartId == cart.Id).ToList();
                    for (int i = 0; i < items.Count; i++)
                    {
                        var item = items[i];
                        var product = db.Products.Where(p => p.Id == item.ProductId).FirstOrDefault();
                        if (product != null)
                        {
                            var detail = new CartItemDetail();
                            detail.ProductName = product.Name;
                            detail.Price = product.Price;
                            detail.Quantity = item. Quantity;
                            detail. Subtotal = product.Price * item.Quantity;
                            result.Add(detail);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }
    }

    public class CartItemDetail
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}