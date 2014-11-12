using System;
using System.Diagnostics.Eventing.Reader;
using System.Text;

namespace MarkToHtml
{
    public enum PositionOfTags
    {
        end, 
        start,
        startAndEnd
    }
    public class TaggedText : ITextWithProperty
    {
        public string Tag { get; private set; }
        public string Text { get; private set; }
        public PositionOfTags Position { get; private set; }

        public TaggedText(string text, string tag, PositionOfTags postion)
        {
            Tag = tag;
            Text = text;
            Position = postion;
        }

        public string ToHtmlString()
        {
            var s = new StringBuilder();
            if (Position != PositionOfTags.end)
                s.Append("<" + Tag + ">");
            s.Append(Text);
            if (Position != PositionOfTags.start)
            s.Append("</" + Tag + ">");
            return s.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof (TaggedText))
                return false;
            var TTObj = (TaggedText) obj;
            return TTObj.Text == Text && TTObj.Tag == Tag;
        }
    }

    public class SimpleText : ITextWithProperty
    {
        public string Text { get; private set; }
        public ITextWithProperty ListOfElemenst { get; private set; }

        public SimpleText(string text)
        {
            this.Text = text;
        }

        public string ToHtmlString()
        {
            return Text;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(SimpleText))
                return false;
            var TTObj = (SimpleText)obj;
            return TTObj.Text == Text;
        }
    }
}