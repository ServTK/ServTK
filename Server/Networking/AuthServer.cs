using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ServTK.Diagnostics;

namespace ServTK.Networking
{
    public class AuthServer : Server
    {
        private List<NetState> _clients;

        public AuthServer(string name) : base(name)
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            _clients = new List<NetState>();

            Listener = new Listener(Config.Settings.Auth.Ip, Config.Settings.Auth.Port, AcceptNetState);
          
        }

        public override void AcceptNetState(NetState state)
        {
            base.AcceptNetState(state);

            Logger.Info("Recieved Connection");

            lock (_clients)
            {
                _clients.Add(state);
            }

            state.Send(new AcceptClient());
        }
        
        public override void Start()
        {
            base.Start();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }

        public override void RegisterHandlers()
        {
            RegisterHandler(0x00, EncryptionMode.Unencrypted, OnVersionRequest);
        }

        public void OnVersionRequest(NetState state, PacketReader reader)
        {
            ushort version = reader.ReadUInt16();
            byte unk1 = reader.ReadByte();
            ushort deep = reader.ReadUInt16();
        }
    }
}
