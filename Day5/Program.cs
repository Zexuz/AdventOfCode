using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Day5
{
    internal class Program
    {

        private void Star()
        {
            var input = "ugkcyxxp";

            var regEx = new Regex(@"^00000(.)");
            var i = -1;
            var keys = new List<string>();
            while (true)
            {
                var md5 = CreateMD5($"{input}{++i}");
                var match = regEx.Match(md5);
                if(!match.Success)continue;
                Console.WriteLine($"Found a match on hash : {md5}, at index :{i}, key: {match.Groups[1].Value}");
                keys.Add(match.Groups[1].Value);
                if(keys.Count == 8) break;
            }

            var keyCode = string.Join("", keys);
            Console.WriteLine(keyCode);
            Console.ReadKey();
        }

        //ty SO
        //http://stackoverflow.com/questions/11454004/calculate-a-md5-hash-from-a-string
        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (var md5 = MD5.Create())
            {
                var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                foreach (var t in hashBytes)
                {
                    sb.Append(t.ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static void Main(string[] args)
        {
            new Program().Star();
        }

    }
}