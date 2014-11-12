using System;
using System.IO;
using System.Text;

namespace MarkToHtml
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var path = Console.ReadLine();
            if (!File.Exists(path))
            {
                Console.WriteLine("Такого файла не существует");
            }
            else
            {
                string data = File.ReadAllText(path);
                var ansText = MarkParser.Parse(data);
                File.WriteAllText("output.html", ansText, Encoding.Unicode);
            }
        }
    }
}