namespace BinReader_Console.Models
{
    public struct ByteWrapper
    {
        public byte Value { get; set; }

        public bool IsWildcard { get; set; }

        public bool AreEqual(byte b)
        {
            if (IsWildcard)
            {
                return true;
            }

            return b == Value;
        }
    }
}