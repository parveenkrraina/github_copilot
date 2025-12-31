using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab2.StarterCode
{
    public class UserManager
    {
        private List<User> users;
        private IEmailService emailService;
        private IPasswordHasher passwordHasher;
        
        public UserManager(IEmailService emailService, IPasswordHasher passwordHasher)
        {
            this.users = new List<User>();
            this.emailService = emailService;
            this.passwordHasher = passwordHasher;
        }
        
        public User CreateUser(string username, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username cannot be empty", nameof(username));
                
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty", nameof(email));
                
            if (users.Any(u => u.Username == username))
                throw new InvalidOperationException("Username already exists");
                
            var user = new User
            {
                Id = users.Count + 1,
                Username = username,
                Email = email,
                PasswordHash = passwordHasher.HashPassword(password),
                IsActive = true,
                CreatedDate = DateTime. UtcNow
            };
            
            users.Add(user);
            emailService.SendWelcomeEmail(email, username);
            
            return user;
        }
        
        public bool AuthenticateUser(string username, string password)
        {
            var user = users.FirstOrDefault(u => u.Username == username);
            
            if (user == null || ! user.IsActive)
                return false;
                
            return passwordHasher.VerifyPassword(password, user.PasswordHash);
        }
        
        public void DeactivateUser(int userId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            
            if (user == null)
                throw new ArgumentException("User not found", nameof(userId));
                
            user.IsActive = false;
        }
        
        public List<User> GetActiveUsers()
        {
            return users.Where(u => u.IsActive).ToList();
        }
    }
    
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    
    public interface IEmailService
    {
        void SendWelcomeEmail(string email, string username);
    }
    
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
}