using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Mail;

namespace Lab2.StarterCode
{
    public class MonolithicService
    {
        // Tight coupling - directly creates dependencies
        private SqlConnection connection = new SqlConnection("connection_string");
        private SmtpClient emailClient = new SmtpClient("smtp.server.com");
        
        public void ProcessOrder(int orderId)
        {
            // Database access directly in service
            connection.Open();
            var command = new SqlCommand($"SELECT * FROM Orders WHERE Id = {orderId}", connection);
            var reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                var total = (decimal)reader["Total"];
                
                // Business logic mixed with data access
                if (total > 100)
                {
                    // Email logic mixed in
                    var message = new MailMessage();
                    message.To.Add("customer@example.com");
                    message. Subject = "Large Order Alert";
                    emailClient.Send(message);
                }
                
                // Logging directly in method
                Console.WriteLine($"Processed order {orderId}");
            }
            
            connection.Close();
        }
        
        public void CreateUser(string username, string email, string password)
        {
            // Multiple responsibilities in one method
            
            // Validation
            if (string.IsNullOrEmpty(username)) throw new Exception("Invalid username");
            
            // Password hashing (insecure)
            var hashedPassword = password.GetHashCode().ToString();
            
            // Database access
            connection.Open();
            var command = new SqlCommand(
                $"INSERT INTO Users (Username, Email, Password) VALUES ('{username}', '{email}', '{hashedPassword}')", 
                connection);
            command.ExecuteNonQuery();
            connection.Close();
            
            // Email sending
            var welcomeEmail = new MailMessage();
            welcomeEmail.To.Add(email);
            welcomeEmail.Subject = "Welcome! ";
            emailClient.Send(welcomeEmail);
            
            // Logging
            Console.WriteLine($"Created user {username}");
        }
    }
}