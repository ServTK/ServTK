using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServTK.Eventing
{
    public static class EventSink
    {
        public static event ClientConnectedEventHandler ClientConnected;
        public static event CrashedEventHandler Crashed;

        public static void InvokeClientConnected(ClientConnectedEventArgs e)
        {
            if (ClientConnected != null)
                ClientConnected(e);
        }

        public static void InvokeCrashed(CrashedEventArgs e)
        {
            if (Crashed != null)
                Crashed(e);
        }
    }
}
