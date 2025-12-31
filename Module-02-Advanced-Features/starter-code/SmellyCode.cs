using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.StarterCode
{
    // God Object - does too many things
    public class OrderManager
    {
        public List<Order> orders = new List<Order>();
        public List<User> users = new List<User>();
        public List<Product> products = new List<Product>();
        
        // Long method - more than 50 lines
        public decimal ProcessCompleteOrder(int userId, List<int> productIds, string paymentMethod, 
            string shippingAddress, string billingAddress, string couponCode)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            if (user == null) throw new Exception("User not found");
            
            var order = new Order();
            order.UserId = userId;
            order.Items = new List<OrderItem>();
            
            decimal subtotal = 0;
            foreach (var productId in productIds)
            {
                var product = products.FirstOrDefault(p => p.Id == productId);
                if (product == null) continue;
                
                var item = new OrderItem();
                item. ProductName = product.Name;
                item.Price = product.Price;
                item.Quantity = 1;
                order.Items.Add(item);
                subtotal += product.Price;
            }
            
            // Duplicated discount calculation logic
            decimal discount = 0;
            if (couponCode == "SAVE10")
            {
                discount = subtotal * 0.10m;
            }
            else if (couponCode == "SAVE20")
            {
                discount = subtotal * 0.20m;
            }
            else if (couponCode == "SAVE30")
            {
                discount = subtotal * 0.30m;
            }
            
            decimal tax = subtotal * 0.08m;
            decimal shipping = 10.00m;
            
            if (subtotal > 100)
            {
                shipping = 0; // Free shipping
            }
            
            decimal total = subtotal - discount + tax + shipping;
            order.Total = total;
            
            // Duplicated payment processing
            if (paymentMethod == "CreditCard")
            {
                Console.WriteLine("Processing credit card payment");
                // Credit card logic
            }
            else if (paymentMethod == "PayPal")
            {
                Console.WriteLine("Processing PayPal payment");
                // PayPal logic
            }
            else if (paymentMethod == "BankTransfer")
            {
                Console.WriteLine("Processing bank transfer");
                // Bank transfer logic
            }
            
            orders.Add(order);
            
            // Email notification
            Console.WriteLine($"Sending confirmation to {user.Email}");
            
            return total;
        }
        
        // Duplicated validation logic
        public bool ValidateEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            if (! email.Contains("@")) return false;
            if (! email.Contains(". ")) return false;
            return true;
        }
        
        public bool ValidateUserEmail(User user)
        {
            // Duplicated from above
            if (string.IsNullOrEmpty(user.Email)) return false;
            if (! user.Email.Contains("@")) return false;
            if (!user. Email.Contains(".")) return false;
            return true;
        }
        
        // Feature envy - uses more data from Order than from this class
        public string GetOrderSummary(Order order)
        {
            return $"Order {order.Id} - {order.Items.Count} items - Total: ${order.Total}";
        }
    }
    
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public List<OrderItem> Items { get; set; }
        public decimal Total { get; set; }
    }
    
    public class OrderItem
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
    
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}