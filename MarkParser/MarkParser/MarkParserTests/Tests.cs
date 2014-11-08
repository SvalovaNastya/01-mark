using NUnit.Framework;

namespace MarkToHtml
{
    class MarkParserShould
    {
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

        [Test]
        public static void DividesIntoParagrafsWithEmptyString()
        {
            var result = MarkParser.Parse("aaa\n\n\n\nmmm");
            Assert.AreEqual("<p>\naaa\n</p>\n<p>\n\n</p>\n<p>\nmmm\n</p>", result);
        }
    }
}
