using System;

namespace Lab2.StarterCode
{
    public class DataProcessor
    {
        public int CalculateSum(int[] numbers)
        {
            int sum = 0;
            for (int i = 0; i <= numbers.Length; i++)  // Bug: should be < not <=
            {
                sum += numbers[i]  // Bug: missing semicolon
            }
            return sum;
        }
        
        public string FormatUser(string name, string email)
        {
            // Bug: no null checking
            return $"User: {name}, Email: {email}";
        }
        
        public decimal DivideNumbers(int numerator, int denominator)
        {
            // Bug: no division by zero check
            return numerator / denominator;
        }
    }
}