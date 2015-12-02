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
        public ConnectManager(IPAddress ipAdress, IPEndPoint endPoint)
        {

            //this.ipAdress = ipAdress;
            this.endPoint = endPoint;
        }
        private IPAddress ipAdress;
        private IPEndPoint endPoint;
        private int idCount;

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
                    BigStaticClass.logger.Log("Подключено");

                    BigStaticClass.Registration(newClient);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

    }
}
