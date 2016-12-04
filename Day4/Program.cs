using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day4
{
    internal class Program
    {
        private string _inputFile;

        private void Start()
        {
            _inputFile = File.ReadAllText("../../input data");
            var lines = _inputFile.Split('\n');
            var regEx = new Regex(@"(\D+)-(\d+)\[(\D+)\]");

            var sumOfSectorIds = lines
                .Select(line => new Room(line, regEx))
                .Where(room => room.IsValidRoom())
                .Sum(room => room.SectorId);

            Console.WriteLine(sumOfSectorIds);

            foreach (var line in lines)
            {
                var room = new Room(line, regEx);
                room.GetRealRoomName();
                if(room.Name.Contains("northpole object")) Console.WriteLine(room.SectorId);
            }

        }

        public static void Main(string[] args)
        {
            new Program().Start();
        }
    }

    internal class Room
    {
        public string Name { get; private set; }
        public int SectorId { get; }
        public string CheckSum { get; }

        public Room(string rawRoomString, Regex regex)
        {
            var match = regex.Match(rawRoomString);

            Name = match.Groups[1].Value.Replace("-", " ");
            SectorId = int.Parse(match.Groups[2].Value);
            CheckSum = match.Groups[3].Value;
        }

        public bool IsValidRoom()
        {
            var strGroupedByChars = Name
                .GroupBy(c => c)
                .OrderByDescending(c => c.ToList().Count)
                .ThenBy(c => c.Key)
                .ToList();

            for (int i = 0; i < strGroupedByChars.Count; i++)
            {
                if (strGroupedByChars[i].Key == ' ')
                    strGroupedByChars.RemoveAt(i);
            }

            for (var i = 0; i < CheckSum.Length; i++)
            {
                if (CheckSum[i] == strGroupedByChars[i].Key) continue;
                return false;
            }

            return true;
        }

        public void GetRealRoomName()
        {
            var newName = "";
            var left = SectorId % 26;
            foreach (var cha in Name)
            {
                if (cha == ' ')
                {
                    newName += " ";
                    continue;
                }


                var c = cha + left;
                if (c > 122)
                    c -= 26;
                newName = newName + (char) c;
            }
            Name = newName;
        }
    }
}