using System;
using NUnit.Framework;

namespace MarkToHtml
{
    class MarkParserShould
    {
        [Test, TestCaseSource("LineFoldings")]
        public static void Divides_Into_Paragraphs_With_Different_Line_Foldings(string inputLine, string[] expectedLine)
        {
            var result = MarkParser.DivideIntoParagraphs(inputLine);
            Assert.AreEqual(expectedLine, result);
        }

        private static object[] LineFoldings =
        {
            new object[] {"aaa\n\nbbb", new string[]{"aaa", "bbb"}},
            new object[] {"aaa\n     \nbbb", new string[]{"aaa", "bbb"}},
            new object[] {"aaa\r\n\r\nbbb", new string[]{"aaa", "bbb"}},
            new object[] {"aaa\r\n     \r\nbbb", new string[]{"aaa", "bbb"}}
        };

        [Test]
        public static void RetursnOnlyTagPOnEmptyString()
        {
            var result = MarkParser.Parse("");
            Assert.AreEqual("<p>\n\n</p>", result);
        }

        [Test]
        public static void DividesIntoParagrafsOnOneParagraf()
        {
            var result = MarkParser.Parse("aaa");
            Assert.AreEqual("<p>\naaa\n</p>", result);
        }

        [Test]
        public static void DividesIntoParagrafsOnTwoParagrafs()
        {
            var result = MarkParser.Parse("aaa\n\nbbb");
            Assert.AreEqual("<p>\naaa\n</p>\n<p>\nbbb\n</p>", result);
        }

        [Test]
        public static void DividesIntoParagrafsOnSomeParagrafs()
        {
            var result = MarkParser.Parse("aaa\n\nbbb\n\nmmm");
            Assert.AreEqual("<p>\naaa\n</p>\n<p>\nbbb\n</p>\n<p>\nmmm\n</p>", result);
        }

//        [Test]
//        public static void DividesIntoParagrafsWithEmptyString()
//        {
//            var result = MarkParser.Parse("aaa\n\n\n\nmmm");
//            Assert.AreEqual("<p>\naaa\n</p>\n<p>\n\n</p>\n<p>\nmmm\n</p>", result);
//        }
    }
}
