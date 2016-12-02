using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    internal class Program
    {
        public List<string> Code;
        private string _inputFile;

        private void Start()
        {
            Code = new List<string>();
            _inputFile = File.ReadAllText("../../input_part1");
            var codeLines = _inputFile.Split('\n');

            var advancedLayout = KeyPadLayout.GetAdvancedLayout();
            var simpleLayout = KeyPadLayout.GetSimpleLayout();

//            SolveKeypad(codeLines, simpleLayout);
//            Console.WriteLine($"Simple layout: The code is {string.Join("", Code)}");

            Code = new List<string>();
            SolveKeypad(codeLines, advancedLayout);
            Console.WriteLine($"Advanced layout: The code is {string.Join("", Code)}");
        }

        public void SolveKeypad(string[] codeLines, List<List<string>> layOut)
        {
            var keyPad = new KeyPad(layOut);

            foreach (var line in codeLines)
            {
                foreach (var c in line.Trim().ToCharArray())
                {
                    keyPad.Move(c);
                }
                Code.Add(keyPad.GetCharFromCurrentCord());
                Console.WriteLine($"Added {keyPad.GetCharFromCurrentCord()}");
            }
        }

        public static void Main(string[] args)
        {
            new Program().Start();
        }
    }

    public class KeyPad
    {
        private readonly List<List<string>> _layOut;
        public Cord CurrentCord { get; }

        public KeyPad(List<List<string>> layOut)
        {
            _layOut = layOut;
            CurrentCord = GetStartCord();
        }

        public string GetCharFromCurrentCord()
        {
            return _layOut[CurrentCord.Y][CurrentCord.X];
        }

        private Cord GetStartCord()
        {
            for (var i = 0; i < _layOut.Count; i++)
            {
                var a = _layOut[i];
                for (var j = 0; j < a.Count; j++)
                {
                    var item = _layOut[i][j];
                    if (item == "5")
                        return new Cord
                        {
                            X = j,
                            Y = i
                        };
                }
            }
            throw new Exception("Did not find start cord");
        }

        public void Move(char s)
        {
            switch (s)
            {
                case 'U':
                    MoveUp();
                    break;
                case 'D':
                    MoveDown();
                    break;
                case 'L':
                    MoveLeft();
                    break;
                case 'R':
                    MoveRight();
                    break;
                default:
                    throw new Exception("Invalid char");
            }
        }

        private void MoveUp()
        {
            double count = _layOut.Count;
            var layoutHeight = _layOut[CurrentCord.Y-1].Count;
            var xCordIsOnFirstOrLastIndex = CurrentCord.X == 0 || CurrentCord.X == layoutHeight - 1;
            var isOnTopHalf = CurrentCord.Y <= Math.Round(count / 2, 0, MidpointRounding.AwayFromZero);

            if (xCordIsOnFirstOrLastIndex && isOnTopHalf)
            {
                return;
            }
            CurrentCord.Y++;
        }

        private void MoveDown()
        {
            double count = _layOut.Count;
            var layoutHeight = _layOut[CurrentCord.Y-1].Count;
            var xCordIsOnFirstOrLastIndex = CurrentCord.X == 0 || CurrentCord.X == layoutHeight - 1;
            var isOnBottomHalf = CurrentCord.Y >= Math.Round(count / 2, 0, MidpointRounding.AwayFromZero);

            if (xCordIsOnFirstOrLastIndex && isOnBottomHalf)
            {
                return;
            }
            CurrentCord.Y--;
        }

        private void MoveLeft()
        {
            if (CurrentCord.X == 0)
            {
                return;
            }
            CurrentCord.X--;
        }

        private void MoveRight()
        {
            var rowCount = _layOut[CurrentCord.Y-1].Count - 1;
            if (CurrentCord.X == rowCount)
            {
                return;
            }
            CurrentCord.X++;
        }
    }


    public class Cord
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class KeyPadLayout
    {
        private const string AdvancedLayout = @"
                                1
                              2 3 4
                            5 6 7 8 9
                              A B C
                                D
";
        private const string SimpleLayout = @"
                                1 2 3
                                4 5 6
                                7 8 9
";

        public static List<List<string>> GetAdvancedLayout()
        {
            var list = new List<List<string>>();
            var layoutLines = AdvancedLayout.Split('\n');

            for (var index = 1; index < layoutLines.Length; index++)
            {
                list.Add(new List<string>());
                var line = layoutLines[index - 1];
                foreach (var c in line.Trim().ToCharArray())
                {
                    if (c == ' ') continue;
                    list[index - 1].Add(c.ToString());
                }
            }

            list.RemoveAt(0);
            return list;
        }

        public static List<List<string>> GetSimpleLayout()
        {
            var list = new List<List<string>>();
            var layoutLines = SimpleLayout.Split('\n');

            for (var index = 1; index < layoutLines.Length; index++)
            {
                list.Add(new List<string>());
                var line = layoutLines[index - 1];
                foreach (var c in line.Trim().ToCharArray())
                {
                    if (c == ' ') continue;
                    list[index - 1].Add(c.ToString());
                }
            }

            list.RemoveAt(0);
            return list;
        }
    }
}