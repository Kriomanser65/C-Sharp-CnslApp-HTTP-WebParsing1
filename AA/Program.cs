using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace AA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the URL of the web resource to be parsed:");
            string url = Console.ReadLine();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamReader = new StreamReader(response.GetResponseStream());
                string content = streamReader.ReadToEnd();
                streamReader.Close();
                Dictionary<string, int> wordFrequency = CreateWordFrequency(content);
                Console.WriteLine("Frequency dictionary:");
                foreach (var pair in wordFrequency)
                {
                    Console.WriteLine($"Word: {pair.Key}, Amount: {pair.Value}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private static Dictionary<string, int> CreateWordFrequency(string content)
        {
            string[] words = content.Split(new char[] { ' ', '\n', '\r', '\t', '.', ',', ';', ':', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> wordFrequency = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach (string word in words)
            {
                if (wordFrequency.ContainsKey(word))
                {
                    wordFrequency[word]++;
                }
                else
                {
                    wordFrequency[word] = 1;
                }
            }
            return wordFrequency;
        }
    }
}
