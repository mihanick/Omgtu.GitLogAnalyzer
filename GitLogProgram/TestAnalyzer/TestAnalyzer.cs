using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Omgtu.GitLog
{
    [TestClass]
    public class TestAnalyzer
    {
        [TestMethod]
        public void TestReadFileReturnsNotEmpty()
        {
            string fileName = "log.txt";
            Analyzer analyzer = new Analyzer(fileName);

            Assert.AreNotEqual(0, analyzer.Entries.Count);
        }

        [TestMethod]
        public void TestNumberOfEntries()
        {
            string fileName = "log.txt";
            Analyzer analyzer = new Analyzer(fileName);

            // Count number of lines starting with "Commit"
            string line;
            int numCommitsExpected = 0;
            using (StreamReader file = new StreamReader(fileName))
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (line.StartsWith("commit"))
                        numCommitsExpected++;
                }
            }

            Assert.AreEqual(numCommitsExpected, analyzer.Entries.Count);
        }
    }
}

