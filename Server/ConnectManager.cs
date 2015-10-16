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

            this.ipAdress = ipAdress;
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


            Socket mainSocket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                mainSocket.Bind(endPoint);
                mainSocket.Listen(10);

                while (true)
                {                   

                    Socket newClient = mainSocket.Accept();
                    Console.WriteLine("Подключено");

                    BigStaticClass.Registration(newClient);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

        }

        public static Message ByteToMessage(byte[] bytes)
        {


            BinaryFormatter f = new BinaryFormatter();
            MemoryStream stream = new MemoryStream(bytes);

            return (Message)f.Deserialize(stream);
        }

    }
}
