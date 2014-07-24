using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DroidPMClient
{
    class WOLClass :UdpClient
    {

        public WOLClass():base()
        { }
        //this is needed to send broadcast packet
        public void SetClientToBrodcastMode()
        {
            if (this.Active)
                this.Client.SetSocketOption(SocketOptionLevel.Socket,SocketOptionName.Broadcast, 0);
        }
    }
}
