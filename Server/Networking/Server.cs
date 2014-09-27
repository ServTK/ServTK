using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServTK.Diagnostics;

namespace ServTK.Networking
{
    public abstract class Server
    {
        #region Static Factory
        private static readonly Dictionary<string, Server> _servers = new Dictionary<string, Server>();

        public static IEnumerable<Server> Servers { get { return _servers.Values; } }
            
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Server Acquire(Type type, string name = null)
        {
            Server server;

            if (!_servers.TryGetValue(name ?? type.Name, out server))
                _servers.Add(name ?? type.Name, server = (Server)Activator.CreateInstance(type, name ?? type.Name));
             
            return server;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool Destroy(Type type, string name = null)
        {
            try
            {
                if (!_servers.ContainsKey(name ?? type.Name))
                    throw new Exception("Type doesn't exist in ServerList");

                if (_servers.ContainsKey(name ?? type.Name))
                    _servers.Remove(name ?? type.Name);

                return true;
            }
            catch (Exception e)
            {
                Logger.Warning(
                    "Unable to destroy server type {0}{1}. Reason: {2}", 
                    type.Name,
                    (name != null) ? " with name " + name : "",
                    e.Message
                    );
                return false;
            }   
        }

        #endregion

        private readonly string _name;
        protected bool _running;
        private Listener _listener;
        private Thread _thread;
        private readonly PacketHandler[] _handlers = new PacketHandler[0x100];

        public string Name { get { return _name; } }
        public bool Running { get { return _running; } }
        public Listener Listener { get { return _listener; } protected set { _listener = value; } }
        public Thread Thread{get { return _thread; }}
        public PacketHandler[] Handlers {get { return _handlers; }}

        protected Server(string name)
        {
            _name = name;
            _running = true;
            _thread = new Thread(Start);
        }

        public virtual void Initialize()
        {
            RegisterHandlers();
        }

        public virtual void Start()
        {
            Listener.Start();
        }

        public abstract void Stop();

        public virtual void AcceptNetState(NetState netState)
        {
            netState.RegisterRouter(PacketRouter);
        }

        public void PacketRouter(NetState state, byte[] buffer)
        {
            if (Handlers[buffer[3]] == null)
            {
                Logger.Warning("Unhandled PacketID: {0}", String.Format("0x{0:x2}", buffer[3]));
                return;
            }

            PacketReader reader = new PacketReader(buffer);

            ushort packetLength = (ushort)((BitConverter.ToUInt16(netState.Buffer, 1)) + 3);

            Handlers[buffer[3]].Callback(state, reader);
        }

        public abstract void RegisterHandlers();

        public virtual void RegisterHandler(byte packetType, EncryptionMode encryptionMode, PacketHandlerCallback callback)
        {
            Handlers[packetType] = new PacketHandler(packetType, encryptionMode, callback);
        }
    }
}
