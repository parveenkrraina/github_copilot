using System;

namespace Lab2.StarterCode
{
    public class NotificationService
    {
        public void SendNotification(string type, string recipient, string message)
        {
            if (type == "Email")
            {
                Console.WriteLine($"Sending email to {recipient}: {message}");
                // Email logic
                var smtpClient = new System.Net.Mail.SmtpClient();
                // ...  email sending code
            }
            else if (type == "SMS")
            {
                Console.WriteLine($"Sending SMS to {recipient}: {message}");
                // SMS logic
                var smsApi = new SmsApiClient();
                // ... SMS sending code
            }
            else if (type == "Push")
            {
                Console.WriteLine($"Sending push notification to {recipient}:  {message}");
                // Push logic
                var pushService = new PushNotificationService();
                // ... push notification code
            }
            else
            {
                throw new ArgumentException("Unknown notification type");
            }
        }
        
        public void SendBulkNotifications(string type, string[] recipients, string message)
        {
            foreach (var recipient in recipients)
            {
                SendNotification(type, recipient, message);
            }
        }
    }
    
    public class SmsApiClient { }
    public class PushNotificationService { }
}