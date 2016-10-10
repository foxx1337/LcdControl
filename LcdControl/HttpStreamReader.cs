using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LcdControl
{
    class HttpStreamReader
    {
        private BinaryReader reader;
        private byte[] buffer;
        private int position, length;

        public HttpStreamReader(Stream clientStream) : this(clientStream, 1 << 16)
        { }

        public HttpStreamReader(Stream clientStream, int bufferSize)
        {
            reader = new BinaryReader(clientStream);
            buffer = new byte[bufferSize];
            position = length = 0;
        }

        private void Refill()
        {
            position = 0;
            length = reader.Read(buffer, 0, buffer.Length);
        }

        public string ReadLine()
        {
            if (0 == length)
                Refill();
            StringBuilder sb = new StringBuilder();
            while (length > 0)
            {
                while (length > 0)
                {
                    if (buffer[position] == '\n' && sb.Length > 0 && '\r' == sb[sb.Length - 1])
                    {
                        ++position;
                        --length;
                        string result = sb.ToString(0, sb.Length - 1);  // ignore the trailing '\r'
                        sb.Length = 0;
                        return result;
                    }
                    sb.Append((char)buffer[position++]);
                    --length;
                }
                Refill();
            }
            return null;
        }

        public int Read(byte[] buffer, int index, int count)
        {
            if (length > 0)
            {
                int actualCount = Math.Min(this.length, count);
                Array.Copy(this.buffer, this.position, buffer, 0, actualCount);
                position += actualCount;
                length -= actualCount;
                return actualCount;
            }
            return reader.Read(buffer, index, count);
        }
    }
}
