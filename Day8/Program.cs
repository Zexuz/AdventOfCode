using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Day8
{
    internal class Program
    {
        private void Start()
        {
            var display = new Display(6, 50);
            var inputFile = File.ReadAllText("../../input");
            var rows = inputFile.Split('\n');

            var regex = new Regex(@"(rect|row|column).*(\d+).*?(\d+)");

            foreach (var row in rows)
            {
                var match = regex.Match(row.Trim());
                var a = int.Parse(match.Groups[2].Value);
                var b = int.Parse(match.Groups[3].Value);
                switch (match.Groups[1].Value)
                {
                    case "column":
                        display.RotateColumn(a, b);
                        break;
                    case "row":
                        display.RotateRow(a, b);
                        break;
                    case "rect":
                        display.TurnOnRect(a, b);
                        break;
                    default:
                        throw new Exception("invalid action");
                }
                Console.WriteLine(display.ToString());
            }

            Console.WriteLine(display.GetLitPixels());


        }


        public static void Main(string[] args)
        {
            new Program().Start();
        }
    }


    public class Display
    {
        private char[,] _grid;

        public Display(int height, int width)
        {
            _grid = new char[height, width];

            for (var k = 0; k < _grid.GetLength(0); k++)
                for (var l = 0; l < _grid.GetLength(1); l++)
                    _grid[k, l] = '.';
        }

        public void TurnOnRect(int h, int w)
        {
            for (var k = 0; k < w; k++)
                for (var l = 0; l < h; l++)
                    _grid[k, l] = '#';
        }

        public void RotateColumn(int columIndex, int byNr)
        {
            for (int i = 0; i < byNr; i++)
            {
                var oldGrid = new char[_grid.GetLength(0), _grid.GetLength(1)];
                Array.Copy(_grid, oldGrid, _grid.Length - 1);
                for (var l = 0; l < _grid.GetLength(0); l++)
                {
                    var index = l + 1;
                    if (index == _grid.GetLength(0)) index = 0;
                    _grid[index, columIndex] = oldGrid[l, columIndex];
                }
            }
        }

        public void RotateRow(int rowIndx, int byNr)
        {
            for (int i = 0; i < byNr; i++)
            {
                var oldGrid = new char[_grid.GetLength(0), _grid.GetLength(1)];
                Array.Copy(_grid, oldGrid, _grid.Length - 1);
                for (var l = 0; l < _grid.GetLength(1); l++)
                {
                    var index = l + 1;
                    if (index == _grid.GetLength(1)) index = 0;
                    _grid[rowIndx, index] = oldGrid[rowIndx, l];
                }
            }
        }

        public override string ToString()
        {
            var str = "";
            for (var k = 0; k < _grid.GetLength(0); k++)
            {
                for (var l = 0; l < _grid.GetLength(1); l++)
                {
                    str += _grid[k, l];
                }
                str += "\n";
            }

            return str;
        }

        public int GetLitPixels()
        {
            var count = 0;
            for (var k = 0; k < _grid.GetLength(0); k++)
                for (var l = 0; l < _grid.GetLength(1); l++)
                    if (_grid[k, l] == '#') count++;

            return count;
        }
    }
}