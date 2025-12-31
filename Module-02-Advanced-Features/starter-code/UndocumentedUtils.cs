using System;
using System.Collections.Generic;
using System.Linq;
using System.Text. RegularExpressions;

namespace Lab2.StarterCode
{
    public class UndocumentedUtils
    {
        public bool ValidateEmail(string email)
        {
            if (string. IsNullOrWhiteSpace(email))
                return false;
                
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }
        
        public string FormatPhoneNumber(string phone)
        {
            var digits = new string(phone.Where(char.IsDigit).ToArray());
            
            if (digits.Length == 10)
            {
                return $"({digits. Substring(0, 3)}) {digits.Substring(3, 3)}-{digits.Substring(6)}";
            }
            
            return phone;
        }
        
        public decimal CalculateTax(decimal amount, string state)
        {
            var taxRates = new Dictionary<string, decimal>
            {
                { "CA", 0.0725m },
                { "NY", 0.08m },
                { "TX", 0.0625m },
                { "FL", 0.06m }
            };
            
            if (taxRates.ContainsKey(state))
            {
                return amount * taxRates[state];
            }
            
            return 0;
        }
        
        public List<T> PaginateResults<T>(List<T> items, int page, int pageSize)
        {
            return items.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
        
        public string GenerateSlug(string title)
        {
            var slug = title.ToLower();
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            slug = Regex.Replace(slug, @"\s+", "-");
            slug = Regex.Replace(slug, @"-+", "-");
            return slug. Trim('-');
        }
    }
}