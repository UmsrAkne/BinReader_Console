using System;
using System.IO;

namespace BinReader_Console.Models
{
    public static class FileReader
    {
        public static byte[] ReadBytes(string filePath)
        {
            var bytes = Array.Empty<byte>();

            try
            {
                bytes = File.ReadAllBytes(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"エラー: {ex.Message}");
            }

            return bytes;
        }
    }
}