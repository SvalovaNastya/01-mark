using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMarkParser
{
    public class MarkParser
    {
        public static string[] ParseMarkToHtml(string[] lines)
        {
            if (lines.Length == 0)
                return new string[1] {""};
            var answer = new List<string>();
            answer.Add("<p>");
            foreach (var e in lines)
            {
                if (e == "")
                {
                    answer.Add("</p>");
                    answer.Add("<p>");
                }
                else
                {
                    answer.Add(e);
                }
            }
            answer.Add("</p>");
            return answer.ToArray();
        }
    }
}
