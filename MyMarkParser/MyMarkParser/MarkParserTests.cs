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
            var result = MarkParser.ParseMarkToHtml(new string[0]);
            Assert.IsTrue(CompareArrays(result, new string[] {""}));
        }
        [Test]
        public static void ThisStringsOnStringsWithoutSpesialSymbols()
        {
            var result = MarkParser.ParseMarkToHtml(new string[] {"aaa", "rrr eee, ee", "ppp"});
            Assert.IsTrue(CompareArrays(result, new string[] {"<p>", "aaa", "rrr eee, ee", "ppp", "</p>"}));
        }
        [Test]
        public static void ThisStringOnStringWithoutSpesialSymbols()
        {
            var result = MarkParser.ParseMarkToHtml(new string[] {"aaa, kkk 'dd' pp . 422%"});
            Assert.IsTrue(CompareArrays(result, new string[] { "<p>", "aaa, kkk 'dd' pp . 422%", "</p>" }));
        }
        [Test]
        public static void StringWithTagP()
        {
            var result = MarkParser.ParseMarkToHtml(new string[] { "aaa", "", "uu" });
            Assert.IsTrue(CompareArrays(result, new string[] { "<p>", "aaa", "</p>", "<p>", "uu", "</p>" }));
        }
        [Test]
        public static void StringWithTagEm()
        {
            var result = MarkParser.ParseMarkToHtml(new string[] { "_aaa_", "", "uu ll _d_ ss" });
            Assert.IsTrue(CompareArrays(result, new string[] { "<p>", "<em>aaa</em>", "</p>", "<p>", "uu ll <em>d</em> ss", "</p>" }));
        }
        [Test]
        public static void StringWithEmptyTagEm()
        {
            var result = MarkParser.ParseMarkToHtml(new string[] { "__", "", "uu ll _d_ ss" });
            Assert.IsTrue(CompareArrays(result, new string[] { "<p>", "<em></em>", "</p>", "<p>", "uu ll <em>d</em> ss", "</p>" }));
        }
        [Test]
        public static void StringWithDoubleTagEm()
        {
            var result = MarkParser.ParseMarkToHtml(new string[] { "_aaa _aaa_ aaa_", "", "uu ll _d_ ss" });
            Assert.IsTrue(CompareArrays(result, new string[] { "<p>", "<em>aaa </em>aaa<em> aaa</em>", "</p>", "<p>", "uu ll <em>d</em> ss", "</p>" }));
        }
        [Test]
        public static void StringWithoutTagEmOnSlesh()
        {
            var result = MarkParser.ParseMarkToHtml(new string[] { @"_aaa \_aaa\_ aaa_", "", "uu ll _d_ ss" });
            Assert.IsTrue(CompareArrays(result, new string[] { "<p>", "<em>aaa _aaa_ aaa</em>", "</p>", "<p>", "uu ll <em>d</em> ss", "</p>" }));
        }
        [Test]
        public static void StringWithOnlySlesh()
        {
            var result = MarkParser.ParseMarkToHtml(new string[] { @"_aaa \aaa aaa_", "", "uu ll _d_ ss" });
            Assert.IsTrue(CompareArrays(result, new string[] { "<p>", @"<em>aaa \aaa aaa</em>", "</p>", "<p>", "uu ll <em>d</em> ss", "</p>" }));
        }
    }

}
