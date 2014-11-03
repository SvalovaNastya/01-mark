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
            foreach (var line in lines)
            {
                if (line == "")
                {
                    answer.Add("</p>");
                    answer.Add("<p>");
                }
                else
                {
                    var tempLine = new StringBuilder();
                    var emFlag = false;
                    foreach (var letter in line)
                    {
                        if (letter == '_')
                            if (!emFlag)
                            {
                                tempLine.Append("<em>");
                                emFlag = true;
                            }
                            else
                            {
                                tempLine.Append("</em>");
                                emFlag = false;
                            }
                        else
                        {
                            tempLine.Append(letter);
                        }
                    }
                    answer.Add(tempLine.ToString());
                }
            }
            answer.Add("</p>");
            return answer.ToArray();
        }
    }
}
