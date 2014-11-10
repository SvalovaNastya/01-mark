using System;

namespace MarkToHtml
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(MarkParser.Parse("aaa\n\n\n\nmmm"));
        }
    }
}