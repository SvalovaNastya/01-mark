using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkToHtml
{
    public class Paragraph
    {
        public List<TaggedText> TaggedTextsList { get; set; }

        public Paragraph(string rawText)
        {
            TaggedTextsList = ParseParagraph(rawText);
        }

        public List<TaggedText> ParseParagraph(string text)
        {
            return new List<TaggedText>() { new TaggedText(text, null) };
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
