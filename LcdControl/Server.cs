using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace LcdControl
{
    class Server
    {
        private TcpListener listener;

        public Server(int port)
        {
            listener = new TcpListener(IPAddress.Loopback, port);
            listener.Start();
        }

        public void AcceptLoop()
        {
            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                Client theClient = new Client(client);
                theClient.Process();
            }
        }
    }
}
