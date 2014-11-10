using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkToHtml
{
    public class TaggedText : ITextWithProperty
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
            var s = new StringBuilder();
            s.Append("<" + Tag + ">");
            s.Append(Text);
            s.Append("</" + Tag + ">");
            return s.ToString();
        }
    }

    public class SimpleText : ITextWithProperty
    {
        public string Text { get; set; }

        public string ToHtmlString()
        {
            throw new NotImplementedException();
        }
    }
}
