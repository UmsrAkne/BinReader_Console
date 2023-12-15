using System.Collections.Generic;
using System.Linq;

namespace BinReader_Console.Models
{
    public class Comparer
    {
        public static List<byte> GetMatchedBytes(IEnumerable<byte> bytes, ByteWrapper[] byteWrapper)
        {
            var matchedCount = 0;
            var matchedBytes = new List<byte>();
            var counter = 0;
            var startAddress = -1;

            var enumerable = bytes.ToList();

            foreach (var b in enumerable)
            {
                counter++;

                if (byteWrapper[matchedCount].AreEqual(b))
                {
                    matchedCount++;
                    if (matchedCount == byteWrapper.Length)
                    {
                        if (startAddress > 0)
                        {
                            var r = enumerable.GetRange(startAddress, counter - startAddress - byteWrapper.Length);
                            matchedBytes.AddRange(r);
                        }

                        matchedCount = 0;
                        startAddress = counter;
                    }

                    continue;
                }

                matchedCount = 0;
            }

            if (startAddress > enumerable.Count - byteWrapper.Length)
            {
                matchedBytes.AddRange(enumerable.GetRange(startAddress, enumerable.Count - startAddress));
            }

            return matchedBytes;
        }

        public static List<List<byte>> GetMatchedByteLists(IEnumerable<byte> bytes, ByteWrapper[] byteWrapper)
        {
            var matchedCount = 0;
            var matchedBytes = new List<List<byte>>();
            var counter = 0;
            var startAddress = -1;

            var enumerable = bytes.ToList();

            foreach (var b in enumerable)
            {
                counter++;

                if (byteWrapper[matchedCount].AreEqual(b))
                {
                    matchedCount++;
                    if (matchedCount == byteWrapper.Length)
                    {
                        if (startAddress > 0)
                        {
                            var list = enumerable.GetRange(startAddress, counter - startAddress - byteWrapper.Length);
                            matchedBytes.Add(list);
                        }

                        matchedCount = 0;
                        startAddress = counter;
                    }

                    continue;
                }

                matchedCount = 0;

                if (byteWrapper[0].AreEqual(b))
                {
                    matchedCount++;
                }
            }

            if (startAddress != enumerable.Count)
            {
                matchedBytes.Add(enumerable.GetRange(startAddress, enumerable.Count - startAddress));
            }

            return matchedBytes;
        }
    }
}