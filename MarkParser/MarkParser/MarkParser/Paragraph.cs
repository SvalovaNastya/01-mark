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
            TaggedTextsList = ParseLine(rawText, false);
        }

        private List<ITextWithProperty> ParseLine(string text, bool IsEnd)
        {
            text = text + "\0";
            string simpleText = @"^((.*?[ ,])([_`]))+?";
            string emText = @"^_([\W\w]+?)(?<!_)_(([^_\w])|\0)";
            string strongText = @"^__([\W\w]+?)__((\W)|\0)";
            string codeText = @"^`([\W\w]+?)`";
            var simpleTextRgx = new Regex(simpleText);
            var emTextRgx = new Regex(emText);
            var strongTextExp = new Regex(strongText);
            var codeTextExp = new Regex(codeText);
            var ansText = text;
            var ans = new List<ITextWithProperty>();
            var previousLength = text.Length;
            while (text.Length > 1)
            {
                var currentTextWithProperty = Regex.Match(text, strongText);
                if (currentTextWithProperty.Length != 0)
                {
                    ansText = currentTextWithProperty.Groups[1].ToString();
                    text = strongTextExp.Replace(text, currentTextWithProperty.Groups[2].ToString());
                    ans.Add(new TaggedText(ansText, "strong", PositionOfTags.startAndEnd));
                    continue;
                }
                currentTextWithProperty = Regex.Match(text, codeText);
                if (currentTextWithProperty.Length != 0)
                {
                    ansText = currentTextWithProperty.Groups[1].ToString();
                    text = codeTextExp.Replace(text, "");
                    ans.Add(new TaggedText(ansText, "code", PositionOfTags.startAndEnd));
                    continue;
                }
                currentTextWithProperty = Regex.Match(text, emText);
                if (currentTextWithProperty.Length != 0)
                {
                    ansText = currentTextWithProperty.Groups[1].ToString();
                    text = emTextRgx.Replace(text, currentTextWithProperty.Groups[2].ToString());
                    if (!IsEnd)
                    {
                        var embeddedText = ParseLine(ansText, true);
                        if (embeddedText.Count == 1)
                            ans.Add(new TaggedText(ansText, "em", PositionOfTags.startAndEnd));
                        else
                        {
                            if (embeddedText[0].GetType() == typeof(SimpleText))
                                embeddedText[0] = new TaggedText(embeddedText[0].Text, "em", PositionOfTags.start);
                            else
                                embeddedText.Insert(0, new TaggedText("", "em", PositionOfTags.start));
                            if (embeddedText[embeddedText.Count - 1].GetType() == typeof(SimpleText))
                                embeddedText[embeddedText.Count - 1] = new TaggedText(embeddedText[embeddedText.Count - 1].Text, "em", PositionOfTags.end);
                            else
                                embeddedText.Add(new TaggedText("", "em", PositionOfTags.end));
                            foreach (var element in embeddedText)
                            {
                                ans.Add(element);
                            }
                        }
                    }
                    else
                        ans.Add(new TaggedText(ansText, "em", PositionOfTags.startAndEnd));
                    continue;
                }
                currentTextWithProperty = Regex.Match(text, simpleText);
                if (currentTextWithProperty.Length != 0)
                {
                    ansText = currentTextWithProperty.Groups[2].ToString();
                    text = simpleTextRgx.Replace(text, currentTextWithProperty.Groups[3].ToString());
                    ans.Add(new SimpleText(ansText));
                    continue;
                }
                ans.Add(new SimpleText(text.Substring(0, text.Length - 1)));
                break;
            }
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
