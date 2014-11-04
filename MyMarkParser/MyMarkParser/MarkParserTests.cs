using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MyMarkParser
{
    class MarkParserShouldReturn
    {
        public static bool CompareArrays(string[] arr1, string[] arr2)
        {
            if (arr1.Length != arr2.Length)
                return false;
            for (int i = 0; i < arr1.Length; i++)
                for (int j = 0; j < arr1[i].Length; j++)
                    if (arr1[i][j] != arr2[i][j])
                        return false;
            return true;
        }

        [Test]
        public static void EmptyStringOnEmptyString()
        {
            var result = MarkParser.ParseMarkToHtml("");
            Assert.AreEqual(result, "");
        }
        [Test]
        public static void ThisStringsOnStringsWithoutSpesialSymbols()
        {
            var result = MarkParser.ParseMarkToHtml("aaa\nrrr eee, ee\nppp");
            Assert.AreEqual("<p>\naaa\nrrr eee, ee\nppp\n</p>", result);
        }
        [Test]
        public static void StringWithOnlyOneNoSpecialSymbol()
        {
            var result = MarkParser.ParseMarkToHtml("_a_");
            Assert.AreEqual("<p>\n<em>a</em>\n</p>", result);
        }
        [Test]
        public static void ThisStringOnStringWithoutSpesialSymbols()
        {
            var result = MarkParser.ParseMarkToHtml("aaa, kkk 'dd' pp . 422%");
            Assert.AreEqual("<p>\naaa, kkk 'dd' pp . 422%\n</p>", result);
        }
        [Test]
        public static void StringWithTagP()
        {
            var result = MarkParser.ParseMarkToHtml("aaa\n\nuu");
            Assert.AreEqual("<p>\naaa\n</p>\n<p>\nuu\n</p>", result);
        }
        [Test]
        public static void StringWithTagEm()
        {
            var result = MarkParser.ParseMarkToHtml("_aaa_\n\nuu ll _d_ ss" );
            Assert.AreEqual("<p>\n<em>aaa</em>\n</p>\n<p>\nuu ll <em>d</em> ss\n</p>", result);
        }//здесь был неверный пример
        [Test]
        public static void StringWithDoubleTagEm()
        {
            var result = MarkParser.ParseMarkToHtml("_aaa _aaa_ aaa_\n\nuu ll _d_ ss");
            Assert.AreEqual("<p>\n<em>aaa </em>aaa<em> aaa</em>\n</p>\n<p>\nuu ll <em>d</em> ss\n</p>", result);
        }
        [Test]
        public static void StringWithoutTagEmOnSlesh()
        {
            var result = MarkParser.ParseMarkToHtml("_aaa \\_aaa\\_ aaa_\n\nuu ll _d_ ss");
            Assert.AreEqual("<p>\n<em>aaa _aaa_ aaa</em>\n</p>\n<p>\nuu ll <em>d</em> ss\n</p>", result);
        }
        [Test]
        public static void StringWithOnlySlesh()
        {
            var result = MarkParser.ParseMarkToHtml( "_aaa \\aaa aaa_\n\nuu ll _d_ ss" );
            Assert.AreEqual("<p>\n<em>aaa aaa aaa</em>\n</p>\n<p>\nuu ll <em>d</em> ss\n</p>", result );
        }
        [Test]
        public static void StringWithStrongTagInOneLine()
        {
            var result = MarkParser.ParseMarkToHtml( "__aaa__" );
            Assert.AreEqual("<p>\n<strong>aaa</strong>\n</p>", result);
        }
        [Test]
        public static void StringWithStrongTagInManyLines()
        {
            var result = MarkParser.ParseMarkToHtml( "__aaa__\n\npp __ o__" );
            Assert.AreEqual("<p>\n<strong>aaa</strong>\n</p>\n<p>\npp <strong> o</strong>\n</p>", result );
        }
        [Test]
        public static void StringWithEmptyStrongTag()
        {
            var result = MarkParser.ParseMarkToHtml( "____aa" );
            Assert.AreEqual("<p>\n<strong></strong>aa\n</p>", result);
        }
    }

}
