using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omgtu.GitLog
{
    public class Analyzer
    {
        public class LogEntry
        {
            public string Author { get; set; }
            public string Message { get; set; }
            public string Date { get; set; }
        }

        public List<LogEntry> Entries { get; set; }

        public Analyzer(string fileName)
        {
            this.Entries = this.Parse(this.ReadFile(fileName));
        }

        public List<LogEntry> Parse(List<string> strings)
        {
            // TODO: Реалзовать
            return new List<LogEntry>();

        }


        internal List<string> ReadFile(string sourcePath)
        {
            List<string> result = new List<string>();

            string fileFullPath = Path.GetFullPath(sourcePath);

            if (!File.Exists(fileFullPath))
                throw new FileNotFoundException();

            string line;
            using (StreamReader file = new StreamReader(sourcePath))
            {
                while ((line = file.ReadLine()) != null)
                {
                    result.Add(line);
                }
            }

            return result;
        }
    }
}
