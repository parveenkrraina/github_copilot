using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.StarterCode
{
    public class UserProcessor
    {
        public void ProcessUserData(List<User> users)
        {
            foreach (var user in users)
            {
                if (user != null)
                {
                    if (user. IsActive)
                    {
                        if (user.Email. Contains("@"))
                        {
                            if (user.Username.Length > 3)
                            {
                                Console.WriteLine($"Processing {user.Username}");
                                // More nested logic
                                if (user.CreatedDate.Year == DateTime.Now.Year)
                                {
                                    Console.WriteLine("New user this year");
                                }
                            }
                        }
                    }
                }
            }
        }
        
        public void HandleCheckout(Order order, User user, PaymentInfo payment)
        {
            // Validates
            if (order == null) throw new ArgumentNullException(nameof(order));
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (payment == null) throw new ArgumentNullException(nameof(payment));
            if (order.Items. Count == 0) throw new InvalidOperationException("Order is empty");
            
            // Calculates
            decimal total = 0;
            foreach (var item in order.Items)
            {
                total += item.Price * item.Quantity;
            }
            order.Total = total;
            
            // Saves
            using (var db = new DatabaseContext())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
            
            // Sends email
            var emailService = new EmailService();
            emailService.SendEmail(user.Email, "Order Confirmation", 
                $"Your order #{order.Id} total:  ${total}");
        }
    }
    
    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal Total { get; set; }
    }
    
    public class OrderItem
    {
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
    
    public class PaymentInfo
    {
        public string CardNumber { get; set; }
        public string CVV { get; set; }
    }
    
    public class DatabaseContext : IDisposable
    {
        public List<Order> Orders { get; set; } = new List<Order>();
        public void SaveChanges() { }
        public void Dispose() { }
    }
    
    public class EmailService
    {
        public void SendEmail(string to, string subject, string body) { }
    }
}