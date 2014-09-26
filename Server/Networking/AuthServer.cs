using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
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

            state.Connection.Send(new byte[]
            {
                0xAA, 0x00, 0x13, 0x7E, 0x1B, 0x43, 0x4F, 0x4E, 0x4E, 0x45, 0x43, 0x54, 0x45, 0x44, 0x20, 0x53, 0x45,
                0x52, 0x56, 0x45, 0x52, 0x00
            });
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
            
        }
    }
}
