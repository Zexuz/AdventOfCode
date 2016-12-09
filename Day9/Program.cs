using System;
using System.IO;
using System.Text;

namespace ConsoleApplication
{
    internal class Program
    {
        private void Start()
        {
            var inputFile = File.ReadAllText("../../input");
            var c = Decompresser.Decompress(inputFile, 0, inputFile.Length, true);
            Console.WriteLine(c);
        }

        public static void Main(string[] args)
        {
            new Program().Start();
        }
    }

    public class Decompresser
    {

        public static long Decompress(string s, int start, int length, bool rec)
        {
            long size = 0;
            for(var i = start; i < start + length;) {
                if(s[i] == '(') {
                    var mark = new StringBuilder();
                    i++;
                    while(s[i] != ')') {
                        mark.Append(s[i]);
                        i++;
                    }
                    i++;
                    var xy = mark.ToString().Split('x');
                    int len = int.Parse(xy[0]), reps = int.Parse(xy[1]);
                    size += reps * (rec ? Decompress(s, i, len, true) : len);
                    i += len;

                }
                else {
                    size++;
                    i++;
                }
            }

            return size;
        }
    }
}