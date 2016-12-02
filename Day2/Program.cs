using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day2
{
    internal class Program
    {
        public List<int> Code;
        private string _inputFile;

        private void Start()
        {
            Code = new List<int>();
            _inputFile = File.ReadAllText("../../input_part1");
            var codeLines = _inputFile.Split('\n');

            var keyPad = new KeyPad();

            foreach (var line in codeLines)
            {
                foreach (var c in line.Trim().ToCharArray())
                {
                    keyPad.Move(c);
                }
                Code.Add(keyPad.CurrentKey);
                Console.WriteLine($"Added {keyPad.CurrentKey}");
            }
            Console.WriteLine($"The code is {string.Join("",Code)}");
        }

        public static void Main(string[] args)
        {
            new Program().Start();
        }
    }

    public class KeyPad
    {
        public int CurrentKey { get; private set; }

        public KeyPad()
        {
            CurrentKey = 5;
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
            if (CurrentKey <= 3) return;
            CurrentKey -= 3;
        }

        private void MoveDown()
        {
            if (CurrentKey >= 7) return;
            CurrentKey += 3;
        }

        private void MoveLeft()
        {
            if (CurrentKey % 3 == 1) return;
            CurrentKey--;
        }

        private void MoveRight()
        {
            if (CurrentKey % 3 == 0) return;
            CurrentKey++;
        }

    }
}