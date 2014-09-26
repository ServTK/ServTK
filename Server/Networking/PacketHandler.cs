using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServTK.Networking
{
    public class PacketHandler
    {
        private readonly EncryptionMode _encryption;
        private readonly byte _packetId;
        private readonly PacketHandlerCallback _callback;

        public EncryptionMode Encryption
        {
            get { return _encryption; }
        }

        public byte PacketId
        {
            get { return _packetId; }
        }

        public PacketHandlerCallback Callback { get { return _callback; } }


        public PacketHandler(byte packetId, EncryptionMode encryptionMode, PacketHandlerCallback callback)
        {
            _encryption = encryptionMode;
            _packetId = packetId;
            _callback = callback;
        }
    }
}
