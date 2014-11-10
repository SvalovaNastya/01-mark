using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace MarkToHtml
{
    internal class ParagraphShould
    {
        [Test]
        public static void CorrectInitializesOnOneLine()
        {
            var paragraph = new Paragraph("aaa");
            Assert.AreEqual(1, paragraph.TaggedTextsList.Count);
            Assert.AreEqual(typeof (SimpleText), paragraph.TaggedTextsList[0].GetType());
            ITextWithProperty simpleText = paragraph.TaggedTextsList[0];
            Assert.AreEqual("aaa", simpleText.Text);
        }

        [Test]
        public static void CorrectInitializesOnSomeLines()
        {
            var paragraph = new Paragraph("aa\na");
            Assert.AreEqual(1, paragraph.TaggedTextsList.Count);
            Assert.AreEqual(typeof (SimpleText), paragraph.TaggedTextsList[0].GetType());
            ITextWithProperty simpleText = paragraph.TaggedTextsList[0];
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

        private static bool CompareLists(List<ITextWithProperty> list1, List<ITextWithProperty> list2)
        {
            if (list1.Count == list2.Count)
            {
                for (int i = 0; i < list1.Count; i++)
                    if (list1[i].Equals(list2[i]))
                        return false;
            }
            else
                return false;
            return true;
        }

        [Test, TestCaseSource("ParseLines")]
        public static void CorrectTranslateToHtmlStringTest_TextWithEmTag(string text, List<ITextWithProperty> ans)
        {
            var paragraph = new Paragraph(text);
            Assert.IsTrue(CompareLists(ans, paragraph.TaggedTextsList));
        }

        private static object[] ParseLines =
        {
            new object[]
            {
                "a _a_ a", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("a", "em"),
                    new SimpleText(" a")
                }
            },
            new object[]
            {
                "a __a__ a", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("a", "strong"),
                    new SimpleText(" a")
                }
            },
            new object[]
            {
                "a _aa\naaa_ a", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("aa\naaa", "em"),
                    new SimpleText(" a")
                }
            },
            new object[]
            {
                "a _aaa_ aa __a__ a", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("aaa", "em"),
                    new SimpleText(" aa "),
                    new TaggedText("aaa", "strong"),
                    new SimpleText(" a")
                }
            },
        };
    }
}