using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServTK.Networking
{

    public delegate void AcceptNetStateHandler(NetState netstate);
    public class Listener
    {
        public ManualResetEvent Done = new ManualResetEvent(false);

        private readonly string _ip;
        private readonly int _port;
        private AcceptNetStateHandler _acceptNetStateHandler;
        private bool _running;

        public Listener(string ip, int port, AcceptNetStateHandler handler)
        {
            _ip = ip;
            _port = port;
            _acceptNetStateHandler = handler;
        }

        public void Start()
        {
            _running = true;

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);

            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);
            
            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (_running)
            {
                Done.Reset();

                listener.BeginAccept(
                    AcceptSocket,
                    listener);

                Done.WaitOne();
            }
        }

        public void AcceptSocket(IAsyncResult result)
        {
            Socket listener = (Socket) result.AsyncState;
            Socket socket = listener.EndAccept(result);

            NetState state = new NetState(socket);

            state.Start();

            _acceptNetStateHandler(state);

        }
    }
}
