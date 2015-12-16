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
        public class LogEntry : IComparable<LogEntry>
        {
            public string Author { get; set; }
            public string Message { get; set; }
            public string Date { get; set; }

            public LogEntry()
            {
                this.Author = string.Empty;
                this.Message = string.Empty;
                this.Date = string.Empty;
            }

            public override string ToString()
            {
                return this.Date + this.Author + this.Message;
            }

            public bool IsValid()
            {
                if (string.IsNullOrEmpty(this.Author))
                    return false;
                if (string.IsNullOrEmpty(this.Date))
                    return false;
                if (string.IsNullOrEmpty(this.Message))
                    return false;

                return true;
            }
            
            public int CompareTo(LogEntry otherEntry)
            {
                return this.Author.ToLower().Trim().CompareTo(otherEntry.Author.ToLower().Trim());
            }
        }

        public List<LogEntry> Entries { get; set; }

        public Analyzer(string fileName)
        {
            this.Entries = new List<LogEntry>();
            List<LogEntry> entries = this.Parse(this.ReadFile(fileName));
            this.Entries = entries;
            this.Entries.Sort();
        }

        public List<LogEntry> Parse(List<string> strings)
        {
            List<LogEntry> result = new List<LogEntry>();

            LogEntry logEntry = new LogEntry();

            foreach (string line in strings)
            {
                string parsedLine = ValueFromString("Commit:", line);
                if (!string.IsNullOrEmpty(parsedLine))
                {
                    if (logEntry.IsValid())
                        result.Add(logEntry);

                    logEntry = new LogEntry();
                }
                else
                {
                    parsedLine = ValueFromString("Author:", line);
                    if (!string.IsNullOrEmpty(parsedLine))
                    {
                        logEntry.Author = parsedLine;
                    }
                    else
                    {
                        parsedLine = ValueFromString("Date:", line);
                        if (!string.IsNullOrEmpty(parsedLine))
                        {
                            logEntry.Date = parsedLine;
                        }
                        else
                        {
                                logEntry.Message+=line;
                        }
                    }
                }
            }

            return result;
        }

        internal string ValueFromString(string lineName, string inputLine)
        {
            if (!inputLine.StartsWith(lineName))
                return string.Empty;

            string result = inputLine.Substring(lineName.Length-1,inputLine.Length - lineName.Length);
            return result;
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

