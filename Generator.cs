using System.Security.Cryptography;
using System.Text;

namespace StringGenerator
{
    public class Generator
    {
        public const string DefaultChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public const int DefaultSize = 11;

        internal readonly char[] _chars;
        internal readonly int _size;

        /// <summary>
        /// Create instance with default chars and default size
        /// <code>Chars: "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"</code>
        /// <code>Size: 11</code>
        /// </summary>
        public Generator()
        {
            _chars = DefaultChars.ToCharArray();
            _size = DefaultSize;
        }

        /// <summary>
        /// Create instance with custom chars and size
        /// </summary>
        /// <param name="chars">Allowed chars</param>
        /// <param name="size">Size of string</param>
        public Generator(string chars, int size) : this()
        {
            if (!string.IsNullOrEmpty(chars))
            {
                _chars = chars.ToCharArray();
            }

            if (size > 0)
            {
                _size = size;
            }
        }

        public string GetUniqueKey()
        {
            return GetUniqueKey(_size);
        }

        public string GetUniqueKey(int size)
        {
            byte[] data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % _chars.Length;

                result.Append(_chars[idx]);
            }

            return result.ToString();
        }
    }
}
