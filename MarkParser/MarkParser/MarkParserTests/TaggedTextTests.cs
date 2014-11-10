using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MarkToHtml
{
    class TaggedTextClass
    {
        [Test]
        public static void ToHtmlStringTest_SpaceText()
        {
            var text = new TaggedText("    ", "SomeTag");
            Assert.AreEqual("<SomeTag>    </SomeTag>", text.ToHtmlString());
        }

        [Test]
        public static void ToHtmlStringTest_SimpleText()
        {
            var text = new TaggedText("aaa", "SomeTag");
            Assert.AreEqual("<SomeTag>aaa</SomeTag>", text.ToHtmlString());
        }

        [Test]
        public static void ToHtmlStringTest_MultilineText()
        {
            var text = new TaggedText("aa\na", "SomeTag");
            Assert.AreEqual("<SomeTag>aa\na</SomeTag>", text.ToHtmlString());
        }
    }

    class SimpleTextClass
    {
        [Test]
        public static void ToHtmlStringTest_SpaceText()
        {
            var text = new SimpleText("    ");
            Assert.AreEqual("    ", text.ToHtmlString());
        }

        [Test]
        public static void ToHtmlStringTest_SimpleText()
        {
            var text = new SimpleText("aaa");
            Assert.AreEqual("aaa", text.ToHtmlString());
        }

        [Test]
        public static void ToHtmlStringTest_MultilineText()
        {
            var text = new SimpleText("aa\na");
            Assert.AreEqual("aa\na", text.ToHtmlString());
        }
    }
}
