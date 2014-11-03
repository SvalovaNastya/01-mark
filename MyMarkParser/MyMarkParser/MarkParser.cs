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
                    ParseEachLine(answer, line);
                }
            }
            answer.Add("</p>");
            return answer.ToArray();
        }

        private static void ParseEachLine(List<string> answer, string line)
        {
            var newLine = new StringBuilder();
            var emFlag = false;
            char previousLetter = ' ';
            foreach (var letter in line)
            {
                if (letter == '_')
                {
                    emFlag = CorrectAddTag(newLine, emFlag, previousLetter, letter);
                }
                else
                {
                    newLine.Append(letter);
                }
                previousLetter = letter;
            }
            answer.Add(newLine.ToString());
        }

        static bool AddEmTag(bool emFlag, StringBuilder line)
        {
            var tag = (!emFlag) ? "<em>" : "</em>";
            line.Append(tag);
            return !emFlag;
        }

        private static bool CorrectAddTag(StringBuilder newLine, bool emFlag, char previousLetter, char letter)
        {
            if (previousLetter == '\\')
            {
                newLine.Remove(newLine.Length - 1, 1);
                newLine.Append(letter);
            }
            else
                emFlag = AddEmTag(emFlag, newLine);
            return emFlag;
        }
    }
}
