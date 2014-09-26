using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServTK.Eventing
{
    public class ClientConnectedEventArgs : EventArgs
    {

    }

    public class CrashedEventArgs : EventArgs
    {
        private readonly Exception _exception;

        public Exception Exception { get { return _exception; } }
        public CrashedEventArgs(Exception e)
        {
            _exception = e;
        }
    }
}
