using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Project2;

namespace Server
{
    class ConnectManager
    {       
        public ConnectManager(IPEndPoint endPoint)
        {

            this.endPoint = endPoint;
        }
        private IPEndPoint endPoint;

        public void Start()
        {

            Task task = new Task(Receive);
            task.Start();
        }

        void Receive()
        {


            Socket mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                mainSocket.Bind(endPoint);
                mainSocket.Listen(10);
                while (true)
                {

                    Socket newClient = mainSocket.Accept();

                    MainController.Instance.Registration(newClient);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

    }
}
