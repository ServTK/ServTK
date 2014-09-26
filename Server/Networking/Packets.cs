using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServTK.Networking
{
    class Packets
    {
    }

    public delegate void PacketHandlerCallback(NetState state, byte[] buffer);

    public enum EncryptionMode
    {
        Unencrypted = 0,
        StaticKeyEncryption,
        CharacterKeyEncryption
    }
}
