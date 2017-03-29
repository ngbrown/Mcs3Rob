using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Mcs3Rob
{
    internal partial class Mcs3RobParser
    {
        public Mcs3RobParser() : base(null) { }

        public void Parse(string s)
        {
            byte[] inputBuffer = System.Text.Encoding.Default.GetBytes(s);
            MemoryStream stream = new MemoryStream(inputBuffer);
            this.Scanner = new Mcs3RobScanner(stream);
            this.Parse();
        }

        public void Parse(Stream file)
        {
            this.Scanner = new Mcs3RobScanner(file);
            this.Parse();
        }
    }
}
