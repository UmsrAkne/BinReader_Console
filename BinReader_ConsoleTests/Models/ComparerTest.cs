using BinReader_Console.Models;
using NUnit.Framework;

namespace BinReader_ConsoleTests.Models
{
    public class ComparerTest
    {
        [Test]
        [TestCase(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x00, 0x01, 0x02, 0x01, 0x00, 0x01, 0x00, },
            new byte[] { 0x02, 0x03, 0x02, 0x01, 0x00, })]
        [TestCase(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x00, 0x00, 0x02, 0x01, 0x00, 0x01, 0x00, },
            new byte[] { 0x02, 0x03, 0x00, 0x00, 0x02, 0x01, 0x00, })]
        public void GetMatchedBytesTest(byte[] bytes, byte[] result)
        {
            var wrappers = new ByteWrapper[]
            {
                new () { Value = 0x0, },
                new () { Value = 0x1, },
            };

            var bs = Comparer.GetMatchedBytes(bytes, wrappers);
            CollectionAssert.AreEqual(result, bs);
        }

        [Test]
        public void GetMatchedBytesTest_ワイルドカード()
        {
            var bytes = new byte[] { 0x00, 0x02, 0x02, 0x03, 0x00, 0x03, };
            var wrappers = new ByteWrapper[]
            {
                new () { Value = 0x0, },
                new () { Value = 0x0, IsWildcard = true, },
            };

            var bs = Comparer.GetMatchedBytes(bytes, wrappers);
            CollectionAssert.AreEqual(new byte[] { 0x02, 0x03, }, bs);
        }

        [Test]
        public void GetMatchedBytesTest_ワイルドカード2()
        {
            var bytes = new byte[] { 0x01, 0x02, 0x00, 0x03, 0x00, 0x03, };
            var wrappers = new ByteWrapper[]
            {
                new () { Value = 0x0, },
                new () { Value = 0x0, IsWildcard = true, },
                new () { Value = 0x0, },
            };

            var bs = Comparer.GetMatchedBytes(bytes, wrappers);
            CollectionAssert.AreEqual(new byte[] { 0x03, }, bs);
        }

        [Test]
        public void GetMatchedByteListsTest()
        {
            var bytes = new byte[] { 0x01, 0x02, 0x00, 0x01, 0x00, 0x03, };
            var wrappers = new ByteWrapper[]
            {
                new () { Value = 0x1, },
                new () { Value = 0x2, },
            };

            var bs = Comparer.GetMatchedByteLists(bytes, wrappers);

            Assert.That(bs.Count, Is.EqualTo(1));
            CollectionAssert.AreEqual(new byte[] { 0x00, 0x01, 0x00, 0x03, }, bs[0]);
        }

        [Test]
        public void GetMatchedByteListsTest_2()
        {
            var bytes = new byte[] { 0x01, 0x02, 0x00, 0x01, 0x01, 0x02, 0x03, };
            var wrappers = new ByteWrapper[]
            {
                new () { Value = 0x1, },
                new () { Value = 0x2, },
            };

            var bs = Comparer.GetMatchedByteLists(bytes, wrappers);

            Assert.That(bs.Count, Is.EqualTo(2));
            CollectionAssert.AreEqual(new byte[] { 0x00, 0x01, }, bs[0]);
            CollectionAssert.AreEqual(new byte[] { 0x03, }, bs[1]);
        }
    }
}