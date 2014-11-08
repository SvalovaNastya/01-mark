using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkToHtml
{
    public class MarkParser
    {
        public static string[] DividesIntoParagrafs(string text)
        {
            var ans = new List<String>();
            int start = 0;
            int end = 0;
            for (int i = 1; i < text.Length; i++)
            {
                if (text[i] == '\n' && text[i - 1] == '\n')
                {
                    end = i - 2;
                    if (start < end)
                        ans.Add(text.Substring(start, end - start + 1));
                    else
                        ans.Add("");
                    start = i + 1;
                    i++;
                }
            }
            end = text.Length - 1;
            if (start < end)
                ans.Add(text.Substring(start, end - start + 1));
            else
                ans.Add("");
            return ans.ToArray();
        }

        public static string Parse(string text)
        {
            var paragrafs = DividesIntoParagrafs(text);
            var answer = new StringBuilder();
            foreach (var paragraf in paragrafs)
            {
                answer.Append("<p>\n");
                answer.Append(paragraf);
                answer.Append("\n</p>\n");
            }
            if (paragrafs.Length == 0)
                answer.Append("<p>\n\n</p>");
            else
                answer.Remove(answer.Length - 1, 1);
            return answer.ToString();
        }
    }
}
