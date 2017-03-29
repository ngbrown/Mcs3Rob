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
        public void ScanRobFile()
        {
            string fullPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\samples", "example1.rob");
            var parser = new Mcs3Rob.Parser();
            parser.Scan(fullPath);
        }

        [Test]
        public void ParseRobFile()
        {
            string fullPath = Path.Combine(TestContext.CurrentContext.TestDirectory, @"..\..\samples", "example1.rob");
            var parser = new Mcs3Rob.Parser();
            parser.Parse(fullPath);
        }
    }
}
