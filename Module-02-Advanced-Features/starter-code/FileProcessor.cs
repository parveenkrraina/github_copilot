using System;
using System.IO;
using System.Collections.Generic;

namespace Lab2.StarterCode
{
    public class FileProcessor
    {
        public List<string> ProcessLargeFile(string filePath)
        {
            // Inefficient:  loads entire file into memory
            var allLines = File.ReadAllLines(filePath);
            var results = new List<string>();
            
            foreach (var line in allLines)
            {
                if (! string.IsNullOrWhiteSpace(line))
                {
                    results.Add(line. ToUpper());
                }
            }
            
            return results;
        }
        
        public string ReadFileContent(string path)
        {
            // Loads entire file into memory at once
            return File.ReadAllText(path);
        }
        
        public void ProcessCsvFile(string filePath)
        {
            // Memory inefficient for large CSV files
            var allContent = File.ReadAllText(filePath);
            var lines = allContent.Split('\n');
            
            foreach (var line in lines)
            {
                var columns = line.Split(',');
                Console.WriteLine($"Processing {columns[0]}");
            }
        }
    }
}