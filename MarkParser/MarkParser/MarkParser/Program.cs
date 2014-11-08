using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkToHtml
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MarkParser.Parse("aaa\n\nbbb"));
        }
    }
}
