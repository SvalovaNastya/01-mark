using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MarkToHtml
{
    class ParagraphShould
    {
        [Test]
        public static void CorrectInitializesOnOneLine()
        {
            var paragraph = new Paragraph("aaa");
            Assert.AreEqual(1, paragraph.TaggedTextsList.Count);
            Assert.AreEqual(typeof(SimpleText), paragraph.TaggedTextsList[0].GetType());
            var simpleText = paragraph.TaggedTextsList[0];
            Assert.AreEqual("aaa", simpleText.Text);
        }

        [Test]
        public static void CorrectInitializesOnSomeLines()
        {
            var paragraph = new Paragraph("aa\na");
            Assert.AreEqual(1, paragraph.TaggedTextsList.Count);
            Assert.AreEqual(typeof(SimpleText), paragraph.TaggedTextsList[0].GetType());
            var simpleText = paragraph.TaggedTextsList[0];
            Assert.AreEqual("aa\na", simpleText.Text);
        }

        [Test]
        public static void CorrectTranslateToHtmlStringTest_SpaceText()
        {
            var paragraph = new Paragraph(" ");
            Assert.AreEqual("<p>\n \n</p>", paragraph.ToHtmlString());
        }

        [Test]
        public static void CorrectTranslateToHtmlStringTest_SimpleText()
        {
            var paragraph = new Paragraph("aaa");
            Assert.AreEqual("<p>\naaa\n</p>", paragraph.ToHtmlString());
        }

        [Test]
        public static void CorrectTranslateToHtmlStringTest_SomeLinesText()
        {
            var paragraph = new Paragraph("aaa\naaa");
            Assert.AreEqual("<p>\naaa\naaa\n</p>", paragraph.ToHtmlString());
        }
    }
}
