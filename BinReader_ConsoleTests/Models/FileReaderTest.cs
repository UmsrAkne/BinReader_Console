using System;
using System.IO;
using BinReader_Console.Models;
using NUnit.Framework;

namespace BinReader_ConsoleTests.Models
{
    [TestFixture]
    public class FileReaderTest
    {
        [SetUp]
        public void SetUp()
        {
            byte[] bytesToWrite = { 0x00, 0x01, 0x00, 0x01, 0x02, };

            try
            {
                File.WriteAllBytes(filePath, bytesToWrite);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"エラー: {ex.Message}");
            }
        }

        [TearDown]
        public void TearDown()
        {
            File.Delete(filePath);
        }

        private readonly string filePath = "testFile.txt";

        [Test]
        public void ReadBytesTest()
        {
            var bs = FileReader.ReadBytes(filePath);
        }
    }
}