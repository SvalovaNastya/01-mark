using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarkToHtml
{
    public class TaggedText
    {
        public string Text { get; set; }
        public string Tag { get; set; }

        public TaggedText(string text, string tag)
        {
            this.Tag = tag;
            this.Text = text;
        }

        public string ToHtmlString()
        {
            if (Tag == null)
                return Text;
            var s = new StringBuilder();
            s.Append("<" + Tag + ">");
            s.Append(Text);
            s.Append("</" + Tag + ">");
            return s.ToString();
        }
    }

    public class Paragraph
    {
        public List<TaggedText> TaggedTextsList { get; set; }

        public Paragraph(string rawText)
        {
            TaggedTextsList = ParseParagraph(rawText);
        }

        public List<TaggedText> ParseParagraph(string text)
        {
            return new List<TaggedText>(){new TaggedText(text, null)};
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

    public class MarkParser
    {
        public static string[] DivideIntoParagraphs(string text)
        {
            var reg = new Regex(@"(\r)?\n\s*(\r)?\n");
            var textWithoutBlunks = reg.Replace(text, "\n\n");
            return textWithoutBlunks.Split(new string[] {"\n\n"}, StringSplitOptions.RemoveEmptyEntries).ToArray();
        }

        public static string Parse(string text)
        {
            var paragraphs = DivideIntoParagraphs(text)
                .Select(x => new Paragraph(x))
                .ToArray();
            var answer = new StringBuilder();
            foreach (var paragraph in paragraphs)
            {
                answer.Append(paragraph.ToHtmlString());
                answer.Append("\n");
            }
            if (answer.Length > 0)
                answer.Remove(answer.Length - 1, 1);
            return answer.ToString();
        }
    }
}
