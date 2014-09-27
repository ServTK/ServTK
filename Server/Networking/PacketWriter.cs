using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServTK.Networking
{
    public class PacketWriter
    {
        public static PacketWriter Create(int capacity)
        {
            return new PacketWriter(capacity);
        }

        public static PacketWriter Create()
        {
            return new PacketWriter();
        }

        private MemoryStream _stream;
        private int _capacity;
        private readonly byte[] _buffer = new byte[4];

        public MemoryStream UnderlyingStream { get { return _stream; } }

        public byte[] ToArray()
        {
            return _stream.ToArray();
        }

        public PacketWriter(int capacity)
        {
            _stream = new MemoryStream(capacity);
            _capacity = capacity;
        }

        public PacketWriter()
        {
            _stream = new MemoryStream();
        }

        public void WriteHead(byte packetId, int length, bool standardHeader = true)
        {
            _stream.WriteByte(0xAA);
            Write((short) length);
            _stream.WriteByte(packetId);
            if (standardHeader)
                _stream.WriteByte(0x00); // TODO: Write a real incrementor
        }

        public void Write(bool value)
        {
            _stream.WriteByte((byte) (value ? 1 : 0));
        }

        public void Write(byte value)
        {
            _stream.WriteByte(value);
        }

        public void Write(sbyte value)
        {
            _stream.WriteByte((byte) value);        
        }

        public void Write(short value)
        {
            _buffer[0] = (byte) (value >> 8);
            _buffer[1] = (byte) value;

            _stream.Write(_buffer, 0, 2);
        }

        public void Write(ushort value)
        {
            _buffer[0] = (byte) (value >> 8);
            _buffer[1] = (byte) value;

            _stream.Write(_buffer, 0, 2);
        }

        public void Write(int value)
        {
            _buffer[0] = (byte)(value >> 24);
            _buffer[1] = (byte)(value >> 16);
            _buffer[2] = (byte)(value >> 8);
            _buffer[3] = (byte)value;

            _stream.Write(_buffer, 0, 4);
        }

        public void Write(uint value)
        {
            _buffer[0] = (byte)(value >> 24);
            _buffer[1] = (byte)(value >> 16);
            _buffer[2] = (byte)(value >> 8);
            _buffer[3] = (byte)value;

            _stream.Write(_buffer, 0, 4);
        }

        public void Write(byte[] buffer, int offset, int size)
        {
            _stream.Write(buffer, offset, size);
        }
    }
}
