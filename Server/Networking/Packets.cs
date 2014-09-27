using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServTK.Diagnostics;

namespace ServTK.Networking
{

    public sealed class AcceptClient : Packet
    {
        public AcceptClient() : base(0x7E, 19)
        {
            byte[] raw = { 0x1B, 0x43, 0x4F, 0x4E, 0x4E, 0x45, 0x43, 0x54, 0x45, 0x44, 0x20, 0x53, 0x45, 0x52, 0x56, 0x45, 0x52, 0x00 };

            _writer.Write(raw, 0, raw.Length);

            Logger.Log("Accept Client built as {0}", Encoding.ASCII.GetString(_writer.ToArray()));
        }
    }

    public abstract class Packet
    {
        protected PacketWriter _writer;
        private readonly int _packetId;
        private readonly int _length;

        public int PacketId { get { return _packetId; } }

        protected Packet(byte id, int length)
        {
            _writer = PacketWriter.Create();
            _packetId = id;
            _length = length;
            _writer.WriteHead(id, length);
        }

        public static implicit operator byte[](Packet packet)
        {
            return packet._writer.ToArray();
        }

        
    }

    public delegate void PacketHandlerCallback(NetState state, PacketReader reader);

    public enum EncryptionMode
    {
        Unencrypted = 0,
        StaticKeyEncryption,
        CharacterKeyEncryption
    }
}
