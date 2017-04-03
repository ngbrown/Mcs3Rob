using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Mcs3Rob.Tests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        [TestCase("example1.rob")]
        [TestCase("example2.rob")]
        public void ScanRobFile(string fileName)
        {
            var errorList = new List<Tuple<object, ErrorEventArgs>>();

            string fullPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\samples", fileName);
            var parser = new Mcs3Rob.Parser();
            parser.Error += (sender, args) => errorList.Add(new Tuple<object, ErrorEventArgs>(sender, args));
            var scannedTokens = parser.Scan(fullPath);

            Assert.That(errorList, Is.Empty, string.Join("\r\n", errorList.Select(x => x.Item2.ToString())));
            Assert.That(scannedTokens, Does.Not.Contain("SCANERROR"));
        }

        [Test]
        [TestCase("example1.rob")]
        [TestCase("example2.rob")]
        public void ParseRobFile(string fileName)
        {
            var errorList = new List<Tuple<object, ErrorEventArgs>>();

            string fullPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\samples", fileName);
            var parser = new Mcs3Rob.Parser();
            parser.Error += (sender, args) => errorList.Add(new Tuple<object, ErrorEventArgs>(sender, args));
            Console.Write(parser.Parse(fullPath));

            Assert.That(errorList, Is.Empty, string.Join("\r\n", errorList.Select(x => x.Item2.ToString())));
        }
    }
}
