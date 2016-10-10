using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace LcdControl
{
    class Client
    {
        private TcpClient tcpClient;
        private HttpStreamReader input;
        private StreamWriter output;

        public Client(TcpClient tcpClient)
        {
            this.tcpClient = tcpClient;
        }

        protected void Init()
        {
            input = new HttpStreamReader(tcpClient.GetStream());
            output = new StreamWriter(tcpClient.GetStream());
        }

        protected void Clean()
        {
            tcpClient.Close();
        }

        public void Process()
        {
            Init();
            string []command = input.ReadLine().Split(' ');
            Console.WriteLine("got " + command);
            Clean();
        }
    }
}
