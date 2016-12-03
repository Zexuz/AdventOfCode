using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day3
{
    public class Program
    {
        private static string _inputFile;

        public static void Main(string[] args)
        {
            _inputFile = File.ReadAllText("../../input_part1");
            var lines = _inputFile.Split('\n');

            var regEx = new Regex(@"^(\d+)[ ]*(\d+)[ ]*(\d+)$");

            var i = 0;
            foreach (var line in lines)
            {
                var match = regEx.Match(line.Trim());
                var sideA = match.Groups[1].Value;
                var sideB = match.Groups[2].Value;
                var sideC = match.Groups[3].Value;
                var triangle = new Triangle(sideA, sideB, sideC);
                if (triangle.IsValid()) i++;
            }
            Console.WriteLine(i);
        }
    }

    public class Triangle
    {
        public int SideA { get; }
        public int SideB { get; }
        public int SideC { get; }

        public Triangle(string sideA, string sideB, string sideC)
        {
            SideA = Convert.ToInt32(sideA);
            SideB = Convert.ToInt32(sideB);
            SideC = Convert.ToInt32(sideC);
        }

        public bool IsValid()
        {
            var list = new List<int> {SideA, SideB, SideC};
            var sortedList = list.OrderBy(s => s).ToList();
            return sortedList[0] + sortedList[1] > sortedList[2];
        }
    }
}