using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;

namespace Day6
{
    internal class Program
    {
        private void Start()
        {
            var inputFile = File.ReadAllText("../../input data");
            var rows = inputFile.Split('\n');

            var dict = new Dictionary<int, string>();

            for (int i = 0; i < 8; i++)
            {
                dict.Add(i, "");
                var rowIndex = 0;
                while (rows.Length > rowIndex)
                {
                    dict[i] += rows[rowIndex][i].ToString();
                    rowIndex++;
                }
            }

            for (int i = 0; i < 8; i++)
                Console.Write(dict[i].GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key);
        }

        public static void Main(string[] args)
        {
            new Program().Start();
        }
    }
}