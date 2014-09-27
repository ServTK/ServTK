using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServTK.Networking
{
    public class PacketReader
    {
        private readonly byte[] _data;
        private readonly int _size;
        private int _index;

        public byte[] Buffer { get { return _data;} }

        public PacketReader(byte[] data)
        {
            _data = data;
            _size = data.Length;
            _index = 0;
        }

        public int ReadInt32()
        {
            if (_index + 4 > _size)
                return 0;

            return (_data[_index++] << 24) | (_data[_index++] << 16) | (_data[_index++] << 8) | _data[_index++];
        }

        public short ReadInt16()
        {
            if (_index + 2 > _size)
                return 0;

            return (short) ((_data[_index++] << 16) | _data[_index++]);
        }

        public byte ReadByte()
        {
            if ((_index + 1) > _size)
            {
                return 0;
            }

            return _data[_index++];
        }

        public uint ReadUInt32()
        {
            if ((_index + 4) > _size)
            {
                return 0;
            }

            return (uint)((_data[_index++] << 24) | (_data[_index++] << 16) | (_data[_index++] << 8) | _data[_index++]);
        }

        public ushort ReadUInt16()
        {
            if ((_index + 2) > _size)
            {
                return 0;
            }

            return (ushort)((_data[_index++] << 8) | _data[_index++]);
        }

        public sbyte ReadSByte()
        {
            if ((_index + 1) > _size)
            {
                return 0;
            }

            return (sbyte)_data[_index++];
        }

        public bool ReadBoolean()
        {
            if ((_index + 1) > _size)
            {
                return false;
            }

            return (_data[_index++] != 0);
        }
    }
}
