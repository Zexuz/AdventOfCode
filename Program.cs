using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    internal class Program
    {
        private string _inputFile;

        private void StartPart1()
        {
            var me = new Me();

            _inputFile = File.ReadAllText("../../input_part1");
            var orders = _inputFile.Split(',');


            foreach (var order in orders)
            {
                var regEx = new Regex(@"([R|L])(\d+)");
                var match = regEx.Match(order);

                var strDir = match.Groups[1].Value;
                var nrOfTimes = int.Parse(match.Groups[2].Value);

                if(strDir == "R") me.TurnRight(nrOfTimes);
                else me.TurnLeft(nrOfTimes);
            }
            Console.WriteLine(me);
        }


        public static void Main(string[] args)
        {
            new Program().StartPart1();
        }
    }

    internal class Me
    {
        public Direction CurrentDirection { get; private set; }
        public int StartX;
        public int StartY;


        public Me()
        {
            CurrentDirection = Direction.North;
        }

        public void TurnLeft(int nrOfTimes)
        {
            CurrentDirection = GetNewDirectionAfterTurn(true);
            Move(CurrentDirection, nrOfTimes);
        }

        public void TurnRight(int nrOfTimes)
        {
            CurrentDirection = GetNewDirectionAfterTurn(false);
            Move(CurrentDirection, nrOfTimes);
        }

        private void Move(Direction direction, int nrOfTimes)
        {
            switch (direction)
            {
                case Direction.North:
                    StartX += nrOfTimes;
                    break;
                case Direction.East:
                    StartY += nrOfTimes;
                    break;
                case Direction.South:
                    StartX -= nrOfTimes;
                    break;
                case Direction.West:
                    StartY -= nrOfTimes;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        private Direction GetNewDirectionAfterTurn(bool turnIsLeft)
        {
            if (turnIsLeft && CurrentDirection == Direction.North) return Direction.West;
            if (turnIsLeft && CurrentDirection == Direction.East) return Direction.North;
            if (turnIsLeft && CurrentDirection == Direction.South) return Direction.East;
            if (turnIsLeft && CurrentDirection == Direction.West) return Direction.South;

            //we know that if we have come this far int he code, we are turing right
            switch (CurrentDirection)
            {
                case Direction.North:
                    return Direction.East;
                case Direction.East:
                    return Direction.South;
                case Direction.South:
                    return Direction.West;
                case Direction.West:
                    return Direction.North;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override string ToString()
        {
            return GetBlocksAway().ToString();
        }

        public int GetBlocksAway()
        {
            return Math.Abs(StartX) + Math.Abs(StartY);
        }
    }


    public enum Direction
    {
        North,
        East,
        South,
        West
    }
}