using System;
using System.Diagnostics.Eventing.Reader;
using System.Text;

namespace MarkToHtml
{
    public class TaggedText : ITextWithProperty
    {
        public string Tag { get; private set; }
        public string Text { get; private set; }

        public TaggedText(string text, string tag)
        {
            Tag = tag;
            Text = text;
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
        public string Text { get; private set; }

        public SimpleText(string text)
        {
            this.Text = text;
        }

        public string ToHtmlString()
        {
            return Text;
        }
    }
}