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

            Console.WriteLine($"It has {rows.Count(row => new IpV7(row.Trim(), 4).SupportsTSL())} IPs that support TLS");
            Console.WriteLine($"It has {rows.Count(row => new IpV7(row.Trim(), 3).SupportsSSL())} IPs that support SSL");
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

        public IpV7(string ipv7, int abbaLenght)
        {
            var protectedStringRegex = new Regex(@"(?<=\[).*?(?=\])");
            var abbaStringsRegex = new Regex(@"([a-z]*)(?![^[].*[^]]*)");

            _listOfProtectedAbbas = MakeAllPossibleAbbas(protectedStringRegex, ipv7, abbaLenght);
            _listOfPossibleAbbas = MakeAllPossibleAbbas(abbaStringsRegex, ipv7, abbaLenght);
        }

        public bool SupportsTSL()
        {
            //if there is a [ABBA] inside [] and if index1 is not same as index 2,
            // the abba is a valid abba but we can't have a ABBA inside the [].
            if (_listOfProtectedAbbas.Any(pas => pas == Reverse(pas) && pas[0] != pas[1]))
            {
                return false;
            }
            var abbas = _listOfPossibleAbbas.Where(pas => pas == Reverse(pas) && pas[0] != pas[1]).ToList();
            return abbas.Count > 0;
        }

        public bool SupportsSSL()
        {
            var abbas = _listOfPossibleAbbas.Where(pas =>
                pas == Reverse(pas)
                && _listOfProtectedAbbas.Contains(SwapPas(pas))).ToList();
            return abbas.Count > 0;
        }

        private List<string> MakeAllPossibleAbbas(Regex protectedStringRegex, string ipv7, int abbaLenght)
        {
            var list = new List<string>();
            foreach (Match match in protectedStringRegex.Matches(ipv7))
            {
                var stringInsideSquareBrackets = match.Value;
                for (var i = 0; i < stringInsideSquareBrackets.Length - (abbaLenght - 1); i++)
                {
                    list.Add(stringInsideSquareBrackets.Substring(i, abbaLenght));
                }
            }

            return list;
        }

        private static string SwapPas(string s)
        {
            return new string(new[] {s[1], s[0], s[1]});
        }

        private static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}