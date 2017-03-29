using System.IO;

namespace Mcs3Rob
{
    public class Parser
    {
        public void Scan(string path)
        {
            using (var file = File.OpenRead(path))
            {
                var scanner = new Mcs3RobScanner(file);
                int tok;
                do
                {
                    tok = scanner.yylex();
                } while (tok > (int)Token.EOF);
            }
        }

        public void Parse(string path)
        {
            using (var file = File.OpenRead(path))
            {
                var parser = new Mcs3RobParser();
                parser.Parse(file);
            }
        }
    }
}