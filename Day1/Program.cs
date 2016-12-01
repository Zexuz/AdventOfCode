using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

                if (strDir == "R") me.TurnRight(nrOfTimes);
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
        public Cord Cord;
        public Cord FirstIntersectCord { get; private set; }

        private readonly List<Cord> _placesWeHaveBeenTo;

        public Me()
        {
            CurrentDirection = Direction.North;
            _placesWeHaveBeenTo = new List<Cord>();
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

        public override string ToString()
        {
            return $"Distance {GetBlocksAway(Cord.X,Cord.Y)}, First intersect {FirstIntersectCord}";
        }

        public static int GetBlocksAway(int x,int y)
        {
            return Math.Abs(x) + Math.Abs(y);
        }

        private void Move(Direction direction, int nrOfTimes)
        {
            for (var i = 0; i < nrOfTimes; i++)
            {
                switch (direction)
                {
                    case Direction.North:
                        Cord.X++;
                        break;
                    case Direction.East:
                        Cord.Y++;
                        break;
                    case Direction.South:
                        Cord.X--;
                        break;
                    case Direction.West:
                        Cord.Y--;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                }
                if (_placesWeHaveBeenTo.Where(c => c.X == Cord.X && c.Y == Cord.Y).ToList().Count > 0
                    && FirstIntersectCord.X == 0 && FirstIntersectCord.Y == 0)
                    FirstIntersectCord = Cord;

                _placesWeHaveBeenTo.Add(Cord);
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
    }


    public struct Cord
    {
        public int X, Y;

        public override string ToString()
        {
            return Me.GetBlocksAway(X,Y).ToString();
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