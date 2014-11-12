using System;
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
                    if (!list1[i].Equals(list2[i]))
                        return false;
            }
            else
                return false;
            return true;
        }

        [Test, TestCaseSource("ParseLines")]
        public static void CorrectTranslateToHtmlStringTest_TextWithTags(string text, List<ITextWithProperty> ans)
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
                    new TaggedText("a", "em", PositionOfTags.startAndEnd),
                    new SimpleText(" a")
                }
            },
            new object[]
            {
                "a,_a_ a", new List<ITextWithProperty>
                {
                    new SimpleText("a,"),
                    new TaggedText("a", "em", PositionOfTags.startAndEnd),
                    new SimpleText(" a")
                }
            },
            new object[]
            {
                "a _aa\naaa_ a", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("aa\naaa", "em", PositionOfTags.startAndEnd),
                    new SimpleText(" a")
                }
            },
            new object[]
            {
                "a _a a_ a", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("a a", "em", PositionOfTags.startAndEnd),
                    new SimpleText(" a")
                }
            },
            new object[]
            {
                "_a_ a", new List<ITextWithProperty>
                {
                    new TaggedText("a", "em", PositionOfTags.startAndEnd),
                    new SimpleText(" a")
                }
            },
            new object[]
            {
                "a _a_", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("a", "em", PositionOfTags.startAndEnd)
                }
            },
            new object[]
            {
                "a _a_!", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("a", "em", PositionOfTags.startAndEnd),
                    new SimpleText("!")
                }
            },
            new object[]
            {
                "a __a__ a", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("a", "strong", PositionOfTags.startAndEnd),
                    new SimpleText(" a")
                }
            },
            new object[]
            {
                "__a a__ a", new List<ITextWithProperty>
                {
                    new TaggedText("a a", "strong", PositionOfTags.startAndEnd),
                    new SimpleText(" a")
                }
            },
            new object[]
            {
                "a,__a a__! a", new List<ITextWithProperty>
                {
                    new SimpleText("a,"),
                    new TaggedText("a a", "strong", PositionOfTags.startAndEnd),
                    new SimpleText("! a")
                }
            },
            new object[]
            {
                "a a_a_a a", new List<ITextWithProperty>
                {
                    new SimpleText("a a_a_a a")
                }
            },
            new object[]
            {
                "a \\__aa__ a", new List<ITextWithProperty>
                {
                    new SimpleText("a \\__aa__ a")
                }
            },
            new object[]
            {
                "a \\_aa__ a", new List<ITextWithProperty>
                {
                    new SimpleText("a \\_aa__ a")
                }
            },
            new object[]
            {
                "a,__a a__! a ,_aa_? ", new List<ITextWithProperty>
                {
                    new SimpleText("a,"),
                    new TaggedText("a a", "strong", PositionOfTags.startAndEnd),
                    new SimpleText("! a ,"),
                    new TaggedText("aa", "em", PositionOfTags.startAndEnd),
                    new SimpleText("? ")
                }
            },
            new object[]
            {
                "a _aaa_ aa __a__ a", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("aaa", "em", PositionOfTags.startAndEnd),
                    new SimpleText(" aa "),
                    new TaggedText("a", "strong", PositionOfTags.startAndEnd),
                    new SimpleText(" a")
                }
            },
            new object[]
            {
                "a `aa` a", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("aa", "code", PositionOfTags.startAndEnd),
                    new SimpleText(" a")
                }
            },
            new object[]
            {
                "`aa` a", new List<ITextWithProperty>
                {
                    new TaggedText("aa", "code", PositionOfTags.startAndEnd),
                    new SimpleText(" a")
                }
            },
            new object[]
            {
                "a `aa`", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("aa", "code", PositionOfTags.startAndEnd),
                }
            },
            new object[]
            {
                "a `a _ aaaa_ a` a", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("a _ aaaa_ a", "code", PositionOfTags.startAndEnd),
                    new SimpleText(" a")
                }
            },
            new object[]
            {
                "a _aa", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new SimpleText("_aa")
                }
            },
            new object[]
            {
                "a `aa", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new SimpleText("`aa")
                }
            },
            new object[]
            {
                "a \\`aa` a", new List<ITextWithProperty>
                {
                    new SimpleText("a \\`aa` a"),
                }
            },
            new object[]
            {
                "a `a _ a_ `", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("a _ a_ ", "code", PositionOfTags.startAndEnd)
                }
            },
            new object[]
            {
                "a _a __a__ a_ ", new List<ITextWithProperty>
                {
                    new SimpleText("a "),
                    new TaggedText("a ", "em", PositionOfTags.start),
                    new TaggedText("a", "strong", PositionOfTags.startAndEnd),
                    new TaggedText(" a", "em", PositionOfTags.end),
                    new SimpleText(" ")
                }
            },
        };
    }
}