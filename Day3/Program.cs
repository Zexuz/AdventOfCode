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
            var row1 = new List<string>();
            var row2 = new List<string>();
            var row3 = new List<string>();
            foreach (var line in lines)
            {
                var match = regEx.Match(line.Trim());
                var sideA = match.Groups[1].Value;
                var sideB = match.Groups[2].Value;
                var sideC = match.Groups[3].Value;
                var triangle = new Triangle(sideA, sideB, sideC);
                if (triangle.IsValid()) i++;

                row1.Add(sideA);
                row2.Add(sideB);
                row3.Add(sideC);
            }
            Console.WriteLine($"Part one anwser {i}");

            var verticalTri = 0;
            ItarOverList(row1, ref verticalTri, regEx);
            ItarOverList(row2, ref verticalTri, regEx);
            ItarOverList(row3, ref verticalTri, regEx);
            Console.WriteLine($"Part one anwser {verticalTri}");


        }

        public static void ItarOverList(List<string> list,ref int counter,Regex regex )
        {
            for (var j = 0; j < list.Count; j+=3)
            {
                var line = $"{list[j]} {list[j+1]} {list[j+2]}";

                var match = regex.Match(line.Trim());
                var sideA = match.Groups[1].Value;
                var sideB = match.Groups[2].Value;
                var sideC = match.Groups[3].Value;
                var triangle = new Triangle(sideA, sideB, sideC);
                if (triangle.IsValid()) counter++;
            }
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