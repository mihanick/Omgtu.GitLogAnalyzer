using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omgtu.GitLog
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = @"C:\github\Omgtu.GitLogAnalyzer\GitLogProgram\TestAnalyzer\log.txt";
            string outputFile = @"C:\github\Omgtu.GitLogAnalyzer\GitLogProgram\TestAnalyzer\output.txt";

            if (args.Length == 2)
            {
                inputFile = args[0];
                outputFile = args[1];
            }

            Analyzer analyzer = new Analyzer(inputFile);

            string author = string.Empty;

            using (StreamWriter sw = new StreamWriter(outputFile))
            {
                foreach (Analyzer.LogEntry logEntry in analyzer.Entries)
                {
                    if (logEntry.Author != author)
                    {
                        Console.WriteLine(logEntry.Author);
                        sw.WriteLine(logEntry.Author);

                        author = logEntry.Author;
                    }
                    string entry = string.Format("\t{0} {1}", logEntry.Date, logEntry.Message);

                    Console.WriteLine(entry);
                    sw.WriteLine(entry);
                }
            }

            Console.ReadLine();
        }
    }
}
