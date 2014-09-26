using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServTK.Eventing
{
    public delegate void ClientConnectedEventHandler(ClientConnectedEventArgs e);

    public delegate void CrashedEventHandler(CrashedEventArgs e);
}
