using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Day10
{
    internal class Program
    {
        private string _inputFile;
        private List<Bot> _bots;
        private List<Storage> _inputs;
        private List<Storage> _outputs;

        private void Start()
        {
            _inputFile = File.ReadAllText("../../input");
            InitBotsAndStorage();
            GiveBotsMicrochip();
            DoOrders();
        }

        public void GiveBotsMicrochip()
        {
            var moveItemRegex = new Regex(@"value (\d+) goes to bot (\d+)");
            var moveItemsOrders = moveItemRegex.Matches(_inputFile);

            foreach (Match match in moveItemsOrders)
            {
                var value = int.Parse(match.Groups[1].Value);
                var botIndex = int.Parse(match.Groups[2].Value);
                var bot = _bots[botIndex ];
                bot.AcceptItem(value);
            }
        }

        public void DoOrders()
        {
            var regex = new Regex(@"bot (\d+) gives (low|high) to (?<entityOne>output|input|bot) (\d+) and (low|high) to (?<entityTwo>output|input|bot) (\d+)");

            foreach (Match order in regex.Matches(_inputFile))
            {
                var botId = int.Parse(order.Groups[1].Value);

                var lowOrHigh1 = order.Groups[2].Value;
                var entity1Str = order.Groups["entityOne"].Value;
                var entity1Id = int.Parse(order.Groups[3].Value);

                var lowOrHigh2 = order.Groups[4].Value;
                var entity2Str = order.Groups["entityTwo"].Value;
                var entity2Id = int.Parse(order.Groups[5].Value);

                var bot = _bots[botId ];
                var entity1 = GetEntityFromString(entity1Str, entity1Id);
                var entity2 = GetEntityFromString(entity2Str, entity2Id);

                SendItem(lowOrHigh1, entity1, bot);
                SendItem(lowOrHigh2, entity2, bot);
            }
        }

        private void SendItem(string lowOrHigh, Storage entity, Bot bot)
        {
            if (lowOrHigh == "low")
            {
                bot.SendLowItemToEntity(entity);
            }
            if (lowOrHigh == "high")
            {
                bot.SendHighItemToEntity(entity);
            }
        }

        private Storage GetEntityFromString(string entity, int id)
        {
            switch (entity)
            {
                case "output":
                    return _outputs[id ];
                case "input":
                    return _inputs[id ];
                case "bot":
                    return _bots[id ];
                default:
                    throw new Exception();
            }
        }

        private void InitBotsAndStorage()
        {
            _inputs = new List<Storage>();
            _outputs = new List<Storage>();

            for (var i = 0; i < 21; i++)
            {
                _outputs.Add(new Storage(i));
                _inputs.Add(new Storage(i));
            }

            _bots = new List<Bot>();
            for (var i = 0; i < 211; i++)
                _bots.Add(new Bot(i));
        }

        public static void Main(string[] args)
        {
            new Program().Start();
        }
    }

    public class Bot : Storage
    {
        public int High { get; set; }
        public int Low { get; set; }

        public Bot(int id) : base(id)
        {
        }

        public new void AcceptItem(int item)
        {
            Console.WriteLine("YEs");

            if ((High == 61 || High == 17) && (Low == 61 || Low == 17))
            {
                Console.WriteLine("YEs");
                if(item == 61 || item == 17)
                    Console.WriteLine(Id);
            }

            if (Low == 0)
            {
                Low = item;
                return;
            }

            if (High > item || High == 0) Low = item;
            else High = item;
        }

        public void SendHighItemToEntity(IEntity bot)
        {
            bot.AcceptItem(High);
            High = 0;
        }

        public void SendLowItemToEntity(IEntity bot)
        {
            bot.AcceptItem(Low);
            Low = 0;
        }
    }

    public class Storage : IEntity
    {
        public int Id { get; }
        private readonly List<int> _microchips;
        public int Count => _microchips.Count;

        public Storage(int id)
        {
            Id = id;
            _microchips = new List<int>();
        }

        public void AcceptItem(int item)
        {
            _microchips.Add(item);
        }
    }

    public interface IEntity
    {
        void AcceptItem(int item);
    }
}