using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApplication
{
    internal class Program
    {
        private void Start()
        {
            var inputFile = File.ReadAllText("../../input data");
            var rows = inputFile.Split('\n');

            Console.WriteLine($"It has {rows.Count(row => new IpV7(row.Trim()).SupportsTSL())} IPs that support TLS");
            Console.WriteLine($"It has {rows.Count(row => new IpV7(row.Trim()).SupportsSSL())} IPs that support TLS");
        }

        public static void Main(string[] args)
        {
            new Program().Start();
        }
    }

    public class IpV7
    {
        private readonly List<string> _listOfProtectedAbbas;
        private readonly List<string> _listOfPossibleAbbas;

        public IpV7(string ipv7)
        {
            var protectedStringRegex = new Regex(@"(?<=\[).*?(?=\])");
            var abbaStringsRegex = new Regex(@"([a-z]*)(?![^[].*[^]]*)");

            _listOfProtectedAbbas = MakeAllPossibleAbbas(protectedStringRegex, ipv7);
            _listOfPossibleAbbas = MakeAllPossibleAbbas(abbaStringsRegex, ipv7);
        }

        public bool SupportsTSL()
        {
            if (_listOfProtectedAbbas.Any(pas => pas == Reverse(pas) && pas[0] != pas[1]))
            {
                return false;
            }
            var abbas = _listOfPossibleAbbas.Where(pas => pas == Reverse(pas) && pas[0] != pas[1]).ToList();
            return abbas.Count > 0;
        }

        public bool SupportsSSL()
        {
            throw new NotImplementedException();
        }

        private List<string> MakeAllPossibleAbbas(Regex protectedStringRegex, string ipv7)
        {
            var list = new List<string>();
            foreach (Match match in protectedStringRegex.Matches(ipv7))
            {
                var stringInsideSquareBrackets = match.Value;
                for (var i = 0; i < stringInsideSquareBrackets.Length - 3; i++)
                {
                    list.Add(stringInsideSquareBrackets.Substring(i, 4));
                }
            }

            return list;
        }

        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }


    }
}