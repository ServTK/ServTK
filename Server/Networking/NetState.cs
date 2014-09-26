using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ServTK.Diagnostics;

namespace ServTK.Networking
{
    public delegate void Router(NetState state, byte[] buffer);
    public class NetState
    {
        private Socket _socket;
        private byte[] _buffer = new byte[1024];
        
        private Router _router;

        public Socket Connection
        {
            get { return _socket; }
        }

        public NetState(Socket socket)
        {
            _socket = socket;
            _socket.NoDelay = true;
        }

        public void Start()
        {
            _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, RecieveHandler, this);
        }

        public void RegisterRouter(Router router)
        {
            _router = router;
        }

        private void RecieveHandler(IAsyncResult result)
        {
            NetState connection = (NetState)result.AsyncState;

            try
            {
                int bytesRead = _socket.EndReceive(result);

                if (bytesRead > 0)
                {
                    if (_buffer[0] == 0xAA)
                    {
                        if (_router != null)
                            _router(connection, _buffer);

                        //Queue the next receive
                        _socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, RecieveHandler, this);
                    }
                    else
                    {
                        Logger.Error("Packet Header Error!");
                        _socket.Close();
                    }

                }
                else
                {
                    _socket.Close();
                }
            }
            catch (SocketException)
            {
                if (_socket != null)
                {
                    _socket.Close();
                }

                throw;
            }
        }
    }
}
