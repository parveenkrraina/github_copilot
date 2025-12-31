using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.StarterCode
{
    public class DatabaseQueries
    {
        private AppDbContext context;
        
        public DatabaseQueries(AppDbContext dbContext)
        {
            context = dbContext;
        }
        
        public void DisplayUserOrders()
        {
            // N+1 query problem
            var users = context.Users.ToList();
            
            foreach (var user in users)
            {
                // Each iteration causes a separate database query
                var orders = context.Orders.Where(o => o.UserId == user.Id).ToList();
                Console.WriteLine($"{user.Username} has {orders.Count} orders");
                
                foreach (var order in orders)
                {
                    // Another query for each order
                    var items = context.OrderItems.Where(i => i.OrderId == order. Id).ToList();
                    Console.WriteLine($"  Order {order.Id} has {items.Count} items");
                }
            }
        }
        
        public List<User> GetActiveUsersWithOrders()
        {
            // Multiple queries instead of one
            var users = context.Users.Where(u => u.IsActive).ToList();
            
            foreach (var user in users)
            {
                user.Orders = context.Orders.Where(o => o.UserId == user.Id).ToList();
            }
            
            return users;
        }
    }
    
    public class AppDbContext
    {
        public IQueryable<User> Users { get; set; }
        public IQueryable<Order> Orders { get; set; }
        public IQueryable<OrderItem> OrderItems { get; set; }
    }
    
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<Order> Orders { get; set; }
    }
    
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> Items { get; set; }
    }
    
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
    }
}