using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarkToHtml
{
    public class Paragraph
    {
        public List<ITextWithProperty> TaggedTextsList { get; private set; }
        private Dictionary<string, string> tagDict = new Dictionary<string, string> {{"_", "em"}, {"__", "strong"}};

        public Paragraph(string rawText)
        {
            TaggedTextsList = ParseParagraph(rawText);
        }

        private List<ITextWithProperty> ParseParagraph(string text)
        {
            text = text + "\0";
            string simpleText = @"^((.*[ ,])(_))+?";
            string emText = @"^_([\W\w]+?)_((\W)|\0)";
            string strongText = @"^__([\W\w]+?)__((\W)|\0)";
            var simpleTextRgx = new Regex(simpleText);
            var emTextRgx = new Regex(emText);
            var strongTextExp = new Regex(strongText);
            var ansText = text;
            var ans = new List<ITextWithProperty>();
            var previousLength = text.Length;
            while (text.Length > 1)
            {
                var currentTextWithProperty = Regex.Match(text, simpleText);
                if (currentTextWithProperty.Length != 0)
                {
                    ansText = currentTextWithProperty.Groups[2].ToString();
                    text = simpleTextRgx.Replace(text, currentTextWithProperty.Groups[3].ToString());
                    ans.Add(new SimpleText(ansText));
                    continue;
                }
                currentTextWithProperty = Regex.Match(text, strongText);
                if (currentTextWithProperty.Length != 0)
                {
                    ansText = currentTextWithProperty.Groups[1].ToString();
                    text = strongTextExp.Replace(text, currentTextWithProperty.Groups[2].ToString());
                    ans.Add(new TaggedText(ansText, "strong"));
                    continue;
                }
                currentTextWithProperty = Regex.Match(text, emText);
                if (currentTextWithProperty.Length != 0)
                {
                    ansText = currentTextWithProperty.Groups[1].ToString();
                    text = emTextRgx.Replace(text, currentTextWithProperty.Groups[2].ToString());
                    ans.Add(new TaggedText(ansText, "em"));
                    continue;
                }
                if (previousLength == text.Length)
                    break;
                previousLength = text.Length;
            }
            if (text.Length != 1)
                ans.Add(new SimpleText(text.Substring(0, text.Length - 1)));
            return ans;
        }

        public string ToHtmlString()
        {
            var s = new StringBuilder();
            s.Append("<p>\n");
            foreach (var taggedText in TaggedTextsList)
                s.Append(taggedText.ToHtmlString());
            s.Append("\n</p>");
            return s.ToString();
        }
    }
}
