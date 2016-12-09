using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace ConsoleApplication
{
    internal class Program
    {
        private void Start()
        {
            var inputFile = File.ReadAllText("../../input");
            var decompresser = new Decompresser(inputFile);
            decompresser.Decompress();
            Console.WriteLine(decompresser.GetLenght());
        }

        public static void Main(string[] args)
        {
            new Program().Start();
        }
    }

    public class Decompresser
    {
        private string _str;
        private int _nextPossibleIndexWhereWeAcceptAMatch;

        public Decompresser(string str)
        {
            _str = str;
        }

        public void Decompress()
        {
            var regex = new Regex(@"\((\d+)x(\d+)\)");

            var matches = regex.Matches(_str);

            foreach (Match match  in matches)
            {
                if (match.Index < _nextPossibleIndexWhereWeAcceptAMatch) continue;
                var nrOfChars = int.Parse(match.Groups[1].Value);
                var nrOfTimes = int.Parse(match.Groups[2].Value);
                var index = match.Index;

                var stringToMultiply = _str.Substring(index, nrOfChars + match.Length);
                _str = _str.Replace(stringToMultiply, "__replace__");
                stringToMultiply = stringToMultiply.Substring(match.Length);
                var multipliedString = "";
                for (var i = 0; i < nrOfTimes; i++)
                {
                    multipliedString += stringToMultiply;
                }

                _str = _str.Replace("__replace__", multipliedString);
                _nextPossibleIndexWhereWeAcceptAMatch = index  + multipliedString.Length;
                Decompress(); //if we have a match, update the matches to match the new _str
            }
        }

        public int GetLenght()
        {
            return Regex.Replace(_str, @"\s+", "").Length;
        }
    }
}