using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkToHtml
{
    public class Paragraph
    {
        public List<ITextWithProperty> TaggedTextsList { get; private set; }

        public Paragraph(string rawText)
        {
            TaggedTextsList = ParseParagraph(rawText);
        }

        private List<ITextWithProperty> ParseParagraph(string text)
        {
            return new List<ITextWithProperty>() { new SimpleText(text) };
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
